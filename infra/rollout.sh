echo "Rollout platform deployment"
kubectl rollout restart deployment platforms-depl

echo "Rollout commands deployment"
kubectl rollout restart deployment commands-depl

