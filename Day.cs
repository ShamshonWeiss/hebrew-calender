using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoladBasedCalender
{
    internal class Day
    {
        /* var protoYear = new Year(5785)
         * protoYear.Leap 
         * protoYear.RH[0]  dow of rosh hashana
         * protoYear.nextRh[0] dow of next year rosh hashana
         * protoYear.months arr of months
         * protoYear["adar"] month obj of adar
         * protoYear[2] month obj of 3rd month
         * protoYear
         * protoYear
         * protoYear
         * protoYear
         * protoYear
         * protoYear
         * protoYear
         * protoYear
         * protoYear
         * protoYear
         * protoYear
         * protoYear 
         * 
         */
        public Day(int date)
        {
            this.date = date;
        }
        public Day(int day,string[] eventName):this(day)
        {
            this.eventName=eventName;
        }
        public int date;
        string[] eventName;
    }
    internal class Month
    {
        public string name;
        Day[] daysInMonth;
        int[] molad;
        public Month(string name,bool leap)
        {
            this.name=name;
            this.daysInMonth = leap ? new Day[30] : new Day[29];
            for (int i = 0; i < daysInMonth.Length; i++)
            {
                this.daysInMonth[i] = new Day(i+1);
            }
        }
        public Month(string name, bool leap,int[] molad): this(name,leap)
        {
            this.molad = molad;
        }
        public void PrintMonth()
        {
            for (int i = 0; i < daysInMonth.Length; i++)
            {
                Console.Write($"(M: {name} D:{this.daysInMonth[i].date}) "); ;
            }
        }
    }
    
}

