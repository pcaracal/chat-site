# Angular
FROM node:18.17.1-slim AS builder

WORKDIR /app

COPY package.json package-lock.json ./
RUN npm ci

COPY . .

RUN npm run build:prod

# Nginx
FROM nginx:1.21.3-alpine as nginx

COPY --from=builder /app/dist /usr/share/nginx/html

# COPY ./nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
