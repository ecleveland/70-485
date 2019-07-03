using System;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Example
{
    class Program
    {
        static void Task1()
        {
            Console.WriteLine("Task 1 Starting");
            Thread.Sleep(2000);
            Console.WriteLine("Task 1 Ending");
        }

        static void Task2()
        {
            Console.WriteLine("Task 2 Starting");
            Thread.Sleep(1000);
            Console.WriteLine("Task 2 Ending");
        }

        static void WorkOnItem(object item)
        {
            Console.WriteLine($"Started processing {item}");
            Thread.Sleep(100);
            Console.WriteLine($"Finished working on {item}");
        }

        static void Main(string[] args)
        {
            // Parallel.Invoke can start a large number of tasks but you have no control over the order in 
            // which the tasks are started or which processor they are assigned to
            Console.WriteLine("Side by side 'Invoke' Processing:");
            Parallel.Invoke(() => Task1(), () => Task2());

            // Parallel.ForEach
            var itemsForEach = Enumerable.Range(0, 500);
            Parallel.ForEach(itemsForEach, item =>
            {
                WorkOnItem(item);
            });

            // Managing a parallel For Loop
            // Calling stop prevents any new iterations with an index value greater than the current index.
            var itemsFor = Enumerable.Range(0, 500).ToArray();
            ParallelLoopResult result = Parallel.For(0, itemsFor.Count(), (int i, ParallelLoopState loopState) => {
                if(i == 200)
                    loopState.Stop();

                WorkOnItem(itemsFor[i]);
            });
            Console.WriteLine($"Completed { result.IsCompleted }");
            Console.WriteLine($"Items: " + result.LowestBreakIteration);

            Console.WriteLine("Press any key to end.");
            Console.ReadKey();
        }
    }
}
