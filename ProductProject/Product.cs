using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductProject
{
    public class Product
    {
        public string Name;
        public int UniqueNumber;

        private decimal _unitPrice;
        private string _unitSize;
        private DateTime _releaseDate;
        private int _term;
        private int[] _discountSequence;
        public Product(string name, int uniqueNumber, decimal unitPrice, string unitSize, DateTime releaseDate, int term)
        {
            Name = name;
            UniqueNumber = uniqueNumber;
            _unitPrice = unitPrice;
            _unitSize = unitSize;
            _releaseDate = releaseDate;
            _term = term;
            _discountSequence = new int[12];
            GenerateRandomDiscounts();
        }
        private void GenerateRandomDiscounts()
        {
            Random random = new Random();
            for (int i = 0; i < _discountSequence.Length; i++)
            {
                _discountSequence[i] = random.Next(10, 51);
            }
        }
        public int GetDiscountForMonth(int month)
        {
            if (month < 1 || month > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(month), "Month must be between 1 and 12.");
            }
            return _discountSequence[month - 1];
        }
        public void PrintData()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Unique Number: {UniqueNumber}");
            Console.WriteLine($"Unit Price: {_unitPrice:C}");
            Console.WriteLine($"Unit Size: {_unitSize}");
            Console.WriteLine($"Release Date: {_releaseDate.ToShortDateString()}");
            Console.WriteLine($"Term: {_term} days");

            Console.WriteLine("Discounts:");
            for (int i = 0; i < _discountSequence.Length; i++)
            {
                Console.WriteLine($"  Month {i + 1}: {_discountSequence[i]}%");
            }
        }
        public bool IsExpired
        {
            get
            {
                return DateTime.Now > _releaseDate.AddDays(_term);
            }
        }
        
        public decimal UnitPrice
        {
            get { return _unitPrice; }
        }

        public DateTime GetReleaseDate()
        {
            return _releaseDate;
        }


    }
}
