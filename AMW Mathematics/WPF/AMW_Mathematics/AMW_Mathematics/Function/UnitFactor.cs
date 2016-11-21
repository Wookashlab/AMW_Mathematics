using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AMW_Mathematics.Function
{
    class UnitFactor
    {


        public double unitF;

        public UnitFactor(string unit1, string unit2) {

            var list = new List<Tuple<string, string, double>>();
            list.Add(new Tuple<string, string, double>("microseconds", "miliseconds", 0.001));
            list.Add(new Tuple<string, string, double>("microseconds", "seconds", 1E-06));
            list.Add(new Tuple<string, string, double>("microseconds", "minutes", 1.66666666666667E-08));
            list.Add(new Tuple<string, string, double>("microseconds", "hours", 2.77777777777778E-10));
            list.Add(new Tuple<string, string, double>("microseconds", "days", 1.15740740740741E-11));


            list.Add(new Tuple<string, string, double>("miliseconds", "seconds", 0.001));
            list.Add(new Tuple<string, string, double>("miliseconds", "minutes", 1.66666666666667E-05));
            list.Add(new Tuple<string, string, double>("miliseconds", "hours", 2.77777777777778E-07));
            list.Add(new Tuple<string, string, double>("miliseconds", "days", 1.15740740740741E-08));

            list.Add(new Tuple<string, string, double>("seconds", "minutes", 0.0166666666666667));
            list.Add(new Tuple<string, string, double>("seconds", "hours", 0.000277777777777778));
            list.Add(new Tuple<string, string, double>("seconds", "days", 1.15740740740741E-05));

            list.Add(new Tuple<string, string, double>("minutes", "hours", 0.0166666666666667));
            list.Add(new Tuple<string, string, double>("minutes", "days", 0.000694444444444444));

            list.Add(new Tuple<string, string, double>("hours", "days", 0.0416666666666667));



            foreach (var lst in list)
            {
                if (unit1.Equals(unit2))
                    unitF = 1;
                else
                {
                    if (lst.Item1.Equals(unit1) && lst.Item2.Equals(unit2))
                    {
                        unitF = lst.Item3;
                    }
                    else
                    {
                        if (lst.Item1.Equals(unit2) && lst.Item2.Equals(unit1))
                        {
                            unitF = 1/lst.Item3;
                        }
                    }
                }
            }
        }
    }

   
}
