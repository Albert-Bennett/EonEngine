using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            const int calculations = 10;

            ManualResetEvent[] doneEvents = new ManualResetEvent[calculations];
            Fibnacci[] array = new Fibnacci[calculations];

            Random r = new Random();

            Console.WriteLine("Launching {0} tasks...", calculations);

            for (int i = 0; i < calculations; i++)
            {
                doneEvents[i] = new ManualResetEvent(false);

                array[i] = new Fibnacci(r.Next(20, 40), doneEvents[i]);
                ThreadPool.QueueUserWorkItem(array[i].ThreadPoolCallBack, i);
            }

            WaitHandle.WaitAll(doneEvents);
            Console.WriteLine("Calculations Completed.");

            for (int i = 0; i < calculations; i++)
                Console.WriteLine("Fibonacci ({0}) = {1}", array[i].N, array[i].FibOfN);

            Console.ReadKey();
        }
    }
}
