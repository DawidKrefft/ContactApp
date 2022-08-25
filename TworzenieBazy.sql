
--Execute the code to create database, add and display records.

CREATE DATABASE [contactappdb]
GO
USE [contactappdb]

CREATE TABLE dbo.Category(
CategoryId int identity(1,1),
CategoryName nvarchar(500)
)


CREATE TABLE dbo.Contact(
ContactId int identity(1,1),
ContactName nvarchar(500),
Category nvarchar(500),
DateOfBirth datetime,
PhotoFileName nvarchar(500),
PhoneNumber nvarchar(500)
)

INSERT INTO dbo.Category VALUES ('Boss')
INSERT INTO dbo.Category VALUES ('Client')

INSERT INTO dbo.Contact VALUES ('Tom','Boss',getdate(),'sample.png','123123123')


select* from dbo.Contact
select* from dbo.Category