CREATE TABLE short_urls (
    short_code    TEXT PRIMARY KEY,
    original_url  TEXT NOT NULL,
    created_at    TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    expires_at    TIMESTAMPTZ NULL
);