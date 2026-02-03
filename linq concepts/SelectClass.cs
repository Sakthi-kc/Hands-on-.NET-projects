using System;
using System.Collections.Generic;
using System.Text;

namespace linq_concepts
{
    public class Person
    {
        public string? Name { get; set; }

        public int Age { get; set; }

        public static List<Person> People;

        static Person()
        {
            People = new List<Person>
            {
                new Person { Name = "Arun", Age = 25 },
                new Person { Name = "Bala", Age = 17 },
                new Person { Name = "Charan", Age = 30 }
            };
        }
    }

    public class SelectClass
    {
        public static void ExecuteSelect()
        {
            var names = Person.People.Select(p => p.Name.ToUpper());

            Console.WriteLine(string.Join(" ", names));

            Console.WriteLine();

            //Select creates anonymous type when we want to select only certain properties
            //Anonmyous types in C# override ToString() automatically and prints { Name = Arun, Age = 25 }
            //It is also object but compiler generated override with props and value in {}
            
            var people = Person.People.Select(p => new { p.Name, p.Age });

            Console.WriteLine(string.Join("\n", people));

            Console.WriteLine();

            //no lambda required in query expression
            var users = from p in Person.People
                       where p.Age == 17
                       select $"{p.Name} - {p.Age}";

            Console.WriteLine(string.Join("\n", users));

        }
    }
}
