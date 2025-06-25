
using System;

namespace Assignment_3
{
    class Student
    {
        int rollNo;
        string name;
        string studentClass;
        int semester;
        string branch;
        int[] marks = new int[5];

        public Student(int rollNo, string name, string studentClass, int semester, string branch)
        {
            this.rollNo = rollNo;
            this.name = name;
            this.studentClass = studentClass;
            this.semester = semester;
            this.branch = branch;
        }

        public void getMarks(int[] inputMarks)
        {
            for (int i = 0; i < 5; i++)
            {
                marks[i] = inputMarks[i];
            }
        }

        public void displayResult()
        {
            int sum = 0;
            bool hasFailed = false;

            for (int i = 0; i < marks.Length; i++)
            {
                sum += marks[i];
                if (marks[i] < 35)
                {
                    hasFailed = true;
                }
            }

            double avg = sum / 5.0;

            Console.WriteLine("--- Result ---");
            if (hasFailed || avg < 50)
            {
                Console.WriteLine("Result: Failed");
            }
            else
            {
                Console.WriteLine("Result: Passed");
            }

            Console.WriteLine("Average Marks: " + avg);
        }

        public void displayData()
        {
            Console.WriteLine("-- Student Details --");
            Console.WriteLine("Roll No: " + rollNo);
            Console.WriteLine("Name: " + name);
            Console.WriteLine("Class: " + studentClass);
            Console.WriteLine("Semester: " + semester);
            Console.WriteLine("Branch: " + branch);
            Console.WriteLine("Marks: ");
            for (int i = 0; i < marks.Length; i++)
            {
                Console.WriteLine("Subject " + (i + 1) + ": " + marks[i]);
            }
        }
    }

}