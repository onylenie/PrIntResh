import os
import time
from typing import Dict, Tuple

_rate_storage: Dict[str, Tuple[int, int]] = {}

# колво запросов за окно
LIMIT = int(os.getenv("RATE_LIMIT", "100"))
WINDOW = int(os.getenv("RATE_LIMIT_WINDOW", "60"))


def is_allowed(identifier: str):

    now = int(time.time())
    window = now // WINDOW
    key = f"{identifier}:{window}"

    window_id, count = _rate_storage.get(key, (window, 0))

    if window_id != window:
        count = 0
        window_id = window

    count += 1
    _rate_storage[key] = (window_id, count)

    remaining = max(0, LIMIT - count)

    if count > LIMIT:
        retry_after = WINDOW - (now % WINDOW)
        return False, remaining, retry_after

    return True, remaining, 0
