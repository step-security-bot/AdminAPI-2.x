// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

namespace EdFi.Ods.AdminApi.Infrastructure.ClaimSetEditor;

public class Action : Enumeration<Action, string>
{
    public static readonly Action _create = new("Create", "Create");
    public static readonly Action _read = new("Read", "Read");
    public static readonly Action _update = new("Update", "Update");
    public static readonly Action _delete = new("Delete", "Delete");

    private Action(string value, string displayName) : base(value, displayName) { }
}
