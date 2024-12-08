using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RBTree
{
    enum Color
    {
        Red, Black
    }

    class RBTreeNode<T>  where T : IComparable<T>
    {
        public RBTreeNode<T>? Parent { get; set; }
        public RBTreeNode<T>? Left { get; set; }
        public RBTreeNode<T>? Right { get; set; }
        public Color Color { get; set; }
        public T Value { get; set; }

        public RBTreeNode(T value, Color color)
        {
            Value = value;
            Color = color;
            Parent = Left = Right = null;
        }

        public RBTreeNode(T value, Color color, RBTreeNode<T>? parent)
        {
            Value = value;
            Color = color;
            Parent = parent;
            Left = Right = null;
        }

    }


    class RBTree<T> : IEnumerable<RBTreeNode<T>> where T : IComparable<T>
    {
        public RBTreeNode<T>? Root { get; set; }
        private IComparer<T> _comparer = Comparer<T>.Default;
        public int Size { get; set; }

        public RBTree()
        {
            Root = null;
            Size = 0;
        }

        public RBTree(IComparer<T> comparer) : this()
        {
            _comparer = comparer;
        }

        public void Insert(T value)
        {
            RBTreeNode<T> newNode = new RBTreeNode<T>(value, Color.Red); 

            if (Root == null)
            {
                Root = newNode;
            }
            else
            {
                RBTreeNode<T>? parentNode = Root;
                RBTreeNode<T>? currentNode = null;
                while (parentNode != null) 
                {
                    currentNode = parentNode;
                    if (parentNode.Value.CompareTo(value) < 0)
                    {
                        parentNode = parentNode.Right;
                    }
                    else
                    {
                        parentNode = parentNode.Left;
                    }
                }

                newNode.Parent = currentNode;

                if (currentNode!.Value.CompareTo(value) < 0)
                {
                    currentNode.Right = newNode;
                }
                else
                {
                    currentNode.Left = newNode; 
                }
            }
            Size++;
            FixInsertion(newNode);
        }

        private void FixInsertion(RBTreeNode<T> node)
        {
            if (node.Equals(Root))
            {
                node.Color = Color.Black;
                return;
            }
            RBTreeNode<T> parent = node.Parent!;
            while ( parent.Color == Color.Red)
            {
                RBTreeNode<T> grandfather = parent.Parent!;
                if (parent.Value.CompareTo(grandfather.Value) < 0)
                {
                    RBTreeNode<T>? uncle = grandfather.Right;
                    if (uncle is not null && uncle.Color == Color.Red)
                    {
                        parent.Color = Color.Black;
                        uncle.Color = Color.Black;
                        grandfather.Color = Color.Red;
                        node = grandfather;
                    }
                    else
                    {
                        if (node.Value.CompareTo(parent.Value) > 0)
                        {
                            node = parent;
                            LeftRotate(node);
                        }
                        parent.Color = Color.Black;
                        grandfather.Color = Color.Red;
                        RightRotate(grandfather);
                    }
                }
                else
                {
                    RBTreeNode<T>? uncle = grandfather.Left;
                    if (uncle is not null && uncle.Color == Color.Red)
                    {
                        parent.Color = Color.Black;
                        uncle.Color = Color.Black;
                        grandfather.Color = Color.Red;
                        node = grandfather;
                    }
                    else
                    {
                        if (node.Value.CompareTo(parent.Value) < 0)
                        {
                            node = node.Parent!;
                            RightRotate(node);
                        }
                        parent.Color = Color.Black;
                        grandfather.Color = Color.Red;
                        LeftRotate(grandfather);
                    }
                }
            }
            Root!.Color = Color.Black;
        }

        private void LeftRotate(RBTreeNode<T> node)
        {
            RBTreeNode<T>? rightChild = node.Right;
            node.Right = rightChild!.Left; // 

            if (node.Right is not null)
            {
                node.Right.Parent = node;
            }

            rightChild.Parent = node.Parent;

            if (node.Parent is null)
            {
                Root = rightChild;
            }
            else if (node.Equals(node.Parent.Left))
            {
                node.Parent.Left = rightChild;
            }
            else
            {
                node.Parent.Right = rightChild;
            }

            rightChild.Left = node;
            node.Parent = rightChild;
        }

        private void RightRotate(RBTreeNode<T> node)
        {
            RBTreeNode<T>? leftChild = node.Left;
            node.Left = leftChild!.Right; // 

            if (node.Left is not null)
            {
                node.Left.Parent = node;
            }

            leftChild.Parent = node.Parent;

            if (node.Parent is null)
            {
                Root = leftChild;
            }
            else if (node.Equals(node.Parent.Left))
            {
                node.Parent.Left = leftChild;
            }
            else
            {
                node.Parent.Right = leftChild;
            }

            leftChild.Right = node;
            node.Parent = leftChild;
        }

        public bool Contains(T value)
        {
            if (Root is null) return false;
            else
            {
                var currentNode = Root;
                while (currentNode is not null)
                {
                    if (currentNode.Value.Equals(value)) return true;

                    if (value.CompareTo(currentNode.Value) > 0 && currentNode.Right is not null) currentNode = currentNode.Right;
                    else if (value.CompareTo(currentNode.Value) < 0 && currentNode.Left is not null) currentNode = currentNode.Left;
                    else return false;
                }
                return false;
            }
        }

        public void Remove(T value)
        {
            if (Contains(value))
            {
                RBTreeNode<T> node = Root!;

                while (!node.Value.Equals(value))
                {
                    if (node.Value.CompareTo(value) < 0)
                    {
                        node = node.Right!;
                    }
                    else node = node.Left!;
                }

                if (node.Left is null && node.Right is null)
                {
                    if (node.Equals(Root))
                    {
                        Root = null;
                    }
                    else
                    {
                        if (node.Parent!.Value.CompareTo(node.Value) > 0)
                        {
                            node.Parent!.Left = null;
                        }
                        else
                        {
                            node.Parent!.Right = null;
                        }
                    }
                    return;
                }
                RBTreeNode<T>? minNode = null;
                if (node.Left is not null && node.Right is null)
                {
                    node.Parent = node.Left;
                }
                else if (node.Right is not null && node.Left is null)
                {
                    node.Parent = node.Right;
                }
                else if (node.Left is not null && node.Right is not null)
                {
                    minNode = MinNode(node);
                    if (minNode.Right is not null)
                    {
                        minNode.Right.Parent = minNode.Parent;
                    }
                    if (minNode.Equals(Root))
                    {
                        Root = minNode.Right;
                    }
                    else
                    {
                        minNode.Parent!.Left = minNode.Right;
                    }
                }

                if (minNode is not null)
                {
                    if (!minNode.Equals(node))
                    {
                        node.Color = minNode.Color;
                        node.Value = minNode.Value;
                    }
                }

                if (minNode is null || minNode.Color == Color.Black)
                {
                    FixDeleting(node);
                }
            }
        }

        private void FixDeleting(RBTreeNode<T> node)
        {
            while (node != null && node.Color == Color.Black && !node.Equals(Root))
            {
                RBTreeNode<T> parent = node.Parent!;
                if (node.Value.CompareTo(parent.Value) < 0)
                {
                    RBTreeNode<T> brother = node.Parent!.Right!;
                    if (brother.Color == Color.Red)
                    {
                        brother.Color = Color.Black;
                        parent.Color = Color.Red;
                        LeftRotate(parent);
                    }
                    if (brother.Left is not null && brother.Right is not null && brother.Right.Color == Color.Black && brother.Left.Color == Color.Black)
                    {
                        brother.Color = Color.Red;
                    }
                    else
                    {
                        if (brother.Right is not null && brother.Right.Color == Color.Black)
                        {
                            if (brother.Left is not null) brother.Left.Color = Color.Black;
                            brother.Color = Color.Red;
                            RightRotate(brother);
                        }
                        brother.Color = parent.Color;
                        parent.Color = Color.Black;
                        if (brother.Right is not null) brother.Right.Color = Color.Black;
                        LeftRotate(parent);
                        node = Root!;
                    }
                }
                else
                {
                    RBTreeNode<T> brother = node.Left!.Right!;
                    if (brother.Color == Color.Red)
                    {
                        brother.Color = Color.Black;
                        parent.Color = Color.Red;
                        RightRotate(parent);
                    }
                    if (brother.Left is not null && brother.Right is not null && brother.Right.Color == Color.Black && brother.Left.Color == Color.Black)
                    {
                        brother.Color = Color.Red;
                    }
                    else
                    {
                        if (brother.Left is not null && brother.Left.Color == Color.Black)
                        {
                            if (brother.Right is not null) brother.Right.Color = Color.Black;
                            brother.Color = Color.Red;
                            LeftRotate(brother);
                        }
                        brother.Color = parent.Color;
                        parent.Color = Color.Black;
                        if (brother.Left is not null) brother.Left.Color = Color.Black;
                        RightRotate(parent);
                        node = Root!;
                    }
                }

            }
            node!.Color = Color.Black;
            Root!.Color = Color.Black;
        }

        public RBTreeNode<T> MinNode(RBTreeNode<T> node)
        {
            while (node.Left is not null)
                node = node.Left;
            return node;
        }

        public IEnumerable<RBTreeNode<T>> TraverseInOrder(RBTreeNode<T>? node)
        {
            if (node != null)
            {
                foreach (var leftValue in TraverseInOrder(node.Left))
                    yield return leftValue;

                yield return node;

                foreach (var rightValue in TraverseInOrder(node.Right))
                    yield return rightValue;
            }
        }

        public IEnumerable<RBTreeNode<T>> ReverseInOrder(RBTreeNode<T>? node)
        {
            if (node != null)
            {
                foreach (var leftValue in ReverseInOrder(node.Right))
                    yield return leftValue;

                yield return node;

                foreach (var rightValue in ReverseInOrder(node.Left))
                    yield return rightValue;
            }
        }

        public IEnumerator<RBTreeNode<T>> GetEnumerator()
        {
            return TraverseInOrder(Root).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            string result = "";
            foreach (var node in this)
            {
                result += $"Value:{node.Value} Color:{node.Color}\n";
            }
            return result;
        }
    }


}
