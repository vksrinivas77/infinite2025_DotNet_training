using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//2.Create a class called Scholarship which has a function Public 
//    void Merit() that takes marks and fees as an input. 
namespace Assignment_5
{
    class InvalidMarkException:Exception
    {
        public InvalidMarkException(string message) : base(message) { }
    }
}
