# Build stage
FROM node:22-alpine AS builder
WORKDIR /app

# Copy package.json first
COPY package*.json ./
RUN npm ci

# Copy all frontend source
COPY . ./

RUN apk add --no-cache bash
# Build frontend (creates /app/dist)
RUN npm run build

# Production stage
FROM node:22-alpine
WORKDIR /app

# Install lightweight server
RUN npm install -g serve

# Copy build output from builder
COPY --from=builder /app/dist ./dist

# Expose port
EXPOSE 4173

# Start server
CMD ["serve", "-s", "dist", "-l", "4173"]
