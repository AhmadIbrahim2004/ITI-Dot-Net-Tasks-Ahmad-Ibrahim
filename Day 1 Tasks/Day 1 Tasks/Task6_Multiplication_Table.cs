using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_1_Tasks
{
    internal class Task6_Multiplication_Table
    {
        public static void Task6()
        {
            Console.Write("Enter a number to display its multiplication table: ");
            int table_Number = int.Parse(Console.ReadLine());

            Console.WriteLine($"\nMultiplication table for {table_Number}:");
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine($"{table_Number} x {i} = {table_Number * i}");
            }
        }
    }
}
