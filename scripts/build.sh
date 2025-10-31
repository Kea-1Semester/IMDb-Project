#!/bin/bash

echo "
╔═══════════════════════════════════╗
║           RUNNING BUILD           ║
╚═══════════════════════════════════╝
"

# Build frontend project
cd ./frontend || exit 1

# Install dependencies
npm install

# Run build script
npm run build