CREATE DATABASE mecca17
USE mecca17;


CREATE TABLE users(
ID INT IDENTITY (1,1) NOT NULL,
name VARCHAR(10)  NOT NULL,
email VARCHAR(200) NOT NULL,
role VARCHAR(20) NOT NULL
)

ALTER TABLE users 
ADD password VARCHAR(100) NOT NULL;

ALTER TABLE users 
DROP COLUMN password;



SELECT * FROM users;



CREATE TABLE LecturerClaim(
id int not null identity(1,1),
email varchar (250) not null,
module varchar(250) not null,
user_id varchar(250) not null,
hours varchar(250) not null,
rate varchar(250) not null,
description  varchar(250) not null,
file_name varchar(250) not null,
file_path varchar(250) not null,
total varchar(200),
files varchar(300) not null,
status varchar(28) not null

)

CREATE TABLE active(
email varchar(300),
id varchar(20)
)

INSERT INTO active values
('no','0');

SELECT * FROM LecturerClaim;
SELECT * FROM active;