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
                int taskNum = i; // Set task number for lambda exprestion
                tasks[i] = Task.Run(() => DoWork(taskNum));
            }

            // Listing 1.14
            // Think of the following as a horse race:
            // WaitAll() ensures that all the horses have crossed the finish line
            // WaitAny() ensures the program continues as soon as the first horse has crossed the finish line
            // I can see the use here if someone makes a notification system where the game continues and notifies players when someone has won!
            // Task.WaitAny(tasks);
            Task.WaitAll(tasks);

            // Listing 1.15
            // Continue task and the concept of an antecedant task.
            Task task2 = Task.Run(() => HelloTask());
            task2.ContinueWith((prevTask) => WorldTask());

            // Listing 1.16
            // Continuation Options
            Task task3 = Task.Run(() => HelloTask());
            task3.ContinueWith((prevTask) => WorldTask(), TaskContinuationOptions.OnlyOnRanToCompletion);
            // task3.ContinueWith((prevTask) => ExceptionTask(), TaskContinuationOptions.OnlyOnFaulted);

            // Listing 1.17
            // Child tasks - Not completed until the parent task ic complete and they are refereed to as detached child tasks
            // or detached nested tasks...
            // Tasks are created by calling the StartNew Method on the default Task.Factory
            // This can be created with TaskCreationOptions.DenyChildAttach options that deny it to be able to have attached children
            var parent = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Parent Starts");
                for (int i = 0; i < 10; i++)
                {
                    int taskNum = i;
                    Task.Factory.StartNew(
                        (x) => DoChild(x), // Lambda Expression
                        taskNum, // state object
                        TaskCreationOptions.AttachedToParent
                    );
                }
            });
            parent.Wait(); // waits for all attached children to complete.


            // End Program
            Console.WriteLine("Finished Processing. Press a Key to end.");
            Console.ReadKey();
        }

        private static void DoWork()
        {
            Console.WriteLine("Work Starting");
            Thread.Sleep(2000);
            Console.WriteLine("Work finished");
        }

        // Listing 1.14
        private static void DoWork(int i)
        {
            Console.WriteLine($"Task {i} starting...");
            Thread.Sleep(2000);
            Console.WriteLine($"Task {i} finished!");
        }

        private static int CalculateResult()
        {
            DoWork();
            return 99;            
        }

        // Listing 1.15 Antecendant Tasks
        private static void HelloTask()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Hello");
        }

        private static void WorldTask()
        {
            Thread.Sleep(1000);
            Console.WriteLine("World");
        }

        // Listing 1.16
        // private static void ExceptionTask()
        // {
        //     Thread.Sleep(1000);
        //     Console.WriteLine("Exception: World Destroyed");
        // }

        // Listing 1.17 
        private static void DoChild(object state)
        {
            Console.WriteLine($"Child {state} starting ...");
            Thread.Sleep(2000);
            Console.WriteLine($"Child {state} finished!");
        }
    }
}
