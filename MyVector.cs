using ArrayList;
using System;
using System.Drawing;


namespace MyVec
{
    class MyVector<T> : MyList<T>
    {
        public class MyItr : MyListIterator<T>
        {
            MyVector<T> _vector;
            int _nextIndex;
            int _previousIndex;
            bool _wasDeleted = false;

            public MyItr(MyVector<T> vector)
            {
                _vector = vector;
                _nextIndex = 0;
                _previousIndex = -2;
            }


            public void Add(T value)
            {
                if (_nextIndex >= 0)
                {
                    _wasDeleted = false;
                    _vector.AddAt(_nextIndex, value);
                }
                else throw new IndexOutOfRangeException();
            }

            public bool HasNext()
            {
                if (_vector.Size - _nextIndex > 0)
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
                    return _vector.Get(_nextIndex++);
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
                    return _vector.Get(_previousIndex--);
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
                    _vector.RemoveAt(_nextIndex - 1);
                }
                else throw new IndexOutOfRangeException();
            }

            public void Set(T value)
            {
                if (_nextIndex > 0)
                {
                    _wasDeleted = true;
                    _vector.Set(_nextIndex - 1, value);
                }
                else throw new IndexOutOfRangeException();
            }
        }

        T[] elementData;
        int elementCount = 0;
        int capacityIncrement;

        public MyVector(int initialCapacity, int capacityIncrement)
        {
            elementData = new T[initialCapacity];
            this.capacityIncrement = capacityIncrement;
        }

        public MyVector(int initialCapacity)
        {
            elementData = new T[initialCapacity];
            capacityIncrement = 0;
        }

        public MyVector()
        {
            elementData = new T[10];
            capacityIncrement = 0;
        }

        public MyVector(T[] array)
        {
            elementData = new T[array.Length];
            AddAll(array);
        }

        public void Add(T item)
        {
            if (elementCount >= elementData.Length)
            {
                T[] copyElementData = new T[capacityIncrement > 0 ? elementData.Length + capacityIncrement : 2 * elementData.Length];
                Array.Copy(elementData, copyElementData, elementCount);
                elementData = copyElementData;
            }
            elementData[elementCount++] = item;
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
            elementData = new T[1];
            elementCount = 0;

        }

        public bool Contains(T obj)
        {
            return Array.LastIndexOf(elementData, obj, elementCount - 1, elementCount) >= 0;
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
            return elementCount == 0;
        }

        public void Remove(T obj)
        {
            if (Contains(obj))
            {
                T[] copyElementData = new T[elementData.Length];
                int delInd = Array.LastIndexOf(elementData, obj);
                int ind = 0;
                for (int i = 0; i < elementCount; i++)
                {
                    if (i == delInd) continue;
                    else copyElementData[ind++] = elementData[i];
                }
                elementData = copyElementData;
                elementCount--;
                
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
                    copyElementData[ind++] = elementData[Array.LastIndexOf(elementData, item, elementCount - 1, elementCount)];
                }
                elementData = copyElementData;
                elementCount = copyElementData.Length;
            }
        }

        public int Size => elementCount;


        public T[] ToArray()
        {
            T[] newElementData = new T[elementCount];
            Array.Copy(elementData, newElementData, elementCount);
            return newElementData;
        }

        public T[] ToArray(T[] array)
        {
            if (array == null) array = new T[elementCount];
            array = ToArray();
            return array;
        }

        public void AddAt(int index, T item)
        {
            if (index >= 0 && index < elementCount)
            {
                T[] copyElementData = new T[++elementCount];
                bool flag = false;
                for (int i = 0; i < elementCount; i++)
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
            else throw new IndexOutOfRangeException();
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
            if ((index >= 0) && (index < elementCount)) return elementData[index];
            else throw new IndexOutOfRangeException();
        }

        public int IndexOf(T item)
        {
            return Array.IndexOf(elementData, item, 0, elementCount);
        }

        public int LastIndexOf(T item)
        {
            return Array.LastIndexOf(elementData, item, elementCount - 1, elementCount);
        }

        public T RemoveIndex(int index)
        {
            if ((index >= 0) && (index < elementCount))
            {
                T item = Get(index);
                Remove(item);
                return item;
            }
            else throw new IndexOutOfRangeException();
        }

        public void Set(int index, T item)
        {
            if ((index >= 0) && (index < elementCount))
            {
                elementData[index] = item;
            }
            else throw new IndexOutOfRangeException();
        }

        public T[] SubList(int fromIndex, int toIndex)
        {
            if (fromIndex >= 0 && fromIndex < toIndex && toIndex < elementCount)
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

        public T FirstElement()
        {
            return elementData[0];
        }

        public T LastElement()
        {
            if (elementCount >= 1)
            {
                return elementData[elementCount - 1];
            }
            else throw new IndexOutOfRangeException();
        }

        public void RemoveAt(int pos)
        {
            RemoveIndex(pos);
        }

        public void RemoveRange(int begin, int end)
        {
            if (begin >= 0 && begin < end && end < elementCount)
            {
                T[] array = new T[end - begin];
                for (int i = begin; i < end; i++)
                {
                    array[i] = elementData[i];
                }

                foreach (T item in array)
                {
                    Remove(item);
                }
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

        public virtual void Print()
        {
            for (int i = 0; i < elementCount; i++)
            {
                Console.Write(elementData[i] + " ");
            }
            Console.WriteLine();
        }
    }
}
