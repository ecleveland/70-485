using System;
using System.Threading;

namespace Threads
{
    class Program
    {
        // Listing 1.17
        static void ThreadHello()
        {
            Console.WriteLine("Hello from the thread");
            Thread.Sleep(2000);
        }

        // Listing 1.21
        private static void WorkOnData(object data)
        {
            Console.WriteLine($"Working on: {data}");
            Thread.Sleep(1000);
        }

        static void Main(string[] args)
        {
            Thread thread = new Thread(ThreadHello);
            thread.Start();

            // Listing 1.20 Thread in a lambda expression
            // Interesting note: The order will come up as press a key then the thread for lambda. This is because
            // the thread running in main reached its end before the new thread has started running.
            Thread thread2 = new Thread(() => 
            {
                Console.WriteLine("Hello from the thread (Lambda style)");
                Thread.Sleep(1000);
            });
            thread2.Start();

            // Listing 1.21
            // Take a look at messing with the object here
            ParameterizedThreadStart ps = new ParameterizedThreadStart(WorkOnData);
            Thread thread3 = new Thread(ps);
            thread3.Start(99);

            // Listing 1.22
            // I think this is a lot more eloquent
            Thread thread4 = new Thread((data) =>
            {
                WorkOnData(data);
            });
            thread4.Start(101);
            
            Console.WriteLine("Press a key to end.");
            Console.ReadKey();
        }
    }
}
