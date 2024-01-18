\---

confluence-id: 127929054

confluence-space: %%CONFLUENCE-SPACE%%

\---

What's New (1.3 copy)
=====================

Created by Ian Christopher \[Contractor\] \[X\], last modified by Jason Hoekstra on Sep 19, 2023

This section provides an overview of what's new in the latest versions of the Admin API.

**Contents**

/\*\*/ Updates in Admin API v1.3 (Latest Release) Updates in Admin API v1.2 Updates in Admin API v1.1 Updates in Admin API v1.0

Updates in Admin API v1.3 (Latest Release)
==========================================

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

![](images/icons/bullet_blue.gif) [image-2023-6-22\_17-44-17.png](attachments/127929054/162202447.png) (image/png)