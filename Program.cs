using MyPriorQueue;


class Bid : IComparable<Bid>
{
    private int _bidPriority;
    private int _bidNumber;
    private int _bidStep;
    private int _bidTime;
    public int TimeAdded
    {
        get { return _bidTime; }
        private set { _bidTime = value; }
    }

    public int Priority
    {
        get { return _bidPriority; }
        private set { _bidPriority = value; }
    }

    public Bid(int number, int step, int time)
    {
        Random rnd = new Random();
        Priority = rnd.Next(1, 6);
        _bidNumber = number;
        _bidStep = step;
        _bidTime = time;
    }

    public int CompareTo(Bid? other)
    {
        if (other is null)
            throw new ArgumentNullException();

        return Priority.CompareTo(other.Priority);
    }

    public override string ToString()
    {
        return $"Номер заявки: {_bidNumber} Приоритет: {Priority} Номер шага: {_bidStep} Время добавления: {TimeAdded}";
    }
}


class MyComparator : IComparer<Bid>
{
    public int Compare(Bid? first, Bid? second)
    {
        if (first is null || second is null)
        {
            throw new ArgumentNullException();
        }
        return first.CompareTo(second);
    }


    class Progrm
    {
        public static void WriteLog(Bid bid, string operation = "")
        {
            using (StreamWriter writer = new StreamWriter("logs.txt", true))
            {
                writer.WriteLine(operation + " " + bid);
            }
        }



        public static void Main(string[] args)
        {
            int N = Convert.ToInt32(Console.ReadLine());
            MyComparator comparator = new MyComparator();
            MyPriorityQueue<Bid> queue = new MyPriorityQueue<Bid>(10, comparator);
            
            int numeration = 0;
            int allTime = 0;
            Bid bid;

            int maxTime = 0;
            Bid maxBid = default;

            for (int i = 0; i < N; i++)
            {
                Random rnd = new Random();
                int newCounter = rnd.Next(1, 11);
                for (int j = 0; j < newCounter; j++)
                {
                    bid = new Bid(++numeration, i, ++allTime);
                    queue.Add(bid);
                    WriteLog(bid, "ADD");
                }
                bid = queue.Poll()!;
                allTime++;
                WriteLog(bid, "REMOVE");
                
                if (allTime - bid.TimeAdded > maxTime)
                {
                    maxTime = allTime - bid.TimeAdded;
                    maxBid = bid;
                }
            }

            while (queue.Size != 0)
            {
                bid = queue.Poll()!;
                allTime++;
                WriteLog(bid, "REMOVE");
                if (allTime - bid.TimeAdded > maxTime)
                {
                    maxTime = allTime - bid.TimeAdded;
                    maxBid = bid;
                }
            }
            WriteLog(maxBid!, "MAX");
        }
    }
}