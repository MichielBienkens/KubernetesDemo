kubectl create -f .\productsAPI.yml
kubectl port-forward productsapi 8081:80 (only works while command is running)
Open localhost:8081/swagger in browser

kubectl delete -f .\productsAPI.yml 