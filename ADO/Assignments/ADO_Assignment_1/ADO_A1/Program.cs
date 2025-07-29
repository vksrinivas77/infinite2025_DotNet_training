using System;
using System.Collections.Generic;
using System.Linq;

namespace ADO_A1
{
    class Program
    {
        static void Main()
        {
            List<Employee> empList = new List<Employee>()
        {
            new Employee{ EmployeeID=1001, FirstName="Malcolm", LastName="Daruwalla", Title="Manager", DOB=new DateTime(1984,11,16), DOJ=new DateTime(2011,6,8), City="Mumbai" },
            new Employee{ EmployeeID=1002, FirstName="Asdin", LastName="Dhalla", Title="AsstManager", DOB=new DateTime(1984,8,20), DOJ=new DateTime(2012,7,7), City="Mumbai" },
            new Employee{ EmployeeID=1003, FirstName="Madhavi", LastName="Oza", Title="Consultant", DOB=new DateTime(1987,11,14), DOJ=new DateTime(2015,4,12), City="Pune" },
            new Employee{ EmployeeID=1004, FirstName="Saba", LastName="Shaikh", Title="SE", DOB=new DateTime(1990,6,3), DOJ=new DateTime(2016,2,2), City="Pune" },
            new Employee{ EmployeeID=1005, FirstName="Nazia", LastName="Shaikh", Title="SE", DOB=new DateTime(1991,3,8), DOJ=new DateTime(2016,2,2), City="Mumbai" },
            new Employee{ EmployeeID=1006, FirstName="Amit", LastName="Pathak", Title="Consultant", DOB=new DateTime(1989,11,7), DOJ=new DateTime(2014,8,8), City="Chennai" },
            new Employee{ EmployeeID=1007, FirstName="Vijay", LastName="Natrajan", Title="Consultant", DOB=new DateTime(1989,12,2), DOJ=new DateTime(2015,6,1), City="Mumbai" },
            new Employee{ EmployeeID=1008, FirstName="Rahul", LastName="Dubey", Title="Associate", DOB=new DateTime(1993,11,11), DOJ=new DateTime(2014,11,6), City="Chennai" },
            new Employee{ EmployeeID=1009, FirstName="Suresh", LastName="Mistry", Title="Associate", DOB=new DateTime(1992,8,12), DOJ=new DateTime(2014,12,3), City="Chennai" },
            new Employee{ EmployeeID=1010, FirstName="Sumit", LastName="Shah", Title="Manager", DOB=new DateTime(1991,4,12), DOJ=new DateTime(2016,1,2), City="Pune" }
        };

            Console.WriteLine("1. Employees who joined before 1/1/2015:");
            var before2015 = empList.Where(e => e.DOJ < new DateTime(2015, 1, 1));
            foreach (var emp in before2015)
                Console.WriteLine($"{emp.FirstName} {emp.LastName}");

            Console.WriteLine("\n2. Employees born after 1/1/1990:");
            var bornAfter1990 = empList.Where(e => e.DOB > new DateTime(1990, 1, 1));
            foreach (var emp in bornAfter1990)
                Console.WriteLine($"{emp.FirstName} {emp.LastName}");

            Console.WriteLine("\n3. Employees with title Consultant or Associate:");
            var consultantAssociate = empList.Where(e => e.Title == "Consultant" || e.Title == "Associate");
            foreach (var emp in consultantAssociate)
                Console.WriteLine($"{emp.FirstName} {emp.LastName} - {emp.Title}");

            Console.WriteLine($"\n4. Total number of employees: {empList.Count}");

            Console.WriteLine($"\n5. Total number of employees in Chennai: {empList.Count(e => e.City == "Chennai")}");

            Console.WriteLine($"\n6. Highest Employee ID: {empList.Max(e => e.EmployeeID)}");

            Console.WriteLine($"\n7. Employees who joined after 1/1/2015: {empList.Count(e => e.DOJ > new DateTime(2015, 1, 1))}");

            Console.WriteLine($"\n8. Employees whose title is not Associate: {empList.Count(e => e.Title != "Associate")}");

            Console.WriteLine("\n9. Number of employees by City:");
            var byCity = empList.GroupBy(e => e.City);
            foreach (var group in byCity)
                Console.WriteLine($"{group.Key}: {group.Count()}");

            Console.WriteLine("\n10. Number of employees by City and Title:");
            var byCityTitle = empList.GroupBy(e => new { e.City, e.Title });
            foreach (var group in byCityTitle)
                Console.WriteLine($"{group.Key.City} - {group.Key.Title}: {group.Count()}");

            Console.WriteLine("\n11. Youngest Employee(s):");
            var maxDOB = empList.Max(e => e.DOB);
            var youngest = empList.Where(e => e.DOB == maxDOB);
            foreach (var emp in youngest)
                Console.WriteLine($"{emp.FirstName} {emp.LastName} - DOB: {emp.DOB.ToShortDateString()}");
            Console.ReadLine();
            
        }
     
    }
    
}
