using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


struct Time
{
    public int hours;
    public int minutes;
    public int seconds;

    public void Print()
    {
        Console.WriteLine($"{this.hours:D2}H:{this.minutes:D2}M:{this.seconds:D2}S");
    }
}
namespace Day_2_Tasks
{

    class Task1_Print_student_names
    {
        public static void Task1()
        {
            Console.Write("Enter the number of students: ");

            int number_of_Students = int.Parse(Console.ReadLine());

            string[] studentNames = new string[number_of_Students];

            for (int i = 0; i < number_of_Students; i++)
            {
                Console.Write($"Enter the name of student number {i + 1}: ");
                studentNames[i] = Console.ReadLine();
            }
            Console.WriteLine("\n--- List of Student Names ---");
            foreach (string name in studentNames)
            {
                Console.WriteLine($"- {name}");
            }
        }
    }

    class Task2_Student_Ages
    {
        public static void Task2()
        {
            Console.Write("How many tracks are there? ");

            int number_of_Tracks = int.Parse(Console.ReadLine());
            int[][] tracks = new int[number_of_Tracks][];

            for (int i = 0; i < number_of_Tracks; i++)
            {
                Console.Write($"\nHow many students are in track #{i + 1}? ");

                int number_of_Students = int.Parse(Console.ReadLine());
                tracks[i] = new int[number_of_Students];

                Console.WriteLine($"--- Enter ages for track #{i + 1} ---");

                for (int j = 0; j < number_of_Students; j++)
                {
                    Console.Write($"Enter age for student #{j + 1}: ");
                    tracks[i][j] = int.Parse(Console.ReadLine());
                }
            }
            Console.WriteLine("\n\n--- Student Ages & Track Averages ---");

            for (int i = 0; i < tracks.Length; i++)
            {
                Console.WriteLine($"\nTrack #{i + 1} Ages: [{string.Join(", ", tracks[i])}]");

                if (tracks[i].Length > 0) Console.WriteLine($"Average age for this track: {tracks[i].Average():F2}");

                else Console.WriteLine("No students in this track to calculate an average.");



            }
        }
    }

    class Task3_Custom_Time
    {
        public static void Task3()
        {
            Time currentTime = new Time { hours = 22, minutes = 33, seconds = 11 };
            Console.Write("The current time is: ");
            currentTime.Print();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("## Choose a Task to Run ##");
                Console.WriteLine("==================================");
                Console.WriteLine("1: Store and Print Student Names");
                Console.WriteLine("2: Student Ages by Track & Average");
                Console.WriteLine("3: Custom Time Struct Demo");
                Console.WriteLine("0: Exit");
                Console.WriteLine("==================================");
                Console.Write("\nEnter your choice: ");

                string choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1":
                        Task1_Print_student_names.Task1();
                        break;
                    case "2":
                        Task2_Student_Ages.Task2();
                        break;
                    case "3":
                        Task3_Custom_Time.Task3();
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
