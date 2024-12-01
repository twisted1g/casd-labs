
namespace BinarySearchTree;
class TreeNode<K, V> where K : IComparable<K>
{
    public K Key { get; set; }
    public V? Value { get; set; }
    public TreeNode<K, V>? Left { get; set; }
    public TreeNode<K, V>? Right { get; set; }

    public TreeNode(K key)
    {
        Key = key;
        Value = default(V);
        Left = Right = null;
    }

    public TreeNode(K key, V? value)
    {
        Key = key;
        Value = value;
        Left = Right = null;
    }
}

class MyBinarySearchTree<K, V> : IEnumerable<TreeNode<K, V>> where K : IComparable<K> 
{
    public TreeNode<K, V>? Root { get; private set; }
    public IComparer<K> _comparer = Comparer<K>.Default;
    public int Size { get; set; }

    public MyBinarySearchTree()
    {
        Root = null;
        Size = 0;
    }

    public MyBinarySearchTree(IComparer<K> comparer) : this()
    {
        _comparer = comparer;
    }

    public void Add(K key, V? value)
    {
        if (Root is null)
        {
            Root = new TreeNode<K, V>(key, value);
            Size++;
        }
        else
        {
            var currentNode = Root;
            bool added = false;

            while (!added)
            {
                if (currentNode.Key.Equals(key))
                {
                    currentNode.Value = value;
                    added = true;
                }
                else if (key.CompareTo(currentNode.Key) > 0)
                {
                    if (currentNode.Right is null)
                    {
                        currentNode.Right = new TreeNode<K, V>(key, value);
                        Size++;
                        added = true;
                    }
                    else currentNode = currentNode.Right;
                }
                else
                {
                    if (currentNode.Left is null)
                    {
                        currentNode.Left = new TreeNode<K, V>(key, value);
                        Size++;
                        added = true;
                    }
                    else currentNode = currentNode.Left;
                }
            }
        }
    }

    public bool Contains(K key)
    {
        if (Root is null) return false;
        else
        {
            var currentNode = Root;
            while (currentNode is not null)
            {
                if (currentNode.Key.Equals(key)) return true;

                if (key.CompareTo(currentNode.Key) > 0 && currentNode.Right is not null) currentNode = currentNode.Right;
                else if (key.CompareTo(currentNode.Key) < 0 && currentNode.Left is not null) currentNode = currentNode.Left;
                else return false;
            }
            return false;
        }
    }

    public void Remove(K key)
    {
        if (Root == null) return;

        TreeNode<K, V>? parent = null;
        TreeNode<K, V>? current = Root;
        bool isLeftChild = false;

        while (current != null && !current.Key.Equals(key))
        {
            parent = current;

            if (key.CompareTo(current.Key) < 0)
            {
                current = current.Left;
                isLeftChild = true;
            }
            else
            {
                current = current.Right;
                isLeftChild = false;
            }
        }

        if (current == null) return;

        if (current.Left == null && current.Right == null) 
        {
            if (current == Root) 
            {
                Root = null;
            }
            else if (isLeftChild)
            {
                parent!.Left = null;
            }
            else
            {
                parent!.Right = null;
            }
        }
        else if (current.Left == null) 
        {
            if (current == Root)
            {
                Root = current.Right;
            }
            else if (isLeftChild)
            {
                parent!.Left = current.Right;
            }
            else
            {
                parent!.Right = current.Right;
            }
        }
        else if (current.Right == null)
        {
            if (current == Root)
            {
                Root = current.Left;
            }
            else if (isLeftChild)
            {
                parent!.Left = current.Left;
            }
            else
            {
                parent!.Right = current.Left;
            }
        }
        else 
        {
           
            TreeNode<K, V>? successorParent = current;
            TreeNode<K, V>? successor = current.Right;

            while (successor?.Left != null)
            {
                successorParent = successor;
                successor = successor.Left;
            }

            current.Key = successor!.Key;
            current.Value = successor.Value;

            
            if (successorParent != current)
            {
                successorParent.Left = successor.Right;
            }
            else
            {
                successorParent.Right = successor.Right;
            }
        }

        Size--;
    }
 
    public IEnumerable<TreeNode<K, V>> TraverseInOrder(TreeNode<K, V>? node)
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

    public IEnumerator<TreeNode<K, V>> GetEnumerator()
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
            result += $"Key:{node.Key}  Value:{node.Value}\n";
        }
        return result;
    }


}
    

