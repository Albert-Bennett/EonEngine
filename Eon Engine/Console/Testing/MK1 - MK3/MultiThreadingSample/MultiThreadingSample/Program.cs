using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MultiThreadingSample
{
    class Program
    {
        static void Main(string[] args)
        {
            TestArea();

            Console.ReadKey();
        }

        static void TestArea()
        {
            AreaClass area = new AreaClass();

            Thread thread = new Thread(area.Calculate);

            area.Base = 30;
            area.Height = 40;

            thread.Start();
        }
    }
}
