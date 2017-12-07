using System;

namespace Parallel_Binary_Search_Tree
{
    class Tree<K, V> : ITree<K, V>
        where K : IComparable<K>
        where V : struct
    {
        public Node<K, V> root;

        public void Print(Node<K, V> node, int height = 0)
        {
            if (node == null) return;

            Print(node.Right, height + 1);

            for (int i = 0; i < height; i++)
            {
                Console.Write("      |");
            }

            Console.WriteLine("{0}({1})", node.Key, node.Value);

            Print(node.Left, height + 1);
        }

        public void Insert(K key, V value)
        {
            Node<K, V> parent = null;
            object locker1 = new object();
            object locker2 = new object();

            var current = root;

            while (current != null)
            {
                if (parent == null)
                {
                    lock (locker1)
                    {
                        parent = current;
                        if (key.CompareTo(current.Key) < 0)
                        {
                            current = current.Left;
                        }
                        else if (key.CompareTo(current.Key) > 0)
                        {
                            current = current.Right;
                        }
                        else if (key.CompareTo(current.Key) == 0)
                        {
                            current.Value = value;
                            return;
                        }
                    }
                }
                else
                {
                    lock (locker2)
                    {
                        lock (locker1)
                        {
                            parent = current;
                            if (key.CompareTo(current.Key) < 0)
                            {
                                current = current.Left;
                            }
                            else if (key.CompareTo(current.Key) > 0)
                            {
                                current = current.Right;
                            }
                            else if (key.CompareTo(current.Key) == 0)
                            {
                                current.Value = value;
                                return;
                            }
                        }
                    }
                }
            }

            if (parent == null)
            {
                root = new Node<K, V>(key, value, null);
                return;
            }

            lock (locker2)
            {
                if (key.CompareTo(parent.Key) < 0)
                {
                    parent.Left = new Node<K, V>(key, value, parent);
                }
                else
                {
                    parent.Right = new Node<K, V>(key, value, parent);
                }
            }
        }

        public V? Search(K key) => SearchNode(key)?.Value;
        private Node<K, V> SearchNode(K key)
        {
            var current = root;
            object locker1 = new object();
            object locker2 = new object();

            while (current != null)
            {
                if (current.Parent == null)
                {
                    lock (locker1)
                    {
                        if (key.CompareTo(current.Key) == 0)
                        {
                            return current;
                        }

                        if (key.CompareTo(current.Key) < 0)
                        {
                            current = current.Left;
                        }
                        else
                        {
                            current = current.Right;
                        }
                    }
                }
                else
                {
                    lock (locker2)
                    {
                        lock (locker1)
                        {
                            if (key.CompareTo(current.Key) == 0)
                            {
                                return current;
                            }

                            if (key.CompareTo(current.Key) < 0)
                            {
                                current = current.Left;
                            }
                            else
                            {
                                current = current.Right;
                            }
                        }
                    }
                }
            }
            return null;
        }

        public void Delete(K key)
        {
            var delNode = SearchNode(key);
            object locker1 = new object();
            object locker2 = new object();

            if (delNode == null) return;

            if (delNode.Parent == null && delNode.Left == null && delNode.Right == null)
            {
                lock (locker1)
                {
                    root = null;
                }
            }
            else if (delNode.Parent != null && delNode.Left == null && delNode.Right == null)
            {
                lock (locker2)
                {
                    lock (locker1)
                    {
                        if (delNode == delNode.Parent.Left)
                        {
                            delNode.Parent.Left = null;
                        }

                        if (delNode == delNode.Parent.Right)
                        {
                            delNode.Parent.Right = null;
                        }
                    }
                }
            }
            else if (delNode.Parent == null && delNode.Left == null && delNode.Right != null)
            {
                lock (locker1)
                {
                    delNode.Right.Parent = null;
                    root = delNode.Right;
                }
            }
            else if (delNode.Parent == null && delNode.Right == null && delNode.Left != null)
            {
                lock (locker1)
                {
                    delNode.Left.Parent = null;
                    root = delNode.Left;
                }
            }
            else if (delNode.Parent != null && (delNode.Right == null || delNode.Left == null))
            {
                lock (locker2)
                {
                    lock (locker1)
                    {
                        if (delNode.Right != null)
                        {
                            if (delNode.Parent.Left == delNode)
                            {
                                delNode.Right.Parent = delNode.Parent;
                                delNode.Parent.Left = delNode.Right;
                            }
                            else if (delNode.Parent.Right == delNode)
                            {
                                delNode.Right.Parent = delNode.Parent;
                                delNode.Parent.Right = delNode.Right;
                            }
                        }
                        else if (delNode.Left != null)
                        {
                            if (delNode.Parent.Left == delNode)
                            {
                                delNode.Left.Parent = delNode.Parent;
                                delNode.Parent.Left = delNode.Left;
                            }
                            else if (delNode.Parent.Right == delNode)
                            {
                                delNode.Left.Parent = delNode.Parent;
                                delNode.Parent.Right = delNode.Left;
                            }
                        }
                    }
                }
            }
            else
            {
                if (delNode.Parent != null)
                {
                    lock (locker2)
                    {
                        lock (locker1)
                        {
                            var y = Min(delNode.Right);
                            delNode.Key = y.Key;

                            if (y.Parent.Left == y)
                            {
                                y.Parent.Left = y.Right;

                                if (y.Right != null)
                                {
                                    y.Right.Parent = y.Parent;
                                }
                            }
                            else
                            {
                                y.Parent.Right = y.Right;

                                if (y.Right != null)
                                {
                                    y.Right.Parent = y.Parent;
                                }
                            }
                        }
                    }
                }
                else if (delNode.Parent == null)
                {
                    lock (locker1)
                    {
                        var y = Min(delNode.Right);
                        delNode.Key = y.Key;

                        if (y.Parent.Left == y)
                        {
                            y.Parent.Left = y.Right;

                            if (y.Right != null)
                            {
                                y.Right.Parent = y.Parent;
                            }
                        }
                        else
                        {
                            y.Parent.Right = y.Right;

                            if (y.Right != null)
                            {
                                y.Right.Parent = y.Parent;
                            }
                        }
                    }
                }
            }
        }

        private Node<K, V> Min(Node<K, V> rootNode)
        {
            if (rootNode.Left == null) return rootNode;
            return Min(rootNode.Left);
        }
    }
}
