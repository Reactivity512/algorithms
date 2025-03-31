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
    private void DFSUtil(int v, bool[] visited)
    {
        // Помечаем текущую вершину как посещенную
        visited[v] = true;
        Console.Write(v + " ");

        // Рекурсивно посещаем все смежные вершины
        foreach (int neighbor in adjacencyList[v])
        {
            if (!visited[neighbor])
            {
                DFSUtil(neighbor, visited);
            }
        }
    }

    // Метод для запуска DFS
    public void DFSRecursive(int startVertex)
    {
        if (startVertex < 0 || startVertex >= vertices)
        {
            throw new ArgumentOutOfRangeException(nameof(startVertex), 
                $"Vertex index must be between 0 and {vertices - 1}");
        }

        // Массив для отслеживания посещенных вершин
        bool[] visited = new bool[vertices];

        DFSUtil(startVertex, visited);
    }

    public void DFSIterative(int startVertex)
    {
        if (startVertex < 0 || startVertex >= vertices)
        {
            throw new ArgumentOutOfRangeException(nameof(startVertex), 
                $"Vertex index must be between 0 and {vertices - 1}");
        }
        
        bool[] visited = new bool[vertices];
        Stack<int> stack = new Stack<int>();

        visited[startVertex] = true;
        stack.Push(startVertex);

        while (stack.Count > 0)
        {
            int current = stack.Pop();
            Console.Write(current + " ");

            // Обрабатываем соседей в обратном порядке для соответствия рекурсивной версии
            for (int i = adjacencyList[current].Count - 1; i >= 0; i--)
            {
                int neighbor = adjacencyList[current][i];
                if (!visited[neighbor])
                {
                    visited[neighbor] = true;
                    stack.Push(neighbor);
                }
            }
        }
    }
}
