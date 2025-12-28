from fastapi import APIRouter, HTTPException
from sqlmodel import Session, select

from app.db import engine
from app.models import User
from app.schemas import UserCreate, UserLogin, Token
from app.auth import get_password_hash, verify_password, create_access_token, create_refresh_token

router = APIRouter(prefix="/api/v1/auth", tags=["auth"])


@router.post("/register", response_model=Token)
def register(payload: UserCreate):
    with Session(engine) as session:
        stmt = select(User).where(User.email == payload.email)
        existing = session.exec(stmt).first()
        if existing:
            raise HTTPException(status_code=400, detail="Email already registered")

        user = User(
            email=payload.email,
            name=payload.name,
            password_hash=get_password_hash(payload.password),
        )
        session.add(user)
        session.commit()
        session.refresh(user)

        access = create_access_token(str(user.id))
        refresh = create_refresh_token(str(user.id))
        return {"access_token": access, "refresh_token": refresh}


@router.post("/login", response_model=Token)
def login(payload: UserLogin):
   
    with Session(engine) as session:
        stmt = select(User).where(User.email == payload.email)
        user = session.exec(stmt).first()
        if not user or not verify_password(payload.password, user.password_hash):
            raise HTTPException(status_code=401, detail="Invalid credentials")

        access = create_access_token(str(user.id))
        refresh = create_refresh_token(str(user.id))
        return {"access_token": access, "refresh_token": refresh}
