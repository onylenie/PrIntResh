from fastapi import FastAPI, Request
from fastapi.responses import JSONResponse
from app.db import init_db
from app.routes import (
    v1_auth,
    v1_users,
    v1_projects,
    v1_tasks,
    v1_comments,
    v2_tasks,
    internal_stats,  
)
from fastapi.middleware.cors import CORSMiddleware
import os


def create_app():
    app = FastAPI(title="Task Manager API", version="1.0.0")

    init_db()

    app.include_router(v1_auth.router)
    app.include_router(v1_users.router)
    app.include_router(v1_projects.router)
    app.include_router(v1_tasks.router)
    app.include_router(v1_comments.router)
    app.include_router(v2_tasks.router)

    app.include_router(internal_stats.router)

    app.add_middleware(
        CORSMiddleware,
        allow_origins=["*"],
        allow_methods=["*"],
        allow_headers=["*"],
    )

    @app.exception_handler(429)
    async def rate_limit_handler(request: Request, exc):
        return JSONResponse(status_code=429, content={"detail": "Too Many Requests"})

    return app


app = create_app()
