# Конечные точки API

# Auth (v1)

### `POST /api/v1/auth/register`
Регистрация нового пользователя.  
Тело: `{email, password, name}`  
Возвращает: `access_token`, `refresh_token`

### `POST /api/v1/auth/login`
Авторизация пользователя.  
Возвращает: `access_token`, `refresh_token`

# Users (v1)

### `GET /api/v1/users/me`
Профиль текущего пользователя.

### `GET /api/v1/users/{user_id}`
Получить пользователя по ID.

# Projects (v1)

### `GET /api/v1/projects?limit=&offset=`
Список проектов текущего пользователя с пагинацией.

### `POST /api/v1/projects`
Создать проект (требуется `Idempotency-Key`).

### `GET /api/v1/projects/{project_id}`
Получить проект.

# Tasks (v1)

### `GET /api/v1/projects/{project_id}/tasks?limit=&offset=`
Список задач проекта с пагинацией.

### `POST /api/v1/projects/{project_id}/tasks`
Создать задачу (идемпотентный запрос).

### `GET /api/v1/tasks/{task_id}?include=project,comments`
Получить задачу:  
— базовые поля (по умолчанию)  
— опционально `project`  
— опционально `comments`

# Comments (v1)

### `POST /api/v1/tasks/{task_id}/comments`
Добавить комментарий (идемпотентно).

### `GET /api/v1/tasks/{task_id}/comments?limit=&offset=`
Список комментариев к задаче.

# Tasks (v2)

Расширенная версия задач:
- новое поле `estimated_time_minutes`
- те же принципы аутентификации и rate limit

### `GET /api/v2/projects/{project_id}/tasks?limit=&offset=`
Список задач проекта (пагинация).

### `POST /api/v2/projects/{project_id}/tasks`
Создать задачу v2.

### `GET /api/v2/tasks/{task_id}?include=project,comments`
Расширенный просмотр задачи (опциональные поля).

# Internal API

### `GET /api/internal/stats`
*Внутренний сервисный эндпоинт.*

Возвращает:
- total_users  
- total_projects  
- total_tasks  
- total_comments  