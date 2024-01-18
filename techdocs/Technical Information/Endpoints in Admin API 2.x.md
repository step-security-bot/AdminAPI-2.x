\---

confluence-id: 162202143

confluence-space: %%CONFLUENCE-SPACE%%

\---

Endpoints in Admin API 2.x
==========================

Created by Jason Hoekstra, last modified by Stephen Fuqua on Nov 20, 2023

Below are the endpoints and their request and response objects for v2.0 of the Ed-Fi ODS / API Admin API.

For the most accurate and detailed documentation of active endpoints in a version, configure and launch your application with `SwaggerEnabled : true` _(this is not recommended in production)_.

All functional endpoints require authentication to access. See [Securing Admin API](Securing-Admin-API_133399675.html) for details.

Important Information for Admin API v2.0

Please note these important details for changes between Admin API v1 and Admin API v2:

*   Admin API v2.x is only compatible with the ODS/API 7.x line of products.  For ODS/API 3.4-6.1 support with Admin API, please see the [Admin API v1 line](https://techdocs.ed-fi.org/display/ADMINAPI/Endpoints+-+Admin+API).
*   The response wrapper from Admin API v1 has been removed and objects are returned directly from their endpoint.
*   Property names which start with an underscore ("\_") represents read-only properties.

Endpoint URLs and Schemas
-------------------------

  

### Actions

Click to view /actions endpoints

|     |     |     |     |     |
| --- | --- | --- | --- | --- |
| Endpoint | HTTP Verb | Description | Request Schema | Response Schema (Success) |
| v2/actions | GET | Retrieves all actions | \-  | \[  <br>  {  <br>    "id": 0  <br>    "name": "string"  <br>    "uri": "string"<br><br>  }  <br>\] |

### Applications

Click to view /applications endpoints

|     |     |     |     |     |
| --- | --- | --- | --- | --- |
| Endpoint | HTTP Verb | Description | Request Schema | Response Schema (Success) |
| v2/applications | GET | Retrieves all applications | \-  | \[  <br>  {  <br>    "id": 0,  <br>    "applicationName": "string",  <br>    "vendorId": 0,  <br>    "claimSetName": "string",  <br>    "profileIds": \[\],  <br>    "educationOrganizationIds": \[\],  <br>    "odsInstanceId": 0      <br>  }  <br>\] |
| v2/applications | POST | Creates a new application | {  <br>  "applicationName": "string",  <br>  "vendorId": 0,  <br>  "claimSetName": "string",  <br>  "profileIds": \[ 0 \],  <br>  "educationOrganizationIds": \[ 0 \],  <br>  "odsInstanceId": 0  <br>} | {  <br>  "id": 0,  <br>  "key": "string",  <br>  "secret": "string"  <br>} |
| v2/applications/{id} | GET | Retrieves a specific application by `id` | \-  | {  <br>  "id": 0,  <br>  "applicationName": "string",  <br>  "vendorId": 0,  <br>  "claimSetName": "string",  <br>  "profileIds": \[\],  <br>  "educationOrganizationIds": \[\],  <br>  "odsInstanceId": 0     <br>} |
| v2/applications/{id} | PUT | Updates a specific application by `id` | {  <br><br>  "applicationName": "string",  <br>  "vendorId": 0,  <br>  "claimSetName": "string",  <br>  "profileIds": \[ 0 \],  <br>  "educationOrganizationIds": \[ 0 \],  <br>  "odsInstanceId": 0  <br>} | HTTP response as documented below |
| v2/applications/{id} | DELETE | Deletes an application by `id` | \-  | HTTP response as documented below |
| v2/applications/{id}/reset-credential | PUT | Resets an application credentials by `id` | \-  | {  <br>  "id": 0,  <br>  "key": "string",  <br>  "secret": "string"  <br>} |

### AuthorizationStrategies

Click to view /authorizationStrategies endpoints

|     |     |     |     |     |
| --- | --- | --- | --- | --- |
| Endpoint | HTTP Verb | Description | Request Schema | Response Schema (Success) |
| v2/authorizationStrategies | GET | Retrieves all auth strategies | \-  | \[  <br>  {  <br>    "id": 0  <br>    "name": "string"  <br>    "displayName": "string"<br><br>  }  <br>\] |

### ClaimSets

Click to view /claimSets endpoints

  

|     |     |     |     |     |
| --- | --- | --- | --- | --- |
| Endpoint | HTTP Verb | Description | Request Schema | Response Schema (Success) |
| v2/claimSets | GET | Retrieves all claimsets | \-  | \[<br>  {<br>    "id": 0,<br>    "name": "string",<br>    "\_isSystemReserved": true,<br>    "\_applications": \[\]<br>  }<br>\] |
| v2/claimSets | POST | Creates a new claimset. | { "name": "string"} | HTTP response as documented below |
| v2/claimSets/{id} | GET | Retrieves a specific claimset by id | \-  | {<br>  "id": 0,<br>  "name": "string",<br>  "\_isSystemReserved": false,<br>  "\_applications": \[\],<br>  "resourceClaims": \[<br>    {  <br>      "id": "string",  <br>      "name": "string",      <br>      "actions": \[  <br>          {   <br>            "name": "string",  <br>            "enabled": true  <br>          }  <br>      \],<br>      "\_defaultAuthorizationStrategies": \[<br>        {  <br>          "actionId": 0,  <br>          "actionName": string,<br>          "authorizationStrategies": \[  <br>           {  <br>              "authStrategyId: 0,  <br>              "authStrategyName": "string",  <br>              "isInheritedFromParent": true  <br>           }\]          <br>        }<br>      \],<br>      "authorizationStrategyOverridesForCRUD": \[<br>        {     <br>          "actionId": 0,           <br>          "actionName": string,   <br>          "authorizationStrategies": \[  <br>           {  <br>            "authStrategyId: 0,  <br>            "authStrategyName": "string",  <br>            "isInheritedFromParent": true  <br>           }\] <br>        }<br>      \],<br>      "children": \[<br>        "list of resource claims"<br>      \]<br>    }<br>  \]<br>} |
| v2/claimSets/{id} | PUT | Update the claim set name. | {  <br>"name": "string"  <br>} | HTTP response as documented below |
| v2/claimSets/{id} | DELETE | Deletes a claimset by `id` | \-  | HTTP response as documented below |
| v2/claimSets/{claimSetId}/resourceClaimActions | POST | Add resourceclaimaction association to claim set. At least one action should be enabled. Valid actions are read, create, update, delete, readchanges.  <br>resouceclaimId is required fields. | {  <br>"resouceclaimId" : 0,  <br>"resourceClaimActions":   <br> \[  <br>     {  <br>      "name": "string",  <br>      "enabled": true  <br>      }  <br>  \]     <br>} | HTTP response as documented below |
| v2/claimSets/{claimSetId}/  <br>resourceClaimActions/{resourceClaimId} | PUT | Updates  the resourceclaimActions to a  specific resource claim on a claimset. At least one action should be enabled. Valid actions are read, create, update, delete, readchanges. | {     <br>  "resourceClaimActions": \[       <br>    {   <br>      "name": "string",  <br>      "enabled": true  <br>    }  <br>   \]   <br>} | HTTP response as documented below |
| v2/claimSets/{claimSetId}/resourceClaimActions/  <br>{resourceClaimId}/overrideAuthorizationStrategy | POST | Override the default authorization strategies on provided resource claim for a specific action.<br><br>ex: actionName = read,  authorizationStrategies= \[ "Ownershipbased" \] | {  <br>"actionName": string,  <br>"authorizationStrategies: \[\]  <br>} | HTTP response as documented below |
| v2/claimSets/{claimSetId}/resourceClaimActions/  <br>{resourceClaimId}/resetAuthorizationStrategies | POST | Reset to default authorization strategies on provided resource claim. | \-  | HTTP response as documented below |
| v2/claimSets/{claimSetId}/  <br>resourceClaimActions/{resourceClaimId} | DELETE | Deletes a resource claims association from a claim set | \-  | HTTP response as documented below |
| v2/claimSets/copy | POST | Copy the existing claimset and create new. | {  <br>   "originalId": 0,  <br>   "name": "string"  <br>} | HTTP response as documented below |
| v2/claimSets/import | POST | Import new claimset | {<br>  "name": "string",<br>  "resourceClaims": \[<br>    {<br>      "name": "string",         <br>      "actions": \[  <br>          {   <br>            "name": "read",  <br>            "enabled": true  <br>          },  <br>          {   <br>            "name": "create",  <br>            "enabled": true  <br>          },  <br>          {   <br>            "name": "update",  <br>            "enabled": true  <br>          },  <br>          {   <br>            "name": "delete",  <br>            "enabled": true  <br>          },  <br>          {   <br>            "name": "readChanges",  <br>            "enabled": true  <br>          }  <br>    \],  <br>      "authorizationStrategyOverridesForCRUD": \[<br>        {  <br>          "actionName": string,<br>          "authorizationStrategies": \[\]          <br>        }<br>      \],<br>      "children": \[<br>        "list of resource claims"<br>      \]<br>    }<br>  \]<br>} | HTTP response as documented below |
| v2/claimSets/{id}/export | GET | Retrieves a specific claimset by id | \-  | {  <br>  "id": 0,  <br>  "name": "string",  <br>  "\_isSystemReserved": false,  <br>  "\_applications": \[\],  <br>  "resourceClaims": \[  <br>    {  <br>      "id": "string",  <br>      "name": "string",  <br>      "actions": \[<br><br>          {   <br>            "name": "read",  <br>            "enabled": true  <br>          },  <br>          {   <br>            "name": "create",  <br>            "enabled": true  <br>          },  <br>          {   <br>            "name": "update",  <br>            "enabled": true  <br>          },  <br>          {   <br>            "name": "delete",  <br>            "enabled": true  <br>          },  <br>          {   <br>            "name": "readChanges",  <br>            "enabled": true  <br>          }  <br>      \],<br><br>    "\_defaultAuthorizationStrategiesForCRUD": \[  <br>        {  <br>          "actionId": 0,  <br>          "actionName": string,  <br>          "authorizationStrategies": \[  <br>           {  <br>            "authStrategyId: 0,  <br>            "authStrategyName": "string",  <br>            "isInheritedFromParent": true  <br>           }\]            <br>        }  <br>      \],  <br>    "authorizationStrategyOverridesForCRUD": \[  <br>        {     <br>          "actionId": 0,           <br>          "actionName": string,   <br>          "authorizationStrategies": \[  <br>           {  <br>             "authStrategyId: 0,  <br>             "authStrategyName": "string",  <br>             "isInheritedFromParent": true  <br>           }\]   <br>        }  <br>      \],  <br>      "children": \[  <br>        "list of resource claims"  <br>      \]  <br>    }  <br>  \]  <br>} |

  

### OdsInstances

Click to view /odsInstances endpoints

|     |     |     |     |     |
| --- | --- | --- | --- | --- |
| Endpoint | HTTP Verb | Description | Request Schema | Response Schema (Success) |
| v2/odsInstances | GET | Retrieves all ODS Instances | \-  | \[  <br>  {  <br>    "id": 0,  <br>    "name": "string",  <br>    "instanceType": "string"<br><br>  }  <br>\] |
| v2/odsInstances | POST | Creates a new ODS instance. <br><br>Note: Will validate the connection string to be proper format.<br><br>All the fields are required. | {      <br> "name": "string"  <br> "instanceType": "string",  <br> "connectionString": "string"  <br>} | HTTP response as documented below |
| v2/odsInstances/{id} | GET | Retrieves a specific ODS instance by `id` | \-  | {  <br>    "id": 0,  <br>    "name": "string",  <br>    "instanceType": "string",  <br>    "odsInstanceContexts": \[    <br><br>      {  <br>       "id": 0,  <br>       "odsInstanceId": 0,  <br>       "contextKey": "string",  <br>       "contextValue": "string"  <br>      }\],  <br>    "odsInstanceDerivatives": \[  <br>       {  <br>        "id": 0,  <br>        "odsInstanceId": 0,  <br>        "derivativeType": "string"  <br>       }\]  <br>} |
| v2/odsInstances/{id} | PUT | Updates a specific ODS instance by id.<br><br>Note: Will validate the connection string to be proper format.<br><br>On update the connection string is optional.<br><br>If user is not intending to update the connection string value as part of update, then empty value will be passed. So that the existing connection string will be retained as plain text or encrypted value. | {    <br>  "name": "string"  <br>  "instanceType": "string",  <br>  "connectionString": "string"  <br>} | HTTP response as documented below |
| v2/odsInstances/{id} | DELETE | Deletes an ODS instance by id | \-  | HTTP response as documented below |
| v2/odsInstances/{id}/applications | GET | Retrieves list of applications assigned to a specific ODS instance. | \-  | \[  <br>  {  <br>      "id": 0,  <br>      "applicationName": "string",  <br>      "vendorId": 0,  <br>      "claimSetName": "string",  <br>      "profileIds": \[\],  <br>      "educationOrganizationIds": \[\],  <br>      "odsInstanceId": 0  <br>   }  <br>\] |

### OdsInstanceContexts

Click to view /odsInstanceContexts endpoints

|     |     |     |     |     |
| --- | --- | --- | --- | --- |
| Endpoint | HTTP Verb | Description | Request Schema | Response Schema (Success) |
| v2/odsInstanceContexts | GET | Retrieves all ODS Instance contexts | \-  | \[  <br>  {  <br>    "id": 0,  <br>    "odsInstanceId": 0,  <br>    "contextKey": "string",  <br>    "contextValue": "string"<br><br>  }  <br>\] |
| v2/odsInstanceContexts | POST | Creates a new ODS instance context<br><br>All the fields are required.<br><br>ex: contextKey = "SchoolYear"<br><br>contextValue = "2023" | {      <br> "odsInstanceId": 0  <br> "contextKey": "string",  <br> "contextValue": "string"  <br>} | HTTP response as documented below |
| v2/odsInstanceContexts/{id} | GET | Retrieves a specific ODS instance  context by `id` | \-  | {  <br>    "id": 0,  <br>    "odsInstanceId": 0,  <br>    "contextKey": "string",  <br>    "contextValue": "string"    <br><br>} |
| v2/odsInstanceContexts/{id} | PUT | Updates a specific ODS instance context by id. | {  <br>"odsInstanceId": 0  <br>"contextKey": "string",  <br>"contextValue": "string"  <br>} | HTTP response as documented below |
| v2/odsInstanceContexts/{id} | DELETE | Deletes an ODS instance context by id | \-  | HTTP response as documented below |

### OdsInstanceDerivatives

Click to view /odsInstanceDerivatives endpoints

|     |     |     |     |     |
| --- | --- | --- | --- | --- |
| Endpoint | HTTP Verb | Description | Request Schema | Response Schema (Success) |
| v2/odsInstanceDerivatives | GET | Retrieves all ODS Instance derivatives | \-  | \[  <br>  {  <br>    "id": 0,  <br>    "odsInstanceId": 0,  <br>    "derivativeType": "string"  <br>  }<br><br>\] |
| v2/odsInstanceDerivatives | POST | Creates a new ODS instance derivative<br><br>All the fields are required.<br><br>Note: Will validate the connection string to be proper format.<br><br>Derivative types would be "ReadReplica" or "Snapshot" | {      <br> "odsInstanceId": 0  <br> "derivativeType": "string",  <br> "connectionString": "string"  <br>} | HTTP response as documented below |
| v2/odsInstanceDerivatives  <br>  <br>/{id} | GET | Retrieves a specific ODS instance derivative by `id` | \-  | {  <br>    "id": 0,  <br>    "odsInstanceId": 0,  <br>    "derivativeType": "string"<br><br>} |
| v2/odsInstanceDerivatives/{id} | PUT | Updates a specific ODS instance derivative by id.<br><br>Note: Will validate the connection string to be proper format.<br><br>On update the connection string is optional.<br><br>If user is not intending to update the connection string value as part of update, then empty value will be passed. So that the existing connection string will be retained as plain text or encrypted value.<br><br>Derivative types would be "ReadReplica" or "Snapshot" | {     <br>"odsInstanceId": 0  <br>"derivativeType": "string",  <br>"connectionString": "string"  <br>} | HTTP response as documented below |
| v2/odsInstanceDerivatives/{id} | DELETE | Deletes an ODS instance derivative by id | \-  | HTTP response as documented below |

### Profiles

Click to view /profiles endpoints

|     |     |     |     |     |
| --- | --- | --- | --- | --- |
| Endpoint | HTTP Verb | Description | Request Schema | Response Schema (Success) |
| v2/profiles | GET | Retrieves all profiles | \-  | \[  <br>  {  <br>    "id": 0,  <br>    "name":string  <br>  }  <br>\] |
| v2/profiles | POST | Creates a new profile | {  "name": "string",  "definition": "string"  } | HTTP response as documented below |
| v2/profiles/{id} | GET | Retrieves a specific profile by `id` | \-  | {  <br>  "id": 0,  <br>  "name":string,  <br>  "definition":string  <br>} |
| v2/profiles/{id} | PUT | Updates a specific profile by `id` | `{   "name": "string",   "definition": "string"   }` | HTTP response as documented below |
| v2/profiles/{id} | DELETE | Deletes a profile by `id` | \-  | HTTP response as documented below |

### ResourceClaims

Click to view /resourceClaims endpoints

|     |     |     |     |     |
| --- | --- | --- | --- | --- |
| Endpoint | HTTP Verb | Description | Request Schema | Response Schema (Success) |
| v2/resourceClaims | GET | Retrieves all resourceclaims | \-  | \[  <br>  {  <br>    "id": 0  <br>    "name": "string",  <br>    "parentId": null,  <br>    "parentName": "",  <br>    "children": \[  <br>      {  <br>       "id": 0  <br>       "name": "string",  <br>       "parentId": 0,  <br>       "parentName": "",  <br>       "children": \[\]  <br>      }  <br>    \]  <br>  }  <br>\] |
| v2/resourceClaims/{id} | GET | Retrieves a specific resource claim by `id` | \-  | {  <br>  "id": 0  <br>  "name": "string",  <br>  "parentId": null,  <br>  "parentName": "",  <br>  "children": \[  <br>     {  <br>      "id": 0  <br>      "name": "string",  <br>      "parentId": 0,  <br>      "parentName": "",  <br>      "children": \[\]  <br>     }  <br>  \]  <br>} |

### Vendors

Click to view /vendors endpoints

|     |     |     |     |     |
| --- | --- | --- | --- | --- |
| Endpoint | HTTP Verb | Description | Request Schema | Response Schema (Success) |
| v2/vendors | GET | Retrieves all vendors | \-  | \[  <br>  {  <br>    "id": 0,  <br>    "company": "string",  <br>    "namespacePrefixes": "string",  <br>    "contactName": "string",  <br>    "contactEmailAddress": "string"  <br>  }  <br>\] |
| v2/vendors | POST | Creates a new vendor | {  "company": "string",  "namespacePrefixes": "string",  "contactName": "string",  "contactEmailAddress": "string"} | HTTP response as documented below |
| v2/vendors/{id} | GET | Retrieves a specific vendor by `id` | \-  | {  <br>  "id": 0,  <br>  "company": "string",  <br>  "namespacePrefixes": "string",  <br>  "contactName": "string",  <br>  "contactEmailAddress": "string"  <br>} |
| v2/vendors/{id} | PUT | Updates a specific vendor by `id` | `{     "company": "string",     "namespacePrefixes": "string",     "contactName": "string",     "contactEmailAddress": "string"   }` | HTTP response as documented below |
| v2/vendors/{id} | DELETE | Deletes a vendor by `id` | \-  | HTTP response as documented below |
| v2/vendors/{id}/applications | GET | Retrieves all applications associated with vendor of `id` | \-  | \[  <br> {  <br>  "id": 0,  <br>  "applicationName": "string",  <br>  "vendorId": 0,  <br>  "claimSetName": "string",  <br>  "profileIds": \[\],  <br>  "educationOrganizationIds": \[\],  <br>  "odsInstanceId": 0   <br> }  <br>\] |

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

Attachments:
------------

![](images/icons/bullet_blue.gif) [image-2023-7-19\_15-50-42.png](attachments/162202143/162203622.png) (image/png)