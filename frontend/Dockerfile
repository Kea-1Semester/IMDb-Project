# ----------------------
# Build stage
# ----------------------
FROM node:18-alpine AS builder
WORKDIR /app

# Install bash if needed for build scripts
RUN apk --no-cache add bash

# Copy package files and install dependencies
COPY package*.json ./
RUN npm install --ignore-scripts

# Copy rest of source code and build the static files
COPY src ./src
COPY vite.config.ts ./
COPY tsconfig.*json ./  
COPY index.html ./


RUN npm run build

# ----------------------
# Production stage
# ----------------------
FROM nginx:alpine

# Switch to root user to modify nginx content
USER root

# Remove default nginx content
RUN rm -rf /usr/share/nginx/html/*

# Copy built static files from builder stage
COPY --from=builder /app/dist /usr/share/nginx/html


# Expose port 80 to serve content
EXPOSE 80

# Start nginx in foreground
CMD ["nginx", "-g", "daemon off;"]
