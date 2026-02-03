using System;
using System.Collections.Generic;
using System.Text;

namespace linq_concepts
{
    class Employee
    {
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Department { get; set; }

        //Overrides the default ToString of Employee class object
        public override string ToString() => $"{Name} {Age} {Department}";

        public static List<Employee> employees = new List<Employee>
        {
            new Employee { Name="Alice", Age=25, Department="HR" },
            new Employee { Name="Bob", Age=30, Department="IT" },
            new Employee { Name="Abraham", Age=28, Department="IT" },
            new Employee { Name="David", Age=35, Department="HR" },
            new Employee { Name="Dora", Age=35, Department="HR" }
        };
    }

    internal class WhereClass
    {
        public static void ExecuteWhere()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            List<int> numbers1 = new List<int> { 1, 2, 3, 4, 5 };
            numbers1.AddRange(Enumerable.Range(6, 5));

            //Here it is only a query
            //This is "lazy evaluation” or “deferred execution” means the query does not run
                    //until you actually enumerate it
                    //enumeration happens in foreach or writeline

            var evenNumbers = from n in numbers
                              where n % 2 == 0
                              select n;

            Console.WriteLine(string.Join(", ", evenNumbers));

            //ToList makes it a list and filter is applied immediately
            //Any new addition to the list will not be filtered

            var oddNumbers = numbers1.Where(n => n % 2 != 0).ToList();

            foreach (var n in oddNumbers) Console.Write($"{n} ");

            Console.WriteLine();


            //here results are objects IEnumerable<Employee>, hence use select else override ToString in class

            //IEnumerable<T> promises to return objects of type T one at a time, not a list, hence needs iteration
            var itEmployees = Employee.employees.Where(e => e.Department == "IT").ToList();
            

            var seniorEmployees = Employee.employees
                .Where(e => e.Age >= 30)
                .Select(e => $"{e.Name} {e.Age}");


            //string.Join accepts IEnumerable<T> but internally calls ToString()
            //string.join internally does the enumeration looping (foreach looping)

            //Here it is IEnumerable<Employee> which invokes the overridden ToString()
            Console.WriteLine(string.Join("\n", itEmployees));
            
            
            //Here it is IEnumerable<string> as we have formed string using select hence not explicitly calls Employee.ToString()
            Console.WriteLine(string.Join("\n", seniorEmployees ));


            Console.WriteLine(string.Join(" ", numbers));

        }
    }
}
