Created by Jason Hoekstra, last modified on Jan 16, 2024

MultiTenancy can be enabled in the appsettings file by updating “MultiTenancy” flag to true.

"AppSettings": {
     
	“MultiTenancy”: true
}

Connection Strings
------------------

The connection strings for the EdFi\_Admin and EdFi\_Security databases are configured differently depending on whether the MultiTenancy feature is enabled or disabled.

### Single-Tenant Configuration (the default)

In a single-tenant configuration, there is only one EdFi\_Admin and EdFi\_Security used by Admin API.

The connection strings are in the "ConnectionStrings" section, as follows:

"ConnectionStrings": {
    "EdFi\_Admin": "Server=(local); Database=EdFi\_Admin; Integrated Security=True; Application Name=AdminApi;",
    "EdFi\_Security": "Server=(local); Database=EdFi\_Security; Integrated Security=True; Application Name=AdminApi;"
}

### Multi-Tenant Configuration

In a multi-tenant configuration, each tenant has its own EdFi\_Admin and EdFi\_Security databases.

The connection strings are organized under distinct tenant entries in a "Tenants" section, as follows:

"Tenants": {
    "Tenant1": {
      "ConnectionStrings": {
        "EdFi\_Admin": "Server=(local); Database=EdFi\_Admin\_Tenant1; Integrated Security=True; Application Name=AdminApi;",
        "EdFi\_Security": "Server=(local); Database=EdFi\_Security\_Tenant1; Integrated Security=True; Application Name=AdminApi;"
      }
    },
    "Tenant2": {
      "ConnectionStrings": {
        "EdFi\_Admin": "Server=(local); Database=EdFi\_Admin\_Tenant2; Integrated Security=True; Application Name=AdminApi;",
        "EdFi\_Security": "Server=(local); Database=EdFi\_Security\_Tenant2; Integrated Security=True; Application Name=AdminApi;"
      }
    }
  }

Request header
--------------

Admin API clients must use request header to carry the well-known tenant identifiers:

**Tenant specific request header key:** _tenant_

**Tenant specific request header value:**

![](attachments/186024307/186024367.png)

Note: The requests from Swagger UI don’t automatically include tenant identifier in the headers. So, if user enables Swagger UI and MultiTenancy,

then “DefaultTenant” should be configured as follows in the appSettings file:

"SwaggerSettings": {
        "EnableSwagger": true,
        "DefaultTenant": "Tenant1"
    }

So, all the requests from Swagger UI will be using “DefaultTenant” as tenant identifier.

  

  

Attachments:
------------

![](images/icons/bullet_blue.gif) [image-2024-1-15\_18-3-15-1.png](attachments/186024307/186024367.png) (image/png)