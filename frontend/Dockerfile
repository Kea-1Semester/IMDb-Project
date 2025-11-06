# Build stage: build your frontend app
FROM node:22-alpine AS builder
WORKDIR /app

# Install bash if needed for build scripts
RUN apk --no-cache add bash

# Copy package files and install dependencies
COPY package*.json ./
RUN npm ci

# Copy rest of source code and build the static files
COPY . ./
RUN npm run build

# Production stage: serve static files with nginx
FROM nginx:alpine

# Remove default nginx content
RUN rm -rf /usr/share/nginx/html/*

# Copy built static files from builder stage
COPY --from=builder /app/dist /usr/share/nginx/html

# Expose port 80 to serve content
EXPOSE 80

# Start nginx in foreground
CMD ["nginx", "-g", "daemon off;"]
