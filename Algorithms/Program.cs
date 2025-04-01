using System.Threading.Tasks;

namespace Algorithms
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            // Пример быстрой сортировки
            int[] arrayForSort = [5, 9, 6, 3, 8, 2, 7, 1, 4];
            Sorting.QuickSort.IterativeSort(arrayForSort);
            foreach(int i in arrayForSort)
            {
                Console.Write($"{i} ");
            }
            Console.Write("\n");


            // Пример бинарного поиска
            int valueForSearch = 73;
            int[] arrayForSearch = [1, 10, 20, 30, 40, 50, 60, 70, 80, 90];

            var resultBinarySearch = Search.BinarySearch.Search(arrayForSearch, valueForSearch);
            Console.WriteLine($"Значения {valueForSearch} в массиве {(resultBinarySearch == -1 ? "не найдено" : "найдено")}");


            // Пример алгоритма Дейкстры
            var graph = new Dictionary<int, Dictionary<int, int>>
            {
                [0] = new Dictionary<int, int> { [1] = 4, [2] = 1 }, // поправить отрицательные веса
                [1] = new Dictionary<int, int> { [3] = 1 },
                [2] = new Dictionary<int, int> { [1] = 2, [3] = 5 },
                [3] = new Dictionary<int, int> { }
            };

            var dijkstra = new Graphs.DijkstraAlgorithm(graph);

            var paths = dijkstra.FindPaths(0);
            var shortestPaths = dijkstra.FindShortestPaths(0);
            
            Console.WriteLine("Кратчайшие расстояния от вершины 0:");
            foreach (var (vertex, distance) in shortestPaths)
            {
                Console.WriteLine($"До {vertex}: {distance}");
            }
            
            Console.WriteLine("Пути от вершины 0:");
            foreach (var (vertex, path) in paths)
            {
                Console.WriteLine($"До {vertex}: [{string.Join(" -> ", path)}]");
            }
        }
    }
}
