using System.Threading.Tasks;

namespace Algorithms
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            int[] array = [64, 34, 25, 12, 22, 11, 90];

            Console.WriteLine("Исходный массив:");
            await Sorting.MergeSortAsync.IterativeSortAsync(array);

            // Сортируем массив
            foreach (int i in array)
            {
                Console.Write($"{i}, ");
            }
        }
    }
}
