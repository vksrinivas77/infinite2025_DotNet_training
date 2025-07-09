using System;
using System.IO;

namespace CC_3
{
  
    public class FileAppend
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter the file name (e.g., mytextfile.txt): ");
            string fileName = Console.ReadLine();

            // Ensure the file name is not empty
            if (string.IsNullOrWhiteSpace(fileName))
            {
                Console.WriteLine("File name cannot be empty. Exiting.");
                return;
            }

            Console.Write("Enter the text to append: ");
            string textToAppend = Console.ReadLine();

            try
            {
                // The 'true' argument in the constructor indicates append mode.
                using (StreamWriter sw = new StreamWriter(fileName, true))
                {
                    sw.WriteLine(textToAppend); 
                }

                Console.WriteLine($"Text successfully appended to '{fileName}'.");

               
                Console.WriteLine("--- Current File Content ---");
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
                Console.WriteLine("----------------------------");
                Console.ReadLine();
            }

            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }

}
