using MyIterator;
using System.Drawing;

namespace TreeMap;

class MyTreeMap<K, V> : MyNavigableMap<K, V> where K : IComparable<K>
{
    MyBinarySearchTree<K, V> tree;
    public IComparer<K> _comparer = Comparer<K>.Default;
    public int Size {  get { return tree.Size; } }

    public MyTreeMap()
    {
        tree = new MyBinarySearchTree<K, V>(_comparer);
    }

    public MyTreeMap(IComparer<K> comparer)
    {
        tree = new MyBinarySearchTree<K, V>(comparer);
        _comparer = comparer;
    }

    public void Clear()
    {
        tree = new MyBinarySearchTree<K, V>(_comparer);
    }

    public bool ContainsKey(K key)
    {
        return tree.Contains(key);
    }

    public bool ContainsValue(V value)
    {
        foreach (var node in tree)
        {
            if (node.Value is not null)
            {
                if (node.Value.Equals(value)) return true;
            }
        }
        return false;
    }

    public HashSet<(K, V)> EntrySet()
    {   
        HashSet<(K, V)> hashSet = new HashSet<(K, V)>();
        foreach (var node in tree)
        {
            hashSet.Add((node.Key, node.Value!));
        }
        return hashSet;
    }

    public MySet<K> KeySet()
    {
        HashSet<K> hashSet = new HashSet<K>();
        foreach (var node in tree)
        {
            hashSet.Add(node.Key);
        }
        return (MySet<K>)hashSet;
    }

    public V? Get(K key)
    {
        foreach (var node in tree)
        {
            if (node.Key.Equals(key)) return node.Value;
        }

        throw new ArgumentException();
    }

    public bool IsEmpty()
    {
        return Size == 0;
    }

    public void Put(K key, V value)
    {
        tree.Add(key, value);
    }

    public void Remove(K key)
    {
        tree.Remove(key);
    }

    public K? FirstKey()
    {
        K? res = default(K); 
        foreach (var node in tree)
        {
            res = node.Key;
            break;
        }
        return res;
    }

    public K? LastKey()
    {
        K? res = default(K);
        foreach (var node in tree)
        {
            res = node.Key;
        }
        return res;
    }

    public MySortedMap<K, V> HeadMap(K end)
    {
        MyTreeMap<K, V> treeMap = new MyTreeMap<K, V>();
        foreach (var node in tree)
        {
            if (end.CompareTo(node.Key) < 0) treeMap.Put(node.Key, node.Value!);
        }
        return treeMap;
    }

    public MySortedMap<K, V> SubMap(K start, K end)
    {
        MyTreeMap<K, V> treeMap = new MyTreeMap<K, V>();
        foreach (var node in tree)
        {
            if (end.CompareTo(node.Key) < 0 && start.CompareTo(node.Key) >= 0) treeMap.Put(node.Key, node.Value!);
        }
        return treeMap;
    }

    public MySortedMap<K, V> TailMap(K start)
    {
        MyTreeMap<K, V> treeMap = new MyTreeMap<K, V>();
        foreach (var node in tree)
        {
            if (start.CompareTo(node.Key) >= 0) treeMap.Put(node.Key, node.Value!);
        }
        return treeMap;
    }

    public (K?, V?) LowerEntry(K key)
    {
        foreach (var node in tree)
        {
            if (node.Key.CompareTo(key) >= 0) return (node.Key, node.Value);
        }
        return (default(K) , default(V));
    }

    public (K?, V?) FloorEntry(K key)
    {
        foreach (var node in tree)
        {
            if (node.Key.CompareTo(key) <= 0) return (node.Key, node.Value);
        }
        return (default(K), default(V));
    }

    public (K?, V?) HigherEntry(K key)
    {
        foreach (var node in tree)
        {
            if (node.Key.CompareTo(key) > 0) return (node.Key, node.Value);
        }
        return (default(K), default(V));
    }

    public (K?, V?) CeilingEntry(K key)
    {
        foreach (var node in tree)
        {
            if (node.Key.CompareTo(key) >= 0) return (node.Key, node.Value);
        }
        return (default(K), default(V));
    }

    public K? LowerKey(K key)
    {
        foreach (var node in tree)
        {
            if (node.Key.CompareTo(key) < 0) return node.Key;
        }
        return default(K);
    }

    public K? FloorKey(K key)
    {
        foreach (var node in tree)
        {
            if (node.Key.CompareTo(key) <= 0) return node.Key;
        }
        return default(K);
    }

    public K? HigherKey(K key)
    {
        foreach (var node in tree)
        {
            if (node.Key.CompareTo(key) > 0) return node.Key;
        }
        return default(K);
    }

    public K? CeilingKey(K key)
    {
        foreach (var node in tree)
        {
            if (node.Key.CompareTo(key) >= 0) return node.Key;
        }
        return default(K);
    }

    public (K?, V?) PollFirstEntry()
    {
        K? res = default(K);
        V? val = default(V);
        foreach (var node in tree)
        {
            res = node.Key;
            val = node.Value;
            break;
        }
        if (res is not null) Remove(res);
        return (res, val);
    }

    public (K?, V?) PollLastEntry()
    {
        K? res = default(K);
        V? val = default(V);
        foreach (var node in tree)
        {
            res = node.Key;
            val = node.Value;
        }
        if (res is not null) Remove(res);
        return (res, val);
    }

    public (K?, V?) FirstEntry()
    {
        K? res = default(K);
        V? val = default(V);
        foreach (var node in tree)
        {
            res = node.Key;
            val = node.Value;
            break;
        }
        return (res, val);
    }

    public (K?, V?) LastEntry()
    {
        K? res = default(K);
        V? val = default(V);
        foreach (var node in tree)
        {
            res = node.Key;
            val = node.Value;
        }
        return (res, val);
    }

    public override string ToString()
    {
        string result = "";
        foreach (var node in tree)
        {
            result += $"Key:{node.Key}  Value:{node.Value}\n";
        }
        return result;
    }
}

