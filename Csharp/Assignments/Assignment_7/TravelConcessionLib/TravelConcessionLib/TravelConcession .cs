using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TravelConcessionLib
{
    public class TravelConcession
    {
        private const double TotalFare = 500;

        public static void CalculateConcession(int age)
        {
            if (age <= 5)
            {
                Console.WriteLine("Little Champs - Free Ticket");
            }
            else if (age > 60)
            {
                double concessionFare = TotalFare * 0.7; // 30% concession
                Console.WriteLine("Senior Citizen - Fare after concession: " + concessionFare);
            }
            else
            {
                Console.WriteLine("Print Ticket Booked - Fare: " + TotalFare);
            }
        }
    }
}
