using ArrayDeque;

namespace LinkedList;

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
    public class MyItr : MyListIterator<T>
    {
        MyLinkedList<T> _list;
        Node<T>? _nextNode;
        Node<T>? _currentNode;
        int _nextIndex;
        int _previousIndex;
        bool _wasDeleted = false;

        public MyItr(MyLinkedList<T> list)
        {
            _nextIndex = 0;
            _previousIndex = -2;
            _list = list;
            _nextNode = list.first;
            _currentNode = null;
        }

        public void Add(T value)
        {
            if (_nextIndex >= 0)
            {
                _list.AddAt(_nextIndex, value);
            }
            else throw new IndexOutOfRangeException();
        }

        public bool HasNext()
        {
            if (_list is not null)
            {
                if (_list.Size - _nextIndex > 0) return true;
            }
            return false;
        }

        public bool HasPrevious()
        {
            if (_list is not null)
            {
                if (_previousIndex > 0) return true;
            }
            return false;
        }

        public T Next()
        {
            if (HasNext())
            {
                _wasDeleted = false;
                var res = _nextNode!.Data;
                _currentNode = _nextNode;
                _nextNode = _nextNode.Next;
                _nextIndex++;
                _previousIndex++;
                return _nextNode!.Data;
            }
            throw new IndexOutOfRangeException();
        }

        public int NextIndex()
        {
            return _nextIndex;
        }

        public T Previous()
        {
            if (HasPrevious())
            {
                _wasDeleted = false;
                var res = _currentNode!.Previous!.Data;
                _currentNode = _currentNode!.Previous;
                _nextNode = _currentNode!.Next;
                _nextIndex--;
                _previousIndex--;
                return _currentNode!.Data;
            }
            throw new IndexOutOfRangeException();
        }

        public int PreviousIndex()
        {
            return _previousIndex;
        }

        public void Remove()
        {
            if (!_wasDeleted && _nextIndex > 0)
            {
                _wasDeleted = true;
                _list.RemoveAt(_nextIndex-1);
            }
            else throw new InvalidOperationException();
        }

        public void Set(T value)
        {
            if (_currentNode is not null)
            {
                _currentNode.Data = value;
            }
            else throw new InvalidOperationException();
        }
    }

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
        foreach (T element in this)
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

            while (currentNode != null && currentNode.Data != null)
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

    public int Size { get { return size; } }

    public T[] ToArray()
    {
        T[] array = new T[size];
        int ind = 0;
        foreach (T item in this)
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
        if ((index >= 0) && (index <= size)) // Добавление до элемента с заданным идексом и после последнего
        {
            Node<T> node = new Node<T>(item);
            if (index == 0)
            {
                if (size > 0)
                {
                    node.Next = first;
                    first!.Previous = node;
                }
                first = node;
            }
            else if (index == size)
            {
                last!.Next = node;
                node.Previous = last;
                last = node;
            }
            else if ((index > 0) && (index < size))
            {
                Node<T>? currentNode = first;
                int currentIndex = 0;
                while (currentNode != null)
                {
                    if (currentIndex == index)
                    {
                        node.Next = currentNode;
                        node.Previous = currentNode.Previous;

                        currentNode.Previous!.Next = node;
                        currentNode.Previous = node;

                    }
                    currentNode = currentNode.Next;
                    currentIndex++;
                }
            }
            size++;
        }
        else throw new IndexOutOfRangeException();
    }

    public void AddAllAt(int index, T[] array)
    {
        foreach (T item in array)
        {
            AddAt(index++, item);
        }
    }

    public T? Get(int index)
    {
        if (index >= 0 && index < size)
        {
            int ind = 0;
            foreach (T item in this)
            {
                if (ind == index)
                {
                    return item;
                }
                ind++;
            }
            return default;
        }
        else throw new IndexOutOfRangeException();
    }

    public int IndexOf(T item)
    {
        int ind = 0;
        foreach (T element in this)
        {
            if (element!.Equals(item))
            {
                return ind;
            }
            ind++;
        }
        return -1;
    }

    public int LastIndexOf(T item)
    {
        int ind = 0;
        int res = -1;
        foreach (T element in this)
        {
            if (element!.Equals(item))
            {
                res = ind;
            }
            ind++;
        }
        return res;
    }

    public T? RemoveAt(int index)
    {
        if (index >= 0 && index < size)
        {
            T? res = default;
            if (index == 0)
            {
                res = first!.Data;
                if (size > 1)
                {
                    first!.Next!.Previous = null;
                    first = first.Next;
                }
                else
                {
                    Clear();
                }
            }
            else if (index == (size - 1))
            {
                res = last!.Data;
                last!.Previous!.Next = null;
                last = last.Previous;
            }
            else
            {
                Node<T>? currentNode = first;

                int ind = 0;
                while (currentNode != null)
                {
                    if (ind == index)
                    {
                        res = currentNode!.Data;
                        currentNode.Previous!.Next = currentNode.Next;
                        currentNode.Next!.Previous = currentNode.Previous;
                    }
                    currentNode = currentNode.Next;
                    ind++;
                }
            }
            size--;
            return res;
        }
        else throw new IndexOutOfRangeException();
    }

    public void Set(int index, T item)
    {
        if (index >= 0 && index < size)
        {
            Node<T>? currentNode = first;
            int ind = 0;
            while (currentNode != null)
            {
                if (index == ind)
                {
                    currentNode.Data = item;
                }
                currentNode = currentNode.Next;
                ind++;
            }
        }
        else throw new IndexOutOfRangeException();
    }

    public T[] SubList(int fromIndex, int toIndex)
    {
        if (fromIndex < toIndex && fromIndex >= 0 && toIndex < size)
        {
            T[] array = new T[toIndex - fromIndex];
            int index = 0;
            int ind = 0;
            Node<T>? currentNode = first;

            while (currentNode != null)
            {
                if (index >= fromIndex && index < toIndex)
                {
                    array[ind++] = currentNode.Data;
                }
                currentNode = currentNode.Next;
                index++;
            }
            return array;
        }
        else throw new IndexOutOfRangeException();
    }

    public T? Element()
    {
        if (first is null)
        {
            return default(T?);
        }
        return first.Data;
    }

    public bool Offer(T item)
    {
        Add(item);
        return true;
    }

    public T? Peek()
    {
        if (first is null)
        {
            return default(T?);
        }
        return first.Data;
    }

    public T? Poll()
    {
        if (first is null)
        {
            return default(T?);
        }
        T res = first.Data;
        RemoveAt(0);
        return res;
    }

    public void AddFirst(T item)
    {
        AddAt(0, item);
    }

    public void AddLast(T item)
    {
        AddAt(size - 1, item);
    }

    public T? GetFirst()
    {
        if (first is null)
        {
            return default(T?);
        }

        return first.Data;
    }

    public T? GetLast()
    {
        if (last is null)
        {
            return default(T?);
        }

        return last.Data;
    }

    public bool OfferFirst(T item)
    {
        AddAt(0, item);
        return true;
    }

    public bool OfferLast(T item)
    {
        AddAt(size - 1, item);
        return true;
    }

    public T? Pop()
    {
        return Poll();
    }

    public void Push(T item)
    {
        AddAt(0, item);
    }

    public T? PeekFirst()
    {
        if (first == null)
        {
            return default(T?);
        }
        return first.Data;
    }

    public T? PeekLast()
    {
        if (last == null)
        {
            return default(T?);
        }
        return last.Data;
    }

    public T? PollFirst()
    {
        if (first == null)
        {
            return default(T?);
        }
        T res = first.Data;
        RemoveAt(0);
        return res;
    }

    public T? PollLast()
    {
        if (last == null)
        {
            return default(T?);
        }
        T res = last.Data;
        RemoveAt(0);
        return res;
    }

    public T? RemoveFirst()
    {
        return PollFirst();
    }

    public T? RemoveLast()
    {
        return PollLast();
    }

    public bool RemoveFirstOccurrence(T item)
    {
        if (Contains(item))
        {
            RemoveAt(IndexOf(item));
            return true;
        }
        return false;
    }

    public bool RemoveLastOccurrence(T item)
    {
        if (Contains(item))
        {
            RemoveAt(LastIndexOf(item));
            return true;
        }
        return false;
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

    public MyItr GetIterator()
    {
        return new MyItr(this);
    }

    public MyItr GetIterator(int index)
    {
        if (index >= 0 && index < Size)
        {
            var itr = new MyItr(this);
            for (int i = 0; i < index; i++)
            {
                itr.Next();
            }
            return itr;

        }
        else throw new IndexOutOfRangeException();
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
