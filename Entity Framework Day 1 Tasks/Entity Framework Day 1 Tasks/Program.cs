using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Framework_Day_1_Tasks
{
    public class Subject
    {
        public int Code { get; set; }
        public string Name { get; set; }
    }

    public class Student
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Subject[] Subjects { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            #region Part 1
            Console.WriteLine("--- Part 1: List of Numbers ---");

            List<int> numbers = new List<int>() { 2, 4, 6, 7, 1, 4, 2, 9, 1 };

            Console.WriteLine("\nQuery 1: Unique and sorted numbers:");

            var unique_Sorted_Numbers = numbers.Distinct().OrderBy(n => n);
            foreach (var num in unique_Sorted_Numbers)
            {
                Console.WriteLine(num);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            Console.WriteLine("\nQuery 2: Number and its multiplication:");
            var numbers_Multiplied = unique_Sorted_Numbers.Select(n => new { Number = n, Multiply = n * n });
            foreach (var item in numbers_Multiplied)
            {
                Console.WriteLine($"( Number = {item.Number}, Multiply = {item.Multiply} )");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            #endregion

            #region Part 2
            Console.WriteLine("\n\n--- Part 2: Array of Names ---");

            string[] names = { "Tom", "Dick", "Harry", "MARY", "Jay" };

            Console.WriteLine("\nQuery 1: Names with length of 3:");
            var names_With_Length3_Method = names.Where(name => name.Length == 3);
            foreach (var name in names_With_Length3_Method)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            Console.WriteLine("\nQuery 2: Names with 'a', sorted by length:");
            var names_With_A_Method = names.Where(name => name.ToLower().Contains("a")).OrderBy(name => name.Length);

            foreach (var name in names_With_A_Method)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            Console.WriteLine("\nQuery 3: First 2 names in the array:");
            var first_Two_Names_Method = names.Take(2);

            foreach (var name in first_Two_Names_Method)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            #endregion


            #region Part 3
            Console.WriteLine("\n\n--- Part 3: List of Students ---");

            List<Student> students = new List<Student>()
            {
                new Student() { ID=1, FirstName="Ali", LastName="Mohammed", Subjects=new Subject[] { new Subject() { Code = 22, Name="EF"}, new Subject(){ Code = 33, Name="UML"}}},
                new Student() { ID=2, FirstName="Mona", LastName="Gala", Subjects=new Subject[] { new Subject() { Code = 22, Name="EF"}, new Subject (){ Code = 34, Name="XML"}, new Subject () { Code = 25, Name="JS"}}},
                new Student() { ID=3, FirstName="Yara", LastName="Yousf", Subjects=new Subject[] { new Subject() { Code=22, Name="EF"}, new Subject (){ Code = 25, Name="JS"}}},
                new Student() { ID=1, FirstName="Ali", LastName="Ali", Subjects=new Subject[] { new Subject() { Code = 33, Name="UML"}}},
            };

            Console.WriteLine("\nQuery 1: Student's full name and number of subjects:");

            var student_Summary = students.Select(s => new
            {
                FullName = $"{s.FirstName} {s.LastName}",
                Num_Of_Subjects = s.Subjects.Length
            });

            foreach (var item in student_Summary)
            {
                Console.WriteLine($"( FullName = {item.FullName}, NoOfSubjects = {item.Num_Of_Subjects} )");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            Console.WriteLine("\nQuery 2: Students ordered by name:");

            var sorted_Students = students
                .OrderByDescending(s => s.FirstName)
                .ThenBy(s => s.LastName);

            foreach (var s in sorted_Students)
            {
                Console.WriteLine($"{s.FirstName} {s.LastName}");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            Console.WriteLine("\nQuery 3: Student and their individual subjects (SelectMany):");

            var student_Subjects = students.SelectMany(
                student => student.Subjects,
                (student, subject) => new
                {
                    StudentName = $"{student.FirstName} {student.LastName}",
                    SubjectName = subject.Name
                });

            foreach (var item in student_Subjects)
            {
                Console.WriteLine($"( StudentName = {item.StudentName}, SubjectName = {item.SubjectName} )");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();


            Console.WriteLine("\nBONUS: Student and their subjects (GroupBy):");
            var grouped_By_Student = student_Subjects.GroupBy(st => st.StudentName);

            foreach (var group in grouped_By_Student)
            {
                Console.WriteLine(group.Key); 
                foreach (var item in group)
                    Console.WriteLine(item.SubjectName);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            #endregion
        }
    }
}