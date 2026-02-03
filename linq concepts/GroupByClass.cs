using System;
using System.Collections.Generic;
using System.Text;

namespace linq_concepts
{
    internal class GroupByClass
    {
        public static void ExecuteGroup()
        {
            //after groupby <Employee> becomes Group<Key, Values> where Key is the group by col
            //hence use different input param to refer them

            var group1 = Employee.employees
                .GroupBy(e => e.Department)
                //.Select(g => new
                //{
                //    Department = g.Key,
                //    Name = string.Join(", ", g.Select(v => v.Name))
                //}
                //);
                .Select(g => $"{g.Key} with values {string.Join(", ", g.Select(v => v))}");
                                    //g.Select(v => $"{v.Name} {v.Age}"))}");



            var group2 = from emp in Employee.employees
                         group emp by emp.Age into g
                         select $"Age: {g.Key} with values {string.Join(", ", g.Select(v => v.Name))}";

            Console.WriteLine(string.Join("\n",group1));

            Console.WriteLine();

            Console.WriteLine(string.Join("\n", group2));

        }
    }
}
