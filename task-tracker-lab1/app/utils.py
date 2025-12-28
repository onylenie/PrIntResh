from fastapi import Response

def set_rate_headers(response: Response, limit: int, remaining: int, retry_after: int = 0):
    response.headers["X-Limit-Limit"] = str(limit)
    response.headers["X-Limit-Remaining"] = str(remaining)
    if retry_after:
        response.headers["Retry-After"] = str(retry_after)
