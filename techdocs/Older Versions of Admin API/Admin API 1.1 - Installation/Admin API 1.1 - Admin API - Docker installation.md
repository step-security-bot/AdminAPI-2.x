Created by Jason Hoekstra on Jan 20, 2023

**Contents:**

/\*<!\[CDATA\[\*/ div.rbtoc1705534704184 {padding: 0px;} div.rbtoc1705534704184 ul {margin-left: 0px;} div.rbtoc1705534704184 li {margin-left: 0px;padding-left: 0px;} /\*\]\]>\*/

*   [Before You Install](#AdminAPI1.1AdminAPIDockerinstallation-BeforeYouInstall)
    *   [Compatibility & Supported ODS / API Versions](#AdminAPI1.1AdminAPIDockerinstallation-Compatibility&SupportedODS/APIVersions)
    *   [IMPORTANT INFORMATION FOR VERSION 1.1.0](#AdminAPI1.1AdminAPIDockerinstallation-IMPORTANTINFORMATIONFORVERSION1.1.0)
*   [Installation Instructions](#AdminAPI1.1AdminAPIDockerinstallation-InstallationInstructions)
    *   [General Prerequisites](#AdminAPI1.1AdminAPIDockerinstallation-GeneralPrerequisites)
*   [Installation Instructions](#AdminAPI1.1AdminAPIDockerinstallation-InstallationInstructions.1)
    *   [1\. Include Admin API in the ODS Docker Setup](#AdminAPI1.1AdminAPIDockerinstallation-1.IncludeAdminAPIintheODSDockerSetup)
    *   [2\. Relaunch the Docker Composition](#AdminAPI1.1AdminAPIDockerinstallation-2.RelaunchtheDockerComposition)
    *   [3\. Execute First-Time Configuration](#AdminAPI1.1AdminAPIDockerinstallation-3.ExecuteFirst-TimeConfiguration)

**Admin API Installation for Docker or On-Premises IIS**

The following is the DockerHub repo for **Admin API v1.1.0 Docker Image** for inclusion in Docker compose:

*   [edfialliance/ods-admin-api:v1.1.0](https://hub.docker.com/layers/edfialliance/ods-admin-api/v1.1.0/images/sha256-431f479e6c7aab96561ad6a8484a06b33ffee3a6c617c5dc14b0d90fadd9faea?context=repo)
    
*   [edfialliance/ods-admin-api-db:v1.1.0](https://hub.docker.com/layers/edfialliance/ods-admin-api-db/v1.1.0/images/sha256-952c885860bde3f20cd6fba2c08d634979b257be3e9e0de076287475fcb02bd0?context=repo)
    

Before You Install
==================

This section provides general information you should review before installing the Ed-Fi ODS / API Admin API for Suite 3 v1.1.0.

Compatibility & Supported ODS / API Versions
--------------------------------------------

This version of the Admin API has been tested with and can be installed for  the Ed-Fi ODS / API Platform v3.4 through v5.3.  
See the [Ed-Fi Technology Version Index](https://techdocs.ed-fi.org/display/ETKB/Ed-Fi+Technology+Version+Index) for more details.

IMPORTANT INFORMATION FOR VERSION 1.1.0
---------------------------------------

Manual First-Time Setup

Admin API v1.1.0 requires minor extra setup after the instructions outlined below. These may be automated in future releases, and could require additional steps to support upgrading. See [Admin API 1.1 - First-Time Configuration for Admin API](Admin-API-1.1---First-Time-Configuration-for-Admin-API_138647052.html) for more details.

Load Balancing and Horizontal Scaling Not Supported

Admin API v1.1.0 only supports running one instance of the application at a time in an ODS / API ecosystem. Future versions may allow for scaling and load balancing.

Upgrading

Admin API does not today support in-place upgrades from prior versions. Please install a fresh copy of Admin API to upgrade from prior versions.

Installation Instructions
=========================

General Prerequisites
---------------------

The following are required to install the Admin API:

*   The Admin API provides an interface to administer an Ed-Fi ODS / API. Understandably, you must have an instance of the Ed-Fi ODS / API v3.4 through v5.3 deployed and operational before you can use the Admin API. Tested configurations include on-premises installation via [binary installation](https://techdocs.ed-fi.org/display/ODSAPIS3V520/Getting+Started+-+Binary+Installation) or [source code installation](https://techdocs.ed-fi.org/display/ODSAPIS3V520/Getting+Started+-+Source+Code+Installation). 
*   A SQL Server 2012 or higher, or Postgres 11 or higher database server (i.e., the same platform requirement applicable to your ODS / API v3.4 through v5.3.0 installation).
*   A modern web browser such as Google Chrome, Mozilla Firefox, or Microsoft Edge is required to view live Swagger documentation. Internet Explorer 11 (a pre-installed browser on Windows Server) may load, but may not function when using Admin API.

Installation Instructions
=========================

Admin API is not included with the ODS-Docker solution by default, but can be hosted as part of that ecosystem.

To install Admin API on Docker, first Install the [ODS / API Docker](https://github.com/Ed-Fi-Alliance-OSS/Ed-Fi-ODS-Docker) environment [following these instructions](https://techdocs.ed-fi.org/display/EDFITOOLS/Docker+Deployment). Then, apply the below changes to the environment to introduce the Admin API.

1\. Include Admin API in the ODS Docker Setup
---------------------------------------------

### Docker Compose

Add the following to your `docker-compose.yml`  file. This can be done either instead of or in addition to the `adminapp`  service.

#### Admin API Application

This service depends on the pb-admin  and subsequently db-admin services to run.

**docker-compose.yml**

\# ... above are other services
adminapi:
    build:
    image: edfialliance/ods-admin-api:${ADMIN\_API\_TAG}
    environment:
      ADMIN\_POSTGRES\_HOST: pb-admin
      POSTGRES\_PORT: "${PGBOUNCER\_LISTEN\_PORT:-6432}"
      POSTGRES\_USER: "${POSTGRES\_USER}"
      POSTGRES\_PASSWORD: "${POSTGRES\_PASSWORD}"
      DATABASEENGINE: "PostgreSql"
      API\_MODE: ${API\_MODE}
      AUTHORITY: ${AUTHORITY}
      ISSUER\_URL: ${ISSUER\_URL}
      SIGNING\_KEY: ${SIGNING\_KEY}
      ADMIN\_API\_VIRTUAL\_NAME: ${ADMIN\_API\_VIRTUAL\_NAME:-adminapi} 
      API\_INTERNAL\_URL: ${API\_INTERNAL\_URL}
    volumes:
      - ../../Docker/ssl:/ssl/
    depends\_on:
      - pb-admin
    restart: always
    hostname: ${ADMIN\_API\_VIRTUAL\_NAME:-adminapi} 
    container\_name: adminapi
    healthcheck:
      test: $$ADMIN\_API\_HEALTHCHECK\_TEST
      start\_period: "60s"
      retries: 3
# ... below are network and volume configs

#### Admin API Database

For the most part, the Admin API shares the same database schema as the Admin App. However, there are a few tables required for storing API client authentication which need to be initialized manually. You can see the details in [Admin API 1.1 - First-Time Configuration for Admin API](Admin-API-1.1---First-Time-Configuration-for-Admin-API_138647052.html).

Rather than introducing these tables explicitly, for Docker we have provided an alternative image for use with Admin API: [`edfialliance/ods-admin-api-db`](https://hub.docker.com/r/edfialliance/ods-admin-api-db), which is to be used **in place of** the existing edfialliance/ods-api-db-admin image for your DB service.

If you are introducing Admin API to an existing composition do NOT change the volume mapping configuration in order to preserve your data. Only change the image and tag of the existing service. The below block is a sample of this, based on an example ODS / API Docker environment composition. Make sure you update the mode ("SharedInstance", "YearSpecific", or "DistrictSpecific") accordingly.

**docker-compose.yml**

\# ... above are other services
db-admin:
    image: edfialliance/ods-admin-api-db-admin:${ADMIN\_API\_DB\_TAG}
    environment:
      POSTGRES\_USER: "${POSTGRES\_USER}"
      POSTGRES\_PASSWORD: "${POSTGRES\_PASSWORD}"
      API\_MODE: <YOUR MODE HERE>
    volumes:
      - vol-db-admin:/var/lib/postgresql/data
    restart: always
    container\_name: ed-fi-db-admin
# ... below are other services

### .env Settings

Add the following to your environment settings file to support Admin API. Note that when running both Admin App and Admin API, some of these settings may overlap. This is expected, and the same values can be used.

**.env for Admin API**

ADMIN\_API\_TAG=<version of image to run>
ADMIN\_API\_DB\_TAG=<version of image to run> 
API\_MODE=<API Mode Eg. SharedInstance, YearSpecific, DistrictSpecific>
ADMIN\_API\_VIRTUAL\_NAME=<virtual name for the Admin API endpoint>
ODS\_VIRTUAL\_NAME=<virtual name for the ods endpoint>

# For Authentication
AUTHORITY=<Authentication Authority Appsetting Eg. http://localhost/${ADMIN\_API\_VIRTUAL\_NAME}>
ISSUER\_URL=<Authentication IssuerUrl Appsetting Eg. https://localhost/${ADMIN\_API\_VIRTUAL\_NAME}>
SIGNING\_KEY=<Authentication Signing Key (Symmetric Security Key) for Auth Tokens>

# For Postgres only
POSTGRES\_USER=<default postgres database user>
POSTGRES\_PASSWORD=<password for default postgres user>
PGBOUNCER\_LISTEN\_PORT=<port for pg bouncer to listen to>

# The following needs to be set to specify a health check test for Admin api.
# RECOMMENDED: To use the default internal Admin Api health check endpoint, set the variable as follows:
ADMIN\_API\_HEALTHCHECK\_TEST="curl -f http://${ADMIN\_API\_VIRTUAL\_NAME}/health"
#  To disable the health check, remove the above and instead set the variable as follows:
# ADMIN\_API\_HEALTHCHECK\_TEST=/bin/true
#  To add a custom health check, consult the documentation at https://docs.docker.com/compose/compose-file/compose-file-v3/#healthcheck

# The following needs to be set to specify the ODS API endpoint for Admin API to internally connect.
# If user chooses direct connection between ODS API and Admin API within docker network, then set the api internal url as follows
API\_INTERNAL\_URL = http://${ODS\_VIRTUAL\_NAME}

### Nginx / Gateway Configuration

Update your nginx server configuration to include the Admin API in the reverse proxy.

**default.conf.template**

\# upstream server config...

server {
    #listen / server config...
    #ssl\_certificate config...

    # other locations...

    location /${ADMIN\_API\_VIRTUAL\_NAME} {
        client\_max\_body\_size 20M;
        proxy\_pass http://${ADMIN\_API\_VIRTUAL\_NAME};
        proxy\_set\_header X-Forwarded-Proto $scheme;
        proxy\_set\_header X-Forwarded-Host $host;
        proxy\_set\_header X-Forwarded-Port 443;
        proxy\_set\_header X-Forwarded-For $proxy\_add\_x\_forwarded\_for;
    }
}

2\. Relaunch the Docker Composition
-----------------------------------

After updating the files, restart the docker composition

docker compose -f ./compose/your-compose-file.yml --env-file ./.env up -d

3\. Execute First-Time Configuration
------------------------------------

Continue on to [Admin API 1.1 - First-Time Configuration for Admin API](Admin-API-1.1---First-Time-Configuration-for-Admin-API_138647052.html).

  

Attachments:
------------

![](images/icons/bullet_blue.gif) [image2019-7-16\_12-58-41.png](attachments/138647023/138647024.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2019-7-17\_16-31-47.png](attachments/138647023/138647025.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2019-7-18\_12-55-24.png](attachments/138647023/138647026.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2019-7-17\_16-15-57.png](attachments/138647023/138647027.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2019-7-17\_16-8-37.png](attachments/138647023/138647028.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2019-7-17\_16-1-46.png](attachments/138647023/138647029.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2019-7-17\_15-57-20.png](attachments/138647023/138647030.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2019-7-17\_15-53-53.png](attachments/138647023/138647031.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2019-7-17\_14-21-50.png](attachments/138647023/138647032.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2019-7-17\_14-19-7.png](attachments/138647023/138647033.png) (image/png)  
![](images/icons/bullet_blue.gif) [image2019-7-17\_14-17-7.png](attachments/138647023/138647034.png) (image/png)  
![](images/icons/bullet_blue.gif) [Populated Installation Folder.png](attachments/138647023/138647035.png) (image/png)  
![](images/icons/bullet_blue.gif) [Empty Installation Folder.png](attachments/138647023/138647036.png) (image/png)