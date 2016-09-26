using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaximaSharp;

namespace AMW_Mathematics.ModelView
{
    public class DataToChart //klasa umożiwa ładowanie punktów danej funkcji do wykresu #M
    {
        public List<string> ListVariablesToChart;
        public DataToChart()
        {
            ListVariablesToChart = new List<string>();
            ListVariablesToChart.Add("x");
        }
        public string SeriesID { get; set; }//akcelerator get; set; umożliwia dostep do właściwości Ayis w klasie DataToChart potrzebnej do ładowania danych do wykresu #M
        public double Ayis { get; set; } //akcelerator get; set; umożliwia dostep do właściwości Ayis w klasie DataToChart potrzebnej do ładowania danych do wykresu #M
        public double Axis { get; set; } //akcelerator get; set; umożliwia dostep do właściwości Axis w klasie DataToChart potrzebnej do ładowania danych do wykresu #M
        public List<DataToChart> CountYwithX(List<string> ListFunction, List<DataToChart> DataToChartList, DataToChart DataToCharts, MainWindow Main, int zoomi, int zoomj)
        {
            foreach (var f in ListFunction)
            {
                for (double i = zoomi; i < zoomj; i = i + 0.1)
                {
                    string v = "";
                    foreach (var vari in DataToCharts.ListVariablesToChart)
                    {
                        v += f.Replace(vari, "(" + Math.Round(i, 1).ToString() + ")");
                    }
                    v = v.Replace(",", ".");
                    v = Main.AddToNumberDot(v);
                    v = Maxima.Eval(v);
                    v = v.Replace(".", ",");
                    DataToChartList.Add(new DataToChart { SeriesID = f, Axis = (double.Parse(Math.Round(i, 1).ToString())), Ayis = (double.Parse(v)) });
                }
            }
            return DataToChartList;
        }
        public List<DataToChart> CountYwithXWithUpdata(List<string> ListFunction, List<DataToChart> DataToChartList, DataToChart DataToCharts, MainWindow Main, int zoomi, int zoomj)
        {
            foreach (var f in ListFunction)
            {
                for (double i = -6.1; i > zoomi; i = i - 0.1)
                {
                    string v = "";
                    foreach (var vari in DataToCharts.ListVariablesToChart)
                    {
                        v += f.Replace(vari, "(" + Math.Round(i, 1).ToString() + ")");
                    }
                    v = v.Replace(",", ".");
                    v = Main.AddToNumberDot(v);
                    v = Maxima.Eval(v);
                    v = v.Replace(".", ",");
                    DataToChartList.Insert(0, (new DataToChart { SeriesID = f, Axis = (double.Parse(Math.Round(i, 1).ToString())), Ayis = (double.Parse(v)) }));
                }
                for (double i = 6; i < zoomj; i = i + 0.1)
                {
                    string v = "";
                    foreach (var vari in DataToCharts.ListVariablesToChart)
                    {
                        v += f.Replace(vari, "(" + Math.Round(i, 1).ToString() + ")");
                    }
                    v = v.Replace(",", ".");
                    v = Main.AddToNumberDot(v);
                    v = Maxima.Eval(v);
                    v = v.Replace(".", ",");
                    DataToChartList.Add(new DataToChart { SeriesID = f, Axis = (double.Parse(Math.Round(i, 1).ToString())), Ayis = (double.Parse(v)) });
                }
            }
            return DataToChartList;
        }
    }
}
