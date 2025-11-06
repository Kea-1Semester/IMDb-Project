# Build stage
FROM node:22-alpine AS builder
WORKDIR /app

COPY frontend/package*.json ./
RUN npm install

COPY . .

RUN apk add --no-cache bash

# Build the app (creates /dist)
RUN npm run build

# Production stage
FROM node:22-alpine
WORKDIR /app

# Copy only what's needed for running the built app
COPY package*.json ./
# RUN npm install 
RUN npm install -g serve


# Copy the built files from the builder stage
# COPY frontend/ ./
COPY --from=builder /app/dist ./dist

# Expose the default preview port
EXPOSE 4173

# Use a lightweight static server instead of vite preview
RUN npm install -g serve

# Start the static server
CMD ["serve", "-s", "dist", "-l", "4173"]


