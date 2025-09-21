using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_6_Tasks
{
    #region Range
    public class Range<T> where T : IComparable<T>
    {
        public T max { get; }
        public T min { get; }

        public Range(T min, T max)
        {
            if (min.CompareTo(max) > 0)
                throw new ArgumentException("Minimum value cannot be greater than maximum value.");

            this.min = min;
            this.max = max;
        }

        public bool In_Range(T num)
        {
            return num.CompareTo(this.max) <= 0 && num.CompareTo(this.min) >= 0;
        }

        public T Length()
        {
            try
            {
                dynamic dMax = this.max;
                dynamic dMin = this.min;
                return dMax - dMin;
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
            {
                Console.WriteLine($"Cannot calculate length for type {typeof(T)}. The type does not support subtraction.");
                return default(T);
            }
        }
    }
    #endregion


    #region Fixed Size
    public class FixedSizeList<T>
    {
        private readonly T[] items;
        private int count;

        public int Capacity => items.Length;
        public int Count => count;

        public FixedSizeList(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentException("Capacity must be a positive number.", nameof(capacity));
            
            items = new T[capacity];
            count = 0;
        }

        public void Add(T item)
        {
            if (count >= Capacity)
                throw new InvalidOperationException("The list is full. Cannot add more items.");
            

            items[count] = item;
            count++;
        }

        public T Get(int index)
        {
            
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException("The specified index is out of the valid range.");
            
            return items[index];
        }
    }
    #endregion


    internal class Program
    {

        #region Optimized Bubble Sort
        public static void Optimized_Bubble_Sort<T>(T[] array) where T : IComparable<T>
        {
            int len  = array.Length;

            if (array == null || len <= 1) return;

            for (int i = 0; i < len; i++)
            {
                bool swapped = false;

                for (int j = 0;j < len - i - 1; j++)
                {
                    if (array[j].CompareTo(array[j + 1]) > 0)
                    {
                        T temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;

                        swapped = true;
                    }
                }

                if (!swapped) break;
            }
        }
        #endregion


        #region Reverse
        public static void Reverse_ArrayList(ArrayList list)
        {
            if (list == null) return;

            int n = list.Count;
            for (int i = 0; i < n / 2; i++)
            {
                object temp = list[i];
                list[i] = list[n - 1 - i];
                list[n - 1 - i] = temp;
            }
        }
        #endregion

        #region Extract Even
        public static List<int> Extract_Even(List<int> inputList)
        {
            List<int> even_Numbers = new List<int>();

            if (inputList == null) return even_Numbers;

            foreach (int number in inputList)
            {
                if (number % 2 == 0)
                {
                    even_Numbers.Add(number);
                }
            }
            return even_Numbers;
        }
        #endregion

        #region Non Repeated
        public static int Find_First_NonRepeated_Char_Index(string input)
        {
            if (string.IsNullOrEmpty(input))  return -1;
            

            var char_Counts = new Dictionary<char, int>();

            foreach (char c in input)
            {
                if (char_Counts.ContainsKey(c))  char_Counts[c]++;
                else  char_Counts[c] = 1;
            }

            int n = input.Length;

            for (int i = 0; i < n; i++)
                if (char_Counts[input[i]] == 1)  return i;   
           
            return -1;
        }
        #endregion


        static void Main(string[] args)
        {

            Console.WriteLine("--- 1. Optimized Bubble Sort ---");
            int[] numbersToSort = { 64, 34, 25, 12, 22, 11, 90 };
            Console.WriteLine("Original array: " + string.Join(", ", numbersToSort));
            Optimized_Bubble_Sort(numbersToSort);
            Console.WriteLine("Sorted array: " + string.Join(", ", numbersToSort));
            Console.WriteLine();



            Console.WriteLine("--- 2. Generic Range<T> Class ---");
            var intRange = new Range<int>(10, 20);
            Console.WriteLine($"Is 15 in range (10, 20)? {intRange.In_Range(15)}");
            Console.WriteLine($"Is 25 in range (10, 20)? {intRange.In_Range(25)}"); 
            Console.WriteLine($"Length of integer range: {intRange.Length()}");     





            Console.WriteLine("--- 3. Reverse ArrayList In-Place ---");
            ArrayList myArrayList = new ArrayList() { 1, "two", 3.0, 'f', 5 };
            Console.Write("Original ArrayList: ");
            foreach (var item in myArrayList) Console.Write(item + " ");
            Console.WriteLine();

            Reverse_ArrayList(myArrayList);

            Console.Write("Reversed ArrayList: ");
            foreach (var item in myArrayList) Console.Write(item + " ");
            Console.WriteLine("\n");




            Console.WriteLine("--- 4. Find Even Numbers ---");
            List<int> originalList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Console.WriteLine("Original List: " + string.Join(", ", originalList));
            List<int> evenList = Extract_Even(originalList);
            Console.WriteLine("List with only even numbers: " + string.Join(", ", evenList));
            Console.WriteLine();




            Console.WriteLine("--- 5. Fixed-Size List ---");
            try
            {
                var fixedList = new FixedSizeList<string>(3);
                Console.WriteLine($"Created a list with capacity {fixedList.Capacity}.");
                fixedList.Add("Apple");
                fixedList.Add("Banana");
                fixedList.Add("Cherry");
                Console.WriteLine($"Item at index 1: {fixedList.Get(1)}");

                Console.WriteLine("Attempting to add a 4th item to a full list...");
                fixedList.Add("Date");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Caught an exception: {ex.Message}");
            }

            try
            {
                var fixedList = new FixedSizeList<int>(5);
                fixedList.Add(100);
                Console.WriteLine("Attempting to get item at index 3 (which is not yet added)...");
                int item = fixedList.Get(3);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Caught another exception: {ex.Message}");
            }
            Console.WriteLine();




            Console.WriteLine("--- 6. First Non-Repeated Character ---");
            string test1 = "swiss";
            Console.WriteLine($"First non-repeated char index in '{test1}' is: {Find_First_NonRepeated_Char_Index(test1)}"); // Expected: 1 (for 'w')

            string test2 = "aabbcc";
            Console.WriteLine($"First non-repeated char index in '{test2}' is: {Find_First_NonRepeated_Char_Index(test2)}"); // Expected: -1

            string test3 = "programming";
            Console.WriteLine($"First non-repeated char index in '{test3}' is: {Find_First_NonRepeated_Char_Index(test3)}"); // Expected: 0 (for 'p')
            Console.WriteLine();
        }
    }
}
