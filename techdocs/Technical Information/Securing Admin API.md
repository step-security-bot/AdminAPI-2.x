Created by Patrick McVeety-Mill, last modified by Jason Hoekstra on Oct 17, 2022

The Ed-Fi ODS / API Admin API application is secured using internally managed clients and following the OAuth 2.0 "client credentials" specification designed for machine-to-machine communication (in other words, not for authenticating individual users).

The following documentation outlines how to register and authenticate against the Admin API using this framework.

A note on the word "client" in a system management API

In the documentation below, the word "client" refers to a client of the Admin API management system, and _not a client of the Ed-Fi ODS / API Platform_, which are managed via the Admin API.

For management of the ODS / API clients, see instead the Applications section in [Endpoints in Admin API 1.x](Endpoints-in-Admin-API-1.x_133399676.html).

/\*<!\[CDATA\[\*/ div.rbtoc1705534709162 {padding: 0px;} div.rbtoc1705534709162 ul {margin-left: 0px;} div.rbtoc1705534709162 li {margin-left: 0px;padding-left: 0px;} /\*\]\]>\*/

*   [Authenticating Requests](#SecuringAdminAPI-AuthenticatingRequests)
*   [Registering New Clients](#SecuringAdminAPI-RegisteringNewClients)
    *   [Enable registering clients by updating your application configuration to set Authentication:EnableRegistration to true . Restart the application.](#SecuringAdminAPI-EnableregisteringclientsbyupdatingyourapplicationconfigurationtosetAuthentication:EnableRegistrationtotrue.Restarttheapplication.)

  

Authenticating Requests
-----------------------

To authenticate your request to the Admin API, first retrieve a token by POSTing a URL-Encoded Form request to the /connect/token endpoint.

|     |     |     |     |     |     |
| --- | --- | --- | --- | --- | --- |
| Endpoint | Http Verb | Description | Request Schema | Response Schema (Success) | Response Schema (Error) |
| /connect/token | POST | Retrieve a bearer token for Admin API | *   client\_id="string"<br>    <br>*   client\_secret="string"<br>    <br>*   grant\_type="client\_credntials"<br>    <br>*   scope="edfi\_admin\_api/full\_access" | {  <br>  "access\_token": "string",  <br>  "token\_type": "Bearer",  <br>  "expires\_in": 0  <br>} | {  <br>  "error": "string",  <br>  "error\_description": "string",  <br>  "error\_uri": "string"  <br>} |

Save the resulting `access_token` from the response and include it as a Bearer token in the `Authorization`  header on subsequent requests. Note the `expires_in`  time and request new tokens accordingly.

Security Endpoints in Admin API

The endpoints outlined here do not follow the same patterns as those documented in [Endpoints in Admin API 1.x](Endpoints-in-Admin-API-1.x_133399676.html). They are not prefixed with a version and do not wrap their responses in the same format consistently.

These endpoints are managed separately from API endpoints in order to support OAuth 2.0 internally and may change in future releases.

Scope and Authorization

Admin API v1 includes a single _scope_ of authorization (`e`dfi\_admin\_api/full\_access) which allows access to all functional endpoints when granted to a given client.

By default, all registered clients are granted access to this scope, however you MUST request the scope when requesting a token. See endpoint description above for detail.

_Future releases may introduce additional scopes or allow configuration or customization of client authorization in Admin API._

Registering New Clients
-----------------------

Only Enable Registration When Necessary

Since the `/connect/register`  endpoint does not require any authentication, it is recommended that it remains disabled when not registering a new client. If you must register a new client, **temporarily enable, then re-disable the endpoint** by updating the configuration. For default first-time client registration see [First-Time Configuration for Admin API 1.x](First-Time-Configuration-for-Admin-API-1.x_138646866.html).

### Enable registering clients by updating your application configuration to set `Authentication:EnableRegistration`  to `true` . Restart the application.

Then, register a new client by POSTing a URL-Encoded Form request to /connect/register .

|     |     |     |     |     |     |     |
| --- | --- | --- | --- | --- | --- | --- |
| Endpoint | Http Verb | Description | Request Schema | Response Schema (Success) | Response Schema (Validation Error) | Response Schema (Error) |
| /connect/register | POST | Registers a new Admin API client | *   ClientId="string"<br>    <br>*   ClientSecret="string"<br>    <br>*   DisplayName="string" | {  <br> "status": 0  <br>  "title": "string"  <br>} | {  <br>  "status": 0,  <br>  "title": "string",  <br>  "errors": \[  <br>    { "string": \[ "string" \] }  <br>  \]  <br>} | {  <br>  "status": 0,  <br>  "title": "string",  <br>  "errors": \[ "string" \]  <br>} |

  

You can now retrieve a bearer token using the process above with your newly registered Client ID and Secret.