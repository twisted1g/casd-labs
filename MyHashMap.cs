
namespace HashMap;

class Entry<K, V>
{
    public K Key { get; set; }

    public V Value { get; set; }

    public Entry(K key, V value)
    {
        Key = key;
        Value = value;
    }
}

class MyHashMap<K, V> : MyMap<K, V>, IEnumerable<Entry<K, V>>
{
    public class MyItr : MyIterator<(K, V)>
    {
        private readonly MyHashMap<K, V> _map;
        private MyLinkedList<Entry<K, V>>? _list;
        private int _nextBucketIndex;
        private int _nextListIndex;
        private bool _wasDeleted;

        public MyItr(MyHashMap<K, V> map)
        {
            _map = map;
            _list = null;
            _nextBucketIndex = 0;
            _nextListIndex = 0;
            _wasDeleted = false;
        }

        public bool HasNext()
        {
            if (_list != null && _nextListIndex < _list.Size)
                return true;

            return FindNextNonEmptyBucket() >= 0;
        }

        public (K, V) Next()
        {
            if (!HasNext())
                throw new IndexOutOfRangeException();

            _wasDeleted = false;

            if (_list != null && _nextListIndex < _list.Size)
            {
                var item = _list.Get(_nextListIndex++);
                return (item!.Key, item.Value);
            }

            int nextBucket = FindNextNonEmptyBucket();
            if (nextBucket == -1)
                throw new IndexOutOfRangeException();

            _nextBucketIndex = nextBucket + 1;
            _list = _map[nextBucket];
            _nextListIndex = 1;

            var firstItem = _list.First();
            if (firstItem == null)
                throw new InvalidOperationException();

            return (firstItem.Key, firstItem.Value);
        }

        public void Remove()
        {
            if (_wasDeleted || _list == null)
                throw new InvalidOperationException();

            _list.RemoveAt(--_nextListIndex);
            _wasDeleted = true;
        }

        private int FindNextNonEmptyBucket()
        {
            for (int i = _nextBucketIndex; i < _map.Length; i++)
            {
                if (_map[i] != null && _map[i].Size > 0)
                    return i; 
            }
            return -1;
        }

    }


    public MyLinkedList<Entry<K, V>>[] table;
    public int Size
    {
        get
        {
            int size = 0;
            foreach (var entry in this)
            {
                size++;
            }
            return size;
        }
    }

    private readonly double _loadFactor;

    public MyHashMap() : this(16) { }

    public MyHashMap(int initialCapacity) : this(initialCapacity, 0.75) { }

    public MyHashMap(int initialCapacity, double loadFactor)
    {
        table = new MyLinkedList<Entry<K, V>>[initialCapacity];
        this._loadFactor = loadFactor;
    }

    public MyLinkedList<Entry<K, V>> this[int index]
    {
        get => table[index];
    }

    public void Clear()
    {
        table = new MyLinkedList<Entry<K, V>>[1];
    }

    public bool ContainsKey(K key)
    {
        foreach (var entry in this)
        {
            if (entry.Key!.Equals(key))
            {
                return true;
            }
        }
        return false;
    }

    public bool ContainsValue(V value)
    {
        foreach (var entry in this)
        {
            if (entry.Value!.Equals(value))
            {
                return true;
            }
        }
        return false;
    }

    public HashSet<Entry<K, V>> EntrySet()
    {
        HashSet<Entry<K, V>> hashSet = new HashSet<Entry<K, V>>();
        foreach(var entry in this)
        {
            hashSet.Add(entry);
        }
        return hashSet;
    }

    public V? Get(K key)
    {
        foreach (var entry in this)
        {
            if (entry.Key!.Equals(key))
            {
                return entry.Value;
            }
        }
        return default(V?);
    }

    public bool IsEmpty()
    {
        return Size == 0;
    }

    public MySet<K> KeySet()
    {
        HashSet<K> hashSet = new HashSet<K>();
        foreach (var entry in this)
        {
            hashSet.Add(entry.Key);
        }
        return (MySet<K>)hashSet;
    }

    public void Put(K key, V value)
    {
        double currentLoad = Size / table.Length;
        if (currentLoad >= _loadFactor)
        {
            MyLinkedList<Entry<K, V>>[] newTable = new MyLinkedList<Entry<K, V>>[table.Length * 2];
            foreach (var list in table)
            {
                if (list != null)
                {
                    Entry<K, V> entry = list.GetFirst()!;
                    newTable[Math.Abs(entry.Key!.GetHashCode()) % table.Length] = list;
                }
            }
        }

        int currentHashCode = Math.Abs(key!.GetHashCode());
        int bucketIndex = currentHashCode % table.Length;

        if (table[bucketIndex] is null)
        {
            table[bucketIndex] = new MyLinkedList<Entry<K, V>>();
            table[bucketIndex].Add(new Entry<K, V>(key, value));
        }
        else
        {
            foreach (var entry in table[bucketIndex])
            {
                if (entry.Key!.Equals(key))
                {
                    entry.Value = value;
                    return;
                }
            }
            table[bucketIndex].Add(new Entry<K, V>(key, value));
        }
    }

    public void Remove(K key)
    {
        if (ContainsKey(key))
        {
            MyLinkedList<Entry<K, V>>[] newTable = new MyLinkedList<Entry<K, V>>[table.Length];
            foreach (var list in table)
            {
                if (list != null)
                {
                    foreach (var entry in list)
                    {
                        if (entry.Key!.Equals(key))
                        {
                            list.Remove(entry);
                            return;
                        }
                    }
                }
            }
        }
    }

    public IEnumerator<Entry<K, V>> GetEnumerator()
    {
        foreach (var list in table)
        {
            if (list != null)
            {
                foreach (var entry in list)
                {
                    yield return entry; 
                }
            }
        }
    }

    public int Length { get { return table.Length; } }

    public MyItr GetIterator()
    {
        return new MyItr(this);
    }

    IEnumerator<Entry<K, V>> IEnumerable<Entry<K, V>>.GetEnumerator()
    {
        return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string ToString()
    {
        string result = "";
        foreach (var entry in this)
        {
            result += $"{entry.Key}: {entry.Value}\n";
        }
        return result;
    }
}
