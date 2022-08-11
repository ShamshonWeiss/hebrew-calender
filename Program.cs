using System;

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
    internal class Program
    {
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
    internal class Year
    {
        public string yearName;
        private bool leap;
        public bool Leap { get { return leap; } }
        public int[] RH { get { return rh; } }
        private int[] rh;
        public int[] nextRh;
        private int extraDays;
        private bool[] leapMonths;
        private string[] monthNames;
        Month[] months;
        public Year(int year)
        {
            int[] oldM = GetMoladRh(year);
            this.leap = GetLeap(year);
            bool prevLeap = GetLeap(year - 1);
            bool nextLeap = GetLeap(year + 1);
            int[] nextMoled = AddMoledYear(oldM, leap);
            this.rh = DaledDechios(oldM, leap, prevLeap);
            this.nextRh = DaledDechios(nextMoled, nextLeap, leap);
            this.extraDays = ((nextRh[0] + 7) - rh[0]) % 7;//might be better off basing on num of dechios but thean you need to cheshbon if the extra chelakim make an extra day
            this.leapMonths = Chesiros(leap, extraDays);

            this.months = leap ? new Month[13] : new Month[12];
            int[] monthMolad = oldM;
            for (int i = 0; i < months.Length; i++)
            {
                months[i] = new Month(monthNames[i], leapMonths[i], monthMolad);
                monthMolad = AddMoled(monthMolad, 1);
            }
        }
        public Month this[int index] => months[index];
        public Month this[string monthName] => GetMonth(monthName);
        public static int[] GetMoladRh(int year)
        {
            year--;
            int[] moladToho = { 2, 5, 204 };
            int[] monthsIntoC = { 0, 12, 24, 37, 49, 61, 74, 86, 99, 111, 123, 136, 148, 160, 173, 185, 197, 210, 222 };
            int[] moladRh = AddLunarCycle(moladToho, year / 19);
            moladRh = AddMoled(moladRh, monthsIntoC[(year % 19)]);
            return moladRh;
        }
        public static bool GetLeap(int yearNum)
        {
            int yearInSCycle = yearNum % 19;
            switch (yearInSCycle)
            {
                case 1:
                case 2:
                case 4:
                case 5:
                case 7:
                case 9:
                case 10:
                case 12:
                case 13:
                case 15:
                case 16:
                case 18:
                    return false;
                default:
                    return true;
            }
        }

        public static int[] AddMoled(int[] previousM, int mLater) // takes a molad[] and an int and returns the molad int months later
        {
            //int[] monthLength = { 29, 12, 793 }; // dd hh chelakim(793/1080)
            int[] odefChodesh = { 1, 12, 793 };
            int[] newM = new int[3];
            for (int i = 0; i < 3; i++)
            {
                newM[i] = (previousM[i] + (odefChodesh[i] * mLater));
            }
            MoledFixer(ref newM);
            return newM;
        }
        public static int[] AddLunarCycle(int[] previousM, int cLater) // takes a molad[] and an int and returns the molad int 19year cycles later
        {
            int[] odefCycle = { 2, 16, 595 };
            int[] newM = new int[3];
            for (int i = 0; i < 3; i++)
            {
                newM[i] = (previousM[i] + (odefCycle[i] * cLater));
            }
            MoledFixer(ref newM);
            return newM;
        }
        public static int[] AddMoledYear(int[] roshHashana, bool leap) // takes a molad[] for Rosh Hashana and a bool if  leap year returns the molad next Rosh Hashana
        {
            int[] newM = !leap ? AddMoled(roshHashana, 12) : AddMoled(roshHashana, 13);
            return newM;
        }
        internal static void MoledFixer(ref int[] moled)//fix a molad[] make chelakeim into hours and hours into days 
        {
            int[] maxValue = { 7, 24, 1080 };
            for (int i = (moled.Length - 1); i >= 0; i--)
            {
                int nextField = moled[i] / maxValue[i];
                if (i > 0)
                {
                    moled[i - 1] = moled[i - 1] + nextField;
                }
                moled[i] = moled[i] % maxValue[i];
            }
        }
        static int[] DaledDechios(int[] rhMolad, bool leap, bool prevLeap)//takes molad[] of Rosh Hashana returns day of week for Rosh Hashana,bool if leap year,bool previous year was leap 
        {
            int dayOfRh = rhMolad[0];
            int dechios = 0;
            if (rhMolad[1] >= 18)//molad zakien
            {
                dechios++;
                dayOfRh++;
            }
            else if (!leap && rhMolad[0] == 3 && (rhMolad[1] > 9 || (rhMolad[1] == 8 && rhMolad[2] >= 204)))
            {//tuesday molad in non leap year with value grater then h:9 chelakim :204
                dechios++;
                dayOfRh++;
            }
            else if (prevLeap && rhMolad[0] == 2 && (rhMolad[1] > 15 || (rhMolad[1] == 15 && rhMolad[2] >= 589)))
            {
                dechios++;
                dayOfRh++;
            }
            if (rhMolad[0] == 1 || rhMolad[0] == 4 || rhMolad[0] == 6)//Rosh Hashana can't be on Sunday, Wendsday or Thursday 
            {
                dechios++;
                dayOfRh++;
            }
            int[] roshHashana = { dayOfRh, dechios };
            return roshHashana;
        }
        public bool[] Chesiros(bool leap, int extraDays)
        {
            bool[] leapMonths = leap ? new bool[] { true, false, true, false, true, true, false, true, false, true, false, true, false } :
                new bool[] { true, false, true, false, true, false, true, false, true, false, true, false };
            this.monthNames = leap ? new string[] { "tishrei", "cheshvan", "kisleiv", "teivis", "shvat", "adarI", "adarII", "nissan", "iyar", "sivan", "tamuz", "av", "elul" } :
                new string[] { "tishrei", "cheshvan", "kisleiv", "teivis", "shvat", "adar", "nissan", "iyar", "sivan", "tamuz", "av", "elul" };
            if ((leap && extraDays == 5) || (!leap && extraDays == 3))//too litle days in year = make kisleiv not leap 
            {
                leapMonths[2] = false;
                yearName = "choser";
            }
            else if ((leap && extraDays == 0) || (!leap && extraDays == 5))
            {
                leapMonths[1] = true;
                yearName = "shlima";
            }
            else if ((leap && extraDays == 6) || (!leap && extraDays == 4))
            {
                yearName = "kisidran";
            }
            else { yearName = "error"; }
            return leapMonths;
        }
        public Month GetMonth(string monthName)
        {
            for (int i = 0; i < monthNames.Length; i++)
            {
                if (months[i].name.ToLower() == monthName.ToLower())
                {
                    return months[i];
                }
            }
            return null;
        }
        public static void PrintMoled(int[] moled)
        {
            Console.WriteLine($"Day of week: {moled[0]}");
            Console.WriteLine($"Hour: {moled[1]}");
            Console.WriteLine($"Chelakim: {moled[2]}");
        }

    }
}
