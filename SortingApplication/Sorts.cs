using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sort
{
    internal class Sorts
    {
        public static void BubbleSort(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
                for (int j = 0; j < array.Length - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int tmp = array[j + 1];
                        array[j + 1] = array[j];
                        array[j] = tmp;
                    }
                }
        }

        public static void ShakerSort(int[] array)
        {
            bool swapped = true;
            int start = 0;
            int end = array.Length;

            while (swapped == true)
            {
                swapped = false;
                for (int i = start; i < end - 1; ++i)
                {
                    if (array[i] > array[i + 1])
                    {
                        int temp = array[i];
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
                    if (array[i] > array[i + 1])
                    {
                        int temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                        swapped = true;
                    }
                }

                ++start;
            }
        }

        public static void CombSort(int[] array)
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
                    if (array[i] > array[i + gap])
                    {
                        int temp = array[i];
                        array[i] = array[i + gap];
                        array[i + gap] = temp;
                        swapped = true;
                    }
                }
            }
        }

        public static void InsertionSort(int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] > array[i])
                {
                    int j = i;
                    while (j > 0 && array[j - 1] > array[j])
                    {
                        int tmp = array[j];
                        array[j] = array[j - 1];
                        array[j - 1] = tmp;
                        j--;
                    }
                }
            }
        }

        public static void ShellSort(int[] array)
        {
            for (int gap = array.Length / 2; gap > 0; gap /= 2)
            {
                for (int i = gap; i < array.Length; i += 1)
                {
                    int temp = array[i];

                    int j;
                    for (j = i; j >= gap && array[j - gap] > temp; j -= gap)
                        array[j] = array[j - gap];

                    array[j] = temp;
                }
            }
        }

        public class TreeNode
        {
            public TreeNode(int data)
            {
                Data = data;
            }

            public int Data { get; set; }
            public TreeNode Left { get; set; }
            public TreeNode Right { get; set; }

            public void Insert(TreeNode node)
            {
                if (node.Data < Data)
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

            public int[] Transform(List<int> elements = null)
            {
                if (elements == null) elements = new List<int>();
                if (Left != null) Left.Transform(elements);
                elements.Add(Data);
                if (Right != null) Right.Transform(elements);

                return elements.ToArray();
            }
        }

        public static int[] TreeSort(int[] array)
        {
            var treeNode = new TreeNode(array[0]);
            for (int i = 1; i < array.Length; i++)
            {
                treeNode.Insert(new TreeNode(array[i]));
            }

            return treeNode.Transform();
        }

        public static void GnomeSort(int[] array)
        {
            int index = 0;

            while (index < array.Length)
            {
                if (index == 0)
                    index++;

                if (array[index] >= array[index - 1])
                    index++;

                else
                {
                    int temp = array[index];
                    array[index] = array[index - 1];
                    array[index - 1] = temp;
                    index--;
                }
            }

        }

        public static void SelectionSort(int[] array)
        {
            int min, indexMin;
            for (int i = 0; i < array.Length - 1; i++)
            {
                min = array[i];
                indexMin = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j] < min)
                    {
                        min = array[j];
                        indexMin = j;
                    }
                }
                int tmp = array[i];
                array[i] = min;
                array[indexMin] = tmp;
            }
        }

        public class BinaryHeap
        {
            private List<int> list;

            public int heapSize
            {
                get
                {
                    return this.list.Count();
                }
            }

            public void add(int value)
            {
                list.Add(value);
                int i = heapSize - 1;
                int parent = (i - 1) / 2;

                while (i > 0 && list[parent] < list[i])
                {
                    int temp = list[i];
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

                    if (leftChild < heapSize && list[leftChild] > list[largestChild])
                    {
                        largestChild = leftChild;
                    }

                    if (rightChild < heapSize && list[rightChild] > list[largestChild])
                    {
                        largestChild = rightChild;
                    }

                    if (largestChild == i)
                    {
                        break;
                    }

                    int temp = list[i];
                    list[i] = list[largestChild];
                    list[largestChild] = temp;
                    i = largestChild;
                }
            }

            public void buildHeap(int[] sourceArray)
            {
                list = sourceArray.ToList();
                for (int i = heapSize / 2; i >= 0; i--)
                {
                    heapify(i);
                }
            }

            public int getMax()
            {
                int result = list[0];
                list[0] = list[heapSize - 1];
                list.RemoveAt(heapSize - 1);
                return result;
            }

            public void heapSort(int[] array)
            {
                buildHeap(array);
                for (int i = array.Length - 1; i >= 0; i--)
                {
                    array[i] = getMax();
                    heapify(0);
                }
            }

        }

        public static void HeapSort(int[] array)
        {
            BinaryHeap heap = new BinaryHeap();
            heap.heapSort(array);
        }

        public static class QuickSortClass
        {
            static void Swap(ref int x, ref int y)
            {
                var t = x;
                x = y;
                y = t;
            }

            static int Partition(int[] array, int minIndex, int maxIndex)
            {
                var pivot = minIndex - 1;
                for (var i = minIndex; i < maxIndex; i++)
                {
                    if (array[i] < array[maxIndex])
                    {
                        pivot++;
                        Swap(ref array[pivot], ref array[i]);
                    }
                }

                pivot++;
                Swap(ref array[pivot], ref array[maxIndex]);
                return pivot;
            }

            public static void QuickSort(int[] array, int minIndex, int maxIndex)
            {
                if (minIndex < maxIndex)
                {
                    var pivotIndex = Partition(array, minIndex, maxIndex);
                    QuickSort(array, minIndex, pivotIndex - 1);
                    QuickSort(array, pivotIndex + 1, maxIndex);
                }
            }
        }

        public static void QuickSort(int[] array)
        {
            QuickSortClass.QuickSort(array, 0, array.Length - 1);
        }

        public static void MergeArray(int[] array, int left, int middle, int right)
        {
            var leftArrayLength = middle - left + 1;
            var rightArrayLength = right - middle;
            var leftTempArray = new int[leftArrayLength];
            var rightTempArray = new int[rightArrayLength];
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
                if (leftTempArray[i] <= rightTempArray[j])
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

        public static void MergeSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                int middle = left + (right - left) / 2;

                MergeSort(array, left, middle);
                MergeSort(array, middle + 1, right);

                MergeArray(array, left, middle, right);
            }
        }

        public static void CountingSort(int[] array)
        {
            var min = array[0];
            var max = array[0];
            foreach (int element in array)
            {
                if (element > max)
                {
                    max = element;
                }
                else if (element < min)
                {
                    min = element;
                }
            }

            var correctionFactor = min != 0 ? -min : 0;
            max += correctionFactor;

            var count = new int[max + 1];
            for (var i = 0; i < array.Length; i++)
            {
                count[array[i] + correctionFactor]++;
            }

            var index = 0;
            for (var i = 0; i < count.Length; i++)
            {
                for (var j = 0; j < count[i]; j++)
                {
                    array[index] = i - correctionFactor;
                    index++;
                }
            }
        }

        public static int GetMaxVal(int[] array, int size)
        {
            var maxVal = array[0];
            for (int i = 1; i < size; i++)
                if (array[i] > maxVal)
                    maxVal = array[i];
            return maxVal;
        }

        public static void countingSort(int[] array, int size, int exponent)
        {
            var outputArr = new int[size];
            var occurences = new int[10];
            for (int i = 0; i < 10; i++)
                occurences[i] = 0;
            for (int i = 0; i < size; i++)
                occurences[(array[i] / exponent) % 10]++;
            for (int i = 1; i < 10; i++)
                occurences[i] += occurences[i - 1];
            for (int i = size - 1; i >= 0; i--)
            {
                outputArr[occurences[(array[i] / exponent) % 10] - 1] = array[i];
                occurences[(array[i] / exponent) % 10]--;
            }
            for (int i = 0; i < size; i++)
                array[i] = outputArr[i];
        }

        public static void RadixSort(int[] array)
        {
            var maxVal = GetMaxVal(array, array.Length);
            for (int exponent = 1; maxVal / exponent > 0; exponent *= 10)
                countingSort(array, array.Length, exponent);
        }

        public class BitonicMergeSort
        {
            static void Swap<T>(ref T leftHandSide, ref T rightHandSide)
            {
                T temp;
                temp = leftHandSide;
                leftHandSide = rightHandSide;
                rightHandSide = temp;
            }
            static void CompareAndSwap(int[] array, int i, int j, int direction)
            {
                int k;

                k = array[i] > array[j] ? 1 : 0;

                if (direction == k)
                {
                    Swap(ref array[i], ref array[j]);
                }
            }

            static void BitonicMerge(int[] array, int low, int count, int direction)
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

            public static void bitonicSort(int[] array, int low, int count, int direction)
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

        public static void BitonicSort(int[] array)
        {
            BitonicMergeSort.bitonicSort(array, 0, array.Length, 1);
        }
    }
}
