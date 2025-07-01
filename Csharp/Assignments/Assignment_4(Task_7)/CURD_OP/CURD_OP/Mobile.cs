using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace CURD_OP
{
    public delegate void RingEventHandler();

    public class MobilePhone
    {
        public event RingEventHandler OnRing;

        public void ReceiveCall()
        {
            OnRing?.Invoke();
        }
    }
}

