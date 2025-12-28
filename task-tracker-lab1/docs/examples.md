## /api/v1/auth/register

{
  "email": "test@example.com",
  "password": "qwer1234",
  "name": "Test User"
}

{
  "access_token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE3NjMyMTg0ODgsInN1YiI6IjEiLCJ0eXBlIjoiYWNjZXNzIn0.xWrvg2jyvpFlD3jVFBTK669nXn5SiZ4PiKr1DIj5wQs",
  "token_type": "bearer",
  "refresh_token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE3NjM4MjIzODgsInN1YiI6IjEiLCJ0eXBlIjoicmVmcmVzaCJ9.xftRT8TDrLCXDIxYVEbEXOpxBqsW7JPdR8LCiJeN9w8"
}

## api/v1/auth/login

{
  "email": "test@example.com",
  "password": "qwer1234"
}

{
  "access_token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE3NjMyMTg2NjgsInN1YiI6IjEiLCJ0eXBlIjoiYWNjZXNzIn0.pmKReApbLWyxRsOM_Ky7wLAbLAgkqxnLWQAJEaikzSo",
  "token_type": "bearer",
  "refresh_token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE3NjM4MjI1NjgsInN1YiI6IjEiLCJ0eXBlIjoicmVmcmVzaCJ9.wdKoy-F6WcWFuQzDCMNbSCkhaS_pLYHoBga4XhiplX0"
}

## api/v1/users/me

{
  "id": 1,
  "email": "test@example.com",
  "name": "Test User"
}

## /api/v1/users/1
{id: 1}


{
  "id": 1,
  "email": "test@example.com",
  "name": "Test User"
}

## api/v1/projects/

{
  "name": "first",
  "description": "test"
}

{
  "id": 1,
  "name": "first",
  "description": "test",
  "owner_id": 1
}

## api/v1/projects/

[
  {
    "id": 1,
    "name": "First",
    "description": "Test",
    "owner_id": 1
  },
  {
    "id": 2,
    "name": "First",
    "description": "test",
    "owner_id": 1
  },
  {
    "id": 3,
    "name": "first",
    "description": "test",
    "owner_id": 1
  }
]

## api/v1/projects/1

{id: 1}

{
  "id": 1,
  "name": "First",
  "description": "Test",
  "owner_id": 1
}

## api/v1/projects/1/tasks

{
  "title": "1",
  "description": "1",
  "assignee_id": 0,
  "due_date": "2025-11-15T15:57:55.911Z"
}

{
  "title": "1",
  "description": "1",
  "assignee_id": 0,
  "due_date": "2025-11-15T15:57:55.911000",
  "id": 2,
  "project_id": 1,
  "status": "open",
  "priority": 3,
  "created_at": "2025-11-15T15:58:08.819756",
  "updated_at": "2025-11-15T15:58:08.819756"
}


## api/v1/tasks/3

{id: 3}

{
  "title": "test",
  "description": "test",
  "assignee_id": 0,
  "due_date": "2025-11-15T15:57:55.911000",
  "id": 3,
  "project_id": 3,
  "status": "open",
  "priority": 3,
  "created_at": "2025-11-15T16:00:41.155575",
  "updated_at": "2025-11-15T16:00:41.155575"
}

## pi/v1/projects/1/tasks
{id: 1}

[
  {
    "title": "1",
    "description": "1",
    "assignee_id": null,
    "due_date": "2025-11-15T15:50:14.031000",
    "id": 1,
    "project_id": 1,
    "status": "open",
    "priority": 3,
    "created_at": "2025-11-15T15:52:25.570881",
    "updated_at": "2025-11-15T15:52:25.570881"
  },
  {
    "title": "1",
    "description": "1",
    "assignee_id": 0,
    "due_date": "2025-11-15T15:57:55.911000",
    "id": 2,
    "project_id": 1,
    "status": "open",
    "priority": 3,
    "created_at": "2025-11-15T15:58:08.819756",
    "updated_at": "2025-11-15T15:58:08.819756"
  }
]

## api/v1/tasks/1/comments

{id:1}

{
  "body": "comment"
}

{
  "id": 1,
  "task_id": 1,
  "author_id": 1,
  "body": "comment",
  "created_at": "2025-11-15T16:05:01.962692"
}

## api/v1/tasks/1/comments

{id:1}

[
  {
    "id": 1,
    "task_id": 1,
    "author_id": 1,
    "body": "comment",
    "created_at": "2025-11-15T16:05:01.962692"
  }
]

## api/v2/projects/1/tasks

