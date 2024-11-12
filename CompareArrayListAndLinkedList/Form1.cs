using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CompareArrayListAndLinkedList
{
    public partial class Form1 : Form
    {
        const int startTimes = 20;

        public Form1()
        {
            InitializeComponent();
            GraphPane pane = zedGraph.GraphPane;
            pane.CurveList.Clear();

            pane.XAxis.Title.Text = "Размер массива, шт";
            pane.YAxis.Title.Text = "Время выполнения, тики";
            pane.Title.Text = "Исследование зависимости времени операций от структуры данных";

            pane.XAxis.Scale.Min = 10000;
            pane.XAxis.Scale.Max = 100000000;


            zedGraph.AxisChange();
            zedGraph.Invalidate();
        }

        private int[] GenerateIntArray(int size)
        {
            int[] array = new int[size];
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next() % 1000;
            }
            return array;
        }

        private (double[], double[]) CompareGet()
        {
            int currentSize = 10000;
            double[] workTimeArrayList = new double[4] {0, 0, 0, 0 };
            double[] workTimeLinkedList = new double[4] { 0, 0, 0, 0 };
            for (int i = 0; i < 4; i++, currentSize *= 10)
            {
                int[] array = GenerateIntArray(currentSize);
                MyArrayList<int> arrayList = new MyArrayList<int>(array);
                MyLinkedList<int> linkedList = new MyLinkedList<int>(array);
                for (int j = 0; j < startTimes; j++)
                {
                    Random random = new Random();
                    int index = random.Next(currentSize); 

                    Stopwatch stopwatch = new Stopwatch();

                    stopwatch.Start();
                    arrayList.Get(index);
                    stopwatch.Stop();
                    workTimeArrayList[i] += stopwatch.Elapsed.Ticks;

                    stopwatch.Restart();
                    linkedList.Get(index);
                    stopwatch.Stop();
                    workTimeLinkedList[i] += stopwatch.Elapsed.Ticks;
                }
                workTimeArrayList[i] /= startTimes;
                workTimeLinkedList[i] /= startTimes;

            }

            return (workTimeArrayList, workTimeLinkedList);

        }

        private (double[], double[]) CompareSet()
        {
            int currentSize = 10000;
            double[] workTimeArrayList = new double[4] { 0, 0, 0, 0 };
            double[] workTimeLinkedList = new double[4] { 0, 0, 0, 0 };
            for (int i = 0; i < 4; i++, currentSize *= 10)
            {
                int[] array = GenerateIntArray(currentSize);
                MyArrayList<int> arrayList = new MyArrayList<int>(array);
                MyLinkedList<int> linkedList = new MyLinkedList<int>(array);
                for (int j = 0; j < startTimes; j++)
                {
                    Random random = new Random();
                    int index = random.Next(currentSize);

                    Stopwatch stopwatch = new Stopwatch();

                    stopwatch.Start();
                    arrayList.Set(index, index);
                    stopwatch.Stop();
                    workTimeArrayList[i] += stopwatch.Elapsed.Ticks;

                    stopwatch.Restart();
                    linkedList.Set(index, index);
                    stopwatch.Stop();
                    workTimeLinkedList[i] += stopwatch.Elapsed.Ticks;
                }
                workTimeArrayList[i] /= startTimes;
                workTimeLinkedList[i] /= startTimes;

            }

            return (workTimeArrayList, workTimeLinkedList);

        }

        private (double[], double[]) CompareAdd()
        {
            int currentSize = 10000;
            double[] workTimeArrayList = new double[4] { 0, 0, 0, 0 };
            double[] workTimeLinkedList = new double[4] { 0, 0, 0, 0 };
            for (int i = 0; i < 4; i++, currentSize *= 10)
            {
                int[] array = GenerateIntArray(currentSize);
                MyArrayList<int> arrayList = new MyArrayList<int>(array);
                MyLinkedList<int> linkedList = new MyLinkedList<int>(array);
                for (int j = 0; j < startTimes; j++)
                {
                    Random random = new Random();
                    int index = random.Next(currentSize);

                    Stopwatch stopwatch = new Stopwatch();

                    stopwatch.Start();
                    arrayList.Add(index);
                    stopwatch.Stop();
                    workTimeArrayList[i] += stopwatch.Elapsed.Ticks;

                    stopwatch.Restart();
                    linkedList.Add(index);
                    stopwatch.Stop();
                    workTimeLinkedList[i] += stopwatch.Elapsed.Ticks;
                }
                workTimeArrayList[i] /= startTimes;
                workTimeLinkedList[i] /= startTimes;

            }

            return (workTimeArrayList, workTimeLinkedList);
        }

        private (double[], double[]) CompareAddAt()
        {
            int currentSize = 10000;
            double[] workTimeArrayList = new double[4] { 0, 0, 0, 0 };
            double[] workTimeLinkedList = new double[4] { 0, 0, 0, 0 };
            for (int i = 0; i < 4; i++, currentSize *= 10)
            {
                int[] array = GenerateIntArray(currentSize);
                MyArrayList<int> arrayList = new MyArrayList<int>(array);
                MyLinkedList<int> linkedList = new MyLinkedList<int>(array);
                for (int j = 0; j < startTimes; j++)
                {
                    Random random = new Random();
                    int index = random.Next(currentSize);

                    Stopwatch stopwatch = new Stopwatch();

                    stopwatch.Start();
                    arrayList.AddAt(index, index);
                    stopwatch.Stop();
                    workTimeArrayList[i] += stopwatch.Elapsed.Ticks;

                    stopwatch.Restart();
                    linkedList.AddAt(index, index);
                    stopwatch.Stop();
                    workTimeLinkedList[i] += stopwatch.Elapsed.Ticks;
                }
                workTimeArrayList[i] /= startTimes;
                workTimeLinkedList[i] /= startTimes;

            }

            return (workTimeArrayList, workTimeLinkedList);
        }

        private (double[], double[]) CompareRemove()
        {
            int currentSize = 10000;
            double[] workTimeArrayList = new double[4] { 0, 0, 0, 0 };
            double[] workTimeLinkedList = new double[4] { 0, 0, 0, 0 };
            for (int i = 0; i < 4; i++, currentSize *= 10)
            {
                int[] array = GenerateIntArray(currentSize);
                MyArrayList<int> arrayList = new MyArrayList<int>(array);
                MyLinkedList<int> linkedList = new MyLinkedList<int>(array);
                for (int j = 0; j < startTimes; j++)
                {
                    Random random = new Random();
                    int index = random.Next(currentSize-100);

                    Stopwatch stopwatch = new Stopwatch();

                    stopwatch.Start();
                    arrayList.RemoveAt(index);
                    stopwatch.Stop();
                    workTimeArrayList[i] += stopwatch.Elapsed.Ticks;

                    stopwatch.Restart();
                    linkedList.RemoveAt(index);
                    stopwatch.Stop();
                    workTimeLinkedList[i] += stopwatch.Elapsed.Ticks;
                }
                workTimeArrayList[i] /= startTimes;
                workTimeLinkedList[i] /= startTimes;

            }

            return (workTimeArrayList, workTimeLinkedList);
        }

        private void DrawGraph((double[], double[]) tuple, string operation)
        {
            GraphPane pane = zedGraph.GraphPane;
            pane.CurveList.Clear();

            PointPairList arrayListPoints = new PointPairList();
            PointPairList linkedListPoints = new PointPairList();

            var (arrayListTime, linkedListTime) = tuple;
            
            for (int n = 100000, i = 0; i < 4; n *= 10, i++)
            {
                arrayListPoints.Add(n, arrayListTime[i]);
                linkedListPoints.Add(n, linkedListTime[i]);
            }

            LineItem curve = pane.AddCurve("ArrayList", arrayListPoints, Color.Blue, SymbolType.None);
            curve = pane.AddCurve("LinkedList", linkedListPoints, Color.Red, SymbolType.None);
            pane.Title.Text = operation;
            zedGraph.AxisChange();
            zedGraph.Invalidate();

        }

        private void button_Click(object sender, EventArgs e)
        {
            if (comboBox.Text == "Get")
            {
                (double[], double[]) tuple = CompareGet();
                DrawGraph(tuple, "Get");

            }
            if (comboBox.Text == "Set")
            {
                (double[], double[]) tuple = CompareSet();
                DrawGraph(tuple, "Set");
            }
            if (comboBox.Text == "Add")
            {
                (double[], double[]) tuple = CompareAdd();
                DrawGraph(tuple, "Add");
            }
            if (comboBox.Text == "AddAt")
            {
                (double[], double[]) tuple = CompareAddAt();
                DrawGraph(tuple, "AddAt");
            }
            if (comboBox.Text == "Remove")
            {
                (double[], double[]) tuple = CompareRemove();
                DrawGraph(tuple, "Remove");
            }
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            button.Visible = true;
        }
    }
}
