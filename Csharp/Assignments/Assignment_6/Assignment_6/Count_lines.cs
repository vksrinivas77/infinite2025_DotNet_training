using System;
using System.Collections.Generic;
using System.IO;
namespace Assignment_6
{
    //3. Write a program in C# Sharp to count the number of lines in a file
    class LineCounter
    {
        static void Main()
        {
            string filePath = "example.txt";

            if (File.Exists(filePath))
            {
                int lineCount = File.ReadAllLines(filePath).Length;
                Console.WriteLine($"Number of lines in the file: {lineCount}");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("File not found.");
                Console.ReadLine();
            }
        }
    }
}