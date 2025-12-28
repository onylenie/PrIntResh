from pydantic import BaseModel, EmailStr, ConfigDict
from typing import Optional, List
from datetime import datetime


class UserCreate(BaseModel):
    email: EmailStr
    password: str
    name: str | None = None


class UserLogin(BaseModel):
    email: EmailStr
    password: str


class UserRead(BaseModel):
    id: int
    email: EmailStr
    name: Optional[str] = None


class Token(BaseModel):
    access_token: str
    token_type: str = "bearer"
    refresh_token: Optional[str] = None


class ProjectCreate(BaseModel):
    name: str
    description: Optional[str] = None


class ProjectRead(BaseModel):
    id: int
    name: str
    description: str | None = None
    owner_id: int

    model_config = ConfigDict(from_attributes=True)


class TaskBase(BaseModel):
    title: str
    description: str | None = None
    assignee_id: int | None = None
    due_date: datetime | None = None


class TaskCreate(TaskBase):
    pass


class TaskRead(TaskBase):
    id: int
    project_id: int
    status: str
    priority: int
    created_at: datetime
    updated_at: datetime

    model_config = ConfigDict(from_attributes=True)


class TaskCreateV2(TaskCreate):
    estimated_time_minutes: Optional[int] = None


class CommentCreate(BaseModel):
    body: str


class CommentRead(BaseModel):
    id: int
    task_id: int
    author_id: int
    body: str
    created_at: datetime

    model_config = ConfigDict(from_attributes=True)


class TaskReadV2(TaskRead):
    estimated_time_minutes: Optional[int] = None



class TaskReadWithRelations(TaskRead):
    project: Optional[ProjectRead] = None
    comments: Optional[List[CommentRead]] = None


class TaskReadV2WithRelations(TaskReadV2):
    project: Optional[ProjectRead] = None
    comments: Optional[List[CommentRead]] = None
