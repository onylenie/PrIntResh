# Предметная область — Task Manager

## Сущности
- **User**
  - id, email, name, password_hash, role, created_at, updated_at
- **Project**
  - id, name, description, owner_id, created_at, updated_at
- **Task**
  - id, title, description, project_id, assignee_id, status, priority, created_at, updated_at, due_date, estimated_time_minutes (v2)
- **Comment**
  - id, task_id, author_id, body, created_at


## ER-диаграмма

![alt text](image.png)

## Архитектура API

Проект включает две версии API:

**API v1**  
Базовые сущности и операции:
- CRUD для проектов и задач
- комментарии
- авторизация
- идемпотентность POST-запросов
- rate limiting v1 (функциональный)

**API v2**  
Улучшенная версия:
- расширенная схема задач (`estimated_time_minutes`)
- улучшенный rate limiting через заголовки
- поддержка include-полей

Обе версии работают параллельно
