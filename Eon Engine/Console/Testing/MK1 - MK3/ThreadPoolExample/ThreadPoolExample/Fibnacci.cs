using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApplication1
{
   public class Fibnacci
    {
       int n;
       int fibOfN;

       ManualResetEvent doneEvent;

       public int N
       {
           get { return n; }
       }

       public int FibOfN
       {
           get { return fibOfN; }
       }

       public Fibnacci(int n, ManualResetEvent doneEvent)
       {
           this.n = n;
           this.doneEvent = doneEvent;
       }

       public void ThreadPoolCallBack(object context)
       {
           int idx = (int)context;

           Console.WriteLine("Thread {0} started...", idx);
           fibOfN = Calculate(n);
           Console.WriteLine("Thread {0} Result Calculated", idx);
           doneEvent.Set();
       }

       public int Calculate(int n)
       {
           if (n <= 1)
               return n;

           return Calculate(n - 1) + Calculate(n - 2);
       }
    }
}
