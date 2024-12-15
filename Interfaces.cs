using System.Data.SqlTypes;

namespace MyIterator;
public interface MyIterator<T>
{
    public bool HasNext();
    public T Next();
    public void Remove();
}

public interface MyListIterator<T>
{
    public bool HasNext();
    public T Next();
    public bool HasPrevious();
    public T Previous();
    public int NextIndex();
    public int PreviousIndex();
    public void Remove();
    public void Set(T value);
    public void Add(T value);
}

public interface MyCollection<T>
{
    public void Add(T item);
    public void AddAll(T[] array);
    public void Clear();
    public bool Contains(T item);
    public bool ContainsAll(T[] array);
    public bool IsEmpty();
    public void Remove(T item);
    public void RemoveAll(T[] array);
    public void RetainAll(T[] array);
    public int Size => 0;
    public T[] ToArray();
    public T[] ToArray(T[] array);
    
}

public interface MyList<T> : MyCollection<T>
{
    public void AddAt(int index, T item);
    public void AddAt(int index, T[] array);
    public T Get(int index);
    public int IndexOf(T item);
    public int LastIndexOf(T item);
    public MyListIterator<T> GetIterator();
    public MyListIterator<T> GetIterator(int index);
    public void RemoveAt(int index);
    public void Set(int index, T item); 
    public T[] SubList(int fromIndex, int toIndex);
}

public interface MyQueue<T> : MyCollection<T>
{
    public T Element();
    public bool Offer(T item);
    public T? Peek();
    public T? Poll();
    public MyIterator<T> GetIterator();
}

public interface MyDeque<T> : MyCollection<T>
{
    public void AddFirst(T item);
    public void AddLast(T item);
    public T? GetFirst();
    public T? GetLast();
    public bool OfferFirst(T item);
    public bool OfferLast(T item);
    public T Pop();
    public void Push(T item);
    public T? PeekFirst();
    public T? PeekLast();
    public T? PollFirst();
    public T? PollLast();
    public T RemoveLast();
    public T RemoveFirst();
    public void RemoveFirstOccurrence(T item);
    public void RemoveLastOccurrence(T item);
}

public interface MySet<T> : MyCollection<T>
{
    public T? First();
    public T? Last();
    public MySet<T> SubSet(T from, T to);
    public MySet<T> HeadSet(T to);
    public MySet<T> TailSet(T from);
}

public interface MySortedSet<T> : MySet<T>
{

}

public interface MyNavigableSet<T> : MySortedSet<T>
{
    public T? Lower(T item);
    public T? Floor(T item);
    public T? Higher(T item);
    public T? Ceiling(T item);
    public T? PollLast();
    public T? PollFirst();

}

public interface MyMap<K, V>
{
    public void Clear();
    public bool ContainsKey(K key);
    public bool ContainsValue(V value);
    public V? Get(K key);
    public bool IsEmpty();
    public MySet<K> KeySet();
    public void Put(K key, V value);
    public void Remove(K key);

}

public interface MySortedMap<K, V> : MyMap<K, V>
{
    public K? FirstKey();
    public K? LastKey();
    public MySortedMap<K, V> HeadMap(K end);
    public MySortedMap<K, V> SubMap(K start, K end);
    public MySortedMap<K, V> TailMap(K start);
}

public interface MyNavigableMap<K, V> : MySortedMap<K, V>
{
    public (K?, V?) LowerEntry(K key);
    public (K?, V?) FloorEntry(K key);
    public (K?, V?) HigherEntry(K key);
    public (K?, V?) CeilingEntry(K key);
    public K? LowerKey(K key);
    public K? FloorKey(K key);
    public K? HigherKey(K key);
    public K? CeilingKey(K key);
    public (K?, V?) FirstEntry();
    public (K?, V?) LastEntry();
}