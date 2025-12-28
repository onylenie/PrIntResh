from typing import Optional

from fastapi import APIRouter, Depends, HTTPException
from sqlmodel import Session, select
from pydantic import BaseModel, EmailStr

from app.db import engine
from app.models import User
from app.schemas import UserRead
from app.auth import get_current_user, get_password_hash
from app.rate_limit import is_allowed

router = APIRouter(prefix="/api/v1/users", tags=["users"])


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


class UserUpdate(BaseModel):
    email: Optional[EmailStr] = None
    name: Optional[str] = None
    password: Optional[str] = None


@router.get("/me", response_model=UserRead)
def get_me(current_user: User = Depends(get_current_user)):
    apply_rate_limit(current_user)
    return current_user


@router.get("/{user_id}", response_model=UserRead)
def get_user(
    user_id: int,
    current_user: User = Depends(get_current_user),
):
    apply_rate_limit(current_user)

    with Session(engine) as session:
        user = session.get(User, user_id)
        if not user:
            raise HTTPException(status_code=404, detail="Not found")
        return user


@router.patch("/{user_id}", response_model=UserRead)
def update_user(
    user_id: int,
    payload: UserUpdate,
    current_user: User = Depends(get_current_user),
):
    apply_rate_limit(current_user)

    if current_user.id != user_id:
        raise HTTPException(status_code=403, detail="Forbidden")

    with Session(engine) as session:
        user = session.get(User, user_id)
        if not user:
            raise HTTPException(status_code=404, detail="Not found")

        data = payload.dict(exclude_unset=True)

        if "email" in data:
            existing = session.exec(
                select(User).where(
                    User.email == data["email"],
                    User.id != user_id,
                )
            ).first()
            if existing:
                raise HTTPException(status_code=400, detail="Email already in use")
            user.email = data["email"]

        if "name" in data:
            user.name = data["name"]

        if "password" in data:
            user.password_hash = get_password_hash(data["password"])

        session.add(user)
        session.commit()
        session.refresh(user)
        return user


@router.delete("/{user_id}", status_code=204)
def delete_user(
    user_id: int,
    current_user: User = Depends(get_current_user),
):
    apply_rate_limit(current_user)

    if current_user.id != user_id:
        raise HTTPException(status_code=403, detail="Forbidden")

    with Session(engine) as session:
        user = session.get(User, user_id)
        if not user:
            raise HTTPException(status_code=404, detail="Not found")

        session.delete(user)
        session.commit()
    return None
