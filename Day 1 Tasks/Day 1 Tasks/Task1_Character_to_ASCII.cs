using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_1_Tasks
{
    internal class Task1_Character_to_ASCII
    {
        public static void Task1()
        {
            Console.Write("Enter a single character: ");
            char character = Console.ReadKey().KeyChar;

            int ascii_value = (int)character;

            Console.WriteLine($"\nThe ASCII code for '{character}' is: {ascii_value}");
        }
    }
}
