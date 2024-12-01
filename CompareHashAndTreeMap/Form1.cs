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

            pane.XAxis.Scale.Min = 1000;
            pane.XAxis.Scale.Max = 10000000;


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

        private void FillHashMap(ref MyHashMap<int, int> hashMap, int[] array)
        {
            foreach (int item in array)
            {
                hashMap.Put(item, item);
            }
        }

        private void FillTreeMap(ref MyTreeMap<int, int> treeMap, int[] array)
        {
            foreach (int item in array)
            {
                treeMap.Put(item, item);
            }
        }

        private (double[], double[]) CompareGet()
        {
            int currentSize = 100;
            double[] workTimeHashMap = new double[4] {0, 0, 0, 0 };
            double[] workTimeTreeMap = new double[4] { 0, 0, 0, 0 };
            for (int i = 0; i < 4; i++, currentSize *= 10)
            {
                int[] array = GenerateIntArray(currentSize);
                MyHashMap<int, int> hashMap = new MyHashMap<int, int>();
                FillHashMap(ref hashMap, array);

                MyTreeMap<int, int> treeMap = new MyTreeMap<int, int>();
                FillTreeMap(ref treeMap, array);

                for (int j = 0; j < startTimes; j++)
                {
                    Random random = new Random();
                    int index = random.Next(currentSize); 

                    Stopwatch stopwatch = new Stopwatch();

                    stopwatch.Start();
                    hashMap.Get(index);
                    stopwatch.Stop();
                    workTimeHashMap[i] += stopwatch.Elapsed.Ticks;

                    stopwatch.Restart();
                    treeMap.Get(index);
                    stopwatch.Stop();
                    workTimeTreeMap[i] += stopwatch.Elapsed.Ticks;
                }
                workTimeHashMap[i] /= startTimes;
                workTimeTreeMap[i] /= startTimes;

            }

            return (workTimeHashMap, workTimeTreeMap);

        }

        private (double[], double[]) ComparePut()
        {
            int currentSize = 100;
            double[] workTimeHashMap = new double[4] {0, 0, 0, 0 };
            double[] workTimeTreeMap = new double[4] { 0, 0, 0, 0 };
            for (int i = 0; i < 4; i++, currentSize *= 10)
            {
                int[] array = GenerateIntArray(currentSize);
                MyHashMap<int, int> hashMap = new MyHashMap<int, int>();
                FillHashMap(ref hashMap, array);

                MyTreeMap<int, int> treeMap = new MyTreeMap<int, int>();
                FillTreeMap(ref treeMap, array);

                for (int j = 0; j < startTimes; j++)
                {
                    Random random = new Random();
                    int index = random.Next(currentSize); 

                    Stopwatch stopwatch = new Stopwatch();

                    stopwatch.Start();
                    hashMap.Put(index, index);
                    stopwatch.Stop();
                    workTimeHashMap[i] += stopwatch.Elapsed.Ticks;

                    stopwatch.Restart();
                    treeMap.Put(index, index);
                    stopwatch.Stop();
                    workTimeTreeMap[i] += stopwatch.Elapsed.Ticks;
                }
                workTimeHashMap[i] /= startTimes;
                workTimeTreeMap[i] /= startTimes;

            }

            return (workTimeHashMap, workTimeTreeMap);

        }


        private (double[], double[]) CompareRemove()
        {
            int currentSize = 100;
            double[] workTimeHashMap = new double[4] { 0, 0, 0, 0 };
            double[] workTimeTreeMap = new double[4] { 0, 0, 0, 0 };
            for (int i = 0; i < 4; i++, currentSize *= 10)
            {
                int[] array = GenerateIntArray(currentSize);
                MyHashMap<int, int> hashMap = new MyHashMap<int, int>();
                FillHashMap(ref hashMap, array);

                MyTreeMap<int, int> treeMap = new MyTreeMap<int, int>();
                FillTreeMap(ref treeMap, array);

                for (int j = 0; j < startTimes; j++)
                {
                    Random random = new Random();
                    int index = random.Next(currentSize);

                    Stopwatch stopwatch = new Stopwatch();

                    stopwatch.Start();
                    hashMap.Remove(index);
                    stopwatch.Stop();
                    workTimeHashMap[i] += stopwatch.Elapsed.Ticks;

                    stopwatch.Restart();
                    treeMap.Remove(index);
                    stopwatch.Stop();
                    workTimeTreeMap[i] += stopwatch.Elapsed.Ticks;
                }
                workTimeHashMap[i] /= startTimes;
                workTimeTreeMap[i] /= startTimes;

            }

            return (workTimeHashMap, workTimeTreeMap);

        }

        private void DrawGraph((double[], double[]) tuple, string operation)
        {
            GraphPane pane = zedGraph.GraphPane;
            pane.CurveList.Clear();

            PointPairList hashMapPoints = new PointPairList();
            PointPairList treeMapPoints = new PointPairList();

            var (hashMapTime, treeMapTime) = tuple;
            
            for (int n = 100000, i = 0; i < 4; n *= 10, i++)
            {
                hashMapPoints.Add(n, hashMapTime[i]);
                treeMapPoints.Add(n, treeMapTime[i]);
            }

            LineItem curve = pane.AddCurve("HashMap", hashMapPoints, Color.Blue, SymbolType.None);
            curve = pane.AddCurve("TreeMap", treeMapPoints, Color.Red, SymbolType.None);
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

            
            if (comboBox.Text == "Put")
            {
                (double[], double[]) tuple = ComparePut();
                DrawGraph(tuple, "Add");
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
