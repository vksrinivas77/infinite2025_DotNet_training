using System;

namespace Assignment_1
{
    class Program
    {
        static void Main(string[] args)
        {
            CheckEquality();
            CheckPostiveORnegtive();
            ArithmeticOperations();
            table();
            Sumoftwonumber();
        }

        //Function to accept two integers and check whether they are equal or not. 
        static public void CheckEquality()
        {
            Console.WriteLine("----------CheckEquality------------");
            Console.Write("Enter the 1st number:");
            int number_1 = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter the 2nd number:");
            int number_2 = Convert.ToInt32(Console.ReadLine());

            if (number_1 == number_2)
            {
                Console.WriteLine($"both {number_1} and {number_2} numbers are equal "); 
            }
            else
            {
                Console.WriteLine($"both {number_1} and {number_2} numbers are not equal ");
            }
        }
        // function to check whether a given number is positive or negative.
        static public void CheckPostiveORnegtive()
        {
            Console.WriteLine("----------CheckPostiveORnegtive------------");
            Console.Write("Enter the number:");
            int number_1 = Convert.ToInt32(Console.ReadLine());

            if (number_1 > 1)
            {
                Console.WriteLine($" {number_1} is postive number ");
            }
            else
            {
                Console.WriteLine($" {number_1} is negtive number ");
            }
        }

        // function for ArithmeticOperations
        static public void ArithmeticOperations()
        {
            Console.WriteLine("----------ArithmeticOperations------------");
            Console.Write("Enter the 1st number:");
            int number_1 = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter the 2nd number:");
            int number_2 = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter the operation (+,-,*,/ ):");
            String op = Console.ReadLine();

            if (op == "+")
            {
                int result = number_1 + number_2;
                Console.WriteLine($" The sum of two numbers is {result} ");
            }
            else if (op=="-")
            {
                int result = number_1 - number_2;
                Console.WriteLine($" The substraction of two numbers is {result} ");
            }
            else if (op == "*")
            {
                int result = number_1 * number_2;
                Console.WriteLine($" The multipication of two numbers is {result} ");
            }
            else if (op == "/")
            {
                int result = number_1 / number_2;
                Console.WriteLine($" The devison of two numbers is {result} ");
            }
            else
            {
                Console.Write("Enter valid opration ");
            }
        }

        //function that prints the multiplication table of a number as input.
        static public void table()
        {
            Console.WriteLine("-------------table----------------");
            Console.Write("Enter the  number:");
            int number_1 = Convert.ToInt32(Console.ReadLine());

            for(int i = 0; i <= 10; i++)
            {
                int result = number_1 * i;
                Console.WriteLine($"{number_1} * {i} = {result}" );
            }
        }
        //function to compute the sum of two given integers. If two values are the same, return the triple of their sum
        static public void Sumoftwonumber()
        {
            Console.WriteLine("-------------Sumoftwonumber----------------");
            Console.Write("Enter the 1st number:");
            int number_1 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter the 1st number:");
            int number_2 = Convert.ToInt32(Console.ReadLine());

            int result = number_1 + number_2;
            if (number_1 == number_2)
            {
                result *=3;
                Console.Write($"both number are equal,so triple of sum is  {result} ");
                Console.Read();
            }
            else
            {
                Console.Write($"Sum of two number {result}");
                Console.Read();
            }
    }

    }
}
