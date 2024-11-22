CREATE DATABASE OUTIS89;
USE OUTIS89;

CREATE TABLE users(
ID INT IDENTITY (1,1) NOT NULL,
name VARCHAR(10)  NOT NULL,
email VARCHAR(200) NOT NULL,
role VARCHAR(20) NOT NULL,
Password VARCHAR(255) NOT NULL
);




select * from users;

CREATE TABLE active(
email varchar(300),
id varchar(20)
)

INSERT INTO active values
('no','0');

select * from active;


 create table LecturerClaims(
 id int not null identity(1,1),
 username varchar(200) ,
email VARCHAR(255)  NULL,
 module  varchar(200),
 rate  varchar(200),
 hours_worked  varchar(200),
 description  varchar(500),
 total  varchar(200),
 filename  varchar(200),
 filepath  varchar(500),
 status  varchar(200)
 )

 select * from LecturerClaims;