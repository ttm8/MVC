# mecca17
Lecturer Claims Management System

Project Description

The Lecturer Claims Management System is a .NET Core MVC web application that allows lecturers to submit claims for approval and enables program coordinators and managers to review and process them. 

It includes features such as claim submission, document uploads, claim status tracking, and feedback management.

Features



Lecturer Features:

Submit claims for hours worked.

Upload supporting documents (e.g., PDFs).

View claim statuses (Pending, Approved, Rejected).

Coordinator/Manager Features:

Review submitted claims.

Approve or reject claims with feedback.

Update claim statuses in real-time.

Database:

Store claims, documents, and feedback.

Use of LecturerClaim table for claim management.


Table Schema (LecturerClaims)

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

 


 Technologies Used
 
Framework: ASP.NET Core MVC

Language: C#

Database: SQL Server

Frontend: Razor Pages, HTML, CSS, Bootstrap

File Storage: Local storage for uploaded documents


Configure Database Connection:

Update the connectionString in the appsettings.json file with your database details:
json
Copy code
"ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=mecca17;Trusted_Connection=True;TrustServerCertificate=True;"
}



Usage

Lecturer Workflow:

-Log in or register.

-Submit claims with hours worked, rate, and description.

-Attach supporting documents.

-Track claim status.

Program Coordinator Workflow:

-Log in as a coordinator.

-View all submitted claims.

-Approve or reject claims with feedback.

-Claim status updates automatically in the database.
