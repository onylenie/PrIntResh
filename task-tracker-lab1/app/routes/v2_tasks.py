from typing import List, Optional
from datetime import datetime

from fastapi import APIRouter, Depends, HTTPException, Header, Response
from pydantic import BaseModel
from sqlmodel import Session, select

from app.db import engine
from app.models import Task, Project, Comment
from app.schemas import (
    TaskCreateV2,
    TaskReadV2,
    TaskReadV2WithRelations,
    ProjectRead,
    CommentRead,
)
from app.auth import get_current_user
from app.idempotency import get_key, set_key
from app.rate_limit import is_allowed
from app.utils import set_rate_headers

router = APIRouter(prefix="/api/v2", tags=["tasks_v2"])


class TaskUpdateV2(BaseModel):
    title: Optional[str] = None
    description: Optional[str] = None
    assignee_id: Optional[int] = None
    due_date: Optional[datetime] = None
    status: Optional[str] = None
    priority: Optional[int] = None
    estimated_time_minutes: Optional[int] = None


@router.get("/projects/{project_id}/tasks", response_model=List[TaskReadV2])
def list_tasks(
    project_id: int,
    response: Response,
    limit: int = 10,
    offset: int = 0,
    current_user=Depends(get_current_user),
):
    identifier = f"user:{current_user.id}"
    allowed, remaining, retry_after = is_allowed(identifier)
    set_rate_headers(
        response,
        limit=100,
        remaining=remaining,
        retry_after=retry_after,
    )
    if not allowed:
        raise HTTPException(status_code=429, detail="Too Many Requests")

    with Session(engine) as session:
        stmt = (
            select(Task)
            .where(Task.project_id == project_id)
            .offset(offset)
            .limit(limit)
        )
        tasks = session.exec(stmt).all()
        return tasks


@router.post(
    "/projects/{project_id}/tasks",
    response_model=TaskReadV2,
    status_code=201,
)
def create_task(
    project_id: int,
    payload: TaskCreateV2,
    response: Response,
    current_user=Depends(get_current_user),
    idempotency_key: Optional[str] = Header(
        default=None,
        alias="Idempotency-Key",
    ),
):
    identifier = f"user:{current_user.id}"
    allowed, remaining, retry_after = is_allowed(identifier)
    set_rate_headers(
        response,
        limit=100,
        remaining=remaining,
        retry_after=retry_after,
    )
    if not allowed:
        raise HTTPException(status_code=429, detail="Too Many Requests")

    if idempotency_key:
        cached = get_key(idempotency_key)
        if cached is not None:
            return cached

    with Session(engine) as session:
        project = session.get(Project, project_id)
        if not project or project.owner_id != current_user.id:
            raise HTTPException(status_code=404, detail="Project not found")

        t = Task(
            title=payload.title,
            description=payload.description,
            project_id=project_id,
            assignee_id=payload.assignee_id,
            due_date=payload.due_date,
            estimated_time_minutes=payload.estimated_time_minutes,
        )
        session.add(t)
        session.commit()
        session.refresh(t)

        result = TaskReadV2.from_orm(t).dict()
        if idempotency_key:
            set_key(idempotency_key, result)
        return result


@router.get("/tasks/{task_id}", response_model=TaskReadV2WithRelations)
def get_task(
    task_id: int,
    include: Optional[str] = None,
    current_user=Depends(get_current_user),
):
    with Session(engine) as session:
        t = session.get(Task, task_id)
        if not t:
            raise HTTPException(status_code=404, detail="Not found")

        task_data = TaskReadV2.from_orm(t).dict()
        extra = {}

        if include:
            parts = {p.strip().lower() for p in include.split(",") if p.strip()}

            if "project" in parts:
                proj = session.get(Project, t.project_id)
                if proj:
                    extra["project"] = ProjectRead.from_orm(proj)

            if "comments" in parts:
                stmt = select(Comment).where(Comment.task_id == task_id)
                comments = session.exec(stmt).all()
                extra["comments"] = [CommentRead.from_orm(c) for c in comments]

        full = {**task_data, **extra}
        return full


@router.patch("/tasks/{task_id}", response_model=TaskReadV2)
def update_task(
    task_id: int,
    payload: TaskUpdateV2,
    response: Response,
    current_user=Depends(get_current_user),
):
    identifier = f"user:{current_user.id}"
    allowed, remaining, retry_after = is_allowed(identifier)
    set_rate_headers(
        response,
        limit=100,
        remaining=remaining,
        retry_after=retry_after,
    )
    if not allowed:
        raise HTTPException(status_code=429, detail="Too Many Requests")

    with Session(engine) as session:
        task = session.get(Task, task_id)
        if not task:
            raise HTTPException(status_code=404, detail="Task not found")
        project = session.get(Project, task.project_id) if task.project_id else None
        if not project or project.owner_id != current_user.id:
            raise HTTPException(status_code=404, detail="Task not found")

        data = payload.dict(exclude_unset=True)
        for field, value in data.items():
            setattr(task, field, value)

        session.add(task)
        session.commit()
        session.refresh(task)
        return task


@router.delete("/tasks/{task_id}", status_code=204)
def delete_task(
    task_id: int,
    response: Response,
    current_user=Depends(get_current_user),
):
    identifier = f"user:{current_user.id}"
    allowed, remaining, retry_after = is_allowed(identifier)
    set_rate_headers(
        response,
        limit=100,
        remaining=remaining,
        retry_after=retry_after,
    )
    if not allowed:
        raise HTTPException(status_code=429, detail="Too Many Requests")

    with Session(engine) as session:
        task = session.get(Task, task_id)
        if not task:
            raise HTTPException(status_code=404, detail="Task not found")

        project = session.get(Project, task.project_id) if task.project_id else None
        if not project or project.owner_id != current_user.id:
            raise HTTPException(status_code=404, detail="Task not found")

        session.delete(task)
        session.commit()

    return None
