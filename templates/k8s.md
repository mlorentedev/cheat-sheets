# {{RESOURCE_NAME}}

## List Resources

| COMMAND | DESCRIPTION |
| --- | --- |
| `kubectl get {{RESOURCE}}` | List all {{RESOURCE}} in current namespace |
| `kubectl get {{RESOURCE}} -A` | List all {{RESOURCE}} across all namespaces |
| `kubectl get {{RESOURCE}} -n <namespace>` | List {{RESOURCE}} in specific namespace |
| `kubectl get {{RESOURCE}} -o wide` | List with additional details |
| `kubectl get {{RESOURCE}} -o yaml` | Show full YAML output |

## Describe and Inspect

| COMMAND | DESCRIPTION |
| --- | --- |
| `kubectl describe {{RESOURCE}} <name>` | Show detailed information |
| `kubectl get {{RESOURCE}} <name> -o yaml` | Get resource as YAML |
| `kubectl get {{RESOURCE}} <name> -o json` | Get resource as JSON |

## Create and Update

| COMMAND | DESCRIPTION |
| --- | --- |
| `kubectl create {{RESOURCE}} <name>` | Create new resource |
| `kubectl apply -f <file>.yaml` | Create/update from file |
| `kubectl edit {{RESOURCE}} <name>` | Edit resource in editor |
| `kubectl patch {{RESOURCE}} <name> -p '<json>'` | Patch resource |

## Delete

| COMMAND | DESCRIPTION |
| --- | --- |
| `kubectl delete {{RESOURCE}} <name>` | Delete specific resource |
| `kubectl delete {{RESOURCE}} --all` | Delete all in namespace |
| `kubectl delete -f <file>.yaml` | Delete from file |

## Debugging

| COMMAND | DESCRIPTION |
| --- | --- |
| `kubectl logs {{RESOURCE}}/<name>` | View logs |
| `kubectl logs {{RESOURCE}}/<name> -f` | Follow logs |
| `kubectl logs {{RESOURCE}}/<name> --previous` | Previous container logs |
| `kubectl exec -it {{RESOURCE}}/<name> -- /bin/bash` | Access shell |
| `kubectl port-forward {{RESOURCE}}/<name> 8080:80` | Forward port |

## Common Patterns

```bash
# Watch for changes
kubectl get {{RESOURCE}} -w

# Filter by label
kubectl get {{RESOURCE}} -l app=myapp

# Sort by age
kubectl get {{RESOURCE}} --sort-by=.metadata.creationTimestamp
```
