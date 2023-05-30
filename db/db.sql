create sequence user_id_seq
    as integer;

alter sequence user_id_seq owner to "user";

create sequence role_id_seq
    as integer;

alter sequence role_id_seq owner to "user";

create sequence ware_id_seq
    as integer;

alter sequence ware_id_seq owner to "user";

create sequence order_id_seq
    as integer;

alter sequence order_id_seq owner to "user";

create sequence service_id_seq
    as integer;

alter sequence service_id_seq owner to "user";

create sequence ware_order_id_seq
    as integer;

alter sequence ware_order_id_seq owner to "user";

create sequence order_service_id_seq
    as integer;

alter sequence order_service_id_seq owner to "user";

create table "Role"
(
    "Id"     integer default nextval('role_id_seq'::regclass) not null
        constraint role_pk
            primary key,
    "Value"  text                                             not null
        constraint role_pk2
            unique,
    "Salary" numeric(7, 2)                                    not null
);

alter table "Role"
    owner to "user";

alter sequence role_id_seq owned by "Role"."Id";

create table "User"
(
    "Id"           integer default nextval('user_id_seq'::regclass) not null
        constraint "User_pk"
            primary key,
    "Email"        text                                             not null,
    "Password"     text                                             not null,
    "Full_name"    text                                             not null,
    "Phone_number" text                                             not null,
    "Date_reg"     date                                             not null,
    "Role_id"      integer
        constraint "User_role_id_fk"
            references "Role"
);

alter table "User"
    owner to "user";

alter sequence user_id_seq owned by "User"."Id";

create table "Ware"
(
    "Id"          integer default nextval('ware_id_seq'::regclass) not null
        constraint "Ware_pk"
            primary key,
    "Item"        text                                             not null,
    "Image"       text                                             not null,
    "Description" text                                             not null,
    "Quantity"    integer                                          not null,
    "Category"    text                                             not null
);

alter table "Ware"
    owner to "user";

alter sequence ware_id_seq owned by "Ware"."Id";

create table "Order"
(
    "Id"          integer default nextval('order_id_seq'::regclass) not null
        constraint "Order_pk"
            primary key,
    "Description" text,
    "User_id"     integer                                           not null
        constraint "Order_user_id_fk"
            references "User"
);

alter table "Order"
    owner to "user";

alter sequence order_id_seq owned by "Order"."Id";

create table "Service"
(
    "Id"          integer default nextval('service_id_seq'::regclass) not null
        constraint "Service_pk"
            primary key,
    "Name"        text                                                not null,
    "Description" text                                                not null,
    "Price"       numeric(7, 2)                                       not null
);

alter table "Service"
    owner to "user";

alter sequence service_id_seq owned by "Service"."Id";

create table "Ware_order"
(
    "Id"       integer default nextval('ware_order_id_seq'::regclass) not null
        constraint "Ware_order_pk"
            primary key,
    "Ware_id"  integer                                                not null
        constraint ware_order_ware_id_fk
            references "Ware",
    "Order_id" integer                                                not null
        constraint ware_order_order_id_fk
            references "Order"
);

alter table "Ware_order"
    owner to "user";

alter sequence ware_order_id_seq owned by "Ware_order"."Id";

create table "Order_service"
(
    "Id"         integer default nextval('order_service_id_seq'::regclass) not null
        constraint "Order_service_pk"
            primary key,
    "Service_id" integer                                                   not null
        constraint "Order_service_service_id_fk"
            references "Service",
    "Order_id"   integer                                                   not null
        constraint order_service_order_id_fk
            references "Order",
    "Status"     text                                                      not null
);

alter table "Order_service"
    owner to "user";

alter sequence order_service_id_seq owned by "Order_service"."Id";

