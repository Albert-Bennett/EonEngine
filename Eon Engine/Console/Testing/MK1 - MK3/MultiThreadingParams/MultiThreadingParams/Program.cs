using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MultiThreadingParams
{
    class Program
    {
        static BackgroundWorker bgWorker = 
            new BackgroundWorker();

        static void Main(string[] args)
        {
            TestArea();

            Console.ReadKey();
        }

        static void TestArea()
        {
            InitializeWorker();

            AreaClass area = new AreaClass();
            area.Base = 20;
            area.Height = 40;

            bgWorker.RunWorkerAsync(area);
        }

        static void InitializeWorker()
        {
            bgWorker.DoWork += 
                new DoWorkEventHandler(bgWorker_DoWork);

            bgWorker.RunWorkerCompleted += 
                new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);
        }

        static void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            AreaClass area = (AreaClass)e.Argument;

            e.Result = area.Calculate();
        }

        static void bgWorker_RunWorkerCompleted(object sender,
            RunWorkerCompletedEventArgs e)
        {
            double area = (double)e.Result;

            Console.WriteLine("The area is : {0}.", area.ToString());
        }
    }
}
