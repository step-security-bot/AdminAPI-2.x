\---

confluence-id: 138647037

confluence-space: %%CONFLUENCE-SPACE%%

\---

Admin API 1.1 - Admin API - IIS Installation
============================================

Created by Jason Hoekstra on Jan 20, 2023

**Contents:**

/\*<!\[CDATA\[\*/ div.rbtoc1705534704436 {padding: 0px;} div.rbtoc1705534704436 ul {margin-left: 0px;} div.rbtoc1705534704436 li {margin-left: 0px;padding-left: 0px;} /\*\]\]>\*/

*   [Before You Install](#AdminAPI1.1AdminAPIIISInstallation-BeforeYouInstall)
    *   [Compatibility & Supported ODS / API Versions](#AdminAPI1.1AdminAPIIISInstallation-Compatibility&SupportedODS/APIVersions)
    *   [IMPORTANT INFORMATION FOR VERSION 1.1.0](#AdminAPI1.1AdminAPIIISInstallation-IMPORTANTINFORMATIONFORVERSION1.1.0)
*   [Installation Instructions](#AdminAPI1.1AdminAPIIISInstallation-InstallationInstructions)
    *   [Prerequisites](#AdminAPI1.1AdminAPIIISInstallation-Prerequisites)
    *   [Installation Steps](#AdminAPI1.1AdminAPIIISInstallation-InstallationSteps)

**Admin API Installation for Docker or On-Premise IIS**

The following is a Nuget package containing the **Admin API v1.1.0 source** **files** for manual deployment to IIS.

*   EdFi.Suite3.ODS.Admin.Api 1.1.0
*   Follow Binary Release for Admin App Database v2.3

Before You Install
==================

This section provides general information you should review before installing the Ed-Fi ODS / API Admin API for Suite 3 v1.1.0.

Compatibility & Supported ODS / API Versions
--------------------------------------------

This version of the Admin API has been tested and can be installed for use with the Ed-Fi ODS / API v3.4 through v5.3.  
See the [Ed-Fi Technology Version Index](https://techdocs.ed-fi.org/display/ETKB/Ed-Fi+Technology+Version+Index) for more details.

IMPORTANT INFORMATION FOR VERSION 1.1.0
---------------------------------------

Manual First-Time Setup

Admin API version 1.1.0 requires minor extra setup after the instructions outlined below. These may be automated in future releases, and may require additional steps to support upgrading. See [Admin API 1.1 - First-Time Configuration for Admin API](Admin-API-1.1---First-Time-Configuration-for-Admin-API_138647052.html) for more details.

Load Balancing and Horizontal Scaling Not Supported

Admin API version 1.1.0 only supports running one instance of the application at a time in an ODS / API ecosystem. Future versions may allow for scaling and load balancing.

Manual IIS Installation

Admin API v1.1.0 does **not** include a PowerShell installer script. Installation to IIS is manual, as outlined below.

Upgrading

Admin API does not today support in-place upgrades from prior versions.  Please install a fresh copy of Admin API to upgrade from prior versions.

Installation Instructions
=========================

Prerequisites
-------------

The following are required to install the Admin API with IIS:

*   Enable IIS (before installing .NET Hosting Bundle).
*   Install [.NET 6 Hosting Bundle v6.0.6 or higher](https://dotnet.microsoft.com/en-us/download/dotnet/6.0). After installing the .NET Hosting Bundle, it may be necessary to restart the computer for the changes to take effect.

Installation Steps
------------------

### **Step 1. Create Admin API Directory**

Create a directory to hold all of the Admin API source files. In this example, we'll use a directory on the following path: "C:\\Ed-Fi\\AdminAPI".

![](attachments/138647037/138647041.png)

  

### **Step 2. Rename and Unzip Admin API Source Files**

Download and rename the linked Nuget Package (.npkg) to .zip

Unzip the contents into the folder created in Step 1.

![](attachments/138647037/138647040.png)

### **Step 3. Ensure SQL Server Login Exists**

In SQL Server Management Studio, make sure that you have a login for the database server. Guidance on how to set this up is explained in this [blog post](https://blogs.msdn.microsoft.com/ericparvin/2015/04/14/how-to-add-the-applicationpoolidentity-to-a-sql-server-login/). A few basics:

*   You can choose either Windows authentication or SQL Server authentication here.
*   If you choose SQL Server authentication, make sure that if you have "User must change password at next login" checked. This means you must connect to SSMS with those credentials to reset the password. Otherwise, the app pools won't be able to connect.
*   On the Server Roles page, ensure the "dbcreator" checkbox is checked since Entity Framework will create the database when the application is launched. You must do this when using either Windows or SQL Server authentication.

Once you have confirmed a proper SQL Server login exists, continue to the next step. 

### **Step 4. Configure App/Web Configuration Files**

You will need to manually edit connection strings, authorization settings, and keys in "AdminApi\\appsettings.json". Some values to note:

*   Authentication Settings
    *   `Authentication:SigningKey`  must be a Base64-encoded string
    *   `Authentication:Authority`  and `Authentication:IssuerUrl`  should be the same URL as your application
    *   Changing `Authentication:AllowRegistration` to true allows unrestricted registration of new Admin API clients
        *   This is not recommended for production
*   Change `EnableSwagger`  to `true` to enable generation of the Swagger UI documentation
    *   This is **not** recommended for production.
*   The connection strings will need to be accurately configured by the user. For more information on how to determine connection strings for your database, please reference Microsoft documentation.

Here is a snippet from a properly configured application settings file:

**appsettings.json**

    "AppSettings": {
        "DatabaseEngine": "SqlServer",
        "DefaultOdsInstance": "EdFi ODS",
        "ProductionApiUrl": "https://YOUR\_SERVER\_NAME\_HERE/WebApi/",
        "SecurityMetadataCacheTimeoutMinutes": "10",
        "ApiStartupType": "YOUR-MODE-HERE",
        "LocalEducationAgencyTypeValue": "Local Education Agency",
        "PostSecondaryInstitutionTypeValue": "Post Secondary Institution",
        "SchoolTypeValue": "School",
        "PathBase": "",
        "GoogleAnalyticsMeasurementId": "",
        "ProductRegistrationUrl": "https://edfi-tools-analytics.azurewebsites.net/data/v1/"
    },
    "Authentication": {
        "Authority": "https://YOUR\_SERVER\_NAME\_HERE/AdminApi",
        "IssuerUrl": "https://YOUR\_SERVER\_NAME\_HERE/AdminApi",
        "SigningKey": "YS1sb25nLXNhbXBsZQ==",
        "AllowRegistration": false
    },
    "EnableSwagger": false,
    "EnableDockerEnvironment": false,
    "ConnectionStrings": {
        "Admin": "Data Source=(local);Initial Catalog=EdFi\_Admin;Trusted\_Connection=True",
        "Security": "Data Source=(local);Initial Catalog=EdFi\_Security;Trusted\_Connection=True",
        "ProductionOds": "Data Source=(local);Initial Catalog=EdFi\_Ods\_Production;Trusted\_Connection=True"
    },
    "Log4NetCore": {
        "Log4NetConfigFileName": "log4net\\\\log4net.config"
    },
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft": "Warning",
            "Microsoft.Hosting.Lifetime": "Information"
        }
    },
    "AllowedHosts": "\*"

### **Step 5. Create Self-Signed Certificate in IIS Manager**

  

Optional: Using Existing Certificate

This step is only necessary if you do not have a certificate from an existing ODS / API and Admin App installation in IIS.

Open IIS Manager, and double-click on "Server Certificates". Note that we must select a server/machine in the left sidebar to see this option.

![](attachments/138647037/138647042.png)

On the Server Certificate page, click on "Create Self-Signed Certificate..." on the Actions bar to the right.

![](attachments/138647037/138647043.png)

For the certificate, use "Ed-Fi-ODS" as the friendly name and make sure the certificate store is set to "Personal". Click OK. We will use this certificate in an upcoming step.

![](attachments/138647037/138647044.png)

### **Step 6. Create Necessary Application Pools**

Back in the IIS Manager main page, expand the server/machine on the left sidebar and click on Application Pools.

![](attachments/138647037/138647045.png)

Click on "Add Application Pool..." on the Actions bar to the right, enter "Ed-Fi" as the name. Click OK.

![](attachments/138647037/138647046.png)

Once that is created, click on the "Ed-Fi" application pool and select "Advanced Settings..." on the Actions bar to the right. Change the Start Mode to "AlwaysRunning".

![](attachments/138647037/138647047.png)

This next bit is optional if you want to use an app pool identity. If you would like to use the default "ApplicationPoolIdentity", then you can skip this section.

In the same Advanced Settings window, click on the browse icon under Process Model > Identity. Choose the custom account option and click "Set...". When setting the credentials, you can simply use the username and password that you use to log in to Windows. If you need to include the app pool domain in the username, then the username can look something like this: "localhost\\username", where "localhost" is the app pool domain. Once you've entered the correct credentials, click OK on all screens until you're back to the main Application Pools page.

![](attachments/138647037/138647048.png)

After that's done, we'll repeat the exact same steps in this section, but enter "Admin API" as the Application Pool name. We should now have 2 app pools.

![](attachments/138647037/138647038.png)

### **Step 8. Create Admin API Website**

On the left sidebar, we'll right-click on the newly created "Ed-Fi" website, select "Add Application...", and perform the following:

1.  Enter "Admin API" for the Alias.
2.  For the Application pool, select the Admin API app pool.
3.  The Physical path should be set to the path of the Admin API folder (located in directory with Admin API source files).
4.  Hit OK.

![](attachments/138647037/138647039.png)

### **Step 9. Execute First-Time Configuration**

Continue on to [Admin API 1.1 - First-Time Configuration for Admin API](Admin-API-1.1---First-Time-Configuration-for-Admin-API_138647052.html).

  

Attachments:
------------

![](images/icons/bullet_blue.gif) [image2022-8-12\_11-59-40.png](attachments/138647037/138647038.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2019-7-17\_16-31-47.png](attachments/138647037/138647039.png) (image/png)  
![](images/icons/bullet_blue.gif) [Populated Installation Folder.png](attachments/138647037/138647040.png) (image/png)  
![](images/icons/bullet_blue.gif) [Empty Installation Folder.png](attachments/138647037/138647041.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2019-7-17\_14-17-7.png](attachments/138647037/138647042.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2019-7-17\_14-19-7.png](attachments/138647037/138647043.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2019-7-17\_14-21-50.png](attachments/138647037/138647044.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2019-7-17\_15-53-53.png](attachments/138647037/138647045.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2019-7-17\_15-57-20.png](attachments/138647037/138647046.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2019-7-17\_16-1-46.png](attachments/138647037/138647047.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2019-7-17\_16-8-37.png](attachments/138647037/138647048.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2019-7-17\_16-15-57.png](attachments/138647037/138647049.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2019-7-18\_12-55-24.png](attachments/138647037/138647050.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2019-7-16\_12-58-41.png](attachments/138647037/138647051.png) (image/png)