using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_1_Tasks
{
    internal class Task5_Student_Grade
    {
        public static void Task5()
        {
            Console.Write("Enter the student's degree [0-100]: ");
            double degree = double.Parse(Console.ReadLine());

            if (degree >= 90 && degree <= 100)  Console.WriteLine("Grade: A (Excellent)");

            else if (degree >= 80 && degree < 90) Console.WriteLine("Grade: B (Very Good)");
            
            else if (degree >= 70 && degree < 80) Console.WriteLine("Grade: C (Good)");
            
            else if (degree >= 60 && degree < 70) Console.WriteLine("Grade: D (Pass)");
            
            else if (degree >= 0 && degree < 60) Console.WriteLine("Grade: F (Fail)");
           
            else Console.WriteLine("Invalid degree. Please enter a value between 0 and 100.");
            
        }
    }
}
