using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMW_Mathematics.Model
{
    class DataToLineChartView                       //Klasa odpowadająca za przekazanie odpowiednich danych do generowania wykresu liniowego #M
    {
        public double currentX { get; set; }                //aktualny x #M
        public double currentY { get; set; }                //aktualny y #M
        public string TooltipData { get; set; }             //dane do legendy #M
        public bool ShowTooltip { get; set; }               //widoczność legendy #M
        public string TypeChart { get; set; }               //typ wykresu #M
        public bool ToogleGridLineView { get; set; }        //widoczność szczegółowej siatki #M
    }
}
