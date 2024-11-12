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
            pane.YAxis.Title.Text = "Время выполнения, мс.";
            pane.Title.Text = "Исследование зависимости времени операций от структуры данных";
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
            double[] workTimeArrayList = new double[4];
            double[] workTimeLinkedList = new double[4];
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
                    workTimeArrayList[i] += stopwatch.ElapsedMilliseconds;

                    stopwatch.Restart();
                    arrayList.Get(index);
                    stopwatch.Stop();
                    workTimeLinkedList[i] += stopwatch.ElapsedMilliseconds;
                }
            }
            return (workTimeArrayList, workTimeLinkedList);

        }

        //private double[] CompareSet()
        //{

        //}

        //private double[] CompareAdd()
        //{

        //}

        //private double[] CompareAddAt()
        //{

        //}

        //private double[] CompareRemove()
        //{

        //}

        private void DrawGraph((double[], double[]) tuple, string operation)
        {

        }

        private void button_Click(object sender, EventArgs e)
        {

            if (comboBox.Text == "Get")
            {
                (double[], double[]) tuple = CompareGet();

            }
            if (comboBox.Text == "Set")
            {

            }
            if (comboBox.Text == "Add")
            {

            }
            if (comboBox.Text == "AddAt")
            {

            }
            if (comboBox.Text == "Remove")
            {

            }
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            button.Visible = true;
        }
    }
}
