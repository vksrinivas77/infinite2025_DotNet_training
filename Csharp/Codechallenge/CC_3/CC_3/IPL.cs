using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//1.Write a program to find the Sum and the Average points scored by the teams in the IPL. 
//    Create a Class called CricketTeam that has a function called Pointscalculation(int no_of_matches) that takes 
//    no.of matches as input and accepts that many scores from the user. 
//    The function should then return the Count of Matches, Average and Sum of the scores.
namespace CC_3
{
    class IPL
    {
        static void Main(string[] args)
        {

            CricketTeam team = new CricketTeam();

            Console.Write("Enter the number of matches: ");
            string matchesInput = Console.ReadLine();
            int numMatches;

            // Validate user input for number of matches
            while (!int.TryParse(matchesInput, out numMatches) || numMatches <= 0)
            {
                Console.WriteLine("Invalid number of matches. Please enter a positive integer.");
                Console.Write("Enter the number of matches: ");
                matchesInput = Console.ReadLine();
            }

            var result = team.Pointscalculation(numMatches);

            Console.WriteLine("--- Match Statistics ---");
            Console.WriteLine($"Count of Matches: {result.count}");
            Console.WriteLine($"Sum of Scores: {result.sum}");
            Console.WriteLine($"Average Score: {result.average:F2}");
            Console.ReadLine();
        }
    }

public class CricketTeam
    {
        // Method to calculate points based on match scores
        public (int count, double sum, double average) Pointscalculation(int no_of_matches)
        {
            // Check for invalid input
            if (no_of_matches <= 0)
            {
                Console.WriteLine("Number of matches must be greater than zero.");
                return (0, 0.0, 0.0);
            }

            double[] scores = new double[no_of_matches];

            
            for (int i = 0; i < no_of_matches; i++)
            {
                Console.Write($"Enter score for match {i + 1}: ");
                string input = Console.ReadLine();
                double score;

                while (!double.TryParse(input, out score) || score < 0)
                {
                    Console.WriteLine("Invalid score. Please enter a non-negative number.");
                    Console.Write($"Enter score for match {i + 1}: ");
                    input = Console.ReadLine();
                }
                scores[i] = score;
            }

            // Calculate sum of scores
            double sum = scores.Sum();
            // Calculate average of scores
            double average = scores.Average();
            // Count of matches is simply the input no_of_matches
            int count = no_of_matches;

            // Return a tuple containing the count, sum, and average
            return (count, sum, average);
        }
    }  
}
