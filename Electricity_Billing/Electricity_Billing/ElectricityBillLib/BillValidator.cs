using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Electricity_Billing.ElectricityBillLib
{
    public class BillValidator
    {
        public string ValidateUnitsConsumed(int unitsConsumed)
        {
            if (unitsConsumed < 0)
                return "Given units is invalid";
            return null;
        }
    }
}