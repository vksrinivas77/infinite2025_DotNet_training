using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelConcessionLib;

namespace TravelBooking
{
        class Program
        {
            static void Main(string[] args)
            {
                Console.Write("Enter your name: ");
                string name = Console.ReadLine();

                Console.Write("Enter your age: ");
                if (int.TryParse(Console.ReadLine(), out int age))
                {
                    Console.WriteLine($"\nHello, {name}!");
                    TravelConcession.CalculateConcession(age);
                }
                else
                {
                    Console.WriteLine("Invalid age input. Please enter a number.");
                }

                Console.WriteLine("\nPress any key to exit.");
                Console.ReadKey();
            }
        }
    }


