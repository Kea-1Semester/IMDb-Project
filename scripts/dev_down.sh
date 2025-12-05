#!/bin/bash

echo "
╔═══════════════════════════════════╗
║       STOPPING DEVELOPMENT        ║
╚═══════════════════════════════════╝
"

# Start Docker containers in development mode
docker-compose -f docker-compose.dev.yml down -v