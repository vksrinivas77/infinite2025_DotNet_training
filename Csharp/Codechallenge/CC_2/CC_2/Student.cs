using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Create an Abstract class Student with Name, StudentId, Grade as members 
//    and also an abstract method Boolean Ispassed(grade) which 
//    takes grade as an input and checks whether student passed the course or not.
//Create 2 Sub classes Undergraduate and Graduate that inherits all members of the 
//    student and overrides Ispassed(grade) method

//For the UnderGrad class, if the grade is above 70.0, then isPassed returns true, 
//    otherwise it returns false. For the Grad class, if the grade is above 80.0, then 
//    isPassed returns true, otherwise returns false.

//Test the above by creating appropriate objects
namespace CC_2
{
    // Abstract class Student
    public abstract class Student
    {
        public string Name { get; set; }
        public string StudentId { get; set; }
        public double Grade { get; set; }

        public abstract bool IsPassed(double grade);
    }

    // Undergraduate subclass
    public class Undergraduate : Student
    {
        public override bool IsPassed(double grade)
        {
            return grade > 70.0;
        }
    }

    // Graduate subclass
    public class Graduate : Student
    {
        public override bool IsPassed(double grade)
        {
            return grade > 80.0;
        }
    }

    // Test class
    public class Student1
    {
        public static void Main(string[] args)
        {
            Undergraduate ugStudent = new Undergraduate
            {
                Name = "Umesh",
                StudentId = "UG001",
                Grade = 75.5
            };

            Graduate gStudent = new Graduate
            {
                Name = "Suresh",
                StudentId = "GR001",
                Grade = 82.0
            };

            Console.WriteLine($"{ugStudent.Name} UG - Grade: {ugStudent.Grade}, Passed: {ugStudent.IsPassed(ugStudent.Grade)}");
            Console.WriteLine($"{gStudent.Name} Grad - Grade: {gStudent.Grade}, Passed: {gStudent.IsPassed(gStudent.Grade)}");

            Console.WriteLine($"{ugStudent.Name} UG with 65.0 - Passed: {ugStudent.IsPassed(65.0)}");
            Console.WriteLine($"{gStudent.Name} Grad with 78.0 - Passed: {gStudent.IsPassed(78.0)}");
            Console.ReadLine();
        }
    }
}