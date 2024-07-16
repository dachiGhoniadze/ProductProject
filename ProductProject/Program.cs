using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Product> products = new List<Product>();

            Console.WriteLine("Enter amount of products: ");
            int numOfProducts = int.Parse(Console.ReadLine());

            for (int i = 0; i < numOfProducts; i++)
            {
                Console.WriteLine($"Enter details for product {i + 1}:");

                Console.Write("Name: ");
                string name = Console.ReadLine();

                Console.Write("Unique Number: ");
                int uniqueNumber = int.Parse(Console.ReadLine());

                Console.Write("Unit Price: ");
                decimal unitPrice = decimal.Parse(Console.ReadLine());

                Console.Write("Unit Size: ");
                string unitSize = Console.ReadLine();

                Console.Write("Release Date, enter in this format (yyyy-mm-dd): ");
                DateTime releaseDate = DateTime.Parse(Console.ReadLine());

                Console.Write("Term (number of days): ");
                int term = int.Parse(Console.ReadLine());

                Product product = new Product(name, uniqueNumber, unitPrice, unitSize, releaseDate, term);
                products.Add(product);
            }

            Console.WriteLine("\nProducts Info:");
            foreach (Product product in products)
            {
                product.PrintData();
                Console.WriteLine($"Expired: {(product.IsExpired ? "Yes" : "No")}");
                Console.WriteLine();
            }


            var expired = products
                .Where(p => p.IsExpired)
                .GroupBy(p => p.UniqueNumber)
                .Select(g => new {
                    UniqueCode = g.Key,
                    Expired = g.Count()
                })
                .ToList();

            int sumOfExpired = expired.Sum(g => g.Expired);

            Console.WriteLine("============================");
            Console.WriteLine($"Vadagasuli produktebis jami {sumOfExpired}");
            Console.WriteLine("Vadagasuli produktebis kodebi");
            foreach (var group in expired)
            {
                Console.WriteLine($"Unique Code: {group.UniqueCode}");
            }

            var topFive = products
                .OrderByDescending(p => p.UnitPrice)
                .Take(5);
            Console.WriteLine("============================");
            Console.WriteLine("Top Five expensive products");
            foreach (var product in topFive)
            {
                product.PrintData();
                Console.WriteLine();
            }

            var cheapestProduct = products
                .Where(p => p.GetReleaseDate().Month == 12)
                .OrderBy(p => p.UnitPrice) 
                .FirstOrDefault();

            Console.WriteLine("============================");
            Console.WriteLine("Davaleba #3");
            if (cheapestProduct != null)
            {
                Console.WriteLine("Most cheapest product in December");
                Console.WriteLine($"Unique Code: {cheapestProduct.UniqueNumber}");
                Console.WriteLine($"Name: {cheapestProduct.Name}");
                int stock = products.Count();
                Console.WriteLine($"Amount of stock of all products: {stock}");
            }
            else
            {
                Console.WriteLine("Dekemberi carieli!");
            }


            Console.ReadKey();
        }
    }
}
