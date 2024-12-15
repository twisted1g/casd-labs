using System.Collections.Generic;
using System.Drawing;

namespace HashSet;

class MyHashSet<T> : MySet<T> where T : IComparable<T>
{
    public class MyItr : MyIterator<(T, object?)>
    {
        MyHashSet<T> _hashSet;
        MyHashMap<T, object?>.MyItr itr;
        public MyItr(MyHashSet<T> hashSet)
        {
            _hashSet = hashSet;
            itr = _hashSet._map.GetIterator();
        }

        public bool HasNext()
        {
            return itr.HasNext();
        }

        public (T, object?) Next()
        { 
            return itr.Next();
        }

        public void Remove()
        {
             itr.Remove();
        }
    }

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

    public void RetainAll(T[] array)
    {
        foreach (var item in _map)
        {
            if (!array.Contains(item.Key)) _map.Remove(item.Key);           
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

    public T[] ToArray(T[] array)
    {
        array = new T[Size];
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

    public MySet<T> SubSet(T from, T to)
    {
        MyHashSet<T> newHashSet = new MyHashSet<T>();
        foreach(var item in _map)
        {
            if (item.Key.CompareTo(from) >= 0 && item.Key.CompareTo(to) <= 0) newHashSet.Add(item.Key);
        }
        return newHashSet;
    }

    public MySet<T> HeadSet( T to)
    {
        MyHashSet<T> newHashSet = new MyHashSet<T>();
        foreach (var item in _map)
        {
            if (item.Key.CompareTo(to) <= 0) newHashSet.Add(item.Key);
        }
        return newHashSet;
    }

    public MySet<T> TailSet(T from)
    {
        MyHashSet<T> newHashSet = new MyHashSet<T>();
        foreach (var item in _map)
        {
            if (item.Key.CompareTo(from) >= 0) newHashSet.Add(item.Key);
        }
        return newHashSet;
    }

    public MyItr GetIterator()
    {
        return new MyItr(this);
    }
}