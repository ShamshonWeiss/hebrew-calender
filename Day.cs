using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoladBasedCalender
{
    internal class Day
    {
        public Day(int date)
        {
            this.date = date;
        }
        /*
         * to be added to code at a later time
        public Day(int day, string[] eventName) : this(day)
        {
            this.eventName = eventName;
        }
                string[] eventName;
                */
        public int date;
    }
    internal class Month
    {
        public string name;
        public bool leap;
        Day[] daysInMonth;
        public int[] molad;
        public Month(string name, bool leap)
        {
            this.name = name;
            this.leap = leap;
            this.daysInMonth = leap ? new Day[30] : new Day[29];
            for (int i = 0; i < daysInMonth.Length; i++)
            {
                this.daysInMonth[i] = new Day(i + 1);
            }
        }
        public Month(string name, bool leap, int[] molad) : this(name, leap)
        {
            this.molad = molad;
        }
        public void PrintMonth()
        {
            Console.WriteLine("Chodesh: " + name);
            for (int i = 0; i < daysInMonth.Length; i++)
            {
                Console.Write($"[{this.daysInMonth[i].date}] "); ;
            }
        }
    }

}

