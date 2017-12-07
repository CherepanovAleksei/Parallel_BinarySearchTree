# Parallel_BinarySearchTree
## Parallel BinarySearchTree with 'fine' synchronization (lock on element and in some times on parents)
## 1.000.000 elements 
|   | Simply BST  | Parallel BST |
| ------------- | ------------- | ------------- |
| Insert  | 00:00:03.8328031  | 00:00:02.7851400  |
| Delete  | 00:00:01.5698063  | 00:00:00.4859841  |
| Search  | 00:00:02.6593189  | 00:00:00.8780709  |

## 10.000.000 elements 
|   | Simply BST  | Parallel BST |
| ------------- | ------------- | ------------- |
| Insert  | 00:00:52.5070984  | 00:00:33.1302594  |
| Delete  | 00:00:37.7719438  | 00:00:14.9705325  |
| Search  | 00:00:33.0557026  | 00:00:13.1717662  |

