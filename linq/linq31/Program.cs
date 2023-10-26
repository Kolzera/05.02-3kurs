using System;
using System.Linq;

namespace Intersection
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите целое число K:");
            int k = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Введите последовательность строк A:");
            string[] a = Console.ReadLine().Split();

            var intersection = a.Take(k).Intersect(a.SkipWhile(s => !char.IsDigit(s.Last())));

            var sortedIntersection = intersection.OrderBy(s => s.Length).ThenBy(s => s);

            Console.WriteLine("Результат:");
            foreach (var item in sortedIntersection)
            {
                Console.WriteLine(item);
            }
        }
    }
}