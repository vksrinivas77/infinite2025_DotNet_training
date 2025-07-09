using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC_3
{

    public class Calculator
    {
        public delegate int MathOperation(int a, int b);

        // Method for Addition
        public static int Add(int a, int b)
        {
            return a + b;
        }

        // Method for Subtraction
        public static int Subtract(int a, int b)
        {
            return a - b;
        }

        // Method for Multiplication
        public static int Multiply(int a, int b)
        {
            return a * b;
        }

        public static int PerformCalculation(MathOperation operation, int x, int y)
        {
            return operation(x, y);
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Delegate-based Calculator");

            Console.Write("Enter the first integer: ");
            int num1;
            while (!int.TryParse(Console.ReadLine(), out num1))
            {
                Console.WriteLine("Invalid input. Please enter an integer.");
                Console.Write("Enter the first integer: ");
            }

            Console.Write("Enter the second integer: ");
            int num2;
            while (!int.TryParse(Console.ReadLine(), out num2))
            {
                Console.WriteLine("Invalid input. Please enter an integer.");
                Console.Write("Enter the second integer: ");
            }

            // Instantiate delegates for each operation
            MathOperation addDelegate = new MathOperation(Add);
            MathOperation subtractDelegate = new MathOperation(Subtract);
            MathOperation multiplyDelegate = new MathOperation(Multiply);

  

            // Addition
            int additionResult = PerformCalculation(addDelegate, num1, num2);
            Console.WriteLine($"Result of Addition ({num1} + {num2}): {additionResult}");

            // Subtraction
            int subtractionResult = PerformCalculation(subtractDelegate, num1, num2);
            Console.WriteLine($"Result of Subtraction ({num1} - {num2}): {subtractionResult}");

            // Multiplication
            int multiplicationResult = PerformCalculation(multiplyDelegate, num1, num2);
            Console.WriteLine($"Result of Multiplication ({num1} * {num2}): {multiplicationResult}");
            Console.ReadLine();
        }
    }

}
