DROP TABLE IF EXISTS "user" CASCADE;
DROP TABLE IF EXISTS channel CASCADE;
DROP TABLE IF EXISTS message CASCADE;
DROP TABLE IF EXISTS user_channel CASCADE;

create table "user"
(
    id         serial
        primary key,
    username   varchar(255) not null
        constraint unique_username
            unique,
    password   varchar(255) not null,
    created_at timestamp    not null
);

create table channel
(
    id          serial
        primary key,
    name        varchar(255) not null
        constraint unique_channel_name
            unique,
    created_at  timestamp    not null,
    fk_admin_id integer      not null
        constraint fk_admin_id
            references "user"
);

create table message
(
    id            serial
        primary key,
    fk_user_id    integer      not null
        constraint fk_user_id
            references "user",
    text          varchar(255) not null,
    created_at    timestamp    not null,
    fk_channel_id integer
        constraint fk_channel_id
            references channel
);

create table user_channel
(
    id            serial primary key,
    fk_user_id    integer not null
        constraint fk_user_id_uc
            references "user",
    fk_channel_id integer not null
        constraint fk_channel_id_uc
            references channel
);