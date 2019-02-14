using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadSynchronizing
{
    class Program
    {
        static AutoResetEvent autoEvent;

        static void Main(string[] args)
        {
            autoEvent = new AutoResetEvent(false);

            Console.WriteLine("Main thread running.");

            Thread t = new Thread(DoWork);
            t.Start();

            Console.WriteLine("Main thread sleeping fo 1 second.");
            Thread.Sleep(1000);

            Console.WriteLine("Main thread signalling worker thread.");
            autoEvent.Set();

            Console.ReadKey();
        }

        static void DoWork()
        {
            Console.WriteLine(" Worker Thread started, Now waiting for an Event...");
            autoEvent.WaitOne();
            Console.WriteLine(" Worker Thread re-activated, now exiting");
        }
    }
}
