#!/bin/bash

echo "
╔═══════════════════════════════════╗
║        RUNNING DEVELOPMENT        ║
╚═══════════════════════════════════╝
"

echo "Navigating to SeedData..."

# Navigate to SeedData project
cd ./SeedData || exit 1

# Run dotnet build
dotnet build

echo "Navigating to root..."

# Navigate to project root
cd .. || exit 1

# Start Docker containers in development mode
docker-compose -f docker-compose.dev.yml up -d