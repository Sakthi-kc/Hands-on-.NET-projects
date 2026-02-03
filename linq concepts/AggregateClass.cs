using System;
using System.Collections.Generic;
using System.Text;

namespace linq_concepts
{
    internal class AggregateClass
    {
        public static void ExecuteAggregate()
        {
            // returns IEnumerable<Employee>, so enumerate or join to print
            var list = Employee.employees
                .Skip(1)
                .SkipLast(2);

            Console.WriteLine(string.Join("\n",list));



            //Count
            Console.WriteLine($"Out of total {Employee.employees.Count()}, " +
                $"{Employee.employees.Count(e => e.Department == "HR")} are HRs");

            //We have Sum() and Average()

            //Min() and Max()
            //Min returns only the value while MinBy returns the whole object

            var minEmp = Employee.employees
                .MinBy(e => e.Age);

            var minAge = Employee.employees
                .Min(e => e.Age);

            Console.WriteLine($"{minEmp?.Name} is the youngest of age {minAge}");

        }
    }
}
