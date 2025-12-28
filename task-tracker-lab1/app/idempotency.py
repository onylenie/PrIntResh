import time
from typing import Optional, Dict, Any, Tuple

# словарь
_idemp_storage: Dict[str, Tuple[dict, float]] = {}

IDEMPOTENCY_TTL = 24 * 3600

# есть ли запись по ключу
def get_key(key: str) -> Optional[dict]:
    record = _idemp_storage.get(key)
    if not record:
        return None
# проверка на истечение срока
    value, expires_at = record
    if time.time() > expires_at:
        _idemp_storage.pop(key, None)
        return None

    return value


def set_key(key: str, value: dict, ttl: int = IDEMPOTENCY_TTL):
    expires_at = time.time() + ttl
    _idemp_storage[key] = (value, expires_at)
