#!/bin/bash

echo "
╔═══════════════════════════════════╗
║         BUILDING PROJECTS         ║
╚═══════════════════════════════════╝
"

echo "Navigating to frontend..."

# Navigate to frontend project
cd ./frontend || exit 1

# Install dependencies
npm install

# Run build script
npm run build

# Audit for safety
npm audit

echo "Navigating to SeedData..."

# Navigate to SeedData project
cd ../SeedData || exit 1

# Run dotnet build
dotnet build