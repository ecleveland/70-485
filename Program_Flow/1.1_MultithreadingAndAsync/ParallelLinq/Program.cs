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

            // Return to listing 1-6 informing parallelization (pdf:32)
            // this call of AsParallel requests that the query be parallelized whether performance is improved or not. 
            // Executed on max
            // 4 processers
            // If you need as ordered - use the as ordered addition. .AsParallel().AsOrdered()
            // .AsSequential() preserves the query order
            var result = from person in people.AsParallel().
                            WithDegreeOfParallelism(4).
                            WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                            where person.City == "Dengupom"
                            select person;
            
            foreach (var person in result)
            {
                Console.WriteLine(person.Name);
            }

            // Exceptions in PLINQ Queries
            try
            {
                var exResult = from person in people.AsParallel()
                    where CheckCity(person.City)
                    select person;
                exResult.ForAll(person => Console.WriteLine(person.Name));
            }
            catch (AggregateException e)
            {
                Console.WriteLine($"{e.InnerExceptions.Count} : exceptions.");
            }

            Console.WriteLine("Finished processing. Press a key to end.");
            Console.ReadKey();
        }

        public static bool CheckCity(string name)
        {
            if(name == "")
                throw new ArgumentException(name);
            return name == "Seattle";
        }
    }
}