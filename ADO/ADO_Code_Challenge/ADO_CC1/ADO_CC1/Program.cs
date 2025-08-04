using System;
using System.Data;
using System.Data.SqlClient;

namespace ADO_CC
{
    class Employee_details
    {
        static SqlConnection getConnection()
        {
            var con = new SqlConnection("Data Source=ICS-LT-1JW37V3\\SQLEXPRESS;Initial Catalog=ADO_CC;Integrated Security=true;");
            con.Open();
            return con;
        }




        //Test the Procedure using ADO classes and show the generated Empid and Salary
        static (int EmpId, decimal Salary) InsertEmployee(string name, decimal givenSalary, char gender)
        {
            using (SqlConnection conn = getConnection())
            using (SqlCommand cmd = new SqlCommand("Insert_Employee_Details", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@GivenSalary", givenSalary);
                cmd.Parameters.AddWithValue("@Gender", gender);

                var empIdParam = new SqlParameter("@GeneratedEmpId", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(empIdParam);

                var salaryParam = new SqlParameter("@CalculatedSalary", SqlDbType.Decimal) { Precision = 10, Scale = 2, Direction = ParameterDirection.Output };
                cmd.Parameters.Add(salaryParam);

                cmd.ExecuteNonQuery();

                int empId = (int)empIdParam.Value;
                decimal salary = (decimal)salaryParam.Value;

                return (empId, salary);
            }
        }




        //2. Test the procedure using ADO classes and display the Employee details of that employee whose salary has been updated
        static (decimal UpdatedSalary, string Name, char Gender) UpdateSalary(int empId)
        {
            using (SqlConnection conn = getConnection())
            using (SqlCommand cmd = new SqlCommand("UpdateSalaryByEmpId", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmpId", empId);

                var updatedSalaryParam = new SqlParameter("@UpdatedSalary", SqlDbType.Decimal) { Precision = 10, Scale = 2, Direction = ParameterDirection.Output };
                cmd.Parameters.Add(updatedSalaryParam);

                cmd.ExecuteNonQuery();

                decimal updatedSalary = (decimal)updatedSalaryParam.Value;

                using (SqlCommand selectCmd = new SqlCommand("SELECT empname, gender FROM Employee_Details WHERE empid = @EmpId", conn))
                {
                    selectCmd.Parameters.AddWithValue("@EmpId", empId);
                    using (SqlDataReader reader = selectCmd.ExecuteReader())
                    {
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
                }
            }
        }



     // display all employees
        static void ShowAllEmployees()
        {
            using (SqlConnection conn = getConnection())
            using (SqlCommand cmd = new SqlCommand("SELECT empid, empname, empsalary, gender FROM Employee_Details", conn))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
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
        }



        static void Main()
        {
            // Insert new employee
            Console.Write("Enter Employee Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Given Salary: ");
            decimal givenSalary = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Enter Gender (M/F or Male/Female): ");
            string genderInput = Console.ReadLine().Trim().ToUpper();
            char gender = (genderInput == "MALE" || genderInput == "M") ? 'M' : 'F';

            var inserted = InsertEmployee(name, givenSalary, gender);
            Console.WriteLine($"\nEmployee inserted with EmpId: {inserted.EmpId}, Calculated Salary: {inserted.Salary}");
            ShowAllEmployees();
            // Update salary for employee
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

            // Show all employees 
            ShowAllEmployees();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
