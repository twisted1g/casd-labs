using RBTree;
using MyTreeSet;
using System.Collections.Generic;
using System.Collections;

class MyComparator : IComparer<string>
{
    public int Compare(string? x, string? y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1; 
        if (y == null) return 1; 

        string[] firstString = x.Split(' ');
        string[] secondString = y.Split(' ');

        Array.Sort(firstString, new MyStringComparator());
        Array.Sort(secondString, new MyStringComparator());

        int len = Math.Min(firstString.Length, secondString.Length);
        for (int i = 0; i < len; i++)
        {
            int result = firstString[i].Length.CompareTo(secondString[i].Length);
            if (result != 0) return result;
        }
        return firstString.Length.CompareTo(secondString.Length);
    }
}

class MyStringComparator : IComparer<string>
{
    public int Compare(string? x, string? y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;

        return x.Length.CompareTo(y.Length);
    }
}

class Program
{
    static void Main(string[] args) 
    {
        MyTreeSet<string> treeSet = new MyTreeSet<string>(new MyComparator());
        using (StreamReader reader = new StreamReader("input.txt"))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                treeSet.Add(line);
            }
        }
        Console.WriteLine(treeSet);
    }
}