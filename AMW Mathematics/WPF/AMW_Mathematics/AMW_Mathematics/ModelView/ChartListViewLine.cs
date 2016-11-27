using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AMW_Mathematics.ModelView
{
    public class ChartListViewLine                      //Klasa opodiwada za załadowanie numeru numeru funkcji oraz typu wykresu do bindowanych pól ListView w XAML #M
    {
        private string labelChartValue { get; set; }    //labelka do dynamicznie generującego się pola w ktore wprowadza się wyrażenie #M
        public string TextInChartPlot { get; set; }     //rodzaj wykresu #M
        private string typeChart { get; set; }          //ryp wykresu #M
        public string CountFunction { get; set; }       //liczba funkcji #M
        public string LabelChartValue
        {
            get
            {
                return this.labelChartValue;
            }
            set
            {
                this.labelChartValue = value;
            }
        }
    }
}
