#!/bin/bash

echo "
╔═══════════════════════════════════╗
║        RUNNING DEVELOPMENT        ║
╚═══════════════════════════════════╝
"

# Start Docker containers in development mode
docker-compose -f docker-compose.dev.yml up -d