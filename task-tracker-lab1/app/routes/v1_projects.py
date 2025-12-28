from typing import List, Optional

from fastapi import APIRouter, Depends, HTTPException, Header
from fastapi.encoders import jsonable_encoder
from pydantic import BaseModel
from sqlmodel import Session, select

from app.db import engine
from app.models import Project, User
from app.schemas import ProjectCreate, ProjectRead
from app.auth import get_current_user
from app.rate_limit import is_allowed
from app.idempotency import get_key, set_key

router = APIRouter(prefix="/api/v1/projects", tags=["projects"])


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


class ProjectUpdate(BaseModel):
    name: Optional[str] = None
    description: Optional[str] = None


@router.get("/", response_model=List[ProjectRead])
def list_projects(
    limit: int = 10,
    offset: int = 0,
    current_user: User = Depends(get_current_user),
):
    apply_rate_limit(current_user)

    with Session(engine) as session:
        stmt = (
            select(Project)
            .where(Project.owner_id == current_user.id)
            .offset(offset)
            .limit(limit)
        )
        projects = session.exec(stmt).all()
        return projects


@router.post("/", response_model=ProjectRead, status_code=201)
def create_project(
    payload: ProjectCreate,
    current_user: User = Depends(get_current_user),
    idempotency_key: Optional[str] = Header(default=None, alias="Idempotency-Key"),
):
    apply_rate_limit(current_user)

    if idempotency_key:
        cached = get_key(idempotency_key)
        if cached is not None:
            return cached

    with Session(engine) as session:
        project = Project(
            name=payload.name,
            description=payload.description,
            owner_id=current_user.id,
        )
        session.add(project)
        session.commit()
        session.refresh(project)

        result = ProjectRead.from_orm(project)
        data = jsonable_encoder(result)

        if idempotency_key:
            set_key(idempotency_key, data)

        return data


@router.get("/{project_id}", response_model=ProjectRead)
def get_project(
    project_id: int,
    current_user: User = Depends(get_current_user),
):
    apply_rate_limit(current_user)

    with Session(engine) as session:
        project = session.get(Project, project_id)
        if not project or project.owner_id != current_user.id:
            raise HTTPException(status_code=404, detail="Project not found")
        return project


@router.patch("/{project_id}", response_model=ProjectRead)
def update_project(
    project_id: int,
    payload: ProjectUpdate,
    current_user: User = Depends(get_current_user),
):
    apply_rate_limit(current_user)

    with Session(engine) as session:
        project = session.get(Project, project_id)
        if not project or project.owner_id != current_user.id:
            raise HTTPException(status_code=404, detail="Project not found")

        data = payload.dict(exclude_unset=True)
        if "name" in data:
            project.name = data["name"]
        if "description" in data:
            project.description = data["description"]

        session.add(project)
        session.commit()
        session.refresh(project)
        return project


@router.delete("/{project_id}", status_code=204)
def delete_project(
    project_id: int,
    current_user: User = Depends(get_current_user),
):
    apply_rate_limit(current_user)

    with Session(engine) as session:
        project = session.get(Project, project_id)
        if not project or project.owner_id != current_user.id:
            raise HTTPException(status_code=404, detail="Project not found")

        session.delete(project)
        session.commit()

    return None
