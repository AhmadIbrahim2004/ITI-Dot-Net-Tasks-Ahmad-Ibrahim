using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_7_Tasks
{
    #region Extensions
    static class MyExtensions
    {
        public static int CountWords(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))  return 0;
            
            char[] delimiters = new char[] { ' ', ',', ';', ':' };

            return str.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
        }


        public static bool IsEven(this int number)
        {
            return number % 2 == 0;
        }

        public static int CalculateAge(this DateTime birthDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;

            if (age < 0)
            {
                age = 0;
            }
            return age;
        }

        public static string ReverseString(this string str)
        {
            if (str == null) return null;

            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }

    #endregion

    #region Product
    class Product
    {
        public string name { get; set; }
        public double price { get; set; }
    }
    #endregion

    internal class Program
    {
        public static object CreateProduct(string product_name, double product_price)
        {
            Product p = new Product { name = product_name, price = product_price };

            return new { ProductName = p.name, Cost = p.price };
        }
        static void Main(string[] args)
        {
            Console.WriteLine("--- Task 1: Reusable Anonymous Object Creator ---");

            var laptop = CreateProduct("Laptop", 1200.50);
            Console.WriteLine(laptop); 

            var mouse = CreateProduct("Gaming Mouse", 75.99);
            Console.WriteLine(mouse);





            Console.WriteLine("\n--- Task 2: Count Words (Updated) ---");
            string sentence = "Hello, world; this: is a test.";
            Console.WriteLine($"'{sentence}' has {sentence.CountWords()} words.");



            Console.WriteLine("\n--- Task 3: Is Even ---");
            int num1 = 10;
            int num2 = 7;
            Console.WriteLine($"{num1} is even: {num1.IsEven()}"); 
            Console.WriteLine($"{num2} is even: {num2.IsEven()}"); 




            Console.WriteLine("\n--- Task 4: Calculate Age ---");
            DateTime myBirthday = new DateTime(1995, 5, 20);
            Console.WriteLine($"Age calculated from {myBirthday.ToShortDateString()} is {myBirthday.CalculateAge()}.");




            Console.WriteLine("\n--- Task 5: Reverse String ---");
            string original = "hello";
            Console.WriteLine($"'{original}' reversed is '{original.ReverseString()}'.");

        }
    }
}
