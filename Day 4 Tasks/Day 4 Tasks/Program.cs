using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Day_4_Tasks
{

    public class Company
    {
        public string name { get; set; }
        public List<Department> Departments { get; } = new List<Department>();

        public Company(String name) {
            this.name = name;
        }
    }

    public class Department
    {
        public String name { get; set; }
        public List<Employee> employees { get; } = new List<Employee>();
        public Department(string name)
        {
            this.name = name;
        }
    }

    public static class IdGenerator
    {
        private static int _currentId = 0;
        public static int GenerateId()
        {
            return ++_currentId;
        }
    }

    public abstract class Person
    {
        public int Id { get; }
        public string name { get; set; }
        public int age { get; set; }

        protected Person(string name, int age)
        {
            this.Id = IdGenerator.GenerateId();
            this.name = name;
            this.age = age;
        }

        public abstract void Introduce();
    }
    public class Employee : Person
    {
        public List<Course> courses { get; } = new List<Course>();

        public Employee(string name, int age) : base(name, age) { }

        public void Register(Course course)
        {
            if (!courses.Contains(course))
            {
                courses.Add(course);
                course.students.Add(this);
                Console.WriteLine($"{this.name} registered to --> {course.name} <--.");
            }
        }


        public override void Introduce()
        {
            Console.WriteLine($"Hi, I'm {this.name} (Employee). ID: {this.Id}");
        }
    }

    public class Engine
    {
        public string model { get; }
        public Engine(string model) {
            this.model = model;
        }
    }

    public class Car
    {
        public string make { get; }
        public Engine Engine { get; } 
        public Car(string make, string engine_model)
        {
            this.make = make;
            this.Engine = new Engine(engine_model);
        }
    }

    public class Instructor : Employee
    {
        public List<Course> teaching_courses { get; } = new List<Course>();
        public Instructor(string name, int age) : base(name, age) { }

        public void Teach(Course course)
        {
            course.Instructor = this;

            if (!teaching_courses.Contains(course)) teaching_courses.Add(course);
            Console.WriteLine($"{this.name} assigned to teach --> {course.name} <--");
        }


        public override void Introduce()
        {
            Console.WriteLine($"Hello, I'm {this.name}, an Instructor. ID: {this.Id}");
        }
    }

    public class Student : Person
    {
        public List<Course> courses { get; } = new List<Course>();
        public List<Grade> grades { get; } = new List<Grade>();


        public Student(string name, int age) : base(name, age) { }


        public void Register(Course course)
        {
            if (!courses.Contains(course))
            {
                courses.Add(course);
                course.students.Add(this);

                Console.Write($"{this.name} registered to --> {course.name} <--");

                switch (course.level)
                {
                    case CourseLevel.Beginner:
                        Console.WriteLine(" Good luck starting out!");
                        break;
                    case CourseLevel.Intermediate:
                        Console.WriteLine(" Nice — keep going!");
                        break;
                    case CourseLevel.Advanced:
                        Console.WriteLine(" This will be challenging!");
                        break;
                }
            }
        }

        public Grade TotalGrades()
        {
            if (!grades.Any()) return new Grade(0);
            Grade sum = grades[0];

            for (int i = 1; i < grades.Count; i++) sum += grades[i];
            return sum;
        }


        public override void Introduce()
        {
            Console.WriteLine($"Hi, I'm {this.name}, a Student. ID: {this.Id}");
        }
    }

    public enum CourseLevel { Beginner, Intermediate, Advanced }

    public class Course
    {
        public string name { get; set; }
        public CourseLevel level { get; set; }
        public Instructor Instructor { get; set; }
        public List<Person> students { get; } = new List<Person>();


        public Course(string name, CourseLevel level)
        {
            this.name = name;
            this.level = level;
        }
    }

    public interface IDrawable {
        void Draw();
    }

    public abstract class Shape : IDrawable
    {
        public abstract double Area();
        public abstract void Draw();
    }

    public class Circle : Shape
    {
        public double radius { get; set; }
        public Circle(double r) { radius = r; }
        public override double Area() => Math.PI * radius * radius;
        public override void Draw()
        { 
            Console.WriteLine("     *  *        ");
            Console.WriteLine("  *        *      ");
            Console.WriteLine(" *          *     ");
            Console.WriteLine(" *          *     ");
            Console.WriteLine("  *        *      ");
            Console.WriteLine("     *  *        ");
        }
    }

    public class Rectangle : Shape
    {
        public double width { get; set; }
        public double height { get; set; }
        public Rectangle(double w, double h) { width = w; height = h; }
        public override double Area() => width * height;
        public override void Draw()
        {
            int w = (int)Math.Round(width);
            int h = (int)Math.Round(height);
            
            Console.WriteLine(" " + new string('-', w));
            
            for (int i = 0; i < h; i++)
            {
                Console.WriteLine("|" + new string(' ', w) + "|");
            }

            Console.WriteLine(" " + new string('-', w));
        }
    }

    

    public class Grade
    {
        public int mark { get; }
        public Grade(int value) { mark = value; }


        public static Grade operator +(Grade a, Grade b) => new Grade(a.mark + b.mark);


        public static bool operator ==(Grade a, Grade b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return a.mark == b.mark;
        }
        public static bool operator !=(Grade a, Grade b) => !(a == b);
    }
    internal class Program
    {
        static void Main(string[] args)
        {

            var company = new Company("Microsoft");
            var dev = new Department("Development");
            var hr = new Department("HR");
            company.Departments.Add(dev);
            company.Departments.Add(hr);

            var alice = new Employee("Alice", 30);
            var bob = new Employee("Bob", 28);
            dev.employees.Add(alice);
            dev.employees.Add(bob);

            var karim = new Instructor("Karim", 40);
            var lina = new Instructor("Lina", 35);
            hr.employees.Add(karim);
            dev.employees.Add(lina);

            var omar = new Student("Omar", 22);
            var Ahmad = new Student("Ahmad", 21);

            var cSharp = new Course("C# Basics", CourseLevel.Beginner);
            var algos = new Course("Algorithms", CourseLevel.Intermediate);
            var advAI = new Course("Advanced AI", CourseLevel.Advanced);

            karim.Teach(cSharp);
            lina.Teach(algos);
            lina.Teach(advAI);
            Console.WriteLine();

            omar.Register(cSharp);
            omar.Register(algos);
            Ahmad.Register(algos);
            Ahmad.Register(advAI);
            alice.Register(cSharp);
            Console.WriteLine();

            omar.grades.Add(new Grade(78));
            omar.grades.Add(new Grade(90));
            Ahmad.grades.Add(new Grade(85));
            Ahmad.grades.Add(new Grade(82));

            var shapes = new List<Shape> { new Circle(3), new Rectangle(3, 4) };
            Console.WriteLine(" ------------ Shapes Demo ------------ ");
            foreach (var s in shapes)
            {
                Console.WriteLine($"Shape: {s.GetType().Name}, Area = {s.Area():F2}");
                s.Draw();
                Console.WriteLine();
            }
            Console.WriteLine();

            // s.GetType().Name --> method from the base class object

            var car = new Car("Toyota", "V6-2024");
            Console.WriteLine($"Car: {car.make}, Engine: {car.Engine.model}");
            Console.WriteLine();


            Console.WriteLine(" ------------ Introductions ------------ ");
            alice.Introduce();
            karim.Introduce();
            omar.Introduce();
            Console.WriteLine();


            Console.WriteLine(" ------------ Employees & Their Departments & Courses ------------ ");
            foreach (var d in company.Departments)
            {
                foreach (var e in d.employees)
                {
                    var courseList = e.courses.Any() ? string.Join(", ", e.courses.Select(c => c.name)) : "None";
                    Console.WriteLine($"Employee: {e.name} | Department: {d.name} | Courses: {courseList}");
                }
            }
            Console.WriteLine();


            Console.WriteLine(" ------------ Students Report ------------ ");
            var students = new List<Student> { omar, Ahmad };
            foreach (var st in students)
            {
                Console.WriteLine($"Student: {st.name} | Enrolled: {string.Join(", ", st.courses.Select(c => c.name))} | TotalGrades: {st.TotalGrades().mark}");
            }
            Console.WriteLine();


            Console.WriteLine(" ------------ Instructors Report ------------ ");
            var instructors = new List<Instructor> { karim, lina };
            foreach (var inst in instructors)
                Console.WriteLine($"Instructor: {inst.name} | Teaches: {string.Join(", ", inst.teaching_courses.Select(c => c.name))}");
            
            Console.WriteLine();


            Console.WriteLine("------------ Departments Summary ------------ ");
            foreach (var dpt in company.Departments)
                Console.WriteLine($"Department: {dpt.name} | Employee Count: {dpt.employees.Count}");


            Console.WriteLine();
            var g1 = new Grade(60);
            var g2 = new Grade(40);
            Console.WriteLine($"Grade equality: g1 == g2 ? { (g1 == g2) } ");
            Console.WriteLine($"g1 + g2 = {(g1 + g2).mark}");


            
        }
    }
}
