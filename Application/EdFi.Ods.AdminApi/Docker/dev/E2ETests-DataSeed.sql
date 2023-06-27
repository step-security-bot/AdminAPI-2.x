-- SPDX-License-Identifier: Apache-2.0
-- Licensed to the Ed-Fi Alliance under one or more agreements.
-- The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
-- See the LICENSE and NOTICES files in the project root for more information.

INSERT INTO dbo.odsinstances(
	name, instancetype, status, isextended, version)
	VALUES ('OdsInstance1', 'OdsInstance', 'Active', '0', '7.0');

INSERT INTO dbo.odsinstances(
	name, instancetype, status, isextended, version)
	VALUES ('OdsInstance2', 'OdsInstance', 'Active', '0', '7.0');
