using System;
using System.Collections.Generic;
using System.Linq;

//1.) 
//Write a query that returns list of numbers and their
//    squares only if square is greater than 20 

namespace Assignment_7
    {
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Squares greater than 20
            Console.WriteLine("1. Squares greater than 20:");
            List<int> numbers = new List<int> { 7, 2, 30 };
            SquareCheck.DisplaySquares(numbers);
            Console.WriteLine();

            // 2. Words starting with 'a' and ending with 'm'
            Console.WriteLine("2. Words starting with 'a' and ending with 'm':");
            List<string> words = new List<string> { "mum", "amsterdam", "bloom" };
            WordFilter.DisplayWordsStartingAEndingM(words);
            Console.WriteLine();

            // 3. Employee operations
            Console.WriteLine("3. Employee Operations:");
            EmployeeManager manager = new EmployeeManager();
            manager.SeedData();

            Console.WriteLine("-- a. All Employees --");
            manager.DisplayAllEmployees();

            Console.WriteLine("-- b. Employees with Salary > 45000 --");
            manager.DisplayHighSalaryEmployees();

            Console.WriteLine("-- c. Employees from Bangalore --");
            manager.DisplayBangaloreEmployees();

            Console.WriteLine("-- d. Employees Sorted by Name --");
            manager.DisplayEmployeesSortedByName();
            Console.WriteLine();
        }
    
    }
}
