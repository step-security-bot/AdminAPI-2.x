Created by Jason Hoekstra on Oct 04, 2023

Dev-protected while in edit

Overview
--------

This is a quick start guide for calling Admin API using Python scripting, it will cover the basic operations of the Admin API:  

Table of Contents
-----------------

/\*<!\[CDATA\[\*/ div.rbtoc1705534709217 {padding: 0px;} div.rbtoc1705534709217 ul {margin-left: 0px;} div.rbtoc1705534709217 li {margin-left: 0px;padding-left: 0px;} /\*\]\]>\*/

*   [Configure Environment with Python and Admin API 2.0](#QuickStartCallingAdmin2.xAPIusingPython-ConfigureEnvironmentwithPythonandAdminAPI2.0)
    *   [Information](#QuickStartCallingAdmin2.xAPIusingPython-Information)
    *   [Authenticate to Admin API](#QuickStartCallingAdmin2.xAPIusingPython-AuthenticatetoAdminAPI)
    *   [Register a new client](#QuickStartCallingAdmin2.xAPIusingPython-Registeranewclient)
    *   [Token](#QuickStartCallingAdmin2.xAPIusingPython-Token)
*   [Vendors](#QuickStartCallingAdmin2.xAPIusingPython-Vendors)
    *   [Retrieve a Listing of Vendors](#QuickStartCallingAdmin2.xAPIusingPython-RetrieveaListingofVendors)
    *   [Create a Vendor](#QuickStartCallingAdmin2.xAPIusingPython-CreateaVendor)
    *   [Get a vendor](#QuickStartCallingAdmin2.xAPIusingPython-Getavendor)
    *   [Update a vendor](#QuickStartCallingAdmin2.xAPIusingPython-Updateavendor)
    *   [Delete a vendor](#QuickStartCallingAdmin2.xAPIusingPython-Deleteavendor)
*   [Claim sets](#QuickStartCallingAdmin2.xAPIusingPython-Claimsets)
    *   [List all Claims](#QuickStartCallingAdmin2.xAPIusingPython-ListallClaims)
    *   [Create a Claim](#QuickStartCallingAdmin2.xAPIusingPython-CreateaClaim)
    *   [Retrieve a claim set](#QuickStartCallingAdmin2.xAPIusingPython-Retrieveaclaimset)
    *   [Update a claim set](#QuickStartCallingAdmin2.xAPIusingPython-Updateaclaimset)
    *   [Delete a claim set](#QuickStartCallingAdmin2.xAPIusingPython-Deleteaclaimset)
*   [Applications](#QuickStartCallingAdmin2.xAPIusingPython-Applications)
    *   [Create an Application and Set of Credentials](#QuickStartCallingAdmin2.xAPIusingPython-CreateanApplicationandSetofCredentials)
    *   [Retrieve application data](#QuickStartCallingAdmin2.xAPIusingPython-Retrieveapplicationdata)
    *   [Update an application](#QuickStartCallingAdmin2.xAPIusingPython-Updateanapplication)
    *   [Delete an application](#QuickStartCallingAdmin2.xAPIusingPython-Deleteanapplication)
    *   [Refresh application credentials](#QuickStartCallingAdmin2.xAPIusingPython-Refreshapplicationcredentials)

Configure Environment with Python and Admin API 2.0
---------------------------------------------------

Once [Admin API](Admin-API_127929051.html) is installed, we can use Python versions above 3.7+. It is necessary to have the [requests](https://requests.readthedocs.io/en/latest/user/quickstart/) library installed or similar. We can use the following command to review our current python version.

python --version

The utility to install packages in Python is called pip, in case you don't have it installed you can follow the instructions in this [link](https://pip.pypa.io/en/stable/installation/).

To install the libraries using pip, you can use the line below.

pip install -U requests

To import it into our script we will use the following imports:

**script.py**

import requests
import warnings

warnings.filterwarnings('ignore') # setting ignore as a parameter

### Information

**GET /**

def information(base\_url: str) -> dict:
    '''
        Retrieve API informational data
    '''
    endpoint = "/"
    url = f"{base\_url}{endpoint}"

    r = requests.get(url, verify=False)

    return r.json()

Our output should bring the information from the Restful API.

**Sample Output**

{
  "version": "1.1",
  "build": "1.0.0.0"
}

### Authenticate to Admin API

In a new installation, it is necessary to previously register the client to connect, for which we will follow the instructions within the document in [Securing Admin API](Securing-Admin-API_133399675.html).

### Register a new client

In order to do so, we can add the functionality to our script by adding the following lines.

**POST /connect/register**

def register(
    base\_url: str,
    client\_payload: str,
) -> dict:
    '''
        Registers a new client

        Parameters
        ----------
        'base\_url': base\_url,
            URL where API is hosted
        client\_payload: dict
            The client information
            {
                'client\_id': str,
                    The client id for the client
                'client\_secret': str,
                    The client secret for the client
                display\_name: str
                    Display name for client
            }
    '''
    endpoint = "/connect/register"
    url = f"{base\_url}{endpoint}"

    r = requests.post(
        url, 
        data={
            "ClientId": client\_payload\["client\_id"\], 
            "ClientSecret": client\_payload\["client\_secret"\],
            "DisplayName": client\_payload\["display\_name"\],
            },
        verify=False
        )

    return r.json()

And we can construct our payload as the following example.

**Sample input**

new\_client = {
        'client\_id': <your\_client\_id>,
        'client\_secret': <your\_secret>,
        'display\_name': "Wille",
    }

The successful output will be JSON formatted.

**Sample output**

{
  "title": "Registered client 1 successfully.",
  "status": 200
}

### Token

Once we register our client according to the parameters specified in the document [Securing Admin API](Securing-Admin-API_133399675.html).

We can obtain the token we will use for each API query. Just pass the same ClientID and ClientSecret we use to register it, with two new variables.

**Sample input**

client\_id = <your\_client\_id>
client\_secret = <your\_secret>
grant\_type = "client\_credentials"
scope = "edfi\_admin\_api/full\_access"

  

**POST /connect/token**

def token(
    base\_url: str,
    client\_id: str, 
    client\_secret: str,
    grant\_type: str, 
    scope: str, 
) -> dict:
    '''
        Retrieves a bearer token

        Parameters
        ----------
        base\_url: str
            URL where API is hosted
        client\_id: str
            The client id provided in the register
        client\_secret: str
            The client secret provided in the register
        grant\_type: str
            default "client\_credentials"
        scope: str
            default "edfi\_admin\_api/full\_access"
    '''
    endpoint = "/connect/token"
    url = f"{base\_url}{endpoint}"

    r = requests.post(
        url, 
        data={
            "client\_id": client\_id, 
            "client\_secret": client\_secret,
            "grant\_type": grant\_type,
            "scope": scope,
            },
        verify=False,
        )

    return r.json()

The outcome will be JSON formatted.

**Sample output**

{
  "access\_token": <your\_token>,
  "token\_type": "Bearer",
  "expires\_in": 3599
}

Then you can use the token as an authentication method, with the header Authorization as the example below.

Vendors
-------

### Retrieve a Listing of Vendors

See the [Endpoints - Admin API](https://techdocs.ed-fi.org/display/ADMINAPI/Endpoints+-+Admin+API#EndpointsAdminAPI-Vendors) page for a complete list of resources and parameters. For this example, we will get a list of providers.  

**GET /v1/vendors**

def get\_vendors(
    base\_url: str,
    access\_token: str,
) -> dict:
    '''
        Retrieves all vendors

        Parameters
        ----------
        base\_url: str
            URL where API is hosted
        access\_token: str
            String with the authorization token bearer
        
        Returns
        -------
        r: List\[Dict\[str, str\]\]
            Returns a list of dictionaries from the request 
            converted from JSON format.
            \[
                {
                    "vendorId": 0,
                    "company": "string",
                    "namespacePrefixes": "string",
                    "contactName": "string",
                    "contactEmailAddress": "string",
                }
            \]
    '''
    endpoint = "/v1/vendors"
    url = f"{base\_url}{endpoint}"
    headers = {
        'Authorization': f'Bearer {access\_token}',
        'Content-type': 'application/json', 
        'Accept': 'text/plain',
        }

    r = requests.get(
        url=url,
        headers=headers,
        verify=False,
        )

    return r.json()

We will get a list of the vendors, JSON formatted, as in the example below.

**Sample output**

{
  "result": \[
    {
      "vendorId": 1,
      "company": "ACME Education",
      "namespacePrefixes": "ACME",
      "contactName": "Wile E. Coyote",
      "contactEmailAddress": "wile@acme.edu"
    }
  \],
  "status": 200,
  "title": "Request successful"
}

### Create a Vendor

To create a new vendor, we will use the POST verb. Although in this example, it is necessary to pass a dictionary with the required data. Again, you can refer to the link [Endpoints - Admin API](https://techdocs.ed-fi.org/display/ADMINAPI/Endpoints+-+Admin+API#EndpointsAdminAPI-Vendors)  to successfully create the provider. In our case, we will use the following information.

**Sample output**

vendor\_payload = {
        "company": "ACME Education",
        "namespacePrefixes": "ACME",
        "contactName": "Wile E. Coyote",
        "contactEmailAddress": "wile@acme.edu",
        }

Which we will pass as a parameter to a function as shown below, or with the method of your choice.

**POST /v1/vendors**

def create\_vendor(
    base\_url: str,
    access\_token: str,
    payload: dict,
) -> dict:
    '''
        Creates a vendor based on supplied values

        Parameters
        ----------
        base\_url: str
            URL where API is hosted
        access\_token: str
            String with the authorization token bearer
        payload: dict
            {
                "company": "string",
                "namespacePrefixes": "string",
                "contactName": "string",
                "contactEmailAddress": "string",
            }
    '''
    endpoint = "/v1/vendors"
    url = f"{base\_url}{endpoint}"
    headers = {
        'Authorization': f'Bearer {access\_token}',
        'Content-type': 'application/json', 
        'Accept': 'text/plain',
        }

    r = requests.post(
        url=url,
        headers=headers,
        json=payload,
        verify=False,
        )

    return r.json()

As a result, we will obtain in JSON format, a dictionary verifying that our information was saved correctly. If necessary, you can store the Vendor ID for future reference.

**Sample output**

{
  "result": {
    "vendorId": 2,
    "company": "ACME Education",
    "namespacePrefixes": "ACME",
    "contactName": "Road Runner",
    "contactEmailAddress": "roadrunner@acme.edu"
  },
  "status": 201,
  "title": "Vendor created successfully"
}

### Get a vendor 

In the case that you want to retrieve information from one of the vendors, you will need to use the resource ID.

**GET /v1/vendors/{id}**

def get\_vendor(
    base\_url: str,
    access\_token: str,
    id: int,
) -> dict:
    '''
        Get an existing vendor using the resource identifier

        Parameters
        ----------
        base\_url: str
            URL where API is hosted
        access\_token: str
            String with the authorization token bearer
        id: int
            Resource identifier
    '''
    endpoint = "/v1/vendors"
    url = f"{base\_url}{endpoint}/{id}"
    headers = {
        'Authorization': f'Bearer {access\_token}',
        'Content-type': 'application/json', 
        'Accept': 'text/plain',
        }

    r = requests.get(
        url=url,
        headers=headers,
        verify=False,
        )

    return r.json()

In case of success we will obtain an output as follow:

**Sample output**

{
  "result": {
    "vendorId": 9,
    "company": "ACME Education",
    "namespacePrefixes": "ACME",
    "contactName": "Road Runner",
    "contactEmailAddress": "roadrunner@acme.edu"
  },
  "status": 200,
  "title": "Request successful"
}

### Update a vendor

For this example, we update the previously created vendor with the following info.

**Sample input**

vendor\_payload = {
        "company": "ACME Education",
        "namespacePrefixes": "ACME",
        "contactName": "Yosemite Sam",
        "contactEmailAddress": "yosemitesam@acme.edu",
        }

We use as an example the code below.

**PUT /v1/vendors/{id}**

def edit\_vendor(
    base\_url: str,
    access\_token: str,
    vendor\_payload: dict,
    id: int
) -> dict:
    '''
        Updates vendor based on the resource identifier

        Parameters
        ----------
        base\_url: str
            URL where API is hosted
        access\_token: str
            String with the authorization token bearer
        vendor\_payload: dict
            {
                "company": "string",
                "namespacePrefixes": "string",
                "contactName": "string",
                "contactEmailAddress": "string",
            }
        id: int
            Resource identifier
    '''
    endpoint = "/v1/vendors"
    url = f"{base\_url}{endpoint}/{id}"
    headers = {
        'Authorization': f'Bearer {access\_token}',
        'Content-type': 'application/json', 
        'Accept': 'text/plain',
        }

    r = requests.put(
        url=url,
        headers=headers,
        json=vendor\_payload,
        verify=False,
        )

    return r.json()

The successful out will look like the following.

**Sample output**

{
  "result": {
    "vendorId": 9,
    "company": "ACME Education",
    "namespacePrefixes": "ACME",
    "contactName": "Yosemite Sam",
    "contactEmailAddress": "yosemitesam@acme.edu"
  },
  "status": 200,
  "title": "Vendor updated successfully"
}

### Delete a vendor

To delete a vendor you can use the next point, as the example provided below.

**/v1/vendors/{id}**

def delete\_vendor(
    base\_url: str,
    access\_token: str,
    id: int,
) -> dict:
    '''
        Deletes an existing vendor using the resource identifier

        Parameters
        ----------
        base\_url: str
            URL where API is hosted
        access\_token: str
            String with the authorization token bearer
        id: int
            Resource identifier
    '''
    endpoint = "/v1/vendors"
    url = f"{base\_url}{endpoint}/{id}"
    headers = {
        'Authorization': f'Bearer {access\_token}',
        'Content-type': 'application/json', 
        'Accept': 'text/plain',
        }

    r = requests.delete(
        url=url,
        headers=headers,
        verify=False,
        )

    return r.json()

The output will be a confirmation as follows:

**Sample output**

{
  "status": 200,
  "title": "Vendor deleted successfully"
}

Claim sets
----------

### List all Claims

To retrieve all the claims we will use the GET verb as follows:

**GET /v1/claimsets**

def get\_claimsets(
    base\_url: str,
    access\_token: str,
) -> dict:
    '''
        Retrieves all claimsets

        Parameters
        ----------
        base\_url: str
            URL where API is hosted
        access\_token: str
            String with the authorization token bearer
        
        Returns
        -------
        r: List\[Dict\[str, str\]\]
            Returns a list of dictionaries from the request 
            converted from JSON format.
            \[
                {
                    "id": 0,
                    "name": "string",
                    "isSystemReserved": true,
                    "applicationsCount": 0
                }
            \]
    '''
    endpoint = "/v1/claimsets"
    url = f"{base\_url}{endpoint}"
    headers = {
        'Authorization': f'Bearer {access\_token}',
        'Content-type': 'application/json', 
        'Accept': 'text/plain',
        }

    r = requests.get(
        url=url,
        headers=headers,
        verify=False,
        )

    return r.json()

The result will be a list of claim sets as the ones shown below:

**Sample output**

{
  "result": \[
    {
      "id": 9,
      "name": "AB Connect",
      "isSystemReserved": true,
      "applicationsCount": 0
    },
    ...
  \],
  "status": 200,
  "title": "Request successful"
}

### Create a Claim

For the creation of a claim, we will use the POST verb again, and we will pass a dictionary with the values to store, an example of payload for this case could be like the following.

**Sample input**

claimset\_payload = {
        "name": "Acme Learning User",
        "resourceClaims": \[
            {
                "name": "educationStandards",
                "read": True,
                "create": True,
                "update": True,
                "delete": True,
                "defaultAuthStrategiesForCRUD": \[
                {
                    "authStrategyName": "NamespaceBased",
                    "isInheritedFromParent": False
                },
                {
                    "authStrategyName": "NoFurtherAuthorizationRequired",
                    "isInheritedFromParent": False
                },
                {
                    "authStrategyName": "NamespaceBased",
                    "isInheritedFromParent": False
                },
                {
                    "authStrategyName": "NamespaceBased",
                    "isInheritedFromParent": False
                }
                \],
                "authStrategyOverridesForCRUD": \[
                None,
                None,
                None,
                None
                \],
                "children": \[
                {
                    "name": "learningObjective",
                    "read": True,
                    "create": True,
                    "update": True,
                    "delete": True,
                    "defaultAuthStrategiesForCRUD": \[
                    {
                        "authStrategyName": "NamespaceBased",
                        "isInheritedFromParent": True
                    },
                    {
                        "authStrategyName": "NoFurtherAuthorizationRequired",
                        "isInheritedFromParent": True
                    },
                    {
                        "authStrategyName": "NamespaceBased",
                        "isInheritedFromParent": True
                    },
                    {
                        "authStrategyName": "NamespaceBased",
                        "isInheritedFromParent": True
                    }
                    \],
                    "authStrategyOverridesForCRUD": \[
                    None,
                    None,
                    None,
                    None
                    \],
                    "children": \[\]
                }
                \]
            },
            {
                "name": "academicSubjectDescriptor",
                "read": True,
                "create": True,
                "update": True,
                "delete": True,
                "defaultAuthStrategiesForCRUD": \[
                {
                    "authStrategyName": "NamespaceBased",
                    "isInheritedFromParent": True
                },
                {
                    "authStrategyName": "NoFurtherAuthorizationRequired",
                    "isInheritedFromParent": True
                },
                {
                    "authStrategyName": "NamespaceBased",
                    "isInheritedFromParent": True
                },
                {
                    "authStrategyName": "NamespaceBased",
                    "isInheritedFromParent": True
                }
                \],
                "authStrategyOverridesForCRUD": \[
                None,
                None,
                None,
                None
                \],
                "children": \[\]
            }      
            \]
        }

Which we will pass as a parameter in a function like the following:

**POST /v1/claimsets**

def create\_claimset(
    base\_url: str,
    access\_token: str,
    payload: dict,
) -> dict:
    '''
        Creates a claimset based on supplied values

        Parameters
        ----------
        base\_url: str
            URL where API is hosted
        access\_token: str
            String with the authorization token bearer
        payload: dict
            {
                "name": "string",
                "resourceClaims": \[
                    {
                        "name": "string",
                        "read": true,
                        "create": true,
                        "update": true,
                        "delete": true,
                        "defaultAuthStrategiesForCRUD": \[
                            {
                            "authStrategyName": "string",
                            "isInheritedFromParent": true
                            }
                        \],
                        "authStrategyOverridesForCRUD": \[
                            {
                            "authStrategyName": "string",
                            "isInheritedFromParent": true
                            }
                        \],
                        "children": \[
                            "string"
                        \]
                        }
                    \]
                }
    '''
    endpoint = "/v1/claimsets"
    url = f"{base\_url}{endpoint}"
    headers = {
        'Authorization': f'Bearer {access\_token}',
        'Content-type': 'application/json', 
        'Accept': 'text/plain',
        }

    r = requests.post(
        url=url,
        headers=headers,
        json=payload,
        verify=False,
        )

    return r.json()  

The output will give the updated information, in JSON format.

**Sample output**

{
  "result": {
    "resourceClaims": \[
      {
        "name": "educationStandards",
        "read": true,
        "create": true,
        "update": true,
        "delete": true,
        "defaultAuthStrategiesForCRUD": \[
          {
            "authStrategyId": 4,
            "authStrategyName": "NamespaceBased",
            "displayName": "Namespace Based",
            "isInheritedFromParent": false
          },
          {
            "authStrategyId": 1,
            "authStrategyName": "NoFurtherAuthorizationRequired",
            "displayName": "No Further Authorization Required",
            "isInheritedFromParent": false
          },
          {
            "authStrategyId": 4,
            "authStrategyName": "NamespaceBased",
            "displayName": "Namespace Based",
            "isInheritedFromParent": false
          },
          {
            "authStrategyId": 4,
            "authStrategyName": "NamespaceBased",
            "displayName": "Namespace Based",
            "isInheritedFromParent": false
          }
        \],
        "authStrategyOverridesForCRUD": \[
          null,
          null,
          null,
          null
        \],
        "children": \[
          {
            "name": "learningObjective",
            "read": true,
            "create": true,
            "update": true,
            "delete": true,
            "defaultAuthStrategiesForCRUD": \[
              {
                "authStrategyId": 4,
                "authStrategyName": "NamespaceBased",
                "displayName": "Namespace Based",
                "isInheritedFromParent": true
              },
              {
                "authStrategyId": 1,
                "authStrategyName": "NoFurtherAuthorizationRequired",
                "displayName": "No Further Authorization Required",
                "isInheritedFromParent": true
              },
              {
                "authStrategyId": 4,
                "authStrategyName": "NamespaceBased",
                "displayName": "Namespace Based",
                "isInheritedFromParent": true
              },
              {
                "authStrategyId": 4,
                "authStrategyName": "NamespaceBased",
                "displayName": "Namespace Based",
                "isInheritedFromParent": true
              }
            \],
            "authStrategyOverridesForCRUD": \[
              null,
              null,
              null,
              null
            \],
            "children": \[\]
          }
        \]
      },
      {
        "name": "academicSubjectDescriptor",
        "read": true,
        "create": true,
        "update": true,
        "delete": true,
        "defaultAuthStrategiesForCRUD": \[
          {
            "authStrategyId": 4,
            "authStrategyName": "NamespaceBased",
            "displayName": "Namespace Based",
            "isInheritedFromParent": true
          },
          {
            "authStrategyId": 1,
            "authStrategyName": "NoFurtherAuthorizationRequired",
            "displayName": "No Further Authorization Required",
            "isInheritedFromParent": true
          },
          {
            "authStrategyId": 4,
            "authStrategyName": "NamespaceBased",
            "displayName": "Namespace Based",
            "isInheritedFromParent": true
          },
          {
            "authStrategyId": 4,
            "authStrategyName": "NamespaceBased",
            "displayName": "Namespace Based",
            "isInheritedFromParent": true
          }
        \],
        "authStrategyOverridesForCRUD": \[
          null,
          null,
          null,
          null
        \],
        "children": \[\]
      }
    \],
    "id": 17,
    "name": "Working-ClaimSet",
    "isSystemReserved": false,
    "applicationsCount": 0
  },
  "status": 201,
  "title": "ClaimSet created successfully"
}

### Retrieve a claim set

To retrieve the claim information, we will use the Claims ID, the example would be as follows.

**GET /v1/claimsets/{id}**

def get\_claimset(
    base\_url: str,
    access\_token: str,
    id: int,
) -> dict:
    '''
        Get an existing claimset using the resource identifier

        Parameters
        ----------
        base\_url: str
            URL where API is hosted
        access\_token: str
            String with the authorization token bearer
        id: int
            Resource identifier
    '''
    endpoint = "/v1/claimsets"
    url = f"{base\_url}{endpoint}/{id}"
    headers = {
        'Authorization': f'Bearer {access\_token}',
        'Content-type': 'application/json', 
        'Accept': 'text/plain',
        }

    r = requests.get(
        url=url,
        headers=headers,
        verify=False,
        )

    return r.json()

### Update a claim set

In case you want to update some info from the previous claim set. For this example, we will use the next input.

**Sample input**

claimset\_payload = {
        "name": "Updated-ClaimSet",
        "resourceClaims": \[
            {
                "name": "educationStandards",
                "read": True,
                "create": True,
                "update": True,
                "delete": True,
                "defaultAuthStrategiesForCRUD": \[
                {
                    "authStrategyName": "NamespaceBased",
                    "isInheritedFromParent": False
                },
                {
                    "authStrategyName": "NoFurtherAuthorizationRequired",
                    "isInheritedFromParent": False
                },
                {
                    "authStrategyName": "NamespaceBased",
                    "isInheritedFromParent": False
                },
                {
                    "authStrategyName": "NamespaceBased",
                    "isInheritedFromParent": False
                }
                \],
                "authStrategyOverridesForCRUD": \[
                None,
                None,
                None,
                None
                \],
                "children": \[
                {
                    "name": "learningObjective",
                    "read": True,
                    "create": True,
                    "update": True,
                    "delete": True,
                    "defaultAuthStrategiesForCRUD": \[
                    {
                        "authStrategyName": "NamespaceBased",
                        "isInheritedFromParent": True
                    },
                    {
                        "authStrategyName": "NoFurtherAuthorizationRequired",
                        "isInheritedFromParent": True
                    },
                    {
                        "authStrategyName": "NamespaceBased",
                        "isInheritedFromParent": True
                    },
                    {
                        "authStrategyName": "NamespaceBased",
                        "isInheritedFromParent": True
                    }
                    \],
                    "authStrategyOverridesForCRUD": \[
                    None,
                    None,
                    None,
                    None
                    \],
                    "children": \[\]
                }
                \]
            },
            {
                "name": "academicSubjectDescriptor",
                "read": True,
                "create": True,
                "update": True,
                "delete": True,
                "defaultAuthStrategiesForCRUD": \[
                {
                    "authStrategyName": "NamespaceBased",
                    "isInheritedFromParent": True
                },
                {
                    "authStrategyName": "NoFurtherAuthorizationRequired",
                    "isInheritedFromParent": True
                },
                {
                    "authStrategyName": "NamespaceBased",
                    "isInheritedFromParent": True
                },
                {
                    "authStrategyName": "NamespaceBased",
                    "isInheritedFromParent": True
                }
                \],
                "authStrategyOverridesForCRUD": \[
                None,
                None,
                None,
                None
                \],
                "children": \[\]
            }      
            \]
        }

And the code to update goes as follows.

**PUT /v1/claimsets/{id}**

def edit\_claimset(
    base\_url: str,
    access\_token: str,
    payload: dict,
    id: int,
) -> dict:
    '''
        Updates a claimset based on resource identifier

        Parameters
        ----------
        base\_url: str
            URL where API is hosted
        access\_token: str
            String with the authorization token bearer
        payload: dict
            {
                "name": "string",
                "resourceClaims": \[
                    {
                        "name": "string",
                        "read": true,
                        "create": true,
                        "update": true,
                        "delete": true,
                        "defaultAuthStrategiesForCRUD": \[
                            {
                            "authStrategyName": "string",
                            "isInheritedFromParent": true
                            }
                        \],
                        "authStrategyOverridesForCRUD": \[
                            {
                            "authStrategyName": "string",
                            "isInheritedFromParent": true
                            }
                        \],
                        "children": \[
                            "string"
                        \]
                        }
                    \]
                }
    '''
    endpoint = "/v1/claimsets"
    url = f"{base\_url}{endpoint}/{id}"
    headers = {
        'Authorization': f'Bearer {access\_token}',
        'Content-type': 'application/json', 
        'Accept': 'text/plain',
        }

    r = requests.put(
        url=url,
        headers=headers,
        json=payload,
        verify=False,
        )

    return r.json()

The given output will look like the following output.

**Sample output**

{
  "result": {
    "resourceClaims": \[
      {
        "name": "educationStandards",
        "read": true,
        "create": true,
        "update": true,
        "delete": true,
        "defaultAuthStrategiesForCRUD": \[
          {
            "authStrategyId": 4,
            "authStrategyName": "NamespaceBased",
            "displayName": "Namespace Based",
            "isInheritedFromParent": false
          },
          {
            "authStrategyId": 1,
            "authStrategyName": "NoFurtherAuthorizationRequired",
            "displayName": "No Further Authorization Required",
            "isInheritedFromParent": false
          },
          {
            "authStrategyId": 4,
            "authStrategyName": "NamespaceBased",
            "displayName": "Namespace Based",
            "isInheritedFromParent": false
          },
          {
            "authStrategyId": 4,
            "authStrategyName": "NamespaceBased",
            "displayName": "Namespace Based",
            "isInheritedFromParent": false
          }
        \],
        "authStrategyOverridesForCRUD": \[
          null,
          null,
          null,
          null
        \],
        "children": \[
          {
            "name": "learningObjective",
            "read": true,
            "create": true,
            "update": true,
            "delete": true,
            "defaultAuthStrategiesForCRUD": \[
              {
                "authStrategyId": 4,
                "authStrategyName": "NamespaceBased",
                "displayName": "Namespace Based",
                "isInheritedFromParent": true
              },
              {
                "authStrategyId": 1,
                "authStrategyName": "NoFurtherAuthorizationRequired",
                "displayName": "No Further Authorization Required",
                "isInheritedFromParent": true
              },
              {
                "authStrategyId": 4,
                "authStrategyName": "NamespaceBased",
                "displayName": "Namespace Based",
                "isInheritedFromParent": true
              },
              {
                "authStrategyId": 4,
                "authStrategyName": "NamespaceBased",
                "displayName": "Namespace Based",
                "isInheritedFromParent": true
              }
            \],
            "authStrategyOverridesForCRUD": \[
              null,
              null,
              null,
              null
            \],
            "children": \[\]
          }
        \]
      },
      {
        "name": "academicSubjectDescriptor",
        "read": true,
        "create": true,
        "update": true,
        "delete": true,
        "defaultAuthStrategiesForCRUD": \[
          {
            "authStrategyId": 4,
            "authStrategyName": "NamespaceBased",
            "displayName": "Namespace Based",
            "isInheritedFromParent": true
          },
          {
            "authStrategyId": 1,
            "authStrategyName": "NoFurtherAuthorizationRequired",
            "displayName": "No Further Authorization Required",
            "isInheritedFromParent": true
          },
          {
            "authStrategyId": 4,
            "authStrategyName": "NamespaceBased",
            "displayName": "Namespace Based",
            "isInheritedFromParent": true
          },
          {
            "authStrategyId": 4,
            "authStrategyName": "NamespaceBased",
            "displayName": "Namespace Based",
            "isInheritedFromParent": true
          }
        \],
        "authStrategyOverridesForCRUD": \[
          null,
          null,
          null,
          null
        \],
        "children": \[\]
      }
    \],
    "id": 17,
    "name": "Updated-ClaimSet",
    "isSystemReserved": false,
    "applicationsCount": 0
  },
  "status": 200,
  "title": "Request successful"
}

### Delete a claim set

To delete a claim set you can use the example below.

**DELETE /v1/claimset/{id}**

def delete\_claimset(
    base\_url: str,
    access\_token: str,
    id: int,
) -> dict:
    '''
        Deletes an existing claimset using the resource identifier

        Parameters
        ----------
        base\_url: str
            URL where API is hosted
        access\_token: str
            String with the authorization token bearer
        id: int
            Resource identifier
    '''
    endpoint = "/v1/claimsets"
    url = f"{base\_url}{endpoint}/{id}"
    headers = {
        'Authorization': f'Bearer {access\_token}',
        'Content-type': 'application/json', 
        'Accept': 'text/plain',
        }

    r = requests.delete(
        url=url,
        headers=headers,
        verify=False,
        )

    return r.json()

The confirmation meesage.

**Sample output**

{
  "status": 200,
  "title": "ClaimSet deleted successfully"
}

Applications
------------

### Create an Application and Set of Credentials

To create an application, we use the POST verb, and we will pass it a dictionary with the values to store, an example of payload for this case could be the following.

**Sample data create appication**

application\_payload = {
            "applicationName": "Acme Learning",
            "vendorId": 1,
            "claimSetName": "Acme Learning User",
            "educationOrganizationIds": \[
                0
            \]
        }

Which we will use inside a variable to pass it inside a function like a payload.

**POST /v1/applications**

def create\_application(
    base\_url: str,
    access\_token: str,
    payload: dict,
) -> dict:
    '''
        Creates a application based on supplied values

        Parameters
        ----------
        base\_url: str
            URL where API is hosted
        access\_token: str
            String with the authorization token bearer
        payload: dict
            {
                "applicationName": "string",
                "vendorId": 0,
                "claimSetName": "string",
                "profileId": 0,
                "educationOrganizationIds": \[
                    0
                \]
            }
    '''
    endpoint = "/v1/applications"
    url = f"{base\_url}{endpoint}"
    headers = {
        'Authorization': f'Bearer {access\_token}',
        'Content-type': 'application/json', 
        'Accept': 'text/plain',
        }
    r = requests.post(
        url=url,
        headers=headers,
        json=payload,
        verify=False,
        )

    return r.json()

The result of the code above it will give you an output as follows.

**Sample output**

{
  "result": {
    "applicationId": 4,
    "key": "ZQeSgtdaj2GI",
    "secret": "XHuwnSJLxkWUKfXzYAXkSkaG"
  },
  "status": 201,
  "title": "Application created successfully"
}

### Retrieve application data

Where you can obtain the key and secret from the response, and save the application ID. If you need to verify that your app was created, you can use the code as follows with the App ID.

**GET /v1/applications/{id}**

def get\_application(
    base\_url: str,
    access\_token: str,
    id: int,
) -> dict:
    '''
        Get an existing application using the resource identifier

        Parameters
        ----------
        base\_url: str
            URL where API is hosted
        access\_token: str
            String with the authorization token bearer
        id: int
            Resource identifier
    '''
    endpoint = "/v1/applications"
    url = f"{base\_url}{endpoint}/{id}"
    headers = {
        'Authorization': f'Bearer {access\_token}',
        'Content-type': 'application/json', 
        'Accept': 'text/plain',
        }

    r = requests.get(
        url=url,
        headers=headers,
        verify=False,
        )

    return r.json()

The confirmation outcome will be like the following:

**Sample output**

{
  "result": {
    "applicationId": 1,
    "applicationName": "Acme Learning",
    "claimSetName": "Acme Learning User",
    "profileName": null,
    "educationOrganizationId": 0,
    "odsInstanceName": null
  },
  "status": 200,
  "title": "Request successful"
}

### Update an application

You can use the following code to update the information in the application.

**PUT /v1/applications/{id}**

def edit\_application(
    base\_url: str,
    access\_token: str,
    payload: dict,
    id: int,
) -> dict:
    '''
        Updates an application based on resource id

        Parameters
        ----------
        base\_url: str
            URL where API is hosted
        access\_token: str
            String with the authorization token bearer
        payload: dict
            {
                "applicationName": "string",
                "vendorId": 0,
                "claimSetName": "string",
                "profileId": 0,
                "educationOrganizationIds": \[
                    0
                \]
            }
        id: int
            Resource ID
    '''
    endpoint = "/v1/applications"
    url = f"{base\_url}{endpoint}/{id}"
    headers = {
        'Authorization': f'Bearer {access\_token}',
        'Content-type': 'application/json', 
        'Accept': 'text/plain',
        }
    r = requests.put(
        url=url,
        headers=headers,
        json=payload,
        verify=False,
        )

    return r.json()

The confirmation result will be similar to the sample output.

**Sample output**

{
  "result": {
    "applicationId": 3,
    "applicationName": "Acme Learning Updated",
    "claimSetName": "Acme Learning User",
    "profileName": null,
    "educationOrganizationId": 0,
    "odsInstanceName": null
  },
  "status": 200,
  "title": "Application updated successfully"
}

### Delete an application

To delete an application the example will be the following.

**DELETE /v1/applications/{id}**

def delete\_application(
    base\_url: str,
    access\_token: str,
    id: int,
) -> dict:
    '''
        Deletes an existing application using the resource identifier

        Parameters
        ----------
        base\_url: str
            URL where API is hosted
        access\_token: str
            String with the authorization token bearer
        id: int
            Resource identifier
    '''
    endpoint = "/v1/applications"
    url = f"{base\_url}{endpoint}/{id}"
    headers = {
        'Authorization': f'Bearer {access\_token}',
        'Content-type': 'application/json', 
        'Accept': 'text/plain',
        }

    r = requests.delete(
        url=url,
        headers=headers,
        verify=False,
        )

    return r.json()

The output will be as follow:

**Sample output**

{
  "status": 200,
  "title": "Application deleted successfully"
}

### Refresh application credentials

In case you want to refresh your credentials or get a new ones you can use the next endpoint.

**PUT /v1/applications/{id}/reset-credential**

def reset\_application\_credentials(
    base\_url: str,
    access\_token: str,
    id: int,
) -> dict:
    '''
        Get an existing application using the resource identifier

        Parameters
        ----------
        base\_url: str
            URL where API is hosted
        access\_token: str
            String with the authorization token bearer
        id: int
            Resource identifier
    '''
    endpoint = "/v1/applications"
    url = f"{base\_url}{endpoint}/{id}/reset-credential"
    headers = {
        'Authorization': f'Bearer {access\_token}',
        'Content-type': 'application/json', 
        'Accept': 'text/plain',
        }

    r = requests.put(
        url=url,
        headers=headers,
        verify=False,
        )

    return r.json()

The resulting output will again print the new secret keys.

**Sample output**

{
  "result": {
    "applicationId": 4,
    "key": "ZQeSgtdaj2GI",
    "secret": "GeAepnauytC1NqaJV2HKfhit"
  },
  "status": 200,
  "title": "Application secret updated successfully"
}