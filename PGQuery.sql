drop database if exists "Test"
create database "Test"
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'Russian_Russia.1251'
    LC_CTYPE = 'Russian_Russia.1251'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;

drop table if exists "JsonFields";
create table "JsonFields" (	
	"FieldName" varchar (50) unique not null
	);
insert into "JsonFields" values ('Host'),('origin'),('url');

drop table if exists "Settings";
create table "Settings"(
	"Name" varchar(50) primary key,
	"Value" varchar(50)
	);
insert into "Settings" values ('targetUrl', 'https://httpbin.org/anything');

drop table if exists "JsonData";
create table "JsonData"(
	"Id" serial primary key unique not null,
	"Name" varchar(50),
	"Value" varchar(50)
	);
