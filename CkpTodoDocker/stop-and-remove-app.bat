docker stop ckp-frontend-node-1
docker rm ckp-frontend-node-1
docker image rm ckp-proj:frontend --force

docker stop ckp-backend-node-1
docker rm ckp-backend-node-1
docker stop ckp-backend-node-2
docker rm ckp-backend-node-2
docker stop ckp-backend-node-3
docker rm ckp-backend-node-3
docker stop ckp-backend-node-4
docker rm ckp-backend-node-4
docker image rm ckp-proj:backend --force

docker volume rm ckp-database