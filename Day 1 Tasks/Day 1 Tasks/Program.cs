using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_1_Tasks
{
    internal class Program
    {

        static void Main(string[] args)
        {
            while (true) // Loop to show the menu repeatedly
            {
                Console.Clear();
                Console.WriteLine("## Choose a Task to Run ##");
                Console.WriteLine("===========================================");
                Console.WriteLine("1: Character to ASCII");
                Console.WriteLine("2: ASCII to Character");
                Console.WriteLine("3: Even or Odd");
                Console.WriteLine("4: Simple Calculator");
                Console.WriteLine("5: Student Grade Calculator");
                Console.WriteLine("6: Multiplication Table");
                Console.WriteLine("0: Exit");
                Console.WriteLine("===========================================");
                Console.Write("\nEnter your choice: ");

                string choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1":
                        Task1_Character_to_ASCII.Task1();
                        break;
                    case "2":
                        Task2_ASCII_to_Character.Task2();
                        break;
                    case "3":
                        Task3_Even_or_Odd.Task3();
                        break;
                    case "4":
                        Task4_Calculator.Task4();
                        break;
                    case "5":
                        Task5_Student_Grade.Task5();
                        break;
                    case "6":
                        Task6_Multiplication_Table.Task6();
                        break;
                    case "0":
                        Console.WriteLine("Exiting program. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ReadKey();
            }
        }
    }
}
