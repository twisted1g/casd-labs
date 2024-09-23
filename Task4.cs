class MyArrayList<T>
{
    T[] elementData;
    int size;

    public MyArrayList()
    {
        elementData = new T[0];
        size = 0;
    }

    public MyArrayList(T[] array)
    {
        elementData = new T[0];
        AddAll(array); 
    }

    public MyArrayList(int capacity)
    {
        elementData = new T[capacity];
        size = 0;
    }

    public void Add(T item)
    {
        if (size >= elementData.Length)
        {
            T[] copyElementData = new T[(int)(elementData.Length * 1.5) + 1];
            Array.Copy(elementData, copyElementData, size);
            elementData = copyElementData;
        }
        elementData[size++] = item;
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
        elementData = new T[0];
        size = 0;
    }

    public bool Contains(object obj)
    {
        foreach(T item in elementData)
        {
            if (obj.Equals(item)) return true;

        }
        return false;
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
        return size == 0;
    }

    public void Remove(object obj)
    {
        if (Contains(obj))
        {
            T[] copyElementData = new T[size];
            int ind = 0;
            foreach (T item in elementData)
            {
                if (obj.Equals(item)) continue;
                else copyElementData[ind++] = item;
                
            }
            elementData = copyElementData;
            size--;
        }
    }

    public void RemoveAll(T[] array)
    {
        if (Contains(array))
        {
            foreach (T item in array) Remove(item);
        }
    }

    public void RetainAll(T[] array)
    {
        if (ContainsAll(array))
        {
            foreach (T item in elementData)
            {
                bool flag = false;
                foreach(T newItem in array)
                {
                    if (item.Equals(newItem))
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag) Remove(item);
            }
            size = array.Length;
        }
    }

    public int Size()
    {
        return size;
    }

    public T[] ToArray()
    {
        T[] array = new T[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = elementData[i];
        }
        return array;
    }

    public T[] ToArray(T[] array)
    {
        if (array == null) array = new T[size];
        array = ToArray();
        return array;
    }

    public void Add(int index, T item)
    {
        if (index >= 0 && index < size)
        {
            T[] copyElementData = new T[++size];
            bool flag = false;
            for (int i = 0; i < size; i++)
            {
                if (i == index)
                {
                    flag = true;
                    copyElementData[i] = item;
                }
                else if (flag) copyElementData[i] = elementData[i - 1];
                else copyElementData[i] = elementData[i];
            }
            elementData = copyElementData;
        }
    }

    public void Add(int index, T[] array)
    {
        foreach (T item in array)
        {
            Add(index++, item);
        }
    }

    public T Get(int index)
    {
        if ((index >= 0) && (index < size)) return elementData[index];
        else throw new IndexOutOfRangeException();
    }

    public int IndexOf(object obj)
    {
        if (Contains(obj))
        {
            for (int i = 0; i < size; i++)
            {
                if (obj.Equals(elementData[i])) return i;
            }
        }
        return -1;
    }

    public int LastIndexOf(object obj)
    {
        if (Contains(obj))
        {
            int lastIndex = 0;
            for (int i = 0; i < size; i++)
            {
                if (obj.Equals(elementData[i])) lastIndex = i;
            }
            return lastIndex;
        }
        return -1;
    }
  
    public T Remove(int index)
    {
        T item = Get(index);
        Remove(item);
        return item;
    }

    public void Set(int index, T item)
    {
        if (index >= 0 && index < size)
        {
            elementData[index] = item;
        }
    }

    public T[] SubList(int fromIndex, int toIndex)
    {   
        if (fromIndex >= 0 && fromIndex < toIndex && toIndex < size)
        {
            T[] subList = new T[toIndex-fromIndex];
            for (int i = fromIndex; i < toIndex; i++)
            {
                subList[i-fromIndex] = elementData[i];
            }
            return subList;
        }
        else throw new IndexOutOfRangeException();
    }

    public void Print()
    {
        for (int i = 0; i < size; i++)
        {
            Console.Write(elementData[i] + " ");
        }
        Console.WriteLine();
    }
}


class Program
{
    public static void Main(string[] args)
    {
        int[] arr = { 1, 2, 2, 3, 4, 5, 6};
        MyArrayList<int> myArrayList = new MyArrayList<int>(arr);
    }
}
