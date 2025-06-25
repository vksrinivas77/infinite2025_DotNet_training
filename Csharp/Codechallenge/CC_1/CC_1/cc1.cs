using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC_1
{
    class cc1
    {
        static void Main(string[] args)
        {
            largest la = new largest();
            la.check_large();
            exchnage ex = new exchnage();
            ex.swap_string();
            remove rm = new remove();
            rm.remove_str();
            Console.Read();
        }
    }
}
