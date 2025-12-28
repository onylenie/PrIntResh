from typing import List, Optional

from fastapi import APIRouter, Depends, HTTPException, Header
from fastapi.encoders import jsonable_encoder
from pydantic import BaseModel
from sqlmodel import Session, select
from datetime import datetime

from app.db import engine
from app.models import Task, Project, User, Comment
from app.schemas import (
    TaskCreate,
    TaskRead,
    TaskReadWithRelations,
    ProjectRead,
    CommentRead,
)
from app.auth import get_current_user
from app.rate_limit import is_allowed
from app.idempotency import get_key, set_key

router = APIRouter(prefix="/api/v1", tags=["tasks_v1"])


def apply_rate_limit(user: User) -> int:
    identifier = f"user:{user.id}"
    allowed, remaining, retry_after = is_allowed(identifier)
    if not allowed:
        headers = {
            "X-Limit-Limit": "100",
            "X-Limit-Remaining": "0",
            "Retry-After": str(retry_after),
        }
        raise HTTPException(
            status_code=429, detail="Too Many Requests", headers=headers
        )
    return remaining


class TaskUpdate(BaseModel):
    title: Optional[str] = None
    description: Optional[str] = None
    assignee_id: Optional[int] = None
    due_date: Optional[datetime] = None
    status: Optional[str] = None
    priority: Optional[int] = None


@router.get("/projects/{project_id}/tasks", response_model=List[TaskRead])
def list_tasks(
    project_id: int,
    limit: int = 10,
    offset: int = 0,
    current_user: User = Depends(get_current_user),
):
    apply_rate_limit(current_user)

    with Session(engine) as session:
        project = session.get(Project, project_id)
        if not project or project.owner_id != current_user.id:
            raise HTTPException(status_code=404, detail="Project not found")

        statement = (
            select(Task)
            .where(Task.project_id == project_id)
            .offset(offset)
            .limit(limit)
        )
        tasks = session.exec(statement).all()
        return tasks


@router.post(
    "/projects/{project_id}/tasks",
    response_model=TaskRead,
    status_code=201,
)
def create_task(
    project_id: int,
    payload: TaskCreate,
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
        project = session.get(Project, project_id)
        if not project or project.owner_id != current_user.id:
            raise HTTPException(status_code=404, detail="Project not found")

        task = Task(
            title=payload.title,
            description=payload.description,
            project_id=project_id,
            assignee_id=payload.assignee_id,
            due_date=payload.due_date,
        )

        session.add(task)
        session.commit()
        session.refresh(task)

        result = TaskRead.from_orm(task)
        data = jsonable_encoder(result)

        if idempotency_key:
            set_key(idempotency_key, data)

        return data


@router.get("/tasks/{task_id}", response_model=TaskReadWithRelations)
def get_task(
    task_id: int,
    include: Optional[str] = None,
    current_user: User = Depends(get_current_user),
):
    apply_rate_limit(current_user)

    with Session(engine) as session:
        task = session.get(Task, task_id)
        if not task:
            raise HTTPException(status_code=404, detail="Task not found")

        project = (
            session.get(Project, task.project_id) if task.project_id else None
        )
        if not project or project.owner_id != current_user.id:
            raise HTTPException(status_code=404, detail="Task not found")

        task_data = TaskRead.from_orm(task).dict()
        extra = {}

        if include:
            parts = {p.strip().lower() for p in include.split(",") if p.strip()}

            if "project" in parts and project is not None:
                extra["project"] = ProjectRead.from_orm(project)

            if "comments" in parts:
                stmt = select(Comment).where(Comment.task_id == task_id)
                comments = session.exec(stmt).all()
                extra["comments"] = [CommentRead.from_orm(c) for c in comments]

        full = {**task_data, **extra}
        return full


@router.patch("/tasks/{task_id}", response_model=TaskRead)
def update_task(
    task_id: int,
    payload: TaskUpdate,
    current_user: User = Depends(get_current_user),
):
    apply_rate_limit(current_user)

    with Session(engine) as session:
        task = session.get(Task, task_id)
        if not task:
            raise HTTPException(status_code=404, detail="Task not found")

        project = (
            session.get(Project, task.project_id) if task.project_id else None
        )
        if not project or project.owner_id != current_user.id:
            raise HTTPException(status_code=404, detail="Task not found")

        update_data = payload.dict(exclude_unset=True)

        for field, value in update_data.items():
            setattr(task, field, value)

        session.add(task)
        session.commit()
        session.refresh(task)
        return task


@router.delete("/tasks/{task_id}", status_code=204)
def delete_task(
    task_id: int,
    current_user: User = Depends(get_current_user),
):
    apply_rate_limit(current_user)

    with Session(engine) as session:
        task = session.get(Task, task_id)
        if not task:
            raise HTTPException(status_code=404, detail="Task not found")

        project = (
            session.get(Project, task.project_id) if task.project_id else None
        )
        if not project or project.owner_id != current_user.id:
            raise HTTPException(status_code=404, detail="Task not found")

        session.delete(task)
        session.commit()
        return None
