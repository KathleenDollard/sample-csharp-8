using GreenRegistration;
using System;
using System.Collections.Generic;
using System.Text;

namespace TollCollectorLib
{
   public  class GreenPointSystem
    {
        public static int GetPoints(Cycle cycle)
           => cycle switch
           {
               (1, 1) => 500,
               (2, _) => 400,
               _ => 300
           };
    }
}
