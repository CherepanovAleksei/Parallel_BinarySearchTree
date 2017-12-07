using System;

namespace Parallel_Binary_Search_Tree
{
    interface ITree<K,V> 
        where K:IComparable<K>
        where V:struct
    {
        void Insert(K key, V value);
        void Delete(K key);
        V? Search(K key);
    }
}
