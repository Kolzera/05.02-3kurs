using System;
using System.Collections.Generic;
using System.Linq;

namespace StringLength
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите последовательность строк A:");
            string[] a = Console.ReadLine().Split();

            var result = a.GroupBy(s => s[0])
                .Select(g => new { FirstLetter = g.Key, SumLength = g.Sum(s => s.Length) })
                .OrderByDescending(r => r.SumLength)
                .ThenBy(r => r.FirstLetter)
                .Select(r => $"{r.SumLength}-{r.FirstLetter}");

            Console.WriteLine("Результат:");
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }
    }
}