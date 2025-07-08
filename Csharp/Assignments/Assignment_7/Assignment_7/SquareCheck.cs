using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_7
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class SquareCheck
    {
        public static void DisplaySquares(List<int> numbers)
        {
            var result = from num in numbers
                         let square = num * num
                         where square > 20
                         select new { Number = num, Square = square };

            foreach (var item in result)
            {
                Console.WriteLine($"{item.Number} - {item.Square}");
            }
        }
    }
}


