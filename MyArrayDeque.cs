class MyArrayDeque<T>
{
    public T[] elements;

    public int Head { get; private set; } = 0;

    public int Tail { get; private set; } = -1;

    public MyArrayDeque() : this(16) { }

    public MyArrayDeque(T[] array)
    {
        elements = new T[array.Length];
        Array.Copy(array, elements, array.Length);
        Tail = array.Length - 1;
    }

    public MyArrayDeque(int numElements)
    {
        elements = new T[numElements];
    }

    public void Add(T item)
    {
        if (Tail >= elements.Length - 1)
        {
            T[] copyElements = new T[elements.Length * 2 + 1];
            Array.Copy(elements, copyElements, elements.Length);
            elements = copyElements;
        }
        elements[++Tail] = item;
    }

    public void AddAll(T[] array)
    {
        foreach(T item in array)
        {
            Add(item);
        }
    }

    public void Clear()
    {
        elements = new T[1];
        Tail = -1;
    }

    public bool Contains(T obj)
    {
        return Array.LastIndexOf(elements, obj, Tail) >= 0;
    }

    public bool ContainsAll(T[] array)
    {
        foreach (T item in array)
        {
            if (!Contains(item)) return false;
        }
        return true;
    }

    public bool IsEmpty()
    {
        return Tail == -1;
    }

    public void RemoveAt(int deleteIndex)
    {
        if (deleteIndex <= Tail && deleteIndex >= Head)
        {
            T[] copyElements = new T[elements.Length];
            int ind = 0;
            for (int i = 0; i <= Tail; i++)
            {
                if (i == deleteIndex) continue;
                else copyElements[ind++] = elements[i];
            }
            elements = copyElements;
            Tail--;
        }
        else throw new IndexOutOfRangeException();

    }

    public void Remove(T obj)
    {
        if (Contains(obj))
        {
            int index = Array.LastIndexOf(elements, obj, Tail);
            RemoveAt(index);
        }
    }

    public void RemoveAll(T[] array)
    {
        if (ContainsAll(array))
        {
            foreach (T item in array)
            {
                Remove(item);
            }
        }
    }

    public void RetainAll(T[] array)
    {
        if (ContainsAll(array))
        {
            T[] copyElementData = new T[array.Length];
            int ind = 0;
            foreach (T item in array)
            {
                copyElementData[ind++] = elements[Array.LastIndexOf(elements, item, Tail)];
            }
            elements = copyElementData;
            Tail = copyElementData.Length - 1;
        }
    }

    public int Size()
    {
        return Tail + 1;
    }

    public T[] ToArray()
    {
        T[] newElements = new T[Tail + 1];
        Array.Copy(elements, newElements, Tail + 1);
        return newElements;
    }

    public T[] ToArray(T[] array)
    {
        if (array == null) array = new T[Tail + 1];
        array = ToArray();
        return array;
    }

    public T? Element()
    {
        if (Tail == -1)
        {
            return default(T);
        }
        return elements[Head];
    }

    public bool Offer(T item)
    {
        Add(item);
        return true;
    }

    public T? Peek()
    {
        if (Tail == -1)
        {
            return default(T);
        }
        return elements[Head];
    }

    public T? Poll()
    {
        if (Tail == -1)
        {
            return default(T);
        }
        T res = elements[Head];
        RemoveAt(Head);

        return res;
    }

    public void AddFirst(T item)
    {
        T[] copyElements = new T[elements.Length];
        if (Tail >= elements.Length - 1)
        {
            copyElements = new T[elements.Length * 2 + 1]; 
            
        }
        Array.Copy(elements, copyElements, elements.Length);

        for (int i = Tail + 1; i > Head; i--)
        {
            copyElements[i] = elements[i - 1];
        }
        copyElements[Head] = item;
        elements = copyElements;
        Tail++;
    }

    public void AddLast(T item)
    {
        Add(item);
    }

    public T? GetFirst()
    {
        if (Tail != -1)
        {
            return elements[Head];
        }
        else throw new IndexOutOfRangeException();
    }

    public T? GetLast()
    {
        if (Tail != -1)
        {
            return elements[Tail];
        }
        else throw new IndexOutOfRangeException();
    }

    public bool OfferFirst(T item)
    {
        if (Tail < elements.Length)
        {
            AddFirst(item);
            return true;
        }
        return false;
    }

    public bool OfferLast(T item)
    {
        if (Tail < elements.Length)
        {
            AddLast(item);
            return true;
        }
        return false;
    }

    public T Pop()
    {
        if (Tail == -1)
        {
            throw new IndexOutOfRangeException();
        }
        T res = elements[Head];
        RemoveAt(Head);

        return res;
    }

    public void Push(T item)
    {
        AddFirst(item);
    }

    public T? PeekFirst()
    {
        return Peek();
    }

    public T? PeekLast()
    {
        if (Tail == -1)
        {
            return default(T);
        }
        return elements[Tail];
    }

    public T? PollFirst()
    {
        return Poll();
    }

    public T? PollLast()
    {
        if (Tail == -1)
        {
            return default(T);
        }
        T res = elements[Tail];
        RemoveAt(Tail);

        return res;
    }

    public T RemoveFirst()
    {
        if (Tail != -1)
        {
            T res = elements[Head];
            RemoveAt(Head);
            return res;
        }
        else throw new IndexOutOfRangeException();
    }

    public T RemoveLast()
    {
        if (Tail != -1)
        {
            T res = elements[Tail];
            RemoveAt(Tail);
            return res;
        }
        else throw new IndexOutOfRangeException();
    }

    public void RemoveFirstOccurrence(T item)
    {
        RemoveAt(Array.IndexOf(elements, item));
    }

    public void RemoveLastOccurrence(T item)
    {
        Remove(item);
    }

    public override string? ToString()
    {
        if (elements is not null)
            return string.Join(", ", elements);

        return null;
    }

}

class Program
{
    static void Main(string[] args)
    {
        MyArrayDeque<int> deque = new MyArrayDeque<int>(new int[]{ });
        Console.WriteLine(deque.ToString());
        Console.WriteLine(deque.Tail);

        deque.AddAll(new int[] { 1, 2, 3 });
        Console.WriteLine(deque.Tail);

        Console.WriteLine(deque.ToString());
        deque.Remove(1);
        Console.WriteLine(deque.ToString());
        deque.RemoveAll(new int[] { 3, 2 });
        Console.WriteLine(deque.ToString());

        deque.AddAll(new int[] { 7, 4, 3 });
        Console.WriteLine(deque.Element());
        Console.WriteLine(deque.Peek());

        Console.WriteLine(deque.Poll());
        Console.WriteLine(deque.ToString());

        deque.AddFirst(10);
        Console.WriteLine(deque.ToString());
        Console.WriteLine(deque.GetFirst());
        Console.WriteLine(deque.GetLast());

        deque.OfferFirst(11);
        deque.OfferLast(12);
        Console.WriteLine(deque.ToString());

        Console.WriteLine(deque.Pop());
        Console.WriteLine(deque.ToString());


    }
}