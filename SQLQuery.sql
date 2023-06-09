use Test
go

﻿if exists (select * from sys.objects 
where object_id = OBJECT_ID(N'[dbo].[JsonFields]') AND type in (N'U'))
begin
	drop table JsonFields
end
create table JsonFields (	
	FieldName nvarchar(50) primary key
	)
	insert into JsonFields values ('Host'),('origin'),('url')
go

if exists (select * from sys.objects 
where object_id = OBJECT_ID(N'[dbo].[Settings]') AND type in (N'U'))
begin
	drop table Settings
end
create table Settings(
	Name nvarchar(50) primary key,
	Value nvarchar(50)
	)
insert into Settings values ('targetUrl', 'https://httpbin.org/anything')
go

if exists (select * from sys.objects 
where object_id = OBJECT_ID(N'[dbo].[JsonData]') AND type in (N'U'))
begin
	drop table JsonData
end
create table JsonData(
	Id int primary key identity,
	Name nvarchar(50),
	Value nvarchar(50)
	)
go