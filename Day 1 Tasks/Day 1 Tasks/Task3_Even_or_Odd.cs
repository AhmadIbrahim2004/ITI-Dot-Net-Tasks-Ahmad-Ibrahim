using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_1_Tasks
{
    internal class Task3_Even_or_Odd
    {
        public static void Task3()
        {
            Console.Write("Please enter a number: ");
            int number = int.Parse(Console.ReadLine());
            if ((number & 1) == 1)  Console.WriteLine($"The number {number} is Odd.");
            else  Console.WriteLine($"The number {number} is Even.");
            
        }
    }
}
