using System;
using System.Collections.Generic;
using System.Text;

namespace linq_concepts
{
    internal class AnyClass
    {
        public static void ExecuteAny()
        {
            //no direct support in query syntax
            //Any always returns bool

            bool anyEmployee = Employee.employees.Any();

            bool anyITEmp = Employee.employees
                .Any(e => e.Department == "CS");

            Console.WriteLine(anyEmployee ? "There are employees" : "There are no employees");

            Console.WriteLine(anyITEmp ? "There are IT employees" : "There are no IT employees");



        }

    }
}
