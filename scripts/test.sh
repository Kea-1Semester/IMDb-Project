#!/bin/bash

echo "
╔═══════════════════════════════════╗
║         BUILDING PROJECTS         ║
╚═══════════════════════════════════╝
"

# Run unit tests
dotnet test backend/unit.tests

# Run integration tests
dotnet test backend/integration.tests