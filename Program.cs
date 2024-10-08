using ArrayList;

class MaxBinaryHeap<T> where T : IComparable<T>
{
    private MyArrayList<T> list;

    public MaxBinaryHeap(T[] array)
    {
        list = new(array);
        for (int i = list.Size() / 2; i >= 0; i--)
        {
            OrderHeap(i);
        }
    }

    public void OrderHeap(int i)
    {
        int leftChild;
        int rightChild;
        int maxChild;

        for (; ; )
        {
            leftChild = 2 * i + 1;
            rightChild = 2 * i + 2;
            maxChild = i;

            if (leftChild < list.Size() && list.Compare(leftChild, maxChild) > 0)
            {
                maxChild = leftChild;
            }

            if (rightChild < list.Size() && list.Compare(rightChild, maxChild) > 0)
            {
                maxChild = rightChild;
            }

            if (maxChild == i)
            {
                break;
            }

            T tmp = list.Get(i);
            list.Set(i, list.Get(maxChild));
            list.Set(maxChild, tmp);
            i = maxChild;
        }
    }

    public T GetMax()
    {
        return list.Get(0);
    }

    public T RemoveMax()
    {
        T res = list.Get(0);
        list.Set(0, list.Get(list.Size() - 1));
        list.RemoveIndex(list.Size() - 1);
        return res;

    }

    public void Add(T item)
    {
        list.Add(item);
        int i = list.Size() - 1;
        int parent = (i - 1) / 2;

        while (i > 0 && list.Compare(i, parent) > 0 )
        {
            T tmp = list.Get(i);
            list.Set(i, list.Get(parent));
            list.Set(parent, tmp);

            i = parent;
            parent = (i - 1) / 2;
        }
    }

    public void SetKey(int index, T item)
    {
        list.Set(index, item);
        for (int i = list.Size() / 2; i >= 0; i--)
        {
            OrderHeap(i);
        }
    }

    public void MergeHeap(MaxBinaryHeap<T> otherHeap)
    {
        list.AddAll(otherHeap.list.ToArray());
        for (int i = list.Size() / 2; i >= 0; i--)
        {
            OrderHeap(i);
        }
    }

    public void Print()
    {
        list.Print();
    }
} 


class Program
{
    static public void Main(string[] args)
    {
        int[] array = new int[] { 1, 2, 3, 4, 5, 6 };
        MaxBinaryHeap<int> heap = new MaxBinaryHeap<int>(array);

        heap.Print();

        heap.SetKey(3, 9);
        heap.Print();

        array = new int[]{ 11, 0, 3, 12 };
        MaxBinaryHeap<int> otherhHeap = new MaxBinaryHeap<int>(array);
        heap.MergeHeap(otherhHeap);
        heap.Print();
    }
}