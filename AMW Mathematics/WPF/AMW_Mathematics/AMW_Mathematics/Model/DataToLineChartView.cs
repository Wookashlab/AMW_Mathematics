using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMW_Mathematics.Model
{
    class DataToLineChartView
    {
        public double currentX { get; set; }
        public double currentY { get; set; }
        public string TooltipData { get; set; }
        public bool ShowTooltip { get; set; }
        public string TypeChart { get; set; }
        public bool ToogleGridLineView { get; set; }
    }
}
