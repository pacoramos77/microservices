
echo "build PlatformService"
docker build -t pacor2010/platformservice ../PlatformService
echo "Push PlatformService"
docker push pacor2010/platformservice

echo "build CommandService"
docker build -t pacor2010/commandservice ../CommandService
echo "Push CommandService"
docker push pacor2010/commandservice
