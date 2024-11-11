using System.Collections;
using System.Collections.Generic;
using System.Linq;

class Node<T>
{
    public T Data { get; set; }

    public Node<T>? Next { get; set; }

    public Node<T>? Previous { get; set; }

    public Node(T data)
    {
        Data = data;
    }
}


class MyLinkedList<T> : IEnumerable<T>
{
    Node<T>? first;
    Node<T>? last;
    int size;

    public MyLinkedList()
    {
        first = null;
        last = null;
        size = 0;
    }

    public MyLinkedList(T[] array) : this()
    {
        AddAll(array);
    }

    public void Add(T item)
    {
        Node<T> node = new Node<T>(item);
        if (first == null)
        {
            first = node;
        }
        else
        {
            node.Previous = last;
            last!.Next = node;
        }

        last = node;
        size++;
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
        first = null;
        last = null;
        size = 0;
    }

    public bool Contains(T item)
    {
        foreach(T element in this)
        {
            if (element!.Equals(item)) return true;
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

    public void Remove(T item)
    {
        if (Contains(item))
        {
            Node<T>? currentNode = first;
            Node<T>? previousNode = null;
            Node<T>? nextNode = null;

            while(currentNode != null && currentNode.Data != null)
            {
                nextNode = currentNode.Next;
                if (currentNode.Data.Equals(item))
                {
                    if (previousNode != null && nextNode != null)
                    {
                        previousNode.Next = nextNode;
                        nextNode.Previous = previousNode;
                    }

                    else if (previousNode == null && nextNode != null)
                    {
                        nextNode.Previous = null;
                        first = nextNode;
                    }

                    else if (previousNode != null && nextNode == null)
                    {
                        previousNode.Next = null;
                        last = previousNode;
                    }

                    else if (previousNode == null && nextNode == null)
                    {
                        Clear();    
                    }

                    size--;
                    break;
                }
                previousNode = currentNode;
                currentNode = currentNode.Next;
            }
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
            foreach (T item in this)
            {
                if (!array.Contains(item)) 
                { 
                    Remove(item);
                }
            }
        }
    }

    public int Size => size;

    public T[] ToArray()
    {
        T[] array = new T[size];
        int ind = 0;
        foreach(T item in this)
        {
            array[ind++] = item;
        }
        return array;
    }

    public T[] ToArray(T[] array)
    {
        if (array == null)
        {
            array = new T[size];
        }
        array = ToArray();
        return array;
    }

    public void AddAt(int index, T item)
    {
        if ((index >= 0) || (index <= size))
        {
            int currentIndex = 0;
            while ()
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        Node<T>? current = first;
        while (current != null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string ToString()
    {
        string res = "";
        foreach (T i in this)
        {
            res += i;
            res += " ";
        }
        return res;
    }
}



class Program
{
    static void Main(string[] args)
    {
        MyLinkedList<int> linkedList = new MyLinkedList<int>(new int[] {1, 2, 3, 3 });
        Console.WriteLine(linkedList);
        linkedList.Clear();
        Console.WriteLine(linkedList);
        Console.WriteLine(linkedList.IsEmpty());

        linkedList.AddAll(new int[] { 1, 2, 3, 4, 2, 5 });
        Console.WriteLine(linkedList);
        Console.WriteLine(linkedList.Contains(4));
        Console.WriteLine(linkedList.ContainsAll(new int[] { 1, 2 }));

        linkedList.RemoveAll(new int[] {1, 2, 3});
        Console.WriteLine(linkedList);

        linkedList.Add(1);
        linkedList.RetainAll(new int[] {1, 2});
        Console.WriteLine(linkedList);

        int[] array = linkedList.ToArray();
        foreach (int i in array)
        {
            Console.WriteLine(i);
        }

    }
}