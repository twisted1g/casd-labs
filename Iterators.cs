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

