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
                for (int i = 0; i < 2; i++)
                {
                    TestArrayDataModules(array);
                    for (int j = 0; j < array.Length; j++)
                    {
                        writer.Write(array[j]);
                    }
                }
            }
        } // До 2

        public static void TestArrayDataSubarray(int[] data)
        {
            TestArrayDataModules(data);
            Random rand = new Random();
            int size = data.Length % rand.Next(0, data.Length);
            Array.Sort(data, 0, size);
        }

        public static void GenerateTestArrayDataSubarray(int size) // До 2
        {
            using (StreamWriter writer = new StreamWriter(@"D:\Study\Code\c#\SortingApplication\DataForSorting.txt", false))
            {
                int[] array = new int[size];
                for (int i = 0; i < 2; i++)
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
                for (int i = 0; i < 2; i++)
                {
                    TestArrayDataSwaps(array);
                    for (int j = 0; j < array.Length; j++)
                    {
                        writer.Write(array[j]);
                    }
                }
            }
        } // До 2

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
                for (int i = 0; i < 2; i++)
                {
                    TestArrayDataSort(array);
                    for (int j = 0; j < array.Length; j++)
                    {
                        writer.Write(array[j]);
                    }
                }
            }
        } // До 2

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

        public static void SortingFirstGroup(int maxSize) // До 2
        {
            using (StreamReader reader = new StreamReader(@"D:\Study\Code\c#\SortingApplication\DataForSorting.txt"))
            {
                double[] totalTimesBubbleSort = new double[4] { 0, 0, 0, 0 };
                //еще 4 массива
                for (int n = 0; n < 2; n++)
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
                        //еще 4 вызова сортировок
                    }
                }

                
                    //GraphPane pane = zedGraph.GraphPane;
                    //pane.CurveList.Clear();
                    //PointPairList list = new PointPairList(); // переменные

                    //for (int n = 10, i = 0; n <= maxSize; n *= 10, i++)
                    //{
                    //    list.Add(n, totalTimesBubbleSort[i] / 2); // дел на 2
                    //}
                    //LineItem curve = pane.AddCurve("Sinc", list, Color.Blue, SymbolType.None); // переменные

                    //zedGraph.AxisChange();
                    //zedGraph.Invalidate();
                
            }
        }

        private void generateArraysButton_Click(object sender, EventArgs e)
        {
            string selectedSorts = sortsDataComboBox.Text;

            int size = 0;

            if (selectedSorts == "Первая группа")
            {
                size = 10000;
                label1.Text = size.ToString();
            }
            else if (selectedSorts == "Вторая группа")
            {
                size = 100000;
                label1.Text = size.ToString();
            }
            else if (selectedSorts == "Третья группа")
            {
                size = 1000000;
                label1.Text = size.ToString();
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
                SortingFirstGroup(size);
                GraphPane pane = zedGraph.GraphPane;
                pane.CurveList.Clear();
                PointPairList list = new PointPairList(); 

                for (int n = 10, i = 0; n <= 1000; n *= 10, i++)
                {
                    list.Add(n, i / 2); 
                }
                LineItem curve = pane.AddCurve("Sinc", list, Color.Blue, SymbolType.None);  

                zedGraph.AxisChange();
                zedGraph.Invalidate();

            }
            else if (selectedSorts == "Вторая группа")
            {
                size = 100000;
            }
            else if (selectedSorts == "Третья группа")
            {
                size = 1000000;
            }
        }

        
    }
}
