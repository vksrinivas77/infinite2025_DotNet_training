using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace Electricity_Billing.ElectricityBillLib
{
    public class DBHandler
    {
        public SqlConnection GetConnection()
        {
            string connStr = ConfigurationManager.ConnectionStrings["ElectricityBillDB"].ConnectionString;
            return new SqlConnection(connStr);
        }
    }
}