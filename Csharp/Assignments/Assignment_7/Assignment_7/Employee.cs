using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_7
{
    public class Employee
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string EmpCity { get; set; }
        public double EmpSalary { get; set; }
    }

    public class EmployeeManager
    {
        private List<Employee> employees = new List<Employee>();

        public void SeedData()
        {
            employees.Add(new Employee { EmpId = 1, EmpName = "Raj", EmpCity = "Bangalore", EmpSalary = 50000 });
            employees.Add(new Employee { EmpId = 2, EmpName = "Simran", EmpCity = "Delhi", EmpSalary = 40000 });
            employees.Add(new Employee { EmpId = 3, EmpName = "Amit", EmpCity = "Bangalore", EmpSalary = 55000 });
            employees.Add(new Employee { EmpId = 4, EmpName = "Neha", EmpCity = "Mumbai", EmpSalary = 30000 });
        }

        public void DisplayAllEmployees()
        {
            foreach (var emp in employees)
            {
                DisplayEmployee(emp);
            }
        }

        public void DisplayHighSalaryEmployees()
        {
            foreach (var emp in employees.Where(e => e.EmpSalary > 45000))
            {
                DisplayEmployee(emp);
            }

        }

        public void DisplayBangaloreEmployees()
        {
            foreach (var emp in employees.Where(e => e.EmpCity.Equals("Bangalore", StringComparison.OrdinalIgnoreCase)))
            {
                DisplayEmployee(emp);
            }
        }

        public void DisplayEmployeesSortedByName()
        {
            foreach (var emp in employees.OrderBy(e => e.EmpName))
            {
                DisplayEmployee(emp);
            }
        }

        private void DisplayEmployee(Employee emp)
        {
            Console.WriteLine($"ID: {emp.EmpId}, Name: {emp.EmpName}, City: {emp.EmpCity}, Salary: {emp.EmpSalary}");

            Console.ReadLine();
        }
    }
}
