using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiThreadingSample
{
    class AreaClass
    {
        public double Base;
        public double Height;
        public double Area;

        public void Calculate()
        {
            Area = 0.5 * Base * Height;
            Console.WriteLine("The area is: {0}", Area.ToString());
        }
    }
}
