namespace CliquesAlg;
class Graph
{
    private List<List<int>> adjacencyList;

    public Graph(int vertices)
    {
        adjacencyList = new List<List<int>>(vertices);
        for (int i = 0; i < vertices; i++)
        {
            adjacencyList.Add(new List<int>());
        }
    }

    public void AddEdge(int source, int destination)
    {
        adjacencyList[source].Add(destination);
        adjacencyList[destination].Add(source);
    }

    public void MergeCliques(List<int> cliqueMembers)
    {
        if (cliqueMembers == null || cliqueMembers.Count == 0) return;

        HashSet<int> mergedClique = new HashSet<int>(cliqueMembers);

        foreach (var member in cliqueMembers)
        {
            foreach (var neighbor in adjacencyList[member])
            {
                mergedClique.Add(neighbor);
            }
        }

        Console.WriteLine("Объединенная клика:");
        foreach (var member in mergedClique)
        {
            Console.Write(member + " ");
        }
        Console.WriteLine();
    }

    public void PrintGraph()
    {
        for (int i = 0; i < adjacencyList.Count; i++)
        {
            Console.Write(i + ": ");
            foreach (var neighbor in adjacencyList[i])
            {
                Console.Write(neighbor + " ");
            }
            Console.WriteLine();
        }
    }
}