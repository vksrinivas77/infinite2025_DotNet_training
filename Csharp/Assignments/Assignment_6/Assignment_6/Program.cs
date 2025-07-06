using System;
using System.IO;

//Write a program in C# Sharp to create a file and write an array of strings to the file.
namespace Assignment_6
{   
    class WriteToFile
    {
        static void Main()
        {
            string[] lines = {
            "First line of text",
            "Second line of text",
            "Third line of text"
        };

            string filePath = "example.txt";

            File.WriteAllLines(filePath, lines);

            Console.WriteLine("Data written to file successfully.");
            Console.ReadLine();
        }
    }

}
