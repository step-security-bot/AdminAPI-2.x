Created by Jason Hoekstra, last modified on Nov 06, 2023

This section provides an overview of what's new in the latest versions of the Admin API.

**Contents**

/\*\*/ Updates in Admin API v2.0 (Latest Release) Updates in Admin API v1.3 Updates in Admin API v1.2 Updates in Admin API v1.1 Updates in Admin API v1.0

Updates in Admin API v2.0 (Latest Release)
==========================================

**ODS/API 7.0 Single-Line Product Support**

Admin API 2.0 only supports ODS / API 7.0.  In Admin API 1.x, we continue to support ODS/API 3.4 through 6.1. ADMINAPI-315 - Remove Backward Compatibility (pre ODS/API 7.0) Done

### ODS / API 7.x Multi-Instance Support

ODS / API 7.0 is a major platform upgrade with many features driven from various field scans reviews and forums.  This led to a major design and platform upgrade, please see [Multi-Tenancy, Deployment Modes, Routing](https://techdocs.ed-fi.org/display/EFTD/Multi-Tenancy%2C+Deployment+Modes%2C+Routing) for more details into the ODS / API 7 upgrades.  ODS/API 7 contains new database tables for ODS instance management, such as the OdsInstances, OdsInstanceDerivatives and OdsInstanceContexts tables, which Admin API 2.0 provides endpoints to manage metadata for these instances.  Admin API 2.0 does not create or delete physical instances, only the information for active ODS / API 7.0 instances within an Ed-Fi environment.  ADMINAPI-101 - ODS/API 7.0 - Updates & Multi-Instance Done

### Claimset Enhancements for API-based Handling

Admin API 2.0 has new API endpoints to allow for a workflow-based setup of claimset management for an ODS / API 7 instance.  The JSON large-format functionality has been moved to new `/import` and `/export` API endpoints to support backup and migrate operations with claimset metadata.  ADMINAPI-350 - Claimset Functionality Enhancements Done

### Dynamic Profile Support

ODS / API 7 brings a new feature for management of dynamic profiles, relying on the database instead of source code required updates in prior ODS / API lines. Admin API 2.0 allows for the updates via API new `/profile` endpoints. ADMINAPI-340 - ODS/API 7 - Dynamic Profiles Done

### Changing Ed Org Id Leaves a Record Behind

A bug was discovered where changing an education organization identifier leaves behind additional data affecting ed org hierarches and data access.  The Admin API 2.0.1 update resolves the issue for this use case.   ADMINAPI-767 - Changing Ed Org Id Leaves a Record Behind Done

Updates in Admin API v1.3 
==========================

### Refactor Admin API for Clean Separation

Admin API 1.3 has been refactored for more separation from Admin App, which was originally the development base for Admin API.   ADMINAPI-91 - Refactor Admin API for Clean Separation from Admin App Done

**  
Return Vendor and Profile IDs in /applications Endpoints**

A field report requested that vendor and profile IDs should be returned as part of the /applications endpoint, which has now been included in Admin API 1.3.

ADMINAPI-311 - Return vendor and profile ID in /applications endpoints Done

  

**Update System.Data.SqlClient to Microsoft.Data.SqlClient**

Due to a recommendation from Microsoft, we have updated the data access library to use Microsoft.Data.SqlClient instead of System.Data.SqlClient. ADMINAPI-47 - Update or Replace System.Data.SqlClient in AdminApi Done

**Disable Shell Debug Messages in Docker**

A field report requested to repress logging of certain elements in Docker configurations. ADMINAPI-86 - Disable Shell Debug Messages in Docker Done

### Changing Ed Org Id Leaves a Record Behind

A bug was discovered where changing an education organization identifier leaves behind additional data affecting ed org hierarches and data access.  The Admin API 1.3.1 update resolves the issue for this use case.   ADMINAPI-767 - Changing Ed Org Id Leaves a Record Behind Done

**Other Updates**

Other technical product updates, such as consolidating namespaces and library renaming, have also been included in this update.  Please see the [Admin API 1.3](https://tracker.ed-fi.org/projects/ADMINAPI/versions/15500#release-report-tab-body) release report for full details.

Updates in Admin API v1.2
=========================

### Multiple Security Model / ODS Version Support

The ODS/API Platform has two different security models in versions 3.4-5.3 and 6.0-6.1.  This version of Admin API supports both versions of that security model with the same operation endpoints for management via API (ODS/API v3.4-5.3 and v6.0-v6.1 and future versions may be supported).

Updates in Admin API v1.1
=========================

### Claim Sets

Admin API v1.1 provides support to importing and exporting claim sets via API. Admin API is available as both a standalone installation under IIS and as a Docker deployment.  Admin API v1.1 supports ODS/API v3.4 to v5.3.

Updates in Admin API v1.0
=========================

### Initial Release

This is the initial release of Admin API v1.0.  It provides functionality to create vendors, applications, and credentials within an Ed-Fi ODS / API Platform instance. Admin API is available as both a standalone installation under IIS and as a Docker deployment.

  

Attachments:
------------

![](images/icons/bullet_blue.gif) [image-2023-6-22\_17-44-17.png](attachments/170591045/170591046.png) (image/png)