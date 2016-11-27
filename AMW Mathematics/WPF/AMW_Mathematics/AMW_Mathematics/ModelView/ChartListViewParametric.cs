using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMW_Mathematics.ModelView
{
    class ChartListViewParametric                                       //model dostarczający dane do listy funkcji generujących wykres #M
    {
        private string labelChartParametricValue { get; set; }          //labelka świadcząca o numrze funkcji #M
        public string LabelChartParametricValue
        {
            get
            {
                return this.labelChartParametricValue;
            }
            set
            {
                this.labelChartParametricValue = value;
            }
        }
    }
}
