using RBTree;
using MyTreeSet;

class Program
{
    static void Main(string[] args)
    {
        //RBTree<int> tree = new RBTree<int> { };

        //tree.Insert(1);
        //tree.Insert(2);
        //tree.Insert(7);
        //tree.Insert(3);
        //tree.Insert(4);
        //tree.Insert(5);
        //tree.Insert(16);
        //tree.Insert(0);
        //Console.WriteLine(tree);
        //Console.WriteLine(tree.Root!.Value);

        //Console.WriteLine(tree.Contains(1));
        //Console.WriteLine(tree.Contains(6));
        //Console.WriteLine(tree.Contains(9));
        //Console.WriteLine(tree.Contains(19));
        //Console.WriteLine(tree.Contains(0));
        //Console.WriteLine(tree.Contains(-1));

        //tree.Remove(7);
        //Console.WriteLine(tree);
        //Console.WriteLine(tree.Root!.Value);


        MyTreeSet<int> treeSet = new MyTreeSet<int>();
        treeSet.Add(1);
        treeSet.Add(2);
        treeSet.AddAll(new int[] { 3, 4, 5, 6 });
        Console.WriteLine(treeSet);

        //treeSet.Clear();
        //Console.WriteLine(treeSet);

        Console.WriteLine(treeSet.ContainsAll(new int[] { 1, 2, 3 }));
        Console.WriteLine(treeSet.Contains(7));
        treeSet.AddAll(new int[] { 7, 8, 9 });
        //treeSet.Remove(5);
        int[] array = treeSet.ToArray();
        foreach (int i in array) Console.Write(i + " ");
        Console.WriteLine();

        Console.WriteLine(treeSet.First());
        Console.WriteLine(treeSet.Last());

        var it = treeSet.DescendingIterator();
        while (it.MoveNext()) Console.WriteLine(it.Current.Value);

    }
}