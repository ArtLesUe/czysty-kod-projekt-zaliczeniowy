FROM node:lts-alpine
MAINTAINER "INF-NIES-SEM3-RA2223"

RUN mkdir -v /app

ADD /CkpTodoFrontend /app/CkpTodoFrontend

EXPOSE 3000

WORKDIR /app/CkpTodoFrontend

RUN npm install
RUN npm run build
RUN npm install serve

CMD ["npx", "serve", "-s", "build"]