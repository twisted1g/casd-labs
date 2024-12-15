using System;
using System.Collections.Generic;
using PtimAlg;
using DinicAlg;
using CliquesAlg;
class Program
{
    static void Main(string[] args)
    {
        int N = 8;
        Console.WriteLine((N - 1) % 9 + 1);
        Console.WriteLine((N - 1) % 3 + 10);
        Console.WriteLine((N - 1) % 6 + 13);
        //8 
        Console.WriteLine($"Задача:{(N - 1) % 9 + 1}");
        int[,] graph1 = new int[,] {
            { 0, 2, 0, 6, 0 },
            { 2, 0, 3, 8, 5 },
            { 0, 3, 0, 0, 7 },
            { 6, 8, 0, 0, 9 },
            { 0, 5, 7, 9, 0 }
        };

        PrimMSTAlgorithm.PrimMST(graph1);

        //11
        Console.WriteLine($"Задача:{(N - 1) % 3 + 10}");
        Dinic dinic = new Dinic(6); 
        dinic.AddEdge(0, 1, 16);
        dinic.AddEdge(0, 2, 13);
        dinic.AddEdge(1, 2, 10);
        dinic.AddEdge(1, 3, 12);
        dinic.AddEdge(2, 1, 4);
        dinic.AddEdge(2, 4, 14);
        dinic.AddEdge(3, 2, 9);
        dinic.AddEdge(3, 5, 20);
        dinic.AddEdge(4, 3, 7);
        dinic.AddEdge(4, 5, 4);

        int maxFlow = dinic.MaxFlow(0, 5);
        Console.WriteLine($"Максимальный поток: {maxFlow}");

        //14
        Console.WriteLine($"Задача:{(N - 1) % 6 + 13}");
        Graph graph = new Graph(6);

        graph.AddEdge(0, 1);
        graph.AddEdge(0, 2);
        graph.AddEdge(1, 2);
        graph.AddEdge(3, 4);
        graph.AddEdge(4, 5);

        Console.WriteLine("Исходный граф:");
        graph.PrintGraph();

        List<int> cliqueMembers = new List<int> { 0, 1 };
        graph.MergeCliques(cliqueMembers);
    }
}
