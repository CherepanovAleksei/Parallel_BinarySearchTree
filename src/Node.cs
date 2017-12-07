using System;
using System.Collections.Generic;

namespace Parallel_Binary_Search_Tree
{
    public class Node<K, V> : IEquatable<Node<K, V>>
        where K : IComparable<K>
        where V : struct
    {
        public K Key;
        public V Value;
        public Node<K, V> Parent;
        public Node<K, V> Left;
        public Node<K, V> Right;

        //конструктор
        public Node(K key, V value, Node<K, V> parent, Node<K, V> left=null, Node<K, V> right=null)
        {
            Key = key;
            Value = value;
            Parent = parent;
            Left = left;
            Right = right;
        }

        public bool Equals(Node<K, V> other)
        {
            return other != null &&
                   EqualityComparer<K>.Default.Equals(Key, other.Key) &&
                   EqualityComparer<V>.Default.Equals(Value, other.Value) &&
                   EqualityComparer<Node<K, V>>.Default.Equals(Parent, other.Parent) &&
                   EqualityComparer<Node<K, V>>.Default.Equals(Left, other.Left) &&
                   EqualityComparer<Node<K, V>>.Default.Equals(Right, other.Right);
        }

        public override int GetHashCode()
        {
            var hashCode = 1922056232;
            hashCode = hashCode * -1521134295 + EqualityComparer<K>.Default.GetHashCode(Key);
            hashCode = hashCode * -1521134295 + EqualityComparer<V>.Default.GetHashCode(Value);
            hashCode = hashCode * -1521134295 + EqualityComparer<Node<K, V>>.Default.GetHashCode(Parent);
            hashCode = hashCode * -1521134295 + EqualityComparer<Node<K, V>>.Default.GetHashCode(Left);
            hashCode = hashCode * -1521134295 + EqualityComparer<Node<K, V>>.Default.GetHashCode(Right);
            return hashCode;
        }
    }
}
