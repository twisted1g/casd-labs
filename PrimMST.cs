using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtimAlg
{
    public static class PrimMSTAlgorithm
    {
        const int V = 5;

        public static int FindMinKey(int[] key, bool[] inMST)
        {
            int min = int.MaxValue, minIndex = -1;

            for (int v = 0; v < V; v++)
            {
                if (inMST[v] == false && key[v] < min)
                {
                    min = key[v];
                    minIndex = v;
                }
            }
            return minIndex;
        }

        public static void PrintMST(int[] parent, int[,] graph)
        {
            Console.WriteLine("Ребра минимального остовного дерева:");
            for (int i = 1; i < V; i++)
            {
                Console.WriteLine($"{parent[i]} - {i} \tВес: {graph[i, parent[i]]}");
            }
        }

        public static void PrimMST(int[,] graph)
        {
            int[] parent = new int[V];
            int[] key = new int[V]; 
            bool[] inMST = new bool[V]; 

            for (int i = 0; i < V; i++)
            {
                key[i] = int.MaxValue;
                inMST[i] = false;
            }

            key[0] = 0;
            parent[0] = -1;

            for (int count = 0; count < V - 1; count++)
            {
                int u = FindMinKey(key, inMST);
                inMST[u] = true;

                for (int v = 0; v < V; v++)
                {
                    if (graph[u, v] != 0 && inMST[v] == false && graph[u, v] < key[v])
                    {
                        parent[v] = u;
                        key[v] = graph[u, v];
                    }
                }
            }
            PrintMST(parent, graph);
        }
    }
}
