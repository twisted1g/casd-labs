namespace ArrayList
{
    class MyArrayList<T> : MyList<T>
    {
        public class MyItr : MyListIterator<T>
        {
            MyArrayList<T> _arrayList;
            int _nextIndex;
            int _previousIndex;
            bool _wasDeleted = false;

            public MyItr(MyArrayList<T> arrayList)
            {
                _arrayList = arrayList;
                _nextIndex = 0;
                _previousIndex = -2;
            }


            public void Add(T value)
            {
                if (_nextIndex >= 0)
                {
                    _wasDeleted = false;
                    _arrayList.AddAt(_nextIndex, value);
                }
                else throw new IndexOutOfRangeException();
            }

            public bool HasNext()
            {
                if (_arrayList.Size - _nextIndex > 0)
                {
                    return true;
                }
                return false;
            }

            public bool HasPrevious()
            {
                if (_previousIndex > 0)
                {
                    return true;
                }
                return false;
            }

            public T Next()
            {
                if (HasNext())
                {
                    _wasDeleted = false;
                    _previousIndex++;
                    return _arrayList.Get(_nextIndex++);
                }
                else throw new InvalidOperationException();
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
                    _nextIndex--;
                    return _arrayList.Get(_previousIndex--);
                }
                else throw new InvalidOperationException();
            }

            public int PreviousIndex()
            {
                return _previousIndex;
            }

            public void Remove()
            {
                if (_nextIndex > 0)
                {
                    _wasDeleted = true;
                    _arrayList.RemoveAt(_nextIndex-1);
                }
                else throw new IndexOutOfRangeException();
            }

            public void Set(T value)
            {
                if (_nextIndex > 0)
                {
                    _wasDeleted = true;
                    _arrayList.Set(_nextIndex - 1, value);
                }
                else throw new IndexOutOfRangeException();
            }
        }


        T[] elementData;
        private int size;

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

        public bool Contains(T item)
        {
           
            foreach (T elem in elementData)
            {
                if (item!.Equals(elem)) return true;
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
            if (Contains(item!))
            {
                T[] copyElementData = new T[--size];
                int ind = 0;
                foreach (T elem in elementData)
                {
                    if (item.Equals(elem)) continue;
                    else copyElementData[ind++] = item;

                }
                elementData = copyElementData;
            }
        }

        public void RemoveAll(T[] array)
        {
            if (ContainsAll(array))
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
                    foreach (T newItem in array)
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

        public int Size => size;

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
            //for (int i = 0; i  < size; i++) array[i] = elementData[i];
            array = ToArray();
            return array;
        }

        public void AddAt(int index, T item)
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

        public void AddAt(int index, T[] array)
        {
            foreach (T item in array)
            {
                AddAt(index++, item);
            }
        }

        public T Get(int index)
        {
            if ((index >= 0) && (index < size)) return elementData[index];
            else throw new IndexOutOfRangeException();
        }

        public int IndexOf(T item)
        {
            if (Contains(item))
            {
                for (int i = 0; i < size; i++)
                {
                    if (item.Equals(elementData[i])) return i;
                }
            }
            return -1;
        }

        public int LastIndexOf(T item)
        {
            if (Contains(item))
            {
                int lastIndex = 0;
                for (int i = 0; i < size; i++)
                {
                    if (item.Equals(elementData[i])) lastIndex = i;
                }
                return lastIndex;
            }
            return -1;
        }

        public void RemoveAt(int index)
        {
            T item = Get(index);
            Remove(item);;
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
                T[] subList = new T[toIndex - fromIndex];
                for (int i = fromIndex; i < toIndex; i++)
                {
                    subList[i - fromIndex] = elementData[i];
                }
                return subList;
            }
            else throw new IndexOutOfRangeException();
        }

        public MyListIterator<T> GetIterator()
        {
            return new MyItr(this);
        }

        public MyListIterator<T> GetIterator(int index)
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

        public void Print()
        {
            for (int i = 0; i < size; i++)
            {
                Console.Write(elementData[i] + " ");
            }
            Console.WriteLine();
        }
    }

}