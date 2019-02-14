using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiThreadingParams
{
    class AreaClass
    {
        public double Base;
        public double Height;

        public double Calculate()
        {
            return 0.5 * Base * Height;
        }
    }
}
