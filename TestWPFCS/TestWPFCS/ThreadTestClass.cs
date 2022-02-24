using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWPFCS
{
    class ThreadTestClass
    {
        public static void fun1(ulong j)
        {
            ulong s = 1;

            DateTime dateTime1 = new DateTime();
            DateTime dateTime2 = new DateTime();

            dateTime1 = DateTime.Now;
            for (ulong i = s; i < j; i++)
                s = s * i;
            dateTime2 = DateTime.Now;
            Trace.WriteLine(String.Format("Затраченое время = " + dateTime2.Subtract(dateTime1)));
        }
    }
}
