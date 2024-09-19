class MyArrayList<T>
{
    T[] elementData;
    int size;

    public MyArrayList()
    {
        elementData = new T[0];
        size = 0;
    }

    public MyArrayList(T[] array)
    {
        this.elementData = array;
    }

    public MyArrayList(int capacity)
    {
        elementData = new T[capacity];
        size = capacity;
    }

    public void PrintMyArrayList()
    {
        foreach (T item in elementData)
        {
            Console.Write(item + " ");
        }
    }

}



partial class Program
{
    public static void Main(string[] args)
    {
        int[] arr = { 1, 2 };
        MyArrayList<int> myArrayList = new MyArrayList<int>(arr);

        myArrayList.PrintMyArrayList();
        arr = new int[]{1, 2, 3, 4 };

        myArrayList.PrintMyArrayList();

    }
}
