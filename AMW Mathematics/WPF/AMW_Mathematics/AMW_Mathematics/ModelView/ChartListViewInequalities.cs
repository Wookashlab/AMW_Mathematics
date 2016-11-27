using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMW_Mathematics.ModelView
{
    class ChartListViewInequalities                                 //model dostarczający dane do listy funkcji generujących wykres #M
    {
        private string labelChartInequalitiesValue { get; set; }    //labelka świadcząca o numrze funkcji #M
        public string LabelChartInequalitiesValue
        {
            get
            {
                return this.labelChartInequalitiesValue;
            }
            set
            {
                this.labelChartInequalitiesValue = value;
            }
        }
    }
}
