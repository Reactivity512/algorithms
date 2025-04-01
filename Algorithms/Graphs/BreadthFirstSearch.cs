namespace Algorithms.Graphs;

public sealed class BreadthFirstSearch
{
    private readonly int vertices; // Количество вершин
    private readonly List<int>[] adjacencyList; // Список смежности

    public BreadthFirstSearch(int v)
    {
        vertices = v;
        adjacencyList = new List<int>[v];
        for (int i = 0; i < v; i++)
        {
            adjacencyList[i] = new List<int>();
        }
    }

    public void AddEdge(int v, int w)
    {
        adjacencyList[v].Add(w);
    }

    public List<int> BFS(int startVertex)
    {
        if (startVertex < 0 || startVertex >= vertices)
        {
            throw new ArgumentOutOfRangeException(nameof(startVertex), 
                $"Vertex index must be between 0 and {vertices - 1}");
        }

        bool[] visited = new bool[vertices];
        Queue<int> queue = new Queue<int>();
        List<int> result = new List<int>();

        visited[startVertex] = true;
        queue.Enqueue(startVertex);

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            result.Add(current);

            foreach (int neighbor in adjacencyList[current])
            {
                if (!visited[neighbor])
                {
                    visited[neighbor] = true;
                    queue.Enqueue(neighbor);
                }
            }
        }

        return result;
    }

    public class BFSResult
    {
        public required List<int> Order { get; set; }
        public required int[] Distances { get; set; }
        public required int[] Predecessors { get; set; }
    }

    /// <summary>Для работы с взвешенными графами лучше использовать алгоритм Дейкстры</summary>
    public BFSResult BFSAdvanced(int startVertex)
    {
        if (startVertex < 0 || startVertex >= vertices)
        {
            throw new ArgumentOutOfRangeException(nameof(startVertex), 
                $"Vertex index must be between 0 and {vertices - 1}");
        }

        bool[] visited = new bool[vertices];
        int[] distances = new int[vertices];
        int[] predecessors = new int[vertices];
        Array.Fill(distances, -1);
        Array.Fill(predecessors, -1);
        
        Queue<int> queue = new Queue<int>();
        List<int> order = new List<int>();

        visited[startVertex] = true;
        distances[startVertex] = 0;
        queue.Enqueue(startVertex);

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            order.Add(current);

            foreach (int neighbor in adjacencyList[current])
            {
                if (!visited[neighbor])
                {
                    visited[neighbor] = true;
                    distances[neighbor] = distances[current] + 1;
                    predecessors[neighbor] = current;
                    queue.Enqueue(neighbor);
                }
            }
        }

        return new BFSResult
        {
            Order = order,
            Distances = distances,
            Predecessors = predecessors
        };
    }
}
