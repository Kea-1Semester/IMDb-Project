FROM node:22-alpine
WORKDIR /app

# Copy only production dependencies
COPY package*.json ./

RUN npm install
# Copy built assets and necessary files
COPY . .

EXPOSE 4173
CMD ["npm", "run", "start"]