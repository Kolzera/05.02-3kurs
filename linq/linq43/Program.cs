using System;
using System.Linq;

namespace ReverseCharacters
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите целое число K:");
            int k = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Введите последовательность строк A:");
            string[] a = Console.ReadLine().Split();

            var characters = a.Take(k)
                .Select(s => new string(s.Where((c, i) => i % 2 == 0).ToArray()))
                .Concat(a.Skip(k).Select(s => new string(s.Where((c, i) => i % 2 != 0).ToArray())))
                .Reverse();

            Console.WriteLine("Результат:");
            foreach (var item in characters)
            {
                Console.WriteLine(item);
            }
        }
    }
}
