using RBTree;
using MyTreeSet;
using System.Collections.Generic;
using System.Collections;

class Program
{
    static void Main(string[] args) 
    {
        MyTreeSet<string> treeSet = new MyTreeSet<string>(new MyComparator());

        string[] lines = File.ReadAllLines("input1.txt");
        var words = lines.SelectMany(line => line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries));
        foreach (string line in words)
        {
            if (line.All(char.IsLetter))
            {
                treeSet.Add(line.ToLower());
            }
        }
        Console.WriteLine(treeSet);
    }
}