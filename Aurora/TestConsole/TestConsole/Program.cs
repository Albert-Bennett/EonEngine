using Aurora;
using System;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Script s = new Script("TestScripts/TestScript0");

            Console.Write(s.Find("btnStyle") as string);

            Console.ReadKey();
        }
    }
}
