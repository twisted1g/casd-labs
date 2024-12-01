using System.Drawing;

class MyHashSet<T> where T : IComparable<T>
{
    private MyHashMap<T, object?> _map;

    public MyHashSet()
    {
        _map = new MyHashMap<T, object?>();
    }

    public MyHashSet(int initialCapacity, double loadFactor)
    {
        _map = new MyHashMap<T, object?>(initialCapacity, loadFactor);
    }

    public MyHashSet(int initialCapacity) : this(initialCapacity, 0.75) { }

    public MyHashSet(T[] array) : this()
    {
        AddAll(array);
    }

    public void Add(T item)
    {
        _map.Put(item, null);
    }

    public void AddAll(T[] array)
    {
        foreach (T item in array)
        {
            _map.Put(item, null);

        }
    }

    public void Clear()
    {
        _map.Clear();
    }

    public bool Contains(T item)
    {
        return _map.ContainsKey(item);
    }

    public bool ContainsAll(T[] array)
    {
        foreach(T item in array)
        {
            if (!Contains(item)) return false;
        }
        return true;
    }

    public bool IsEmpty()
    {
        return _map.IsEmpty();
    }

    public void Remove(T item) 
    {
        _map.Remove(item);
    }

    public void RemoveAll(T[] array)
    {
        if (ContainsAll(array))
        {
            foreach(T item in array) Remove(item);
        }
    }

    public int Size {  get { return _map.Size; } }

    public T[] ToArray()
    {
        T[] array = new T[Size];
        int ind = 0;
        foreach (var item in _map)
        {
            array[ind++] = item.Key;
        }
        return array;
    }

    public T? First()
    {
        T? min = default(T);
        foreach (var item in _map)
        {
            if (item.Key.CompareTo(min) < 0) min = item.Key;
        }
        return min;
    }

    public T? Last()
    {
        T? min = default(T);
        foreach (var item in _map)
        {
            if (item.Key.CompareTo(min) > 0) min = item.Key;
        }
        return min;
    }

    public MyHashSet<T> SubSet(T from, T to)
    {
        MyHashSet<T> newHashSet = new MyHashSet<T>();
        foreach(var item in _map)
        {
            if (item.Key.CompareTo(from) >= 0 && item.Key.CompareTo(to) <= 0) newHashSet.Add(item.Key);
        }
        return newHashSet;
    }

    public MyHashSet<T> HeadSet( T to)
    {
        MyHashSet<T> newHashSet = new MyHashSet<T>();
        foreach (var item in _map)
        {
            if (item.Key.CompareTo(to) <= 0) newHashSet.Add(item.Key);
        }
        return newHashSet;
    }

    public MyHashSet<T> TailSet(T from)
    {
        MyHashSet<T> newHashSet = new MyHashSet<T>();
        foreach (var item in _map)
        {
            if (item.Key.CompareTo(from) >= 0) newHashSet.Add(item.Key);
        }
        return newHashSet;
    }

}