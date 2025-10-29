#!/bin/bash

echo "
╔═══════════════════════════════════╗
║        RUNNING DEVELOPMENT        ║
╚═══════════════════════════════════╝
"

# Navigate to project root
cd ../IMDb-Project || exit 1

# Start Docker containers in development mode
docker-compose -f docker-compose.dev.yml up -d