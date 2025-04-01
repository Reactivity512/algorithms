namespace Algorithms.Graphs;

public sealed class DepthFirstSearch
{
    private readonly int vertices; // Количество вершин
    private readonly List<int>[] adjacencyList; // Список смежности

    public DepthFirstSearch(int v)
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

    // Рекурсивный метод DFS
    private void DFSUtil(int current, bool[] visited, List<int> result)
    {
        // Помечаем текущую вершину как посещенную
        visited[current] = true;
        result.Add(current);

        // Рекурсивно посещаем все смежные вершины
        foreach (int neighbor in adjacencyList[current])
        {
            if (!visited[neighbor])
            {
                DFSUtil(neighbor, visited, result);
            }
        }
    }

    // Метод для запуска DFS
    public List<int> DFSRecursive(int startVertex)
    {
        if (startVertex < 0 || startVertex >= vertices)
        {
            throw new ArgumentOutOfRangeException(nameof(startVertex), 
                $"Vertex index must be between 0 and {vertices - 1}");
        }

        // Массив для отслеживания посещенных вершин
        bool[] visited = new bool[vertices];
        var result = new List<int>(vertices);

        DFSUtil(startVertex, visited, result);

        return result;
    }

    public List<int> DFSIterative(int startVertex)
    {
        if (startVertex < 0 || startVertex >= vertices)
        {
            throw new ArgumentOutOfRangeException(nameof(startVertex), 
                $"Vertex index must be between 0 and {vertices - 1}");
        }
        
        bool[] visited = new bool[vertices];
        var result = new List<int>(vertices);
        var stack = new Stack<int>();
        stack.Push(startVertex);

        while (stack.Count > 0)
        {
            int current = stack.Pop();
            if (visited[current]) continue;
            
            visited[current] = true;
            result.Add(current);
            
            for (int i = adjacencyList[current].Count - 1; i >= 0; i--)
            {
                int neighbor = adjacencyList[current][i];
                if (!visited[neighbor])
                {
                    stack.Push(neighbor);
                }
            }
        }

        return result;
    }
}
