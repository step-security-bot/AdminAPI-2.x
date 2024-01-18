Created by Patrick McVeety-Mill, last modified by Stephen Fuqua on Nov 20, 2023

Below are the endpoints and their request and response objects for v1.x of the Ed-Fi ODS / API Admin API.

For the most accurate and detailed documentation of active endpoints in a version, configure and launch your application with `SwaggerEnabled : true` _(this is not recommended in production)_.

All functional endpoints require authentication to access. See [Securing Admin API](Securing-Admin-API_133399675.html) for details.

Endpoint URLs and Schemas
-------------------------

/\*<!\[CDATA\[\*/ div.rbtoc1705534709072 {padding: 0px;} div.rbtoc1705534709072 ul {margin-left: 0px;} div.rbtoc1705534709072 li {margin-left: 0px;padding-left: 0px;} /\*\]\]>\*/

*   [Endpoint URLs and Schemas](#EndpointsinAdminAPI1.x-EndpointURLsandSchemas)
    *   [Response Wrapper Schema](#EndpointsinAdminAPI1.x-ResponseWrapperSchema)
    *   [Vendors](#EndpointsinAdminAPI1.x-Vendors)
    *   [Claimsets](#EndpointsinAdminAPI1.x-Claimsets)
    *   [Applications](#EndpointsinAdminAPI1.x-Applications)
*   [Common Responses](#EndpointsinAdminAPI1.x-CommonResponses)

###   
Response Wrapper Schema

Responses with a body have a common "wrapper" around their result object (which may be empty) or a collection of errors.

These wrappers are **not** reflected in the below documentation. Assume they are the contents of `result` when successful.

Click to view response wrappers

|     |     |     |
| --- | --- | --- |
| Response | Codes | Schema |
| Success | 200, 201 | {  <br>  "status": 0,  <br>  "title": "string",  <br>  "result": object?  <br>} |
| Error | 401, 403, 404, 500 | {  <br>  "status": 0,  <br>  "title": "string",  <br>  "errors": \[ "string" \]  <br>} |
| Validation Error | 400 | {  <br>  "status": 0,  <br>  "title": "string",  <br>  "errors": \[  <br>    { "string": \[ "string" \] }  <br>  \]  <br>} |

### Vendors

Click to view /vendors endpoints

|     |     |     |     |     |
| --- | --- | --- | --- | --- |
| Endpoint | HTTP Verb | Description | Request Schema | Response Schema (Success) |
| v1/vendors/ | GET | Retrieves all vendors | \-  | \[  <br>  {  <br>    "vendorId": 0,  <br>    "company": "string",  <br>    "namespacePrefixes": "string",  <br>    "contactName": "string",  <br>    "contactEmailAddress": "string"  <br>  }  <br>\] |
| v1/vendors/{id} | GET | Retrieves a specific vendor by `id` | \-  |     |
| v1/vendors/ | POST | Creates a new vendor | {  "company": "string",  "namespacePrefixes": "string",  "contactName": "string",  "contactEmailAddress": "string"} | {  <br>  "vendorId": 0,  <br>  "company": "string",  <br>  "namespacePrefixes": "string",  <br>  "contactName": "string",  <br>  "contactEmailAddress": "string"  <br>} |
| v1/vendors/{id} | PUT | Updates a specific vendor by `id` | `{     "company": "string",     "namespacePrefixes": "string",     "contactName": "string",     "contactEmailAddress": "string"   }` | {  <br>  "vendorId": 0,  <br>  "company": "string",  <br>  "namespacePrefixes": "string",  <br>  "contactName": "string",  <br>  "contactEmailAddress": "string"  <br>} |
| v1/vendors/{id} | DELETE | Deletes a vendor by `id` | \-  | \-  |
| v1/vendors/{id}/applications | GET | Retrieves all applications associated with vendor of `id` | \-  | \[  <br>  {  <br>    "applicationId": 0,  <br>    "applicationName": "string",  <br>    "claimSetName": "string",  <br>    "profileName": "string",  <br>    "educationOrganizationId": 0,  <br>    "odsInstanceName": "string"  <br>  }  <br>\] |

### Claimsets

Click to view /claimsets endpoints

  

|     |     |     |     |     |
| --- | --- | --- | --- | --- |
| Endpoint | HTTP Verb | Description | Request Schema | Response Schema (Success) |
| v1/claimsets/ | GET | Retrieves all claimsets | \-  | \[<br>  {<br>    "id": 0,<br>    "name": "string",<br>    "isSystemReserved": true,<br>    "applicationsCount": 0<br>  }<br>\] |
| v1/claimsets/{id} | GET | Retrieves a specific claimset by id | \-  | {<br>  "id": 0,<br>  "name": "string",<br>  "isSystemReserved": true,<br>  "applicationsCount": 0,<br>  "resourceClaims": \[<br>    {<br>      "name": "string",<br>      "read": true,<br>      "create": true,<br>      "update": true,<br>      "delete": true,<br>      "defaultAuthStrategiesForCRUD": \[<br>        {<br>          "authStrategyName": "string",<br>          "isInheritedFromParent": true<br>        }<br>      \],<br>      "authStrategyOverridesForCRUD": \[<br>        {<br>          "authStrategyName": "string",<br>          "isInheritedFromParent": true<br>        }<br>      \],<br>      "children": \[<br>        "list of resource claims"<br>      \]<br>    }<br>  \]<br>} |
| v1/claimsets/ | POST | Creates a new claimset | {<br>  "name": "string",<br>  "resourceClaims": \[<br>    {<br>      "name": "string",<br>      "read": true,<br>      "create": true,<br>      "update": true,<br>      "delete": true,<br>      "defaultAuthStrategiesForCRUD": \[<br>        {<br>          "authStrategyName": "string",<br>          "isInheritedFromParent": true<br>        }<br>      \],<br>      "authStrategyOverridesForCRUD": \[<br>        {<br>          "authStrategyName": "string",<br>          "isInheritedFromParent": true<br>        }<br>      \],<br>      "children": \[<br>        "list of resource claims"<br>      \]<br>    }<br>  \]<br>} | {<br>  "id": 0,<br>  "name": "string",<br>  "isSystemReserved": true,<br>  "applicationsCount": 0,<br>  "resourceClaims": \[<br>    {<br>      "name": "string",<br>      "read": true,<br>      "create": true,<br>      "update": true,<br>      "delete": true,<br>      "defaultAuthStrategiesForCRUD": \[<br>        {<br>          "authStrategyName": "string",<br>          "isInheritedFromParent": true<br>        }<br>      \],<br>      "authStrategyOverridesForCRUD": \[<br>        {<br>          "authStrategyName": "string",<br>          "isInheritedFromParent": true<br>        }<br>      \],<br>      "children": \[<br>        "list of resource claims"<br>      \]<br>    }<br>  \]<br>} |
| v1/claimsets/{id} | PUT | Updates a specific claimset by id | {<br>  "id": 0,<br>  "name": "string",<br>  "resourceClaims": \[<br>    {<br>      "name": "string",<br>      "read": true,<br>      "create": true,<br>      "update": true,<br>      "delete": true,<br>      "defaultAuthStrategiesForCRUD": \[<br>        {<br>          "authStrategyName": "string",<br>          "isInheritedFromParent": true<br>        }<br>      \],<br>      "authStrategyOverridesForCRUD": \[<br>        {<br>          "authStrategyName": "string",<br>          "isInheritedFromParent": true<br>        }<br>      \],<br>      "children": \[ <br>         "list of resource claims"  <br>      \]  <br>    }     <br>  \]   <br>} | {<br>  "id": 0,<br>  "name": "string",<br>  "isSystemReserved": true,<br>  "applicationsCount": 0,<br>  "resourceClaims": \[<br>    {<br>      "name": "string",<br>      "read": true,<br>      "create": true,<br>      "update": true,<br>      "delete": true,<br>      "defaultAuthStrategiesForCRUD": \[<br>        {<br>          "authStrategyName": "string",<br>          "isInheritedFromParent": true<br>        }<br>      \],<br>      "authStrategyOverridesForCRUD": \[<br>        {<br>          "authStrategyName": "string",<br>          "isInheritedFromParent": true<br>        }<br>      \],<br>      "children": \[<br>         "list of resource claims"  <br>       \]<br>    }<br>  \]<br>} |
| v1/claimsets/{id} | DELETE | Deletes a claimset by `id` | \-  | \-  |

  

### Applications

Click to view /applications endpoints

|     |     |     |     |     |
| --- | --- | --- | --- | --- |
| Endpoint | HTTP Verb | Description | Request Schema | Response Schema (Success) |
| v1/applications/ | GET | Retrieves all applications | \-\| | \[  <br>  {  <br>    "applicationId": 0,  <br>    "applicationName": "string",  <br>    "claimSetName": "string",  <br>    "profileName": "string",  <br>    "educationOrganizationId": 0,  <br>    "odsInstanceName": "string"  <br>  }  <br>\] |
| v1/applications/{id} | GET | Retrieves a specific application by `id` | \-  | {  <br>  "applicationId": 0,  <br>  "applicationName": "string",  <br>  "claimSetName": "string",  <br>  "profileName": "string",  <br>  "educationOrganizationId": 0,  <br>  "odsInstanceName": "string"  <br>} |
| v1/applications/ | POST | Creates a new application | {  <br>  "applicationName": "string",  <br>  "vendorId": 0,  <br>  "claimSetName": "string",  <br>  "profileId": 0,  <br>  "educationOrganizationIds": \[  <br>    0  <br>  \]  <br>} | {  <br>  "applicationId": 0,  <br>  "key": "string",  <br>  "secret": "string"  <br>} |
| v1/applications/{id} | PUT | Updates a specific application by `id` | {  <br>  "applicationId": 0,  <br>  "applicationName": "string",  <br>  "vendorId": 0,  <br>  "claimSetName": "string",  <br>  "profileId": 0,  <br>  "educationOrganizationIds": \[ 0 \]  <br>} | {  <br>  "applicationId": 0,  <br>  "applicationName": "string",  <br>  "claimSetName": "string",  <br>  "profileName": "string",  <br>  "educationOrganizationId": 0,  <br>  "odsInstanceName": "string"  <br>} |
| v1/applications/{id} | DELETE | Deletes an application by `id` | \-  | \-  |
| v1/applications/{id}/reset-credential | PUT | Resets an application credentials by `id` | \-  | {  <br>  "applicationId": 0,  <br>  "key": "string",  <br>  "secret": "string"  <br>} |

Common Responses
----------------

|     |     |     |     |
| --- | --- | --- | --- |
| Response Code | Description | Valid for Verbs | Notes |
| 200 SUCCESS | Request was successful | ALL |     |
| 201 CREATED | Resource was created successfully | POST | Response will also include a `location`  header which directs to the new resource |
| 400 BAD REQUEST | Invalid request payload - See errors for details | POST, PUT |     |
| 401 UNAUTHORIZED | Missing or invalid authentication token | ALL |     |
| 403 FORBIDDEN | Authentication token is valid but resource is outside of authenticated scope | ALL |     |
| 404 NOT FOUND | Resource with given `id`  not found | ALL |     |
| 500 INTERNAL SERVER ERROR | Unexpected error on the system - See error for details | ALL |     |