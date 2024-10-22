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

        public static void TestDataInt(int[] data)
        {
            Random rand = new Random();
            for (int i = 0; i < data.Length; i++) data[i] = rand.Next(1, 1000);
        }

        public static void TestDataDouble(double[] data)
        {
            Random rand = new Random();
            for (int i = 0; i < data.Length; i++) data[i] = rand.NextDouble();
        }

        public static void TestDataChar(char[] data)
        {
            Random rand = new Random();
            for (int i = 0; i < data.Length; i++) data[i] = (char)rand.Next();
        }

        public static int[] MakeSubarrayInt(int[] array, int size)
        {
            int[] subarray = new int[size];    
            for (int i = 0; i < size; i++)
            {
                subarray[i] = array[i];
            }        
            return subarray;
        }

        public static double[] MakeSubarrayDouble(double[] array, int size)
        {
            double[] subarray = new double[size];
            for (int i = 0; i < size; i++)
            {
                subarray[i] = array[i];
            }
            return subarray;
        }

        public static char[] MakeSubarrayChar(char[] array, int size)
        {
            char[] subarray = new char[size];
            for (int i = 0; i < size; i++)
            {
                subarray[i] = array[i];
            }
            return subarray;
        }

        public static double CountTime<T>(T[] array, Action<T[]> sortFunc)
        {
            Stopwatch sw = Stopwatch.StartNew();
            T[] newArray = (T[])array.Clone();
            sw.Start();
            sortFunc(newArray);
            sw.Stop();
            return sw.Elapsed.TotalMilliseconds;
        }

        public static void SortingFirstGroup(string typeData, int maxSize, double[] totalTimesBubbleSort, double[] totalTimesInsertionSort, double[] totalTimesSelectionSort, double[] totalTimesShakerSort, double[] totalTimesGnomeSort)
        {
            if (typeData == "int")
            {
                int[] array = new int[maxSize];
                TestDataInt(array);

                for (int len = 10, ind = 0; len <= maxSize; len *= 10, ind++)
                {
                    int[] subarray = MakeSubarrayInt(array, len);
                    totalTimesBubbleSort[ind] += CountTime(subarray, Sorts.BubbleSort);
                    totalTimesInsertionSort[ind] += CountTime(subarray, Sorts.InsertionSort);
                    totalTimesSelectionSort[ind] += CountTime(subarray, Sorts.SelectionSort);
                    totalTimesShakerSort[ind] += CountTime(subarray, Sorts.ShakerSort);
                    totalTimesGnomeSort[ind] += CountTime(subarray, Sorts.GnomeSort);
                }
            }
            else if (typeData == "double")
            {
                double[] array = new double[maxSize];
                TestDataDouble(array);

                for (int len = 10, ind = 0; len <= maxSize; len *= 10, ind++)
                {
                    double[] subarray = MakeSubarrayDouble(array, len);
                    totalTimesBubbleSort[ind] += CountTime(subarray, Sorts.BubbleSort);
                    totalTimesInsertionSort[ind] += CountTime(subarray, Sorts.InsertionSort);
                    totalTimesSelectionSort[ind] += CountTime(subarray, Sorts.SelectionSort);
                    totalTimesShakerSort[ind] += CountTime(subarray, Sorts.ShakerSort);
                    totalTimesGnomeSort[ind] += CountTime(subarray, Sorts.GnomeSort);
                }
            }
            else if (typeData == "char")
            {
                char[] array = new char[maxSize];
                TestDataChar(array);

                for (int len = 10, ind = 0; len <= maxSize; len *= 10, ind++)
                {
                    char[] subarray = MakeSubarrayChar(array, len);
                    totalTimesBubbleSort[ind] += CountTime(subarray, Sorts.BubbleSort);
                    totalTimesInsertionSort[ind] += CountTime(subarray, Sorts.InsertionSort);
                    totalTimesSelectionSort[ind] += CountTime(subarray, Sorts.SelectionSort);
                    totalTimesShakerSort[ind] += CountTime(subarray, Sorts.ShakerSort);
                    totalTimesGnomeSort[ind] += CountTime(subarray, Sorts.GnomeSort);
                }
            }
        }

        public static void SortingSecondGroup(string typeData, int maxSize, double[] totalTimesBitonicSort, double[] totalTimesShellSort)
        {
            if (typeData == "int")
            {
                int[] array = new int[maxSize];
                TestDataInt(array);

                for (int len = 10, ind = 0; len <= maxSize; len *= 10, ind++)
                {
                    int[] subarray = MakeSubarrayInt(array, len);
                    totalTimesBitonicSort[ind] += CountTime(subarray, Sorts.BitonicSort);
                    totalTimesShellSort[ind] += CountTime(subarray, Sorts.ShellSort);
                }
            }
            else if (typeData == "double")
            {
                double[] array = new double[maxSize];
                TestDataDouble(array);

                for (int len = 10, ind = 0; len <= maxSize; len *= 10, ind++)
                {
                    double[] subarray = MakeSubarrayDouble(array, len);
                    totalTimesBitonicSort[ind] += CountTime(subarray, Sorts.BitonicSort);
                    totalTimesShellSort[ind] += CountTime(subarray, Sorts.ShellSort);
                }
            }
            else if (typeData == "char")
            {
                char[] array = new char[maxSize];
                TestDataChar(array);

                for (int len = 10, ind = 0; len <= maxSize; len *= 10, ind++)
                {
                    char[] subarray = MakeSubarrayChar(array, len);
                    totalTimesBitonicSort[ind] += CountTime(subarray, Sorts.BitonicSort);
                    totalTimesShellSort[ind] += CountTime(subarray, Sorts.ShellSort);
                }
            }
        }

        public static void SortingThirdGroup(string typeData, int maxSize, double[] totalTimesCombSort, double[] totalTimesHeapSort)
        {
            if (typeData == "int")
            {
                int[] array = new int[maxSize];
                TestDataInt(array);

                for (int len = 10, ind = 0; len <= maxSize; len *= 10, ind++)
                {
                    int[] subarray = MakeSubarrayInt(array, len);
                    totalTimesCombSort[ind] += CountTime(subarray, Sorts.CombSort);
                    totalTimesHeapSort[ind] += CountTime(subarray, Sorts.HeapSort);
                }
            }
            else if (typeData == "double")
            {
                double[] array = new double[maxSize];
                TestDataDouble(array);

                for (int len = 10, ind = 0; len <= maxSize; len *= 10, ind++)
                {
                    double[] subarray = MakeSubarrayDouble(array, len);
                    totalTimesCombSort[ind] += CountTime(subarray, Sorts.CombSort);
                    totalTimesHeapSort[ind] += CountTime(subarray, Sorts.HeapSort);
                }
            }
            else if (typeData == "char")
            {
                char[] array = new char[maxSize];
                TestDataChar(array);

                for (int len = 10, ind = 0; len <= maxSize; len *= 10, ind++)
                {
                    char[] subarray = MakeSubarrayChar(array, len);
                    totalTimesCombSort[ind] += CountTime(subarray, Sorts.CombSort);
                    totalTimesHeapSort[ind] += CountTime(subarray, Sorts.HeapSort);
                }
            }
        }
        

        private void launchTestsButton_Click(object sender, EventArgs e)
        {
            string selectedSorts = sortsDataComboBox.Text;
            int size;
            string typeData = testDataComboBox.Text;

            if (selectedSorts == "Первая группа")
            {
                size = 10000;

                double[] totalTimesBubbleSort = new double[4] { 0, 0, 0, 0 };
                double[] totalTimesInsertionSort = new double[4] { 0, 0, 0, 0 };
                double[] totalTimesSelectionSort = new double[4] { 0, 0, 0, 0 };
                double[] totalTimesShakerSort = new double[4] { 0, 0, 0, 0 };
                double[] totalTimesGnomeSort = new double[4] { 0, 0, 0, 0 };


                SortingFirstGroup(typeData, size, totalTimesBubbleSort, totalTimesInsertionSort, totalTimesSelectionSort, totalTimesShakerSort, totalTimesGnomeSort);
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

                SortingSecondGroup(typeData, size, totalTimesBitonicSort, totalTimesShellSort);
                GraphPane pane = zedGraph.GraphPane;
                pane.CurveList.Clear();

                PointPairList listBitonicSort = new PointPairList();
                PointPairList listShellSort = new PointPairList();
                

                for (int n = 10, i = 0; n <= 100000; n *= 10, i++)
                {
                    listBitonicSort.Add(n, totalTimesBitonicSort[i] / 5);
                    listShellSort.Add(n, totalTimesShellSort[i] / 5);  
                }
                LineItem curve = pane.AddCurve("битонная", listBitonicSort, Color.Blue, SymbolType.None);
                curve = pane.AddCurve("Шелла", listShellSort, Color.Red, SymbolType.None);  

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

                SortingThirdGroup(typeData, size, totalTimesCombSort, totalTimesHeapSort);
                GraphPane pane = zedGraph.GraphPane;
                pane.CurveList.Clear();

                PointPairList listCombSort = new PointPairList();
                PointPairList listHeapSort = new PointPairList();

                for (int n = 10, i = 0; n <= 1000000; n *= 10, i++)
                {
                    listCombSort.Add(n, totalTimesCombSort[i] / 5); // Дел на 5 
                    listHeapSort.Add(n, totalTimesHeapSort[i] / 5);
                }
                LineItem curve = pane.AddCurve("расчёской", listCombSort, Color.Blue, SymbolType.None);
                curve = pane.AddCurve("пирамидальная", listHeapSort, Color.Red, SymbolType.None);
             
                pane.XAxis.Scale.Min = 0;
                pane.XAxis.Scale.Max = 1000000;

                zedGraph.AxisChange();
                zedGraph.Invalidate();
            }
        }
    }
}
