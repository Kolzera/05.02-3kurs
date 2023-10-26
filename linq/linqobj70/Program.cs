using System;
using System.Collections.Generic;
using System.Linq;

namespace BestStudents
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите данные об оценках учащихся:");
            List<string> data = new List<string>();

            string input = Console.ReadLine();
            while (!string.IsNullOrEmpty(input))
            {
                data.Add(input);
                input = Console.ReadLine();
            }

            var result = data.Select(s => s.Split())
                .Where(s => int.Parse(s[0]) == 5 && int.Parse(s[1]) >= 2 && int.Parse(s[1]) <= 5)
                .GroupBy(s => int.Parse(s[1]))
                .Select(g => new
                {
                    Class = g.Key,
                    BestStudents = g.GroupBy(s => s[2] + " " + s[3])
                        .Select(g2 => new
                        {
                            FullName = g2.Key,
                            TotalFives = g2.Sum(s => int.Parse(s[0]))
                        })
                        .Where(s => s.TotalFives > 0)
                        .OrderBy(s => s.FullName)
                })
                .Where(g => g.BestStudents.Any())
                .OrderBy(g => g.Class);

            if (result.Any())
            {
                Console.WriteLine("Результат:");
                foreach (var item in result)
                {
                    foreach (var student in item.BestStudents)
                    {
                        Console.WriteLine($"{item.Class} {student.FullName} {student.TotalFives}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Требуемые учащиеся не найдены");
            }
        }
    }
}
