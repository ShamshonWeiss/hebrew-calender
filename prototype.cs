using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoladBasedCalender
{
    
    /* var protoYear = new Year(5785)
    * protoYear.Leap bool if leap
    * protoYear.yearName chosor molei or shaliem
    * protoYear.RH[0]  dow of rosh hashana
    * protoYear.nextRh[0] dow of next year rosh hashana
    * protoYear.months arr of months
    * protoYear["adar"] month obj of adar
    * protoYear[2] month obj of 3rd month
    * 
    * Year.PrintMoled(moled[]) Prints moled nicely
    * protoYear[11].name gets name of month 
    * protoYear[11].leap gets if month is leap
    *  protoYear["adar"].PrintMonth() prints the month
    */
        internal class prototype {
        static void Main(string[] args)
        {
            for (int i = 5783; i < 5790; i++)
            {
                Year thisYear = new(5783);
                Console.WriteLine($"rosh hashana {thisYear.RH[0]}");
                Console.WriteLine($"next rosh hashana {thisYear.nextRh[0]}");
                Console.WriteLine($"year name {thisYear.yearName}");
                thisYear[2].PrintMonth();
                thisYear["elul"].PrintMonth();
                Year.PrintMoled(thisYear[2].molad);
            }

        }
    }
}
