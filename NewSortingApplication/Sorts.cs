using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sort
{
    internal class Sorts
    {
        public static void BubbleSort<T>(T[] array) where T : IComparable<T> 
        {

            for (int i = 0; i < array.Length - 1; i++)
                for (int j = i + 1; j < array.Length; j++)
                    if (array[i].CompareTo(array[j]) > 0) 
                    {
                        T tmp = array[j];
                        array[j] = array[i];
                        array[i] = tmp;
                    };
        }

        public static void ShakerSort<T>(T[] array) where T : IComparable<T>
        {
            bool swapped = true;
            int start = 0;
            int end = array.Length;

            while (swapped == true)
            {
                swapped = false;
                for (int i = start; i < end - 1; ++i)
                {
                    if (array[i].CompareTo(array[i + 1]) > 0)
                    {
                        T temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                        swapped = true;
                    }
                }

                if (swapped == false)
                    break;
                swapped = false;

                --end;

                for (int i = end - 1; i >= start; i--)
                {
                    if (array[i].CompareTo(array[i + 1]) > 0)
                    {
                        T temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                        swapped = true;
                    }
                }

                ++start;
            }
        }

        public static void CombSort<T>(T[] array) where T : IComparable<T>
        {
            int getNextGap(int newGap)
            {
                newGap = (newGap * 10) / 13;
                if (newGap < 1)
                    return 1;
                return newGap;
            }

            int gap = array.Length;
            bool swapped = true;

            while (gap != 1 || swapped == true)
            {
                gap = getNextGap(gap);
                swapped = false;

                for (int i = 0; i < array.Length - gap; i++)
                {
                    if (array[i].CompareTo(array[i + gap]) > 0)
                    {
                        T temp = array[i];
                        array[i] = array[i + gap];
                        array[i + gap] = temp;
                        swapped = true;
                    }
                }
            }
        }

        public static void InsertionSort<T>(T[] array) where T : IComparable<T>
        {
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1].CompareTo(array[i]) > 0)
                {
                    int j = i;
                    while (j > 0 && array[j - 1].CompareTo(array[j]) > 0)
                    {
                        T tmp = array[j];
                        array[j] = array[j - 1];
                        array[j - 1] = tmp;
                        j--;
                    }
                }
            }
        }

        public static void ShellSort<T>(T[] array) where T : IComparable<T>
        {
            for (int gap = array.Length / 2; gap > 0; gap /= 2)
            {
                for (int i = gap; i < array.Length; i += 1)
                {
                    T temp = array[i];

                    int j;
                    for (j = i; j >= gap && array[j - gap].CompareTo(temp) > 0; j -= gap)
                        array[j] = array[j - gap];

                    array[j] = temp;
                }
            }
        }

        public class TreeNode<T> where T : IComparable<T>
        {
            public TreeNode(T data)
            {
                Data = data;
            }

            public T Data { get; set; }
            public TreeNode<T> Left { get; set; }
            public TreeNode<T> Right { get; set; }

            public void Insert(TreeNode<T> node)
            {
                if (Data.CompareTo(node.Data) < 0)
                {
                    if (Left == null) Left = node;
                    else Left.Insert(node);
                }
                else
                {
                    if (Right == null) Right = node;
                    else Right.Insert(node);
                }
            }

            public T[] Transform(List<T> elements = null)
            {
                if (elements == null) elements = new List<T>();
                if (Left != null) Left.Transform(elements);
                elements.Add(Data);
                if (Right != null) Right.Transform(elements);

                return elements.ToArray();
            }
        }

        public static void TreeSort<T>(T[] array) where T : IComparable<T>
        {
            array = treeSort(array);
        }

        public static T[] treeSort<T>(T[] array) where T : IComparable<T> 
        {

            var treeNode = new TreeNode<T>(array[0]);
            for (int i = 1; i < array.Length; i++)
            {
                treeNode.Insert(new TreeNode<T>(array[i]));
            }

            return treeNode.Transform();
        }

        public static void GnomeSort<T>(T[] array) where T : IComparable<T>
        {
            int index = 0;

            while (index < array.Length)
            {
                if (index == 0)
                    index++;

                if (array[index].CompareTo(array[index - 1]) >= 0)
                    index++;

                else
                {
                    T temp = array[index];
                    array[index] = array[index - 1];
                    array[index - 1] = temp;
                    index--;
                }
            }

        }

        public static void SelectionSort<T>(T[] array) where T : IComparable<T>
        {
            
            int indexMin;
            T min;
            for (int i = 0; i < array.Length - 1; i++)
            {
                min = array[i];
                indexMin = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j].CompareTo(min) < 0)
                    {
                        min = array[j];
                        indexMin = j;
                    }
                }
                T tmp = array[i];
                array[i] = min;
                array[indexMin] = tmp;
            }
        }

        public class BinaryHeap<T> where T : IComparable<T>
        {
            private List<T> list;

            public int heapSize
            {
                get
                {
                    return this.list.Count();
                }
            }

            public void add(T value)
            {
                list.Add(value);
                int i = heapSize - 1;
                int parent = (i - 1) / 2;

                while (i > 0 && list[parent].CompareTo(list[i]) < 0)
                {
                    T temp = list[i];
                    list[i] = list[parent];
                    list[parent] = temp;

                    i = parent;
                    parent = (i - 1) / 2;
                }
            }

            public void heapify(int i)
            {
                int leftChild;
                int rightChild;
                int largestChild;

                for (; ; )
                {
                    leftChild = 2 * i + 1;
                    rightChild = 2 * i + 2;
                    largestChild = i;

                    if (leftChild < heapSize && list[leftChild].CompareTo(list[largestChild]) > 0)
                    {
                        largestChild = leftChild;
                    }

                    if (rightChild < heapSize && list[rightChild].CompareTo(list[largestChild]) > 0)
                    {
                        largestChild = rightChild;
                    }

                    if (largestChild == i)
                    {
                        break;
                    }

                    T temp = list[i];
                    list[i] = list[largestChild];
                    list[largestChild] = temp;
                    i = largestChild;
                }
            }

            public void buildHeap(T[] sourceArray)
            {
                list = sourceArray.ToList();
                for (int i = heapSize / 2; i >= 0; i--)
                {
                    heapify(i);
                }
            }

            public T getMax()
            {
                T result = list[0];
                list[0] = list[heapSize - 1];
                list.RemoveAt(heapSize - 1);
                return result;
            }

            public void heapSort(T[] array)
            {
                buildHeap(array);
                for (int i = array.Length - 1; i >= 0; i--)
                {
                    array[i] = getMax();
                    heapify(0);
                }
            }

        }

        public static void HeapSort<T>(T[] array) where T : IComparable<T>
        {
            BinaryHeap<T> heap = new BinaryHeap<T>();
            heap.heapSort(array);
        }

        public static class QuickSortClass<T> where T : IComparable<T>
        {
            static void Swap(ref T x, ref T y)
            {
                var t = x;
                x = y;
                y = t;
            }

            static int Partition(T[] array, int minIndex, int maxIndex)
            {
                var pivot = minIndex - 1;
                for (var i = minIndex; i < maxIndex; i++)
                {
                    if (array[i].CompareTo(array[maxIndex]) < 0)
                    {
                        pivot++;
                        Swap(ref array[pivot], ref array[i]);
                    }
                }

                pivot++;
                Swap(ref array[pivot], ref array[maxIndex]);
                return pivot;
            }

            public static void QuickSort(T[] array, int minIndex, int maxIndex)
            {
                if (minIndex < maxIndex)
                {
                    var pivotIndex = Partition(array, minIndex, maxIndex);
                    QuickSort(array, minIndex, pivotIndex - 1);
                    QuickSort(array, pivotIndex + 1, maxIndex);
                }
            }
        }

        public static void QuickSort<T>(T[] array) where T : IComparable<T>
        {
            QuickSortClass<T>.QuickSort(array, 0, array.Length - 1);
        }

        public static void MergeArray<T>(T[] array, int left, int middle, int right) where T : IComparable<T>
        {
            var leftArrayLength = middle - left + 1;
            var rightArrayLength = right - middle;
            var leftTempArray = new T[leftArrayLength];
            var rightTempArray = new T[rightArrayLength];
            int i, j;

            for (i = 0; i < leftArrayLength; ++i)
                leftTempArray[i] = array[left + i];
            for (j = 0; j < rightArrayLength; ++j)
                rightTempArray[j] = array[middle + 1 + j];

            i = 0;
            j = 0;
            int k = left;

            while (i < leftArrayLength && j < rightArrayLength)
            {
                if (leftTempArray[i].CompareTo(rightTempArray[j]) <= 0)
                {
                    array[k++] = leftTempArray[i++];
                }
                else
                {
                    array[k++] = rightTempArray[j++];
                }
            }

            while (i < leftArrayLength)
            {
                array[k++] = leftTempArray[i++];
            }

            while (j < rightArrayLength)
            {
                array[k++] = rightTempArray[j++];
            }
        }

        public static void MergeSort<T>(T[] array, int left, int right) where T : IComparable<T>
        {
            if (left < right)
            {
                int middle = left + (right - left) / 2;

                MergeSort(array, left, middle);
                MergeSort(array, middle + 1, right);

                MergeArray(array, left, middle, right);
            }
        }

        public class BitonicMergeSort
        {
            static void Swap<T>(ref T leftHandSide, ref T rightHandSide) where T : IComparable<T>
            {
                T temp;
                temp = leftHandSide;
                leftHandSide = rightHandSide;
                rightHandSide = temp;
            }
            static void CompareAndSwap<T>(T[] array, int i, int j, int direction) where T : IComparable<T>
            {
                int k;

                k = array[i].CompareTo(array[j]) > 0 ? 1 : 0;

                if (direction == k)
                {
                    Swap(ref array[i], ref array[j]);
                }
            }

            static void BitonicMerge<T>(T[] array, int low, int count, int direction) where T : IComparable<T>
            {
                if (count > 1)
                {
                    int k = count / 2;
                    for (int i = low; i < low + k; i++)
                    {
                        CompareAndSwap(array, i, i + k, direction);
                    }
                    BitonicMerge(array, low, k, direction);
                    BitonicMerge(array, low + k, k, direction);
                }
            }

            public static void bitonicSort<T>(T[] array, int low, int count, int direction) where T : IComparable<T>
            {
                if (count > 1)
                {
                    int k = count / 2;

                    bitonicSort(array, low, k, 1);

                    bitonicSort(array, low + k, k, 0);

                    BitonicMerge(array, low, count, direction);
                }
            }
        }

        public static void BitonicSort<T>(T[] array) where T : IComparable<T>
        {
            BitonicMergeSort.bitonicSort(array, 0, array.Length, 1);
        }
    }
}
