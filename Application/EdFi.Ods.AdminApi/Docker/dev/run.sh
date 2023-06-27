#!/bin/bash
# SPDX-License-Identifier: Apache-2.0
# Licensed to the Ed-Fi Alliance under one or more agreements.
# The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
# See the LICENSE and NOTICES files in the project root for more information.

set -e
set +x

if [[ -z "$POSTGRES_PORT" ]]; then
  export POSTGRES_PORT=5432
fi

envsubst < /app/appsettings.Docker.json > /app/temp.json

mv /app/temp.json /app/appsettings.json

echo "Loading seed data..."
psql --no-password --username "$POSTGRES_USER" --port "5402" --dbname "EdFi_Admin" --file /DBSeed/E2ETests-DataSeed.sql 1> /dev/null

if [[ -f /ssl/server.crt ]]; then
 cp /ssl/server.crt /usr/local/share/ca-certificates/
 update-ca-certificates
fi

dotnet EdFi.Ods.AdminApi.dll
