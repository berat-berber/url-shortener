FROM node:22-alpine AS client-builder
WORKDIR /app
COPY client/package.json client/package-lock.json ./
RUN npm ci
COPY client/ .
ARG PUBLIC_API_URL=http://localhost:8080
ENV PUBLIC_API_URL=$PUBLIC_API_URL
RUN npm run build

FROM nginx:stable-alpine
COPY gateway/nginx.conf /etc/nginx/nginx.conf
COPY --from=client-builder /app/dist /usr/share/nginx/html