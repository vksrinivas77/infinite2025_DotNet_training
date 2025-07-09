using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//2.Write a class Box that has Length and breadth as its members. 
//    Write a function that adds 2 box objects and stores in the 
//    3rd. Display the 3rd object details. Create a Test class to execute the above.
namespace CC_3
{
    public class Box
    {
        public double Length { get; set; }
        public double Breadth { get; set; }

        public Box(double length, double breadth)
        {
            Length = length;
            Breadth = breadth;
        }

        // Method to add two Box objects
        // This method creates a new Box object with the sum of lengths and breadths
        public Box Add(Box box2)
        {
            double newLength = this.Length + box2.Length;
            double newBreadth = this.Breadth + box2.Breadth;
            return new Box(newLength, newBreadth);
        }

        public void DisplayDetails()
        {
            Console.WriteLine($"Length: {Length:F2}, Breadth: {Breadth:F2}");
        }
    }

    public class Test
    {
        public static void Main(string[] args)
        {
            // Create the first Box object
            Console.WriteLine("Enter details for Box 1:");
            Console.Write("Length: ");
            double length1;
            while (!double.TryParse(Console.ReadLine(), out length1) || length1 < 0)
            {
                Console.WriteLine("Invalid input. Please enter a non-negative number for length.");
                Console.Write("Length: ");
            }

            Console.Write("Breadth: ");
            double breadth1;
            while (!double.TryParse(Console.ReadLine(), out breadth1) || breadth1 < 0)
            {
                Console.WriteLine("Invalid input. Please enter a non-negative number for breadth.");
                Console.Write("Breadth: ");
            }
            Box box1 = new Box(length1, breadth1);

            // Create the second Box object
            Console.WriteLine("Enter details for Box 2:");
            Console.Write("Length: ");
            double length2;
            while (!double.TryParse(Console.ReadLine(), out length2) || length2 < 0)
            {
                Console.WriteLine("Invalid input. Please enter a non-negative number for length.");
                Console.Write("Length: ");
            }

            Console.Write("Breadth: ");
            double breadth2;
            while (!double.TryParse(Console.ReadLine(), out breadth2) || breadth2 < 0)
            {
                Console.WriteLine("Invalid input. Please enter a non-negative number for breadth.");
                Console.Write("Breadth: ");
            }
            Box box2 = new Box(length2, breadth2);

            //Box1
            Console.Write("Box 1 Details: ");
            box1.DisplayDetails();

            //box2
            Console.Write("Box 2 Details: ");
            box2.DisplayDetails();

            Box box3 = box1.Add(box2);

            Console.Write("Box 3 (Sum of Box 1 and Box 2) Details: ");
            box3.DisplayDetails();
            Console.ReadLine();
        }
    }
}
