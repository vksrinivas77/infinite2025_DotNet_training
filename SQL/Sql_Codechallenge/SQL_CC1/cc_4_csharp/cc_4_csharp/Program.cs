
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//C Sharp Question

//Now once the collection is created write down and execute the LINQ queries for collection 
//as follows :

//a.Display detail of all the employee
//b. Display details of all the employee whose location is not Mumbai
//c. Display details of all the employee whose title is AsstManager
//d. Display details of all the employee whose Last Name start with S

namespace cc_4_csharp
{
    class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string DOB { get; set; }
        public string DOJ { get; set; }
        public string City { get; set; }



        public override string ToString()
        {

            return "Employee ID:" + this.EmployeeID + ",Title:" + this.Title + ",FirstName: " + this.FirstName + ",LastName: " + this.LastName + ",DOB:" + this.DOB + ",DOJ:" + this.DOJ + ",City:" + this.City;
        }


    }
    class Program
    {

        static void Main(string[] args)
        {
            List<Employee> employee = new List<Employee>();
            employee.Add(new Employee { EmployeeID = 1001, FirstName = "Malcolm", LastName = "Daruwalla ", Title = "Manager ", DOB = "16-11-1984", DOJ = "08-06-2011", City = "Mumbai" });
            employee.Add(new Employee { EmployeeID = 1002, FirstName = "Asdin", LastName = "Dhalla ", Title = "AsstManager", DOB = "20-08-1994", DOJ = "07-07-2012", City = "Mumbai" });
            employee.Add(new Employee { EmployeeID = 1003, FirstName = "Madhavi", LastName = "Oza ", Title = "Consultant ", DOB = "14-11-1987", DOJ = "12-04-2015", City = "Pune" });
            employee.Add(new Employee { EmployeeID = 1004, FirstName = "Saba", LastName = "Shaikh", Title = "SE ", DOB = "03-06-1990", DOJ = "02-02-2016", City = "Pune" });
            employee.Add(new Employee { EmployeeID = 1005, FirstName = "Nazia", LastName = "Shaikh", Title = "SE ", DOB = "08-03-1991", DOJ = "02-02-2016", City = "Mumbai" });
            employee.Add(new Employee { EmployeeID = 1006, FirstName = "Amit", LastName = "Pathak ", Title = "Consultant ", DOB = "07-11-1989", DOJ = "08-08-2014", City = "Chennai" });
            employee.Add(new Employee { EmployeeID = 1007, FirstName = "Vijay", LastName = "Natrajan", Title = "Consultant ", DOB = "02-12-1989", DOJ = "01-06-2015", City = "Mumbai" });
            employee.Add(new Employee { EmployeeID = 1008, FirstName = "Rahul", LastName = "Dubey ", Title = "Associate", DOB = "11-11-1993", DOJ = "06-11-2014", City = "Chennai" });
            employee.Add(new Employee { EmployeeID = 1009, FirstName = "Suresh", LastName = "Mistry", Title = "Associate ", DOB = "12-08-1992", DOJ = "03-12-2014", City = "Chennai" });
            employee.Add(new Employee { EmployeeID = 1010, FirstName = "Sumit", LastName = "Shah ", Title = "Manager ", DOB = "12-04-1991", DOJ = "02-01-2016", City = "Pune" });




            //a.Display detail of all the employee
            Console.WriteLine("Display detail of all the employee");
            foreach (Employee obj in employee)
            {
                Console.WriteLine(obj);
            }
            Console.ReadLine();

            Console.WriteLine("Display details of all the employee whose location is not Mumbai");
            // b.Display details of all the employee whose location is not Mumbai
            List<Employee> employee_obj1 = (from obj in employee where obj.City != "Mumbai" select obj).ToList();
            foreach (Employee obj in employee_obj1)
            {
                Console.WriteLine(obj);
            }
            Console.ReadLine();
            Console.WriteLine("Display details of all the employee whose title is AsstManager");
            //c. Display details of all the employee whose title is AsstManager
          
            List<Employee> employee_obj2 = (from obj in employee where obj.Title.Equals("AsstManager") select obj).ToList();
            foreach (Employee obj in employee_obj2)
            {
                Console.WriteLine(obj);
            }
            Console.ReadLine();

            //d. Display details of all the employee whose Last Name start with S
            Console.WriteLine("Display details of all the employee whose Last Name start with S");
            List<Employee> employee_obj3 = (from obj in employee where obj.LastName.StartsWith("S") select obj).ToList();
            foreach (Employee obj in employee_obj3)
            {
                Console.WriteLine(obj);
            }

            Console.ReadLine();
        }

    }
}