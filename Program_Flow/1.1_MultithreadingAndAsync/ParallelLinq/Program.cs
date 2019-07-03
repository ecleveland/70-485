using System;
using System.Linq;

namespace ParallelLinq
{
    class Program
    {
        class Person
        {
            public string Name { get; set; }
            public string City { get; set; }
        }

        static void Main(string[] args)
        {
            Person[] people = new Person[] {
                new Person { Name = "Eric Cleveland", City = "Frisco" },
                new Person { Name = "Jesus Christ", City = "Nazareth" },
                new Person { Name = "Abbie Schwartz", City = "Ziopupo" },
                new Person { Name = "Todd Hunter", City = "Tetiswi" },
                new Person { Name = "Katharine Perkins", City = "Dengupom" },
                new Person { Name = "Amy Miller", City = "Nuzejo" },
                new Person { Name = "Ann Boyd", City = "Dengupom" },
                new Person { Name = "Clara Henderson", City = "Dengupom" },
                new Person { Name = "Harvey Santos", City = "Gatjukbas" },
                new Person { Name = "Iva Baldwin", City = "Rumgijed" },
                new Person { Name = "Ella Bryant", City = "Ekzavud" }
            };
// TODO: Return to listing 1-6 informing parallelization (pdf:32)
            var result = from person in people.AsParallel()
                         where person.City == "Dengupom"
                         select person;
            
            foreach (var person in result)
            {
                Console.WriteLine(person.Name);
            }

            Console.WriteLine("Finished processing. Press a key to end.");
            Console.ReadKey();
        }
    }
}
