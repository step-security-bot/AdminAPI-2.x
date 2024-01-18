\---

confluence-id: 138646866

confluence-space: %%CONFLUENCE-SPACE%%

\---

First-Time Configuration for Admin API 1.x
==========================================

Created by Jason Hoekstra, last modified on Sep 15, 2023

After [Installing the Admin API](Admin-API-1.x---Docker-installation_138646837.html), there are a couple manual steps that must be completed before the application can be used.

/\*<!\[CDATA\[\*/ div.rbtoc1705534708915 {padding: 0px;} div.rbtoc1705534708915 ul {margin-left: 0px;} div.rbtoc1705534708915 li {margin-left: 0px;padding-left: 0px;} /\*\]\]>\*/

*   [1\. Launch the Application](#FirstTimeConfigurationforAdminAPI1.x-1.LaunchtheApplication)
*   [2\. Create the First API Client](#FirstTimeConfigurationforAdminAPI1.x-2.CreatetheFirstAPIClient)
*   [Optional - Self-Signed Certificates](#FirstTimeConfigurationforAdminAPI1.x-Optional-Self-SignedCertificates)

1\. Launch the Application
==========================

The database is now initialized and ready to function. Visit the configured URL from a browser (or launch from IIS or Docker Desktop if using those) and verify the application is running. The root page (`/`) should return JSON with the API and Build versions. Additionally you can visit `/swagger/index.html` if Swagger is enabled in appsettings and view the web-based documentation.

  

  

**Sample Response from /**

{
  "version": "1.0",
  "build": "1.0.0.0"
}

![](attachments/138646866/138646867.png)

2\. Create the First API Client
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

Optional - Self-Signed Certificates
===================================

If using a self-signed certificate for a developer or other non-production instances of Admin API, add "Encrypt=False" to appSettings.json in the ConnectingStrings section to allow them to function.  Below is a screen capture of the error that will display using self-signed certificates.  The ASP.NET Core client does not trust these certificates by default and this parameter will allow development environments to continue with self-signed certificates.

![](attachments/138646866/162202477.png)

Example below:

"ConnectionStrings": {
        "Admin": "Data Source=.\\\\;Initial Catalog=EdFi\_Admin;Integrated Security=True;Encrypt=False",
        "Security": "Data Source=.\\\\;Initial Catalog=EdFi\_Security;Integrated Security=True;Encrypt=False"
    },

Attachments:
------------

![](images/icons/bullet_blue.gif) [image2022-8-3\_17-32-20.png](attachments/138646866/138646867.png) (image/png)  
![](images/icons/bullet_blue.gif) [image-2023-6-22\_17-44-17.png](attachments/138646866/162202477.png) (image/png)