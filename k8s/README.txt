Command to create a secret to allow the k8s cluster to get images from your container registry:

kubectl create secret docker-registry regcred --docker-server=<xxx.azurecr.io> --docker-username=<xxx> --docker-password=<lookup in Azure> --docker-email=<email>