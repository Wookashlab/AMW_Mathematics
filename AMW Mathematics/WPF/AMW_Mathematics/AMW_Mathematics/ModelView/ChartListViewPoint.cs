using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMW_Mathematics.ModelView
{
    class ChartListViewPoint                                    //model dostarczający dane do listy funkcji generujących wykres #M      
    {
        private string labelChartPointValue { get; set; }       //labelka dynamicznej listy #M
        public int Index { get; set; }                          //pozycja funkcji #M
        public string TextBoxText { get; set; }                 //wartośc funkcji #M
        public string LabelChartPointValue
        {
            get
            {
                return this.labelChartPointValue;
            }
            set
            {
                this.labelChartPointValue = value;
            }
        }
    }
}
