using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMW_Mathematics.Model
{
    class DataToLineChartView                       //Klasa odpowadająca za przekazanie odpowiednich danych do generowania wykresu liniowego #M
    {
        public double currentX { get; set; }                //aktualna wartość x w danym punkcie #M
        public double currentY { get; set; }                //aktualny wartość y w danym punkcie #M
        public string TooltipData { get; set; }             //dane znadujące się w tooltipie  #M
        public bool ShowTooltip { get; set; }               //widoczność tooltip zależne od kliknięcia #M
        public string TypeChart { get; set; }               //typ wykresu #M
        public bool ToogleGridLineView { get; set; }        //widoczność szczegółowej siatki #M
    }
}
