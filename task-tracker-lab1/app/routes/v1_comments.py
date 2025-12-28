from typing import List, Optional

from fastapi import APIRouter, Depends, HTTPException, Header
from fastapi.encoders import jsonable_encoder
from pydantic import BaseModel
from sqlmodel import Session, select

from app.db import engine
from app.models import Comment, Task, User, Project
from app.schemas import CommentCreate, CommentRead
from app.auth import get_current_user
from app.rate_limit import is_allowed
from app.idempotency import get_key, set_key

router = APIRouter(
    prefix="/api/v1/tasks/{task_id}/comments",
    tags=["comments"],
)


def apply_rate_limit(user: User):
    identifier = f"user:{user.id}"
    allowed, remaining, retry_after = is_allowed(identifier)
    if not allowed:
        headers = {
            "X-Limit-Limit": "100",
            "X-Limit-Remaining": "0",
            "Retry-After": str(retry_after),
        }
        raise HTTPException(
            status_code=429,
            detail="Too Many Requests",
            headers=headers,
        )


class CommentUpdate(BaseModel):
    body: Optional[str] = None


def _get_task_for_user(session: Session, task_id: int, user: User) -> Task:
    task = session.get(Task, task_id)
    if not task:
        raise HTTPException(status_code=404, detail="Task not found")

    project = session.get(Project, task.project_id) if task.project_id else None
    if not project or project.owner_id != user.id:
        raise HTTPException(status_code=404, detail="Task not found")

    return task


@router.post("", response_model=CommentRead, status_code=201)
def create_comment(
    task_id: int,
    payload: CommentCreate,
    current_user: User = Depends(get_current_user),
    idempotency_key: Optional[str] = Header(
        default=None, alias="Idempotency-Key"
    ),
):
    apply_rate_limit(current_user)

    if idempotency_key:
        cached = get_key(idempotency_key)
        if cached is not None:
            return cached

    with Session(engine) as session:
        _ = _get_task_for_user(session, task_id, current_user)

        comment = Comment(
            task_id=task_id,
            author_id=current_user.id,
            body=payload.body,
        )
        session.add(comment)
        session.commit()
        session.refresh(comment)

        result = CommentRead.from_orm(comment)
        data = jsonable_encoder(result)

        if idempotency_key:
            set_key(idempotency_key, data)

        return data


@router.get("", response_model=List[CommentRead])
def list_comments(
    task_id: int,
    limit: int = 10,
    offset: int = 0,
    current_user: User = Depends(get_current_user),
):
    apply_rate_limit(current_user)

    with Session(engine) as session:
        _ = _get_task_for_user(session, task_id, current_user)

        stmt = (
            select(Comment)
            .where(Comment.task_id == task_id)
            .offset(offset)
            .limit(limit)
        )
        items = session.exec(stmt).all()
        return items


@router.patch("/{comment_id}", response_model=CommentRead)
def update_comment(
    task_id: int,
    comment_id: int,
    payload: CommentUpdate,
    current_user: User = Depends(get_current_user),
):
    apply_rate_limit(current_user)

    with Session(engine) as session:
        _ = _get_task_for_user(session, task_id, current_user)

        comment = session.get(Comment, comment_id)
        if not comment or comment.task_id != task_id:
            raise HTTPException(status_code=404, detail="Comment not found")

        if comment.author_id != current_user.id:
            raise HTTPException(status_code=403, detail="Forbidden")

        data = payload.dict(exclude_unset=True)
        if "body" in data:
            comment.body = data["body"]

        session.add(comment)
        session.commit()
        session.refresh(comment)
        return comment


@router.delete("/{comment_id}", status_code=204)
def delete_comment(
    task_id: int,
    comment_id: int,
    current_user: User = Depends(get_current_user),
):
    apply_rate_limit(current_user)

    with Session(engine) as session:
        _ = _get_task_for_user(session, task_id, current_user)

        comment = session.get(Comment, comment_id)
        if not comment or comment.task_id != task_id:
            raise HTTPException(status_code=404, detail="Comment not found")

        if comment.author_id != current_user.id:
            raise HTTPException(status_code=403, detail="Forbidden")

        session.delete(comment)
        session.commit()

    return None
