﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RBTree;

namespace MyTreeSet
{
    class MyTreeSet<T> : MyNavigableSet<T> where T : IComparable<T>
    {
        public class MyItr : MyIterator<T>
        {
            MyTreeSet<T> _treeSet;
            int _nextIndex;
            bool _wasDeleted = false;
            T[] _table;

            public MyItr(MyTreeSet<T> treeSet)
            {
                _treeSet = treeSet;
                _nextIndex = 0;
                _table = treeSet.ToArray();
            }

            public bool HasNext()
            {
                if (_table is not null && _treeSet.Size - _nextIndex > 0)
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
                    return _table[_nextIndex++];    
                }
                else throw new InvalidOperationException();
            }

            public void Remove()
            {
                if (_table is not null && _treeSet.Size - _nextIndex >= 0)
                {
                    _treeSet.Remove(_table[--_nextIndex]);
                    _table = _treeSet.ToArray();
                    _wasDeleted=true;
                }
                else throw new InvalidOperationException();
            }
        }

        RBTree<T> _tree = new RBTree<T>();

        IComparer<T> _comparer = Comparer<T>.Default;

        public int Size { get { return _tree.Size; } }

        public MyTreeSet()
        {
            _tree = new RBTree<T>(_comparer);
        }

        public MyTreeSet(IComparer<T> comparer)
        {
            _tree = new RBTree<T>(comparer);
            _comparer = comparer;
        }

        public void Add(T item)
        {
            if (!_tree.Contains(item))
            {
                _tree.Insert(item);
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
            foreach (var node in _tree)
            {
                node.Left = null;
                node.Right = null;
            }
            _tree = new RBTree<T>();
        }

        public bool Contains(T item)
        {
            return _tree.Contains(item);
        }

        public bool ContainsAll(T[] array)
        {
            foreach (T item in array)
            {
                if (!Contains(item)) return false;
            }
            return true;
        }

        public void Remove(T item)
        {
            _tree.Remove(item);
        }

        public void RemoveAll(T[] array)
        {
            if (ContainsAll(array))
            {
                foreach (T item in array)
                {
                    _tree.Remove(item);
                }
            }
        }

        public void RetainAll(T[] array)
        {
            if (ContainsAll(array))
            {
                foreach (var node in _tree)
                {
                    if (!array.Contains(node.Value)) Remove(node.Value);
                }
            }
        }

        public T[] ToArray()
        {
            T[] array = new T[Size];
            int i = 0;
            foreach (var node in _tree)
            {
                array[i++] = node.Value;
            }
            return array;
        }

        public T[] ToArray(T[] array)
        {
            array = new T[Size];
            int i = 0;
            foreach (var node in _tree)
            {
                array[i++] = node.Value;
            }
            return array;
        }

        public bool IsEmpty()
        {
            return Size == 0;
        }
        public T? First()
        {
            foreach (var node in _tree)
            {
                return node.Value;
            }
            return default(T);
        }

        public T? Last()
        {
            var last = default(T);
            foreach (var node in _tree)
            {
                last = node.Value;
            }
            return last;
        }

        public MySet<T> SubSet(T from, T to)
        {
            MyTreeSet<T> newTreeSet = new MyTreeSet<T>();
            foreach (var node in _tree)
            {
                if (node.Value.CompareTo(from) >= 0 && node.Value.CompareTo(to) < 0) newTreeSet.Add(node.Value);
            }
            return newTreeSet;
        }

        public MySet<T> HeadSet(T to)
        {
            MyTreeSet<T> newTreeSet = new MyTreeSet<T>();
            foreach (var node in _tree)
            {
                if (node.Value.CompareTo(to) < 0) newTreeSet.Add(node.Value);
            }
            return newTreeSet;
        }

        public MySet<T> TailSet(T from)
        {
            MyTreeSet<T> newTreeSet = new MyTreeSet<T>();
            foreach (var node in _tree)
            {
                if (node.Value.CompareTo(from) >= 0) newTreeSet.Add(node.Value);
            }
            return newTreeSet;
        }

        public T? Ceiling(T item)
        {
            foreach (var node in _tree)
            {
                if (node.Value.CompareTo(item) >= 0) return node.Value;
            }
            return default(T);
        }

        public T? Floor(T item)
        {
            T? res = default(T);
            foreach (var node in _tree)
            {
                if (node.Value.CompareTo(item) <= 0)  res = node.Value;
            }
            return res;
        }

        public T? Higher(T item)
        {
            foreach (var node in _tree)
            {
                if (node.Value.CompareTo(item) > 0) return node.Value;
            }
            return default(T);
        }

        public T? Lower(T item)
        {
            T? res = default(T);
            foreach (var node in _tree)
            {
                if (node.Value.CompareTo(item) < 0) res = node.Value;
            }
            return res;
        }

        public MyNavigableSet<T> HeadSet(T upperBound, bool include)
        {
            MyTreeSet<T> newTreeSet = new MyTreeSet<T>();
            foreach (var node in _tree)
            {
                if (include)
                {
                    if (node.Value.CompareTo(upperBound) <= 0) newTreeSet.Add(node.Value);
                }
                else if (node.Value.CompareTo(upperBound) < 0) newTreeSet.Add(node.Value);
            }
            return newTreeSet;
        }

        public MyNavigableSet<T> SubSet(T upperBound, T lowerBound, bool lowInclude, bool highInclude)
        {
            MyTreeSet<T> newTreeSet = new MyTreeSet<T>();
            foreach (var node in _tree)
            {
                if (lowInclude)
                {
                    if (highInclude)
                    {
                        if (node.Value.CompareTo(upperBound) <= 0 && node.Value.CompareTo(lowerBound) >= 0) newTreeSet.Add(node.Value);
                    }
                    else
                    {
                        if (node.Value.CompareTo(upperBound) <= 0 && node.Value.CompareTo(lowerBound) > 0) newTreeSet.Add(node.Value);
                    }
                }
                else
                {
                    if (highInclude)
                    {
                        if (node.Value.CompareTo(upperBound) <= 0 && node.Value.CompareTo(lowerBound) >= 0) newTreeSet.Add(node.Value);
                    }
                    else
                    {
                        if (node.Value.CompareTo(upperBound) <= 0 && node.Value.CompareTo(lowerBound) > 0) newTreeSet.Add(node.Value);
                    }
                }
            }
            return newTreeSet;
        }

        public MyNavigableSet<T> TailSet(T lowerBound, bool include)
        {
            MyTreeSet<T> newTreeSet = new MyTreeSet<T>();
            foreach (var node in _tree)
            {
                if (include)
                {
                    if (node.Value.CompareTo(lowerBound) >= 0) newTreeSet.Add(node.Value);
                }
                else if (node.Value.CompareTo(lowerBound) < 0) newTreeSet.Add(node.Value);
            }
            return newTreeSet;
        }

        public T? PollLast()
        {
            T? elem = Last();
            if (elem is not null) Remove(elem);
            return elem;

        }

        public T? PollFirst()
        {
            T? elem = First();
            if (elem is not null) Remove(elem);
            return elem;

        }

        public IEnumerator<RBTreeNode<T>> DescendingIterator()
        {
            return _tree.ReverseInOrder(_tree.Root).GetEnumerator();
        }

        public MyTreeSet<T> DescendingSet()
        {
            MyTreeSet<T> newTreeSet = new MyTreeSet<T>();
            var it = DescendingIterator();
            while (it.MoveNext()) newTreeSet.Add(it.Current.Value);
            return newTreeSet;
        }

        public MyItr GetIterator()
        {
            return new MyItr(this);
        }

        public override string ToString()
        {
            string res = "";

            foreach (var node in _tree)
            {
                res += node.Value;
                res += " ";
            }

            return res;
        }
    }
}