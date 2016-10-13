using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaximaSharp;

namespace AMW_Mathematics.ModelView
{
    public class DataToPointChartView //klasa umożiwa ładowanie punktów danej funkcji do wykresu #M
    {
        public double dataX { get; set; }//zmienna zabezpiczająca przed zbyt dużym zwiekszaniem się wartości Y na wykresie w momencie gdy jest zbyt duża #M 
        public double dataY { get; set; }
        public int functionId { get; set; }
    }
}