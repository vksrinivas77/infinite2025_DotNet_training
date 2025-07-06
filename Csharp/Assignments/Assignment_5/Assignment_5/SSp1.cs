using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//2.Create a class called Scholarship which has a function Public 
//    void Merit() that takes marks and fees as an input. 

namespace Assignment_5
{
    class SSp1
    {
        static void Main(string[] args)
        {
            Scholarship sc = new Scholarship();

            try
            {
                double scholarshipAmount = sc.Merit(85, 10000);
                Console.WriteLine($"Scholarship Amount: {scholarshipAmount}");
                Console.ReadLine();
            }
            catch (InvalidMarkException ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                Console.ReadLine();
            }

        }
    }
}
