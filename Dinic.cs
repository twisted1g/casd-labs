using System;
using System.Collections.Generic;
namespace DinicAlg;
class Dinic
{
    private class Edge
    {
        public int To, Capacity, Flow, ReverseIndex;
        public Edge(int to, int capacity, int reverseIndex)
        {
            To = to;
            Capacity = capacity;
            Flow = 0;
            ReverseIndex = reverseIndex;
        }
    }

    private int[] level;
    private List<Edge>[] graph;
    private int source, sink;

    public Dinic(int vertexCount)
    {
        graph = new List<Edge>[vertexCount];
        for (int i = 0; i < vertexCount; i++)
        {
            graph[i] = new List<Edge>();
        }
    }

    public void AddEdge(int from, int to, int capacity)
    {
        graph[from].Add(new Edge(to, capacity, graph[to].Count));
        graph[to].Add(new Edge(from, 0, graph[from].Count - 1)); 
    }

    private bool BFS()
    {
        level = new int[graph.Length];
        Array.Fill(level, -1);

        level[source] = 0;
        Queue<int> queue = new Queue<int>();
        queue.Enqueue(source);

        while (queue.Count > 0)
        {
            int node = queue.Dequeue();
            foreach (var edge in graph[node])
            {
                if (level[edge.To] < 0 && edge.Flow < edge.Capacity)
                {
                    level[edge.To] = level[node] + 1;
                    queue.Enqueue(edge.To);
                    if (edge.To == sink)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private int DFS(int node, int flow)
    {
        if (node == sink)
        {
            return flow;
        }

        foreach (var edge in graph[node])
        {
            if (level[edge.To] == level[node] + 1 && edge.Flow < edge.Capacity)
            {
                int currentFlow = Math.Min(flow, edge.Capacity - edge.Flow);
                int pushedFlow = DFS(edge.To, currentFlow);
                if (pushedFlow > 0)
                {
                    edge.Flow += pushedFlow;
                    graph[edge.To][edge.ReverseIndex].Flow -= pushedFlow; 
                    return pushedFlow;
                }
            }
        }
        return 0;
    }

    public int MaxFlow(int s, int t)
    {
        source = s;
        sink = t;
        int totalFlow = 0;

        while (BFS())
        {
            int flow;
            while ((flow = DFS(source, int.MaxValue)) != 0)
            {
                totalFlow += flow;
            }
        }
        return totalFlow;
    }
}
