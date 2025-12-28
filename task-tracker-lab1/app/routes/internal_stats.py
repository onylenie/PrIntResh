import os

from fastapi import APIRouter, Depends, Header, HTTPException
from sqlmodel import Session, select
from sqlalchemy import func

from app.db import engine
from app.models import User, Project, Task, Comment

router = APIRouter(prefix="/api/internal", tags=["internal"])

INTERNAL_API_KEY = os.getenv("INTERNAL_API_KEY", "int123")


def verify_internal_key(
    x_internal_key: str = Header(..., alias="X-Internal-Key"),
):
    if x_internal_key != INTERNAL_API_KEY:
        raise HTTPException(status_code=403, detail="Forbidden: invalid internal key")
    return True


@router.get("/stats")
def get_internal_stats(
    _ok: bool = Depends(verify_internal_key),
):
    with Session(engine) as session:
        total_users = session.exec(select(func.count(User.id))).one()
        total_projects = session.exec(select(func.count(Project.id))).one()
        total_tasks = session.exec(select(func.count(Task.id))).one()
        total_comments = session.exec(select(func.count(Comment.id))).one()

    return {
        "total_users": total_users,
        "total_projects": total_projects,
        "total_tasks": total_tasks,
        "total_comments": total_comments,
    }
