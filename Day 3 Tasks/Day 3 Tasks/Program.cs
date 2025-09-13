using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_3_Tasks
{
    #region Calculator Class
    public class Calc
    {
        #region sum
        public int sum(int x, int y)
        {
            return x + y;
        }

        public double sum(double x, double y)
        {
            return x + y;
        }

        public int sum(int x, int y, int z) => x + y + z;
        #endregion
        #region subtract
        public int sub(int x, int y) => x - y;
        public double sub(double x, double y) => x - y;
        public int sub(int x, int y, int z) => x - y - z;
        #endregion
        #region Multiply
        public int mul(int x, int y) => x * y;
        public double mul(double x, double y) => x * y;
        public int mul(int x, int y, int z) => x * y * z;
        #endregion
        #region Division
        public double div(int x, int y)
        {
            if (y == 0)
            {
                Console.WriteLine("Error: Cannot divide by zero.");
                return 0;
            }
            return (double)x / y;
        }

        public double div(double x, double y)
        {
            if (y == 0)
            {
                Console.WriteLine("Error: Cannot divide by zero.");
                return 0;
            }
            return x / y;
        }
        #endregion
    }
    #endregion

    #region Question Class
    public class Question
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public int Mark { get; set; }

        public Question()
        {
            Header = "Question";
            Body = string.Empty;
            Mark = 0;
        }

        public Question(string header, string body, int mark)
        {
            Header = header;
            Body = body;
            Mark = mark;
        }

        public virtual void Show()
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"{Header}   ({Mark} Marks)");
            Console.WriteLine(Body);
        }
    }
    #endregion

    #region MCQ Question Class
    public class MCQ : Question
    {
        public string[] Choices { get; set; }
        public int Correct_Answer_Index { get; set; } 

        public MCQ() : base() 
        {
            Choices = new string[0];
            Correct_Answer_Index = -1;
        }

        public MCQ(string header, string body, int mark, string[] choices, int correctAnswerIndex): base(header, body, mark) 
        {
            Choices = choices;
            Correct_Answer_Index = correctAnswerIndex;
        }

       
        public override void Show()
        {
            base.Show(); 
            if (Choices != null && Choices.Length > 0)
            {
                Console.WriteLine("Choices:");
                for (int i = 0; i < Choices.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {Choices[i]}");
                }
            }
            Console.WriteLine("-------------------------------");
        }
    }
    #endregion


    internal class Program
    {
        static void Main(string[] args)
        {
            #region Calculator Demo
            Console.WriteLine("------ Calculator Demo ------");
            Calc calculator = new Calc();
            Console.WriteLine($"Sum of 10 and 10 = {calculator.sum(10, 10)}");
            Console.WriteLine($"Sum of 5.5 and 10.2 = {calculator.sum(5.5, 10.2)}");
            Console.WriteLine($"Division of 10 and 4 = {calculator.div(10, 4)}");
            Console.WriteLine("\n");
            #endregion

            #region Single MCQ Object Demo
            Console.WriteLine("------ Single MCQ Demo ------");
            string[] sample_Choices = { "A. 4", "B. 5", "C. 6" };
            MCQ mcq1 = new MCQ("Math Question", "What is 2 + 2 ?", 5, sample_Choices, 0); 
            mcq1.Show();
            #endregion

            #region Polymorphism Demo
            Console.WriteLine("------ Polymorphism Demo (Question q = new MCQ()) ------");
            Question q = new MCQ("DB Question", "What does SQL stand for?", 10, new string[] { "Structured Query Language", "Simple Query Language", "Standard Query Language" }, 0);
            q.Show(); 
            #endregion

            #region User-Created MCQ Array
            Console.WriteLine("\n------ Create an Array of MCQs ------");
            Console.Write("How many MCQs would you like to create? ");
            int count = int.Parse(Console.ReadLine());

            MCQ[] mcqArray = new MCQ[count];

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"\nEnter details for MCQ #{i + 1}:");

                Console.Write("Header: ");
                string header = Console.ReadLine();

                Console.Write("Body: ");
                string body = Console.ReadLine();

                Console.Write("Mark: ");
                int mark = int.Parse(Console.ReadLine());

                Console.Write("How many choices? ");
                int choice_Count = int.Parse(Console.ReadLine());
                string[] choices = new string[choice_Count];

                for (int j = 0; j < choice_Count; j++)
                {
                    Console.Write($"Choice #{j + 1}: ");
                    choices[j] = Console.ReadLine();
                }

                Console.Write($"Which choice is correct (1-{choice_Count})? ");
                int correct_Choice_Number = int.Parse(Console.ReadLine());
                int correct_Index = correct_Choice_Number - 1; 

                mcqArray[i] = new MCQ(header, body, mark, choices, correct_Index);
            }

            Console.WriteLine("\n------ Here are all the MCQs you created: ------");
            foreach (var mcq in mcqArray)
            {
                mcq.Show();
            }
            #endregion

            #region Bonus: Take Quiz and Calculate Result
            if (mcqArray.Length > 0)
            {
                Console.WriteLine("\n------ Let's Take the Quiz! ------");
                int totalScore = 0;
                int maxScore = 0;

                for (int i = 0; i < mcqArray.Length; i++)
                {
                    mcqArray[i].Show();
                    maxScore += mcqArray[i].Mark;

                    Console.Write("Your answer (enter the choice number): ");
                    int userAnswer = int.Parse(Console.ReadLine());
                    int userIndex = userAnswer - 1;

                    if (userIndex == mcqArray[i].Correct_Answer_Index)
                    {
                        Console.WriteLine("Correct!");
                        totalScore += mcqArray[i].Mark;
                    }
                    else
                    {
                        Console.WriteLine($"Incorrect. The correct answer was {mcqArray[i].Correct_Answer_Index + 1}.");
                    }
                    Console.WriteLine();
                }

                Console.WriteLine("------ Quiz Complete! ------");
                Console.WriteLine($"Your final score is: {totalScore} out of {maxScore}");
            }
            #endregion
        }
    }
}
