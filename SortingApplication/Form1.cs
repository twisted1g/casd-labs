using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Sort;
using ZedGraph;


namespace SortingApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GraphPane pane = zedGraph.GraphPane;
            pane.CurveList.Clear();

            pane.XAxis.Title.Text = "Размер массива, шт";
            pane.YAxis.Title.Text = "Время выполнения, мс.";
            pane.Title.Text = "Исследование зависимости времени сортировок от размера массива";
        }

        public static void TestArrayDataModules(int[] data)
        {
            Random rand = new Random();
            for (int i = 0; i < data.Length; i++) data[i] = rand.Next(0, 1000);
        }

        public static void GenerateTestArrayDataMoules(int size)
        {
            using (StreamWriter writer = new StreamWriter(@"D:\Study\Code\c#\SortingApplication\DataForSorting.txt", false))
            {
                int[] array = new int[size];
                for (int i = 0; i < 5; i++)
                {
                    TestArrayDataModules(array);
                    for (int j = 0; j < array.Length; j++)
                    {
                        writer.Write(array[j]);
                    }
                }
            }
        } // До 5

        public static void TestArrayDataSubarray(int[] data)
        {
            TestArrayDataModules(data);
            Random rand = new Random();
            int size = data.Length % rand.Next(0, data.Length);
            Array.Sort(data, 0, size);
        }

        public static void GenerateTestArrayDataSubarray(int size) // До 5
        {
            using (StreamWriter writer = new StreamWriter(@"D:\Study\Code\c#\SortingApplication\DataForSorting.txt", false))
            {
                int[] array = new int[size];
                for (int i = 0; i < 5; i++)
                {
                    TestArrayDataSubarray(array);
                    for (int j = 0; j < array.Length; j++)
                    {
                        writer.Write(array[j]);
                    }
                }
            }
        }

        public static void TestArrayDataSwaps(int[] data)
        { 
            TestArrayDataModules(data);
            Array.Sort(data);

            Random rand = new Random();
            for (int i = 0; i < 3; i++)
            {
                int indexFirst = rand.Next(0, data.Length);
                int indexSecond = rand.Next(0, data.Length);

                int tmp = data[indexFirst];
                data[indexFirst] = data[indexSecond];
                data[indexSecond] = tmp;
            }
        }

        public static void GenerateTestArrayDataSwaps(int size)
        {
            using (StreamWriter writer = new StreamWriter(@"D:\Study\Code\c#\SortingApplication\DataForSorting.txt", false))
            {
                int[] array = new int[size];
                for (int i = 0; i < 5; i++)
                {
                    TestArrayDataSwaps(array);
                    for (int j = 0; j < array.Length; j++)
                    {
                        writer.Write(array[j]);
                    }
                }
            }
        } // До 5

        public static void TestArrayDataSort(int[] data)
        {
            TestArrayDataModules(data);
            Array.Sort(data);
        }

        public static void GenerateTestArrayDataSort(int size)
        {
            using (StreamWriter writer = new StreamWriter(@"D:\Study\Code\c#\SortingApplication\DataForSorting.txt", false))
            {
                int[] array = new int[size];
                for (int i = 0; i < 5; i++)
                {
                    TestArrayDataSort(array);
                    for (int j = 0; j < array.Length; j++)
                    {
                        writer.Write(array[j]);
                    }
                }
            }
        } // До 5

        public static int[] MakeSubarray(int[] array, int size)
        {
            int[] subarray = new int[size];    
            for (int i = 0; i < size; i++)
            {
                subarray[i] = array[i];
            }        
            return subarray;
        }

        public static double CountTime(int[] array, Action<int[]> sortFunc) // 
        {
            Stopwatch sw = Stopwatch.StartNew();
            int[] newArray = (int[])array.Clone();
            sw.Start();
            sortFunc(newArray);
            sw.Stop();
            return sw.Elapsed.TotalMilliseconds;
        }

        public static double CountTime(int[] array, Action<int[], int, int> sortFunc, int start, int end) // 
        {
            Stopwatch sw = Stopwatch.StartNew();
            int[] newArray = (int[])array.Clone();
            sw.Start();
            sortFunc(newArray, start, end);
            sw.Stop();
            return sw.Elapsed.TotalMilliseconds;
        }
        public static void SortingFirstGroup(int maxSize, double[] totalTimesBubbleSort, double[] totalTimesInsertionSort, double[] totalTimesSelectionSort, double[] totalTimesShakerSort, double[] totalTimesGnomeSort) // До 5
        {
            using (StreamReader reader = new StreamReader(@"D:\Study\Code\c#\SortingApplication\DataForSorting.txt"))
            {

                for (int n = 0; n < 5; n++)
                {
                    int[] array = new int[maxSize];
                    for (int i = 0; i < maxSize; i++)
                    {
                        array[i] = reader.Read();
                    }

                    for (int len = 10, ind = 0; len <= maxSize; len *= 10, ind++)
                    {
                        int[] subarray = MakeSubarray(array, len);
                        totalTimesBubbleSort[ind] += CountTime(subarray, Sorts.BubbleSort);
                        totalTimesInsertionSort[ind] += CountTime(subarray, Sorts.InsertionSort);
                        totalTimesSelectionSort[ind] += CountTime(subarray, Sorts.SelectionSort);
                        totalTimesShakerSort[ind] += CountTime(subarray, Sorts.ShakerSort);
                        totalTimesGnomeSort[ind] += CountTime(subarray, Sorts.GnomeSort);
                    }
                }
            }
        }

        public static void SortingSecondGroup(int maxSize, double[] totalTimesBitonicSort, double[] totalTimesShellSort) //,double[] totalTimesTreeSort)
        {
            using (StreamReader reader = new StreamReader(@"D:\Study\Code\c#\SortingApplication\DataForSorting.txt"))
            {

                for (int n = 0; n < 5; n++)
                {
                    int[] array = new int[maxSize];
                    for (int i = 0; i < maxSize; i++)
                    {
                        array[i] = reader.Read();
                    }

                    for (int len = 10, ind = 0; len <= maxSize; len *= 10, ind++)
                    {
                        int[] subarray = MakeSubarray(array, len);
                        totalTimesBitonicSort[ind] += CountTime(subarray, Sorts.BitonicSort);
                        totalTimesShellSort[ind] += CountTime(subarray, Sorts.ShellSort);
                        //totalTimesTreeSort[ind] += CountTime(subarray, Sorts.TreeSort);
                    }
                }
            }
        }

        public static void SortingThirdGroup(int maxSize, double[] totalTimesCombSort, double[] totalTimesHeapSort, double[] totalTimesMergeSort, double[] totalTimesCountingSort, double[] totalTimesRadixSort)
        {
            using (StreamReader reader = new StreamReader(@"D:\Study\Code\c#\SortingApplication\DataForSorting.txt"))
            {

                for (int n = 0; n < 5; n++)
                {
                    int[] array = new int[maxSize];
                    for (int i = 0; i < maxSize; i++)
                    {
                        array[i] = reader.Read();
                    }

                    for (int len = 10, ind = 0; len <= maxSize; len *= 10, ind++)
                    {
                        int[] subarray = MakeSubarray(array, len);
                        totalTimesCombSort[ind] += CountTime(subarray, Sorts.CombSort);
                        totalTimesHeapSort[ind] += CountTime(subarray, Sorts.HeapSort);
                        //totalTimesQuickSort[ind] += CountTime(subarray, Sorts.QuickSort);
                        totalTimesMergeSort[ind] += CountTime(subarray, Sorts.MergeSort, 0, subarray.Length-1); // 
                        totalTimesCountingSort[ind] += CountTime(subarray, Sorts.CountingSort);
                        totalTimesRadixSort[ind] += CountTime(subarray, Sorts.RadixSort);
                    }
                }
            }
        }
        private void generateArraysButton_Click(object sender, EventArgs e)
        {
            string selectedSorts = sortsDataComboBox.Text;

            int size = 0;

            if (selectedSorts == "Первая группа")
            {
                size = 10000;
                
            }
            else if (selectedSorts == "Вторая группа")
            {
                size = 100000;
                
            }
            else if (selectedSorts == "Третья группа")
            {
                size = 1000000;
                
            }

            if (size != 0)
            {
                string selectedTests = testDataComboBox.Text;

                if (selectedTests == "Случайные числа")
                {
                    GenerateTestArrayDataMoules(size);
                }
                else if (selectedTests == "Отсортированные подмассивы")
                {
                    GenerateTestArrayDataSubarray(size);
                }
                else if (selectedTests == "Отсортированные масивы с перестановками")
                {
                    GenerateTestArrayDataSwaps(size);
                }
                else if (selectedTests == "Полностью отсортированные")
                {
                    GenerateTestArrayDataSort(size);
                }

            }


        }

        private void launchTestsButton_Click(object sender, EventArgs e)
        {
            string selectedSorts = sortsDataComboBox.Text;
            int size;

            if (selectedSorts == "Первая группа")
            {
                size = 10000;

                double[] totalTimesBubbleSort = new double[4] { 0, 0, 0, 0 };
                double[] totalTimesInsertionSort = new double[4] { 0, 0, 0, 0 };
                double[] totalTimesSelectionSort = new double[4] { 0, 0, 0, 0 };
                double[] totalTimesShakerSort = new double[4] { 0, 0, 0, 0 };
                double[] totalTimesGnomeSort = new double[4] { 0, 0, 0, 0 };


                SortingFirstGroup(size, totalTimesBubbleSort, totalTimesInsertionSort, totalTimesSelectionSort, totalTimesShakerSort, totalTimesGnomeSort);
                GraphPane pane = zedGraph.GraphPane;
                pane.CurveList.Clear();

                PointPairList listBubbleSort = new PointPairList();
                PointPairList listInsertionSort = new PointPairList();
                PointPairList listSelectionSort = new PointPairList();
                PointPairList listShakerSort = new PointPairList();
                PointPairList listGnomeSort = new PointPairList();

                for (int n = 10, i = 0; n <= 10000; n *= 10, i++)
                {
                    listBubbleSort.Add(n, totalTimesBubbleSort[i]/5); // Дел на 5 
                    listInsertionSort.Add(n, totalTimesInsertionSort[i]/5);
                    listSelectionSort.Add(n, totalTimesSelectionSort[i]/5);
                    listShakerSort.Add(n, totalTimesShakerSort[i]/5);
                    listGnomeSort.Add(n, totalTimesGnomeSort[i]/5);
                }
                LineItem curve = pane.AddCurve("пузырьком", listBubbleSort, Color.Blue, SymbolType.None);
                curve = pane.AddCurve("вставками", listInsertionSort, Color.Red, SymbolType.None);
                curve = pane.AddCurve("выбором", listSelectionSort, Color.Green, SymbolType.None);
                curve = pane.AddCurve("шейкерная", listShakerSort, Color.Yellow, SymbolType.None);
                curve = pane.AddCurve("гномья", listGnomeSort, Color.Black, SymbolType.None);

                pane.XAxis.Scale.Min = 0;
                pane.XAxis.Scale.Max = 10000;

                zedGraph.AxisChange();
                zedGraph.Invalidate();

            }

            else if (selectedSorts == "Вторая группа")
            {
                size = 100000;

                double[] totalTimesBitonicSort = new double[5] { 0, 0, 0, 0, 0 };
                double[] totalTimesShellSort = new double[5] { 0, 0, 0, 0, 0 };
                //double[] totalTimesTreeSort = new double[5] { 0, 0, 0, 0, 0 };


                SortingSecondGroup(size, totalTimesBitonicSort, totalTimesShellSort);//, totalTimesTreeSort);
                GraphPane pane = zedGraph.GraphPane;
                pane.CurveList.Clear();

                PointPairList listBitonicSort = new PointPairList();
                PointPairList listShellSort = new PointPairList();
                //PointPairList listTreeSort = new PointPairList();

                for (int n = 10, i = 0; n <= 100000; n *= 10, i++)
                {
                    listBitonicSort.Add(n, totalTimesBitonicSort[i] / 5); // Дел на 5 
                    listShellSort.Add(n, totalTimesShellSort[i] / 5);
                    //listTreeSort.Add(n, totalTimesTreeSort[i] / 5);
                }
                LineItem curve = pane.AddCurve("битонная", listBitonicSort, Color.Blue, SymbolType.None);
                curve = pane.AddCurve("Шелла", listShellSort, Color.Red, SymbolType.None);
                //curve = pane.AddCurve("сортировка деревом", listTreeSort, Color.Green, SymbolType.None);

                pane.XAxis.Scale.Min = 0;
                pane.XAxis.Scale.Max = 100000;

                zedGraph.AxisChange();
                zedGraph.Invalidate();
            }

            else if (selectedSorts == "Третья группа")
            {
                size = 1000000;

                double[] totalTimesCombSort = new double[6] { 0, 0, 0, 0, 0, 0 };
                double[] totalTimesHeapSort = new double[6] { 0, 0, 0, 0, 0, 0 };
                //double[] totalTimesQuickSort = new double[6] { 0, 0, 0, 0, 0, 0 };
                double[] totalTimesMergeSort = new double[6] { 0, 0, 0, 0, 0, 0 };
                double[] totalTimesCountingSort = new double[6] { 0, 0, 0, 0, 0, 0 };
                double[] totalTimesRadixSort = new double[6] { 0, 0, 0, 0, 0, 0 };


                SortingThirdGroup(size, totalTimesCombSort, totalTimesHeapSort, totalTimesMergeSort, totalTimesCountingSort, totalTimesRadixSort);
                GraphPane pane = zedGraph.GraphPane;
                pane.CurveList.Clear();

                PointPairList listCombSort = new PointPairList();
                PointPairList listHeapSort = new PointPairList();
                //PointPairList listQuickSort = new PointPairList();
                PointPairList listMergeSort = new PointPairList();
                PointPairList listCountingSort = new PointPairList();
                PointPairList listRadixSort = new PointPairList();


                for (int n = 10, i = 0; n <= 1000000; n *= 10, i++)
                {
                    listCombSort.Add(n, totalTimesCombSort[i] / 5); // Дел на 5 
                    listHeapSort.Add(n, totalTimesHeapSort[i] / 5);
                    //listQuickSort.Add(n, totalTimesQuickSort[i] / 5);
                    listMergeSort.Add(n, totalTimesMergeSort[i] / 5);
                    listCountingSort.Add(n, totalTimesCountingSort[i] / 5);
                    listRadixSort.Add(n, totalTimesRadixSort[i] / 5);


                }
                LineItem curve = pane.AddCurve("расчёской", listCombSort, Color.Blue, SymbolType.None);
                curve = pane.AddCurve("пирамидальная", listHeapSort, Color.Red, SymbolType.None);
                //curve = pane.AddCurve("быстрая сортировка", listQuickSort, Color.Green, SymbolType.None);
                curve = pane.AddCurve("слиянием", listMergeSort, Color.Black, SymbolType.None);
                curve = pane.AddCurve("подсчётом", listCountingSort, Color.Yellow, SymbolType.None);
                curve = pane.AddCurve("поразрядная", listRadixSort, Color.Orange, SymbolType.None);

                pane.XAxis.Scale.Min = 0;
                pane.XAxis.Scale.Max = 1000000;


                zedGraph.AxisChange();
                zedGraph.Invalidate();
            }
        }

        
    }
}
