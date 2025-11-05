# Build stage
FROM node:22-alpine AS builder
WORKDIR /app

COPY package*.json ./
RUN npm ci

COPY . .

RUN apk add --no-cache bash

RUN npm run build

# Production stage
FROM node:22-alpine
WORKDIR /app

COPY package*.json ./
# RUN npm install --omit=dev
RUN npm install

COPY --from=builder /app/dist ./dist
COPY --from=builder /app/vite.config.* ./
COPY --from=builder /app/index.html ./

EXPOSE 4173
CMD ["npm", "run", "start"]

