using System;

public class Check_int
{
    // Method that throws an exception if the number is negative
    public static void ProcessNumber(int number)
    {
        if (number < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(number), "Input number cannot be negative.");
            Console.ReadLine();
        }
        Console.WriteLine($"Number processed successfully: {number}");
        Console.ReadLine();
    }

    public static void Main(string[] args)
    {
        // Test with a positive number
        try
        {
            ProcessNumber(10);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Caught an exception: {ex.Message}");
            Console.ReadLine();
        }
       
    }
}
