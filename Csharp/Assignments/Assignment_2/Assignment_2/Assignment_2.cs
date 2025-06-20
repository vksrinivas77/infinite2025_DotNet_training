using System;

namespace Assignment_2
{
    class Programs
    {
        static void Main(string[] args)
        {
            //swap two numbers.
            Swap_two_numbers swap_two_numbers = new Swap_two_numbers();
            swap_two_numbers.swap();

            //a number as input and displays it four times in a row(separated by blank spaces),
            //and then four times in the next row, with no separation.You should do it twice: Use the console.Write and use { 0}
            Displays_rows displays_rows = new Displays_rows();
            displays_rows.rows();

            //read any day number as an integer and display the name of the day as a word.
            weekdays day = new weekdays();
            day.days();

           // accept ten marks and display
            Marks marks = new Marks();
            marks.Tenmarks();
           
            min_max_avg op = new min_max_avg();
            op.minmax();

            //copy the elements of one array into another array.(do not use any inbuilt functions)
            CopiedArray copy = new CopiedArray();
            copy.Copyarray();

            //Strings tasks
            string_p str = new string_p();
            str.length_And_Word_reverse();
            str.check_word();
            
        }
    }

    class Swap_two_numbers
    {

        public void swap()
        {
            Console.WriteLine("Enter the 1st numbers");
            int n1 = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the 2nd numbers");
            int n2 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("-----------------------");
            Console.WriteLine($"before swap n1:{n1} and n2:{n2}");
            int temp = n1;
            n1 = n2;
            n2 = temp;
            Console.WriteLine($"after swap n1:{n1} and n2:{n2}");
            Console.WriteLine("-----------------------");

        }

    }
    class Displays_rows
    {
        public void rows()
        {
            Console.WriteLine("----------------");
            Console.WriteLine("Enter the number for to diplay rows");
            int n1 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("----------------");
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write($"{n1}   ");
                }
                Console.WriteLine();
                for (int k = 0; k < 4; k++)
                {
                    Console.Write($"{n1}");
                }
                Console.WriteLine();
            }
            Console.WriteLine("-----------------------");
        }
    }

    class weekdays
    {
        public void days()
        {
            Console.WriteLine("Enter the numeber for to search a day");
            int n1 = Convert.ToInt32(Console.ReadLine());

            switch (n1)
            {
                case 1:
                    Console.WriteLine("Monday");
                    break;
                case 2:
                    Console.WriteLine("Thusday");
                    break;
                case 3:
                    Console.WriteLine("Wednesday");
                    break;
                case 4:
                    Console.WriteLine("Thursday");
                    break;
                case 5:
                    Console.WriteLine("Friday");
                    break;
                case 6:
                    Console.WriteLine("Saturday");
                    break;
                case 7:
                    Console.WriteLine("Monday");
                    break;

                default:
                    Console.WriteLine("Enter valid day");
                    break;
            }
            Console.WriteLine("-----------------------");
        }
    }

    class Marks
    {
        public void Tenmarks()
        {
            int[] marks = new int[10];
            Console.WriteLine("Enter 10 marks:");
            for (int i = 0; i < 10; i++)
            {
                marks[i] = int.Parse(Console.ReadLine());
            }

            int total = 0, min = marks[0], max = marks[0];

            for (int i = 0; i < 10; i++)
            {
                total += marks[i];
                if (marks[i] < min) 
                    min = marks[i];
                if (marks[i] > max) 
                    max = marks[i];
            }

            int average = (int)total / marks.Length;
            Console.WriteLine("----------------");
            Console.WriteLine($"\nTotal: {total}");
            Console.WriteLine($"Average: {average}");
            Console.WriteLine($"Minimum Marks: {min}");
            Console.WriteLine($"Maximum Marks: {max}");

            Array.Sort(marks);
            Console.WriteLine("Ascending Order: " + string.Join(", ", marks));

            Array.Reverse(marks);
            Console.WriteLine("Descending Order: " + string.Join(", ", marks));
            Console.WriteLine("----------------");
        }
    }

    class min_max_avg
    {
        public void minmax()
        {
            int[] numbers = { 10, 20, 30, 40, 50 };
            int sum = 0;
            int min = numbers[0];
            int max = numbers[0];

            foreach (int num in numbers)
            {
                sum += num;
                if (num < min) min = num;
                if (num > max) max = num;
            }

            int average = (int)sum / numbers.Length;
            for(int i=0;i<numbers.Length;i++)
            {
                Console.Write(numbers[i]+",");
            }
            Console.WriteLine("Average value: " + average);
            Console.WriteLine("Minimum value: " + min);
            Console.WriteLine("Maximum value: " + max);
        }
    }

    class CopiedArray
    {
        public void Copyarray()
        {
            int[] original_numbers = { 12, 45, 23, 67, 34 };

            int[] copyArray = new int[original_numbers.Length];
            for (int i = 0; i < original_numbers.Length; i++)
            {
                copyArray[i] = original_numbers[i];
            }
            Console.WriteLine("orginal Array:");
            for (int i = 0; i < original_numbers.Length; i++)
            {
                Console.Write($"{original_numbers[i]} ");
            }
            Console.WriteLine("");
            Console.WriteLine("Copied Array:");
            Console.WriteLine("-------------------------");
            for (int i = 0; i < original_numbers.Length; i++)
            {
                copyArray[i] = original_numbers[i];
                Console.Write($"{copyArray[i]} ");
            }
            
        }
    }
        
    class string_p
        {
            public void length_And_Word_reverse()
            {
            Console.WriteLine("");
            Console.WriteLine("Enter a word to reverse: ");
                String word = Console.ReadLine();
                Console.WriteLine($"The word to length: {word.Length} ");
                String reversed = "";
                for (int i = word.Length - 1; i >= 0; i--)
                {
                    reversed += word[i];
                }
                Console.WriteLine("Reversed word: " + reversed);
            }
        public void check_word()
        {
            Console.Write("Enter first word: ");
            string word1 = Console.ReadLine();
            Console.Write("Enter second word: ");
            string word2 = Console.ReadLine();
            if (word1 == word2)
            {
                Console.WriteLine("The words are the same.");
            }
            else
            {
                Console.WriteLine("The words are different.");
            }
            Console.Read();
        }
    }
     
    }
    

