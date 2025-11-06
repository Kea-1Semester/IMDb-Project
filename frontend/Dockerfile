# Build stage
FROM node:22-alpine AS builder
WORKDIR /app
COPY package*.json ./
RUN npm ci
COPY . ./
RUN npm run build

# Production stage
FROM node:22-alpine
WORKDIR /app
COPY --from=builder /app ./
ENV PORT=4173
EXPOSE 4173
CMD ["sh", "-c", "npm run start"]
