# Build stage
FROM node:22-alpine AS builder
WORKDIR /app

# Install production dependencies for build (use npm ci for reproducible installs)
COPY package*.json ./
RUN npm install

# Copy sources
COPY . .


# Ensure bash is available for scripts starting with #!/bin/bash
RUN apk add --no-cache bash

# Run build (npm run build calls bash ./scripts/build.sh)
RUN npm run build

# Production stage
FROM node:22-alpine AS runner
WORKDIR /app
ENV NODE_ENV=production

# Install only production dependencies
COPY package*.json ./
# RUN npm ci --omit=dev
RUN npm install

# Copy built assets and config files from builder
COPY --from=builder /app/dist ./dist
COPY --from=builder /app/vite.config.* ./
COPY --from=builder /app/index.html ./

EXPOSE 4173
CMD ["npm", "run", "start"]
