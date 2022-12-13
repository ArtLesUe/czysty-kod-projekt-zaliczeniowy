cd ..

git reset --hard
git clean -f -d -x
git submodule init

copy CkpTodoDocker\Config\baseUrl.ts CkpTodoFrontend\src\api\consts\baseUrl.ts /Y

docker stop ckp-frontend-node-1
docker rm ckp-frontend-node-1
docker image rm ckp-proj:frontend --force
docker build --network=host --tag ckp-proj:frontend --file CkpTodoDocker/Frontend/Dockerfile ./
docker run --restart unless-stopped --publish 3000:3000 --name ckp-frontend-node-1 --detach ckp-proj:frontend

docker stop ckp-backend-node-1
docker rm ckp-backend-node-1
docker stop ckp-backend-node-2
docker rm ckp-backend-node-2
docker stop ckp-backend-node-3
docker rm ckp-backend-node-3
docker stop ckp-backend-node-4
docker rm ckp-backend-node-4
docker image rm ckp-proj:backend --force
docker build --network=host --tag ckp-proj:backend --file CkpTodoDocker/Backend/Dockerfile ./

docker volume rm ckp-database

docker run --restart unless-stopped --publish 8001:80 --name ckp-backend-node-1 --detach -v ckp-database:/app/database ckp-proj:backend
docker run --restart unless-stopped --publish 8002:80 --name ckp-backend-node-2 --detach -v ckp-database:/app/database ckp-proj:backend
docker run --restart unless-stopped --publish 8003:80 --name ckp-backend-node-3 --detach -v ckp-database:/app/database ckp-proj:backend
docker run --restart unless-stopped --publish 8004:80 --name ckp-backend-node-4 --detach -v ckp-database:/app/database ckp-proj:backend

cd CkpTodoDocker