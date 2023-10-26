using System;
using System.Linq;

namespace Factorial
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите целое число N:");
            int n = Convert.ToInt32(Console.ReadLine());

            double factorial = Enumerable.Range(1, n).Aggregate(1.0, (acc, x) => acc * x);

            Console.WriteLine($"Факториал числа {n} равен {factorial}");
        }
    }
}