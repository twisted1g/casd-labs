namespace HashMap;

class Entry<K, V>
{
    public K? Key { get; set; }

    public V? Value { get; set; }

    public Entry<K, V>? Next { get; set; }

    public Entry(K? key, V? value)
    {
        Key = key;
        Value = value;
        Next = null;
    }
    public override int GetHashCode()
    {
        return Key!.GetHashCode();
    }

}

class MyHashMap<K, V> : IEnumerable<Entry<K, V>>
{
    Entry<K, V>[] table;
    int size;
    readonly double loadFactor;

    MyHashMap() : this(16) { }

    MyHashMap(int initialCapacity) : this(initialCapacity, 0.75) { }

    MyHashMap(int initialCapacity, double loadFactor)
    {
        table = new Entry<K, V>[initialCapacity];
        this.loadFactor = loadFactor;
    }

    public void Clear()
    {
        table = new Entry<K, V>[1];
        size = 0;
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

    public void Put(K key, V value)
    {
        double currentLoad = (size + 1) / table.Length;
        if (currentLoad >= loadFactor)
        {
            Entry<K, V>[] newTable = new Entry<K, V>[table.Length * 2];
            foreach (var enrty in this)
            {
                newTable[enrty.Key!.GetHashCode() % table.Length] = enrty;
            }
        }

        int currentHashCode = key!.GetHashCode();
        int bucketIndex = currentHashCode % table.Length;

        if (table[bucketIndex] is null)
        {
            table[bucketIndex] = new Entry<K, V>(key, value);
        }
        else
        {
            var pointer = table[bucketIndex];
            while (pointer is not null)
            {
                if (table[bucketIndex].Key!.Equals(key))
                {
                    table[bucketIndex].Value = value;
                    return;
                }
                pointer = pointer.Next;
            }




        }
    }

    public IEnumerator<Entry<K, V>> GetEnumerator()
    {
        foreach (var entry in table)
        {
            if (entry == null) continue;

            var step = entry;
            while (step != null)
            {
                yield return step;
                step = step.Next;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable<Entry<K, V>>)this).GetEnumerator();
    }


}
