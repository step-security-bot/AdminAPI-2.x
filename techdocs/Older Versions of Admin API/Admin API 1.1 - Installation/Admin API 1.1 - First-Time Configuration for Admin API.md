Created by Jason Hoekstra on Jan 20, 2023

After [Installing the Admin API](Admin-API-1.1---Admin-API---Docker-installation_138647023.html), there are a couple manual steps that must be completed before the application can be used.

/\*<!\[CDATA\[\*/ div.rbtoc1705534704868 {padding: 0px;} div.rbtoc1705534704868 ul {margin-left: 0px;} div.rbtoc1705534704868 li {margin-left: 0px;padding-left: 0px;} /\*\]\]>\*/

*   [1\. IIS ONLY: Initialize Admin API Database Tables](#AdminAPI1.1FirstTimeConfigurationforAdminAPI-1.IISONLY:InitializeAdminAPIDatabaseTables)
*   [2\. Launch the Application](#AdminAPI1.1FirstTimeConfigurationforAdminAPI-2.LaunchtheApplication)
*   [3\. Create the First API Client](#AdminAPI1.1FirstTimeConfigurationforAdminAPI-3.CreatetheFirstAPIClient)

1\. IIS ONLY: Initialize Admin API Database Tables
==================================================

This step is only required when installing manually using the Nuget application package under Internet Information Server (IIS).  Additional tables are required for storing client authentication for Admin API, which need to be initialized manually, as shown below.

Please execute the below script against the Admin App / Admin API database, using SQL Server Management Studio, Azure Data Studio, PowerShell SQL Tools, psql.exe, or PgAdmin as per your database (SQL Server or Postgres) and database tool preference. 

Execute the below script against the Admin App / Admin API database using psql , PgAdmin, or the tool of your choice.

**adminapi-tables-pgsql.sql**

CREATE SCHEMA IF NOT EXISTS adminapi;

CREATE TABLE adminapi.Applications (
    Id INT NOT NULL GENERATED ALWAYS AS IDENTITY,
    ConcurrencyToken VARCHAR(128) NULL,
    ClientId VARCHAR(256) NULL,
    ClientSecret VARCHAR(256) NULL,
    Type VARCHAR(256) NULL,
    ConsentType VARCHAR(256) NULL,
    Permissions VARCHAR NULL,
    Properties VARCHAR NULL,
    Requirements VARCHAR NULL,
    DisplayName VARCHAR(256) NULL,
    DisplayNames VARCHAR NULL,
    RedirectUris VARCHAR NULL,
    PostLogoutRedirectUris VARCHAR NULL,
    CONSTRAINT PK\_Applications PRIMARY KEY (Id)
);

CREATE TABLE adminapi.Scopes (
    Id INT NOT NULL GENERATED ALWAYS AS IDENTITY,
    Name VARCHAR(256) NULL,
    ConcurrencyToken VARCHAR(128) NULL,
    Description VARCHAR NULL,
    Descriptions VARCHAR NULL,
    DisplayName VARCHAR(256) NULL,
    DisplayNames VARCHAR NULL,
    Properties VARCHAR NULL,
    Resources VARCHAR NULL,
    CONSTRAINT PK\_Scopes PRIMARY KEY (Id)
);

CREATE TABLE adminapi.Authorizations (
    Id INT NOT NULL GENERATED ALWAYS AS IDENTITY,
    ConcurrencyToken VARCHAR(128) NULL,
    ApplicationId int NOT NULL,
    Scopes VARCHAR NULL,
    Subject VARCHAR(256) NULL,
    Status VARCHAR(256) NULL,
    Properties VARCHAR NULL,
    CreationDate TIMESTAMP NULL,
    CONSTRAINT PK\_Authorizations PRIMARY KEY (Id),
    CONSTRAINT FK\_AuthorizationsId\_ApplicationId FOREIGN KEY (ApplicationId) REFERENCES adminapi.Applications (Id) ON DELETE RESTRICT
);

CREATE TABLE adminapi.Tokens (
    Id INT NOT NULL GENERATED ALWAYS AS IDENTITY,
    ConcurrencyToken VARCHAR(128) NULL,
    ApplicationId int NULL,
    AuthorizationId int NULL,
    Type VARCHAR(256) NULL,
    CreationDate TIMESTAMP NULL,
    ExpirationDate TIMESTAMP NULL,
    RedemptionDate TIMESTAMP NULL,
    Payload VARCHAR NULL,
    Properties VARCHAR NULL,
    Subject VARCHAR(256) NULL,
    Status VARCHAR(256) NULL,
    ReferenceId VARCHAR(256) NULL,
    CONSTRAINT PK\_Tokens PRIMARY KEY (Id)
);

Execute the below script against the Admin App / Admin API database using SQL Server Management Studio, Azure Data Studio, PowerShell SQL Tools, or the tool of your choice.

**adminapi-tables-mssql.sql**

IF NOT EXISTS (SELECT 1 FROM sys.schemas WHERE name = 'adminapi')
BEGIN
EXEC( 'CREATE SCHEMA adminapi' );
END

CREATE TABLE adminapi.Applications (
    \[Id\] int identity NOT NULL,
    \[ConcurrencyToken\] NVARCHAR(128) NULL,
    \[ClientId\] NVARCHAR(256) NULL,
    \[ClientSecret\] NVARCHAR(256) NULL,
    \[Type\] NVARCHAR(256) NULL,
    \[ConsentType\] NVARCHAR(256) NULL,
    \[Permissions\] NVARCHAR(MAX) NULL,
    \[Properties\] NVARCHAR(MAX) NULL,
    \[Requirements\] NVARCHAR(MAX) NULL,
    \[DisplayName\] NVARCHAR(256) NULL,
    \[DisplayNames\] NVARCHAR(MAX) NULL,
    \[RedirectUris\] NVARCHAR(MAX) NULL,
    \[PostLogoutRedirectUris\] NVARCHAR(MAX) NULL,
    CONSTRAINT PK\_Applications PRIMARY KEY (Id)
);

CREATE TABLE adminapi.Scopes (
    \[Id\] int identity NOT NULL,
    \[Name\] NVARCHAR(256) NULL,
    \[ConcurrencyToken\] NVARCHAR(128) NULL,
    \[Description\] NVARCHAR(MAX) NULL,
    \[Descriptions\] NVARCHAR(MAX) NULL,
    \[DisplayName\] NVARCHAR(256) NULL,
    \[DisplayNames\] NVARCHAR(MAX) NULL,
    \[Properties\] NVARCHAR(MAX) NULL,
    \[Resources\] NVARCHAR(MAX) NULL,
    CONSTRAINT PK\_Scopes PRIMARY KEY (Id)
);

CREATE TABLE adminapi.Authorizations (
    \[Id\] int identity NOT NULL,
    \[ConcurrencyToken\] NVARCHAR(128) NULL,
    \[ApplicationId\] int NOT NULL,
    \[Scopes\] NVARCHAR(MAX) NULL,
    \[Subject\] NVARCHAR(256) NULL,
    \[Status\] NVARCHAR(256) NULL,
    \[Properties\] NVARCHAR(MAX) NULL,
    \[CreationDate\] DATETIME NULL,
    CONSTRAINT PK\_Authorizations PRIMARY KEY (Id),
    CONSTRAINT FK\_AuthorizationsId\_ApplicationId FOREIGN KEY (ApplicationId) REFERENCES adminapi.Applications (Id) ON DELETE NO ACTION,
);

CREATE TABLE adminapi.Tokens (
    \[Id\] int identity NOT NULL,
    \[ConcurrencyToken\] NVARCHAR(128) NULL,
    \[ApplicationId\] int NULL,
    \[AuthorizationId\] int NULL,
    \[Type\] NVARCHAR(256) NULL,
    \[CreationDate\] DATETIME NULL,
    \[ExpirationDate\] DATETIME NULL,
    \[RedemptionDate\] DATETIME NULL,
    \[Payload\] NVARCHAR(MAX) NULL,
    \[Properties\] NVARCHAR(MAX) NULL,
    \[Subject\] NVARCHAR(256) NULL,
    \[Status\] NVARCHAR(256) NULL,
    \[ReferenceId\] NVARCHAR(256) NULL,
    CONSTRAINT PK\_Tokens PRIMARY KEY (Id)
);

2\. Launch the Application
==========================

The database is now initialized and ready to function. Visit the configured URL from a browser (or launch from IIS or Docker Desktop if using those) and verify the application is running. The root page (`/`) should return JSON with the API and Build versions. Additionally you can visit `/swagger/index.html` if Swagger is enabled in appsettings and view the web-based documentation.

  

  

**Sample Response from /**

{
  "version": "1.0",
  "build": "1.0.0.0"
}

![](attachments/138647052/138647053.png)

3\. Create the First API Client
===============================

In order to authenticate with the API you must first register a client key and secret. This is a client for the _Admin API_, not an Application and key / secret for interfacing with the Ed-Fi ODS / API.

Client registration is done by sending a url-encoded form request to `/connect/register.` The ability to register new clients is similar to adding users in Admin App. By default, **this endpoint is only available when no clients have been created**. After the first client is created, the endpoint is disabled. The endpoint can be re-enabled with the `Authentication/AllowRegistration`  setting or environment variable (`false`  by default).

Only Enable Registration When Necessary

Since the `/connect/register`  endpoint does not require any authentication, it is recommended that it remains disabled when not registering a new client. Admin API v1.0 does not include any scope limitations, so all clients are able to utilize all endpoints.

The sample below is using `curl,`  however this request can be performed from Postman, the Swagger site, or from your application.

curl -X POST https://your-admin-api/connect/register -H "Content-Type: application/x-www-form-urlencoded" -d "ClientId=YourClientId&ClientSecret=YourClientSecret&DisplayName=YourDisplayName" 

After registering the client, verify it was created by retrieving an authorization token.

curl -X POST https://your-admin-api/connect/token-H "Content-Type: application/x-www-form-urlencoded" -d "client\_id=YourClientId&client\_secret=YourClientSecret&grant\_type=client\_credentials" 

This should return a JSON result including a bearer token. Note that the above request does _not_ include a request scope, so the token will be invalid for accessing most endpoints. See [Securing Admin API](Securing-Admin-API_133399675.html) for more info.

{
  "access\_token": "eyJhbGciOiJIUzI1NiIsInR5cCI6ImF0K2p3dCJ9.eyJzdWIiOiJ0ZXN0MSIsIm5hbWUiOiJ0ZXN0Iiwib2lfcHJzdCI6InRlc3QxIiwiY2xpZW50X2lkIjoidGVzdDEiLCJvaV90a25faWQiOiIzMDU2IiwiZXhwIjoxNjU5NTY5ODc4LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjE0LyIsImlhdCI6MTY1OTU2NjI3OH0.W8RMjmGIA-US6faXuG\_mbmfbRIDrvrc8QheW5imtj-k",
  "token\_type": "Bearer",
  "expires\_in": 3599
}

Attachments:
------------

![](images/icons/bullet_blue.gif) [image2022-8-3\_17-32-20.png](attachments/138647052/138647053.png) (image/png)