using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Electricity_Billing.ElectricityBillLib
{
    public class ElectricityBoard
    {
        DBHandler dbHandler = new DBHandler();

        public void AddBill(ElectricityBill ebill)
        {
            using (SqlConnection con = dbHandler.GetConnection())
            {
                string query = "INSERT INTO ElectricityBill (consumer_number, consumer_name, units_consumed, bill_amount) VALUES (@cnum, @cname, @units, @bill)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@cnum", ebill.ConsumerNumber);
                cmd.Parameters.AddWithValue("@cname", ebill.ConsumerName);
                cmd.Parameters.AddWithValue("@units", ebill.UnitsConsumed);
                cmd.Parameters.AddWithValue("@bill", ebill.BillAmount);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void CalculateBill(ElectricityBill ebill)
        {
            int units = ebill.UnitsConsumed;
            double bill = 0;

            if (units <= 100)
                bill = 0;
            else if (units <= 300)
                bill = (units - 100) * 1.5;
            else if (units <= 600)
                bill = (200 * 1.5) + ((units - 300) * 3.5);
            else if (units <= 1000)
                bill = (200 * 1.5) + (300 * 3.5) + ((units - 600) * 5.5);
            else
                bill = (200 * 1.5) + (300 * 3.5) + (400 * 5.5) + ((units - 1000) * 7.5);

            ebill.BillAmount = bill;
        }

        public List<ElectricityBill> Generate_N_BillDetails(int num)
        {
            var bills = new List<ElectricityBill>();
            using (SqlConnection con = dbHandler.GetConnection())
            {
                string query = $"SELECT TOP {num} consumer_number, consumer_name, units_consumed, bill_amount FROM ElectricityBill ORDER BY consumer_number DESC";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ElectricityBill ebill = new ElectricityBill();
                        ebill.ConsumerNumber = reader["consumer_number"].ToString();
                        ebill.ConsumerName = reader["consumer_name"].ToString();
                        ebill.UnitsConsumed = Convert.ToInt32(reader["units_consumed"]);
                        ebill.BillAmount = Convert.ToDouble(reader["bill_amount"]);
                        bills.Add(ebill);
                    }
                }
            }
            return bills;
        }
    }
}