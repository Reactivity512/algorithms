using System.Threading.Tasks;

namespace Algorithms
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            // Пример быстрой сортировки
            Console.WriteLine("Пример быстрой сортировки");
            int[] arrayForSort = [5, 9, 6, 3, 8, 2, 7, 1, 4];
            Sorting.QuickSort.IterativeSort(arrayForSort);
            foreach(int i in arrayForSort)
            {
                Console.Write($"{i} ");
            }
            Console.Write("\n\n");


            // Пример бинарного поиска
            Console.WriteLine("Пример бинарного поиска");
            int valueForSearch = 73;
            int[] arrayForSearch = [1, 10, 20, 30, 40, 50, 60, 70, 80, 90];

            var resultBinarySearch = Search.BinarySearch.Search(arrayForSearch, valueForSearch);
            Console.WriteLine($"Значения {valueForSearch} в массиве {(resultBinarySearch == -1 ? "не найдено" : "найдено")}");
            Console.WriteLine();


            // Пример алгоритма Дейкстры
            Console.WriteLine("Пример алгоритма Дейкстры");
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
            Console.WriteLine();


            // Пример бинарное дерево поиска
            Console.WriteLine("Пример бинарное дерево поиска");
            var bst = new Tree.BinarySearchTree();
        
            bst.Insert(50);
            bst.Insert(30);
            bst.Insert(20);
            bst.Insert(40);
            bst.Insert(70);
            bst.Insert(60);
            bst.Insert(80);

            // Обход дерева
            Console.WriteLine($"InOrder обход: {string.Join(" ", bst.InOrder())}"); // 20 30 40 50 60 70 80
            Console.WriteLine($"PreOrder обход: {string.Join(" ", bst.PreOrder())}"); // 50 30 20 40 70 60 80
            Console.WriteLine($"PostOrder обход: { string.Join(" ", bst.PostOrder()) }"); // 20 40 30 60 80 70 50

            // Поиск элементов
            Console.WriteLine("Содержит 40: " + bst.Contains(40)); // True
            Console.WriteLine("Содержит 90: " + bst.Contains(90)); // False

            // Удаление элементов
            bst.Delete(20);
            Console.WriteLine($"После удаления 20 (InOrder): {string.Join(" ", bst.InOrder())}"); // 30 40 50 60 70 80

            bst.Delete(30);
            Console.WriteLine($"После удаления 30 (InOrder): {string.Join(" ", bst.InOrder())}"); // 40 50 60 70 80

            bst.Delete(50);
            Console.WriteLine($"После удаления 50 (InOrder): {string.Join(" ", bst.InOrder())}"); // 40 60 70 80
            Console.WriteLine();
            

            // Пример B-дерево
            Console.WriteLine("Пример B-дерево");
            var bTree = new Tree.BTree(3); // B-дерево степени 3

            bTree.Insert(10);
            bTree.Insert(20);
            bTree.Insert(5);
            bTree.Insert(6);
            bTree.Insert(12);
            bTree.Insert(30);
            bTree.Insert(7);
            bTree.Insert(17);

            Console.WriteLine($"Обход B-дерева: {string.Join(" ", bTree.Traverse())}");
            Console.WriteLine("Содержит 6: " + bTree.Contains(6));
            Console.WriteLine("Содержит 15: " + bTree.Contains(15));

            bTree.Delete(6);
            bTree.Delete(17);

            Console.WriteLine($"После удаления 6 и 17: {string.Join(" ", bTree.Traverse())}");
        }
    }
}
