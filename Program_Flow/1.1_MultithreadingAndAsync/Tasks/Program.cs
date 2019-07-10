using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tasks
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create Task and run to completion
            Task newTask = new Task(() => DoWork());
            newTask.Start();
            // Or for the above two calls just do one as:
            // Task newTask = Task.Run(() => DoWork());
            // newTask.Wait();
            newTask.Wait();

            // Returning a Value from a task
            Task<int> task = Task.Run(() => 
            {
                return CalculateResult();
            });

            Console.WriteLine(task.Result);

            // Wait all tasks
            Task [] tasks = new Task[10];
            for (int i = 0; i < 10; i++)
            {
                int TaskNum = i; // Set task number for lambda exprestion
                // TODO: p37
            }

            // End Program
            Console.WriteLine("Finished Processing. Press a Key to end.");
            Console.ReadKey();
        }

        public static void DoWork()
        {
            Console.WriteLine("Work Starting");
            Thread.Sleep(2000);
            Console.WriteLine("Work finished");
        }

        public static int CalculateResult()
        {
            DoWork();
            return 99;            
        }
    }
}
