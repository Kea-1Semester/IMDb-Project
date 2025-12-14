#!/bin/bash

echo "
╔═══════════════════════════════════╗
║         BUILDING PROJECTS         ║
╚═══════════════════════════════════╝
"

# Docker compose test environment
docker-compose -f scripts/docker-compose.test.yml up -d 

export MySqlConnectionString_test="server=localhost;port=3308;database=testdb;user=testuser;password=testpass"

# # Run unit tests
dotnet test backend/unit.tests

sleep 30

# # Run integration tests
dotnet test backend/integration.tests

# # Tear down test environment
docker-compose -f scripts/docker-compose.test.yml down -v
