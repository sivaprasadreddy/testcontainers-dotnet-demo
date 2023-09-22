CREATE TABLE bookmarks
(
    id      SERIAL PRIMARY KEY,
    title   VARCHAR NOT NULL,
    url     VARCHAR NOT NULL
);

INSERT INTO bookmarks (title, url) VALUES ('foo', 'http://example.com');
INSERT INTO bookmarks (title, url) VALUES ('bar', 'http://example.com');
