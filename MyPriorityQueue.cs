
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;

class Comparator<T> where T : IComparable<T>
{
    public int CompareTo(T first, T second)
    {
        return first.CompareTo(second);
    }
}


class MyPriorityQueue<T> where T : IComparable<T>
{
    private List<T> queue = new(0);

    public int Size
    {
        get
        {
            return queue.Count();
        }
    }

    private Comparator<T> comparator = new Comparator<T>();

    public MyPriorityQueue() : this(11) { }

    public MyPriorityQueue(T[] array)
    {
        queue = new(array);
        for (int i = queue.Count() / 2; i >= 0; i--)
        {
            OrderPriorityQueue(i);
        }
    }

    public MyPriorityQueue(int initialCapasity)
    {
        queue = new List<T>(initialCapasity);
    }

    public MyPriorityQueue(int initialCapacity, Comparator<T> newComparator) :this(initialCapacity)
    {
        Comparator<T> comparator = newComparator;
    }

    public MyPriorityQueue(MyPriorityQueue<T> c) : this(c.ToArray()) { }

    private void OrderPriorityQueue(int i)
    {
        int leftChild;
        int rightChild;
        int maxChild;

        for (; ; )
        {
            leftChild = 2 * i + 1;
            rightChild = 2 * i + 2;
            maxChild = i;

            if (leftChild < Size && comparator.CompareTo(queue[leftChild], queue[maxChild]) > 0)
            {
                maxChild = leftChild;
            }

            if (rightChild < Size && comparator.CompareTo(queue[rightChild], queue[maxChild]) > 0)
            {
                maxChild = rightChild;
            }

            if (maxChild == i)
            {
                break;
            }

            T tmp = queue[i];
            queue[i] = queue[maxChild];
            queue[maxChild] = tmp;
            i = maxChild;
        }
    }

    public void Add(T item)
    {
        T[] copyQueue = new T[queue.Capacity < 64 ? queue.Capacity * 2 : (int)(queue.Capacity * 1.5)];
        Array.Copy(queue.ToArray(), copyQueue, Size);
        queue = new(copyQueue);

        queue.Add(item);

        int i = Size - 1;
        int parent = (i - 1) / 2;
        while (i > 0 && comparator.CompareTo(queue[i], queue[parent]) > 0)
        {
            T tmp = queue[i];
            queue[i] = queue[parent];
            queue[parent] = tmp;

            i = parent;
            parent = (i - 1) / 2;
        }
    }

    public void AddAll(T[] array)
    {
        foreach (T item in array)
        {
            Add(item);
        }
    }

    public void Clear()
    {
        queue.Clear();
    }

    public bool Contains(T item)
    {
        return queue.Contains(item);
    }

    public bool ContainsAll(T[] array)
    {

        foreach (T item in array)
        {
            if (!queue.Contains(item)) return false;
        }
        return true;
    }

    public bool IsEmpty()
    {
        return Size == 0;
    }

    public void RemoveAt(int index)
    {
        try 
        {
            queue.RemoveAt(index);
            OrderPriorityQueue(index);
        }
        catch(Exception e) 
        {
            throw new IndexOutOfRangeException();
        }
    }

    public bool Remove(T item)
    {
        int index = queue.IndexOf(item);
        if(index >= 0)
        {
            RemoveAt(index);
            return true;
        }

        return false;
    }

    public void RemoveAll(T[] array)
    {

        queue.RemoveAll(x => array.Contains(x));
    }

    public void RetainAll(T[] array)
    {
        queue.RemoveAll(x => !array.Contains(x));
    }

    public T[] ToArray()
    {
        return (T[]) queue.ToArray();
    }

    public void ToArray(T[] array)
    {
        if (array is null)
        {
            array = new T[Size];
        }
        queue.CopyTo(array, 0);
    }

    public T Element()
    {
        if (Size == 0) throw new IndexOutOfRangeException();
        return queue[0];
    }

    public bool Offer(T item)
    {
        Add(item);
        return true;
    }

    public T? Peek()
    {
        if (Size == 0)
        {
            return default(T);
        }
        return queue[0]; 

    }

    public T? Poll()
    {
        if (Size == 0)
        {
            return default(T);
        }
        T res = queue[0];
        RemoveAt(0);

        return res;

    }

    public override string ToString()
    {
        string res = "";
        foreach(T item in queue)
        {
            res += item;
            res += " ";
        }
        
        return res;
    } 
}




class Program
{
    static public void Main(string[] args)
    {
        int[] array = new int[] { 8, 6 , 3 , 2, 1};
        MyPriorityQueue<int> queue = new MyPriorityQueue<int>(array);
        
        Console.WriteLine(queue);

        array = new int[] { 8, 6, 1 };

        queue.RetainAll(array);

        Console.WriteLine(queue.Size);

    }
}

