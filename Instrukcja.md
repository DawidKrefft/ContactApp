# ContactApp
1. Należy utworzyć bazę danych plikiem TworzenieBazy.sql lub wkleić i uruchomić podany niżej kod sql.
   Korzytstałem z Microsoft SQL Server Management Studio.


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

1.5. Otworzyć solucję w folderze ContactApp -> api -> ContactApp

2. Po otworzeniu solucji ContactApp należy przejść do appsettings.json.

"ContactAppCon": "Data Source=DESKTOP-N9HS753\\TASK1SQL; Initial Catalog=contactappdb; User Id=sa; password=Admin123"


"ContactAppCon": "Data Source=nazwa servera sql w microsoft management studio; Initial Catalog=nazwa bazy danych; User Id=login; password=hasło (wg sql server authentication)"


Należy zmienić na swoje dane, by utworzyć połączenie z bazą danych.


3. W przypadku braku pakietów w solucji ContactApp Dependencies-> Packages : Microsoft.AspNetCore.Mvc.NewtonsoftJson (3.1.16) , System.Data.SqlClient (4.8.2)
   należy je zainstalować klikając prawym na lolucję, wybierając opcję manage NuGet Packages.

4. Zacząć debbugować ContactApp w celu uruchomienia api

5. Otworzyć solucję w ContactApp -> ui -> BlazorApp

6. W appsettings.json zmienić

"API_URL": "http://localhost:24292/api/",
"PHOTO_URL": "http://localhost:24292/Photos",

na swoje url

7. W przypadku braku w Dependencies -> Packages: BlazorInputFile (0.2.0) trzeba zainstalować.
