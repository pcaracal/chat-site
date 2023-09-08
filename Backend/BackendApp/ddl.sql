DROP DATABASE IF EXISTS "chat_site_db";
CREATE DATABASE "chat_site_db";
\c "chat_site_db";
   
CREATE TABLE "user"
(
    "id"         SERIAL PRIMARY KEY,
    "username"   VARCHAR(255) NOT NULL,
    "password"   VARCHAR(255) NOT NULL,
    "created_at" TIMESTAMP    NOT NULL,
    CONSTRAINT "unique_username" UNIQUE ("username")
);

CREATE TABLE "message"
(
    "id"         SERIAL PRIMARY KEY,
    "fk_user_id" INT          NOT NULL,
    "text"       VARCHAR(255) NOT NULL,
    "created_at" TIMESTAMP    NOT NULL,
    CONSTRAINT "fk_user_id" FOREIGN KEY ("fk_user_id") REFERENCES "user" ("id")
);

CREATE TABLE "channel"
(
    "id"          SERIAL PRIMARY KEY,
    "name"        VARCHAR(255) NOT NULL,
    "created_at"  TIMESTAMP    NOT NULL,
    "fk_admin_id" INT          NOT NULL,
    CONSTRAINT "unique_channel_name" UNIQUE ("name"),
    CONSTRAINT "fk_admin_id" FOREIGN KEY ("fk_admin_id") REFERENCES "user" ("id")
);

CREATE TABLE "user_channel"
(
    "fk_user_id"    INT NOT NULL,
    "fk_channel_id" INT NOT NULL,
    PRIMARY KEY ("fk_user_id", "fk_channel_id"),
    CONSTRAINT "fk_user_id_uc" FOREIGN KEY ("fk_user_id") REFERENCES "user" ("id"),
    CONSTRAINT "fk_channel_id_uc" FOREIGN KEY ("fk_channel_id") REFERENCES "channel" ("id")
);

ALTER TABLE "message" ADD COLUMN "fk_channel_id" INT;
ALTER TABLE "message" ADD CONSTRAINT "fk_channel_id" FOREIGN KEY ("fk_channel_id") REFERENCES "channel" ("id");