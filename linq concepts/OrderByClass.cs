using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace linq_concepts
{
    internal class OrderByClass
    {
        public void ExecuteOrder()
        {
            //here orderby supports only 1 key so use thenby
            //they also have orderbydescending and thenbydescending

            var emp_list = Employee.employees
                .OrderBy(e => e.Name?[0])
                .ThenBy(e => e.Age);


            //select should be the last statement of query syntax
            //then by is not supported beacuse we can have all in same orderby

            var emp_list_desc = from emp in Employee.employees
                                orderby emp.Name?[0] descending, emp.Age ascending
                                select $"{emp.Name} of age {emp.Age}";

            Console.WriteLine(string.Join("\n", emp_list_desc));
        }
    }
}
