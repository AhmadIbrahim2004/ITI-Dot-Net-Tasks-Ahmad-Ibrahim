using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_1_Tasks
{
    internal class Task2_ASCII_to_Character
    {
        public static void Task2()
        {
            Console.Write("Enter an ASCII code: ");
            int ascii_code = int.Parse(Console.ReadLine());

            char character = (char)ascii_code;

            Console.WriteLine($"The character for ASCII code {ascii_code} is: '{character}'");
        }
    }
}
