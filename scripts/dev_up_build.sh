#!/bin/bash

echo "
╔═══════════════════════════════════╗
║        RUNNING DEVELOPMENT        ║
╚═══════════════════════════════════╝
"

echo "Navigating to root..."

# Navigate to project root
cd .. || exit 1

# Start Docker containers in development mode
docker-compose -f docker-compose.dev.yml up -d --build