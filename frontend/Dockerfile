# Build stage
FROM node:22-alpine AS builder
WORKDIR /app

COPY package*.json ./
RUN npm ci

COPY . .

RUN apk add --no-cache bash

# Build the app (creates /dist)
RUN npm run build

# Production stage
FROM node:22-alpine
WORKDIR /app

# Copy only what's needed for running the built app
COPY package*.json ./
RUN npm install 

# Copy the built files from the builder stage
COPY . .
# Expose the default preview port
EXPOSE 4173

# Use a lightweight static server instead of vite preview
RUN npm install -g serve

# Start the static server
CMD ["npm", "run", "start"]
