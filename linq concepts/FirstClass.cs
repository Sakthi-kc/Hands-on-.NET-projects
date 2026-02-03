using System;
using System.Collections.Generic;
using System.Text;

namespace linq_concepts
{
    internal class FirstClass
    {
        public static void ExecuteFirst()
        {
            //no direct support in query syntax, use as below:
            var first_emp = (from emp in Employee.employees
                             select emp).FirstOrDefault();


            //first throws exception when empty or null so use FirstOrDefault
            var last_emp = Employee.employees
                .Where(e => e.Age == 10)
                //.First();
                .FirstOrDefault();


            //always print name as below dnt select directly in query as there can be no match sometimes
            //here ?. checks null, if yes, sets name with the string where ?? is null coalescing

            var last_emp_name = last_emp?.Name ?? "No employee found";


            Console.WriteLine(string.Join(" ", first_emp));

            Console.WriteLine(string.Join(" ", last_emp_name));

            var last_emp_dept = Employee.employees
                .Select(e => e.Department)
                .LastOrDefault();

            Console.WriteLine(string.Join(" ", last_emp_dept));


            //SingleOrDefault to be used when we want single element else it will throw error
            Console.WriteLine(Employee.employees
                .SingleOrDefault(e => e.Name == "Alice"));

        }
    }
}
