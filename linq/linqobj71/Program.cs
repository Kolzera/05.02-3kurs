using System;
using System.Collections.Generic;
using System.Linq;

namespace MaxDiscount
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите данные о потребителях:");
            List<string> consumers = new List<string>();

            string input = Console.ReadLine();
            while (!string.IsNullOrEmpty(input))
            {
                consumers.Add(input);
                input = Console.ReadLine();
            }

            Console.WriteLine("Введите данные о магазинах:");
            List<string> shops = new List<string>();

            input = Console.ReadLine();
            while (!string.IsNullOrEmpty(input))
            {
                shops.Add(input);
                input = Console.ReadLine();
            }

            var result = shops.Select(s => s.Split())
                .Select(s => new
                {
                    ShopName = s[2],
                    ConsumerDiscounts = consumers.Where(c => c.Split()[0] == s[0])
                        .Select(c => new
                        {
                            ConsumerCode = c.Split()[0],
                            BirthYear = c.Split()[1],
                            Discount = int.Parse(c.Split()[2])
                        })
                        .GroupBy(c => c.Discount)
                        .OrderByDescending(g => g.Key)
                        .First()
                        .OrderBy(c => c.ConsumerCode)
                        .First()
                })
                .OrderBy(r => r.ShopName);

            if (result.Any())
            {
                Console.WriteLine("Результат:");
                foreach (var item in result)
                {
                    Console.WriteLine($"{item.ShopName} {item.ConsumerDiscounts.ConsumerCode} {item.ConsumerDiscounts.BirthYear} {item.ConsumerDiscounts.Discount}");
                }
            }
            else
            {
                Console.WriteLine("Данные о потребителях или магазинах отсутствуют");
            }
        }
    }
}
