using System;
using System.Data;
using System.Data.SqlClient;

namespace ADO_CC
{
    class Employee_details
    {
        static SqlConnection getConnection()
        {
            SqlConnection con = new SqlConnection("Data Source=ICS-LT-1JW37V3\\SQLEXPRESS;Initial Catalog=ADO_CC;Integrated Security=true;");
            con.Open();
            return con;
        }

        static (int EmpId, decimal Salary) InsertEmployee(string name, decimal givenSalary, char gender)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = getConnection();
                cmd = new SqlCommand("Insert_Employee_Details", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@GivenSalary", givenSalary);
                cmd.Parameters.AddWithValue("@Gender", gender);

                SqlParameter empIdParam = new SqlParameter("@GeneratedEmpId", SqlDbType.Int);
                empIdParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(empIdParam);

                SqlParameter salaryParam = new SqlParameter("@CalculatedSalary", SqlDbType.Decimal);
                salaryParam.Precision = 10;
                salaryParam.Scale = 2;
                salaryParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(salaryParam);

                cmd.ExecuteNonQuery();

                int empId = (int)empIdParam.Value;
                decimal salary = (decimal)salaryParam.Value;

                return (empId, salary);
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        static (decimal UpdatedSalary, string Name, char Gender) UpdateSalary(int empId)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlCommand selectCmd = null;
            SqlDataReader reader = null;

            try
            {
                conn = getConnection();

                cmd = new SqlCommand("UpdateSalaryByEmpId", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmpId", empId);

                SqlParameter updatedSalaryParam = new SqlParameter("@UpdatedSalary", SqlDbType.Decimal);
                updatedSalaryParam.Precision = 10;
                updatedSalaryParam.Scale = 2;
                updatedSalaryParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(updatedSalaryParam);

                cmd.ExecuteNonQuery();

                decimal updatedSalary = (decimal)updatedSalaryParam.Value;

                selectCmd = new SqlCommand("SELECT empname, gender FROM Employee_Details WHERE empid = @EmpId", conn);
                selectCmd.Parameters.AddWithValue("@EmpId", empId);

                reader = selectCmd.ExecuteReader();

                if (reader.Read())
                {
                    string name = reader.GetString(0);
                    char gender = Convert.ToChar(reader.GetString(1));
                    return (updatedSalary, name, gender);
                }
                else
                {
                    throw new Exception("Employee not found.");
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();

                if (selectCmd != null)
                    selectCmd.Dispose();

                if (cmd != null)
                    cmd.Dispose();

                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        static void ShowAllEmployees()
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;

            try
            {
                conn = getConnection();
                cmd = new SqlCommand("SELECT empid, empname, empsalary, gender FROM Employee_Details", conn);
                reader = cmd.ExecuteReader();

                Console.WriteLine("\n--- All Employee Details ---");
                Console.WriteLine("EmpId\tName\t\tSalary\t\tGender");
                Console.WriteLine("-------------------------------------------------");

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string empName = reader.GetString(1);
                    decimal salary = reader.GetDecimal(2);
                    string gen = reader.GetString(3);

                    Console.WriteLine($"{id}\t{empName}\t\t{salary}\t\t{gen}");
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (cmd != null)
                    cmd.Dispose();
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        static void Main()
        {
            Console.Write("Enter Employee Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Given Salary: ");
            decimal givenSalary = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Enter Gender (M/F or Male/Female): ");
            string genderInput = Console.ReadLine().Trim().ToUpper();
            char gender = (genderInput == "MALE" || genderInput == "M") ? 'M' : 'F';

            var inserted = InsertEmployee(name, givenSalary, gender);
            Console.WriteLine($"\nEmployee inserted with EmpId: {inserted.EmpId}, Calculated Salary: {inserted.Salary}");

            Console.Write("\nEnter Employee ID to increase salary by 100: ");
            int empIdToUpdate = Convert.ToInt32(Console.ReadLine());

            try
            {
                var updated = UpdateSalary(empIdToUpdate);
                Console.WriteLine($"\nSalary updated to: {updated.UpdatedSalary}");
                Console.WriteLine($"Employee Details - Name: {updated.Name}, Gender: {updated.Gender}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            ShowAllEmployees();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
