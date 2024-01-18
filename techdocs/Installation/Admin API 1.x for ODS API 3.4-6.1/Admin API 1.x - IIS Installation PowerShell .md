\---

confluence-id: 138646902

confluence-space: %%CONFLUENCE-SPACE%%

\---

Admin API 1.x - IIS Installation (PowerShell)
=============================================

Created by Suganya Rajendran, last modified by Jason Hoekstra on Nov 02, 2023

**Contents:**

/\*<!\[CDATA\[\*/ div.rbtoc1705534708112 {padding: 0px;} div.rbtoc1705534708112 ul {margin-left: 0px;} div.rbtoc1705534708112 li {margin-left: 0px;padding-left: 0px;} /\*\]\]>\*/

*   [Before You Install](#AdminAPI1.xIISInstallation(PowerShell)-BeforeYouInstall)
    *   [Compatibility & Supported ODS / API Versions](#AdminAPI1.xIISInstallation(PowerShell)-Compatibility&SupportedODS/APIVersions)
*   [Installation Instructions](#AdminAPI1.xIISInstallation(PowerShell)-InstallationInstructions)
    *   [Prerequisites](#AdminAPI1.xIISInstallation(PowerShell)-Prerequisites)
    *   [Installation Steps](#AdminAPI1.xIISInstallation(PowerShell)-InstallationSteps)

Before You Install
==================

This section provides general information to review before installing the Ed-Fi ODS / API Admin API for v1.3.1.

Compatibility & Supported ODS / API Versions
--------------------------------------------

This version of the Admin API has been tested and can be installed for use with the Ed-Fi ODS / API v3.4-6.1. See the [Ed-Fi Technology Version Index](https://techdocs.ed-fi.org/display/ETKB/Ed-Fi+Technology+Version+Index) for more details.

Installation Instructions
=========================

Prerequisites
-------------

A running instance of the ODS / API v3.4 - 6.1 platform must be configured and running before installing Admin API.  

Admin API only supports running one instance of the application at a time in an ODS / API ecosystem. Future versions may allow for scaling and load balancing.

Admin API does not support in-place upgrades from prior versions.  Please install a fresh copy of Admin API to upgrade from prior versions.

The following are required to install the Admin API with IIS:

*   Enable IIS (before installing .NET Hosting Bundle).
*   Install [.NET 6 Hosting Bundle v6.0.6 or higher](https://dotnet.microsoft.com/en-us/download/dotnet/6.0). After installing the .NET Hosting Bundle, it may be necessary to restart the computer for the changes to take effect.

Installation Steps
------------------

Each step is outlined in detail below for the PowerShell deployment. Ensure that you have permission to execute PowerShell scripts. For more information, see http://go.microsoft.com/fwlink/?LinkID=135170.

### **Step 1. Rename and Unzip Admin API Source Files**

Download and rename the linked Nuget Package (.npkg) to .zip

Unzip the contents.

![](attachments/138646902/138646919.png)

There will be two folders. AdminApi folder will have binaries. Installer folder contains PowerShell scripts required for installation. 

### Step 2. Configure Installation

Open the "install.ps1" file in a text editor. You will need to edit this file with your configuration details. If a value is not present for any of the parameters, it will use its default value.

**Note: Editing Items 3(a, b) and 4b below are mandatory for installation to complete.**

1.  Configure `$dbConnectionInfo`. These values are used to construct the connection strings.
    1.  `Server`. The name of the database server. For a local server, we can use "(local)" for SQL and "localhost" for PostgreSQL.
        
    2.  `Engine.` Admin App supports SQL and PostgreSQL database engines. So setting up the `Engine` will decide which database engine to be used. Valid values are "SQLServer" and "PostgreSQL".
    3.  `UseIntegratedSecurity.` Will either be "$true" or "$false".
        1.  If you plan to use Windows authentication, this value will be "$true"
        2.  If you plan to use SQL Server/ PostgreSQL server authentication, this value will be "$false" and the Username and `Password` must be provided.
    4.  `Username`. Optional. The username to connect to the database. If `UseIntegratedSecurity` is set to $true, this entry is not needed
    5.  `Password`. Optional. The password to connect to the database. If `UseIntegratedSecurity` is set to $true, this entry is not needed
    6.  `Port.` Optional. Used to specify the database server port, presuming the server is configured to use the specific port.
2.  Configure `$adminAppFeatures`. These values are used to set Optional overrides for features and settings in the appsetting.json.
    1.  `ApiMode.` Possible values: `sharedinstance`, `districtspecific` and `yearspecific`. Defaults to `sharedinstance`
3.  Configure `$authenticationSettings`. These values are mandatory for authentication process.
    

               a. `SigningKey:` must be a Base64-encoded string  
               b. `Authority and IssuerUrl:` should be the same URL as your application  
               c. `AllowRegistration:` to true allows unrestricted registration of new Admin API clients. This is not recommended for production. 

     4. Configure `$p`. This is the variable used to send all the information to the installation process.

1.  1.  `ToolsPath`. Path for storing installation tools, e.g., nuget.exe. Defaults to "C:/temp/tools"
    2.  **`OdsApiUrl`. Base URL for the ODS / API. Mandatory.**
    3.  `PackageVersion`. Optional. If not set, will retrieve the latest full release package.

$dbConnectionInfo = @{  
        Server = "(local)"  
        Engine = "SqlServer"  
        UseIntegratedSecurity = $false  
        Username = "exampleAdmin"  
        Password = "examplePassword"  
}  
$adminApiFeatures = @{  
    ApiMode = "sharedinstance"  
}  
$authenticationSettings = @{  
    Authority = "[https://localhost/adminapi](https://localhost/adminapi)"  
    IssuerUrl = ""[https://localhost/adminapi](https://localhost/adminapi)"  
    SigningKey = "Base64-encoded string"  
    AllowRegistration = $false  
}  
$p = @{  
    ToolsPath = "C:/temp/tools"  
    DbConnectionInfo = $dbConnectionInfo  
    OdsApiUrl = "[http://web-api.example.com/WebApi](http://web-api.example.com/WebApi)"  
    PackageVersion = '1.3.1.0'  
    PackageSource = $adminApiSource  
    AuthenticationSettings = $authenticationSettings  
    AdminApiFeatures = $adminApiFeatures  
}

$dbConnectionInfo = @{  
        Server = "localhost"  
        Engine = "PostgreSQL"  
        UseIntegratedSecurity = $false  
        Username = "postgres"  
        Password = "examplePassword"  
}  
$adminApiFeatures = @{  
    ApiMode = "sharedinstance"  
}  
$authenticationSettings = @{  
    Authority = "[https://localhost/adminapi](https://localhost/adminapi)"  
    IssuerUrl = ""[https://localhost/adminapi](https://localhost/adminapi)"  
    SigningKey = "Base64-encoded string"  
    AllowRegistration = $false  
}  
$p = @{  
    ToolsPath = "C:/temp/tools"  
    DbConnectionInfo = $dbConnectionInfo  
    OdsApiUrl = "[http://web-api.example.com/WebApi](http://web-api.example.com/WebApi)"  
    PackageVersion = '1.3.1.0'  
    PackageSource = $adminApiSource  
    AuthenticationSettings = $authenticationSettings  
    AdminApiFeatures = $adminApiFeatures  
}

### **Step 3. Open a PowerShell Prompt in Administrator Mode** 

Method 1: Open \[Windows Key\]-R which will open a Run dialog for tasks needing administrative privileges. Type "PowerShell" to open a PowerShell prompt in Administrator mode.

![](attachments/138646902/138646925.png)

Method 2: Click on the Windows icon in the lower-left corner. Type "PowerShell" and right-click the "Windows PowerShell" option when provided. Select "Run as Administrator" to open a PowerShell prompt in Administrator mode.

![](attachments/138646902/138646926.png)

Change the directory to the unzipped directory for the Admin Api Installer.

### **Step 4 .** **Run the Installation via PowerShell**

Run "install.ps1" script.

### Database login setup on integrated security mode:

During the installation process, you will be prompted to choose database login details. Entering "Y" will continue with default option (Installation process will create IIS APPPOOL\\AdminApi database login on the server).

Choosing 'n' will prompt you to enter windows username. The installation process will validate and create database login using entered username, if the login does not exist on the database server already. 

![](attachments/138646902/138646927.png)

### **Step 5. Verify SQL Server Login**

The installation process sets up an appropriate SQL Login for use with the dedicated AdminApi Application Pool in IIS. You can verify this in SQL Server Management Studio:

On the Server Roles page, make sure that  "public" and "sysadmin" checkboxes are checked. Once you have confirmed a proper SQL Server login exists, continue to the next step. 

### **Step 6. Update Application Pool Identity (Optional)**

As mentioned on Step 5, installation process sets up an appropriate SQL Login for use with the dedicated AdminApi Application Pool in IIS. If you would like to use the default "ApplicationPoolIdentity", then you can skip this bit.

Else in the Advanced Settings window, click on the browse icon under Process Model > Identity. We'll choose the custom account option and click "Set...". When setting the credentials, you can just use the username and password that you use to log in to Windows. If you need to include the app pool domain in the username, then the username can look something like this: "localhost\\username", where "localhost" is the app pool domain. Once we have entered the correct credentials, we'll click OK on all screens until we're back to the main Application Pools page.

![](attachments/138646902/138646933.png)

### Step 7. Confirming appSettings.json

Change `EnableSwagger`  to `true` to enable generation of the Swagger UI documentation.

*   *   This is **not** recommended for production.

### **Step 8.** Initialize Admin API Database Tables

Additional tables are required for storing client authentication for Admin API, which need to be initialized manually, as shown below.

Please execute the below script against the EdFi\_Admin database, using SQL Server Management Studio, Azure Data Studio, PowerShell SQL Tools, psql.exe, or PgAdmin as per your database (SQL Server or Postgres) and database tool preference. 

Execute the below script against the EdFi\_Admin database using psql , PgAdmin, or the tool of your choice.

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

Execute the below script against the EdFi\_Admin  database using SQL Server Management Studio, Azure Data Studio, PowerShell SQL Tools, or the tool of your choice.

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

### **Step 9. Execute First-Time Configuration**

Continue on to [First-Time Configuration for Admin 1.x](First-Time-Configuration-for-Admin-API-1.x_138646866.html).

**Admin API Installation for Docker or On-Premise IIS**

The following is a Nuget package containing the **Admin API v1.3.1** binaries and installer scripts for deployment to IIS.

*   [EdFi.Suite3.ODS.AdminApi 1.3.1](https://dev.azure.com/ed-fi-alliance/Ed-Fi-Alliance-OSS/_artifacts/feed/EdFi/NuGet/EdFi.Suite3.ODS.AdminApi/overview/1.3.1)
*   [Follow Binary Release for Admin App Database v2.3](https://techdocs.ed-fi.org/display/ADMIN/Admin+App+for+Suite+3+v2.3)

Attachments:
------------

![](images/icons/bullet_blue.gif) [image2019-7-16\_12-58-41.png](attachments/138646902/138646903.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2019-7-18\_12-55-24.png](attachments/138646902/138646904.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2019-7-17\_16-15-57.png](attachments/138646902/138646905.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2019-7-17\_16-8-37.png](attachments/138646902/138646906.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2019-7-17\_16-1-46.png](attachments/138646902/138646907.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2019-7-17\_15-57-20.png](attachments/138646902/138646908.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2019-7-17\_15-53-53.png](attachments/138646902/138646909.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2019-7-17\_14-21-50.png](attachments/138646902/138646910.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2019-7-17\_14-19-7.png](attachments/138646902/138646911.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2019-7-17\_14-17-7.png](attachments/138646902/138646912.png) (image/png)  
![](images/icons/bullet_blue.gif) [Empty Installation Folder.png](attachments/138646902/138646913.png) (image/png)  
![](images/icons/bullet_blue.gif) [Populated Installation Folder.png](attachments/138646902/138646914.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2019-7-17\_16-31-47.png](attachments/138646902/138646915.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2022-8-12\_11-59-40.png](attachments/138646902/138646916.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2023-1-19\_12-43-31.png](attachments/138646902/138646919.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2020-4-20\_12-37-43.png](attachments/138646902/138646925.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2020-4-20\_12-37-57.png](attachments/138646902/138646926.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2023-1-19\_13-38-10.png](attachments/138646902/138646927.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2023-1-19\_13-45-17.png](attachments/138646902/138646932.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2022-9-20\_12-24-43.png](attachments/138646902/138646933.png) (image/png)