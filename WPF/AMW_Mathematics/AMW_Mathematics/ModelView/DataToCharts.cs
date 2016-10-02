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
        public List<DataToChart> CountYwithX(List<string> ListFunction1, List<string> ListFunction, List<DataToChart> DataToChartList, DataToChart DataToCharts, MainWindow Main, int zoomi, int zoomj)
        {
            bool IFunction1 = false;
            for (int j = 0; j < ListFunction.Count; j++)
            {

                for (double i = zoomi; i < zoomj; i = i + 0.1)
                {
                    string v = "";
                    foreach (var vari in DataToCharts.ListVariablesToChart)
                    {
                        v += ListFunction[j].Replace(vari, "(" + Math.Round(i, 1).ToString() + ")");
                    }
                    v = v.Replace(",", ".");
                    v = Main.AddToNumberDot(v);
                    int index = v.IndexOf("/");
                    string check = v.Substring(index + 1, v.Length - index - 1);
                    //if (check.Contains("(0.0)^") == false)
                    //{
                    check = Maxima.Eval(check);
                    if (check != "0.0" || v.Contains("/") == false)
                    {
                    }
                    else
                    {
                        ListFunction1.Add(ListFunction[j]);
                        IFunction1 = true;
                    }
                }
                if (IFunction1 == true)
                {
                    ListFunction.RemoveAt(j);
                    j = j - 1;
                    IFunction1 = false;
                }
            }
            foreach (var f in ListFunction1)
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
                    int index = v.IndexOf("/");
                    string check = v.Substring(index + 1, v.Length - index - 1);
                    //if (check.Contains("(0.0)^") == false)
                    //{
                    check = Maxima.Eval(check);
                    if (check != "0.0" || v.Contains("/") == false)
                    {
                        v = Maxima.Eval(v);
                        v = v.Replace(".", ",");
                        DataToChartList.Add(new DataToChart { SeriesID = f, Axis = (double.Parse(Math.Round(i, 1).ToString())), Ayis = (double.Parse(v)) });
                    }
                    //}
                }
            }
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
                    int index = v.IndexOf("/");
                    v = Maxima.Eval(v);
                    v = v.Replace(".", ",");
                    DataToChartList.Add(new DataToChart { SeriesID = f, Axis = (double.Parse(Math.Round(i, 1).ToString())), Ayis = (double.Parse(v)) });
                }
            }
            return DataToChartList;
        }
        public List<DataToChart> CountYwithXWithUpdata(List<string> ListFunction, List<DataToChart> DataToChartList, DataToChart DataToCharts, MainWindow Main, double zoomistart, double zoomjstart, int zoomi, int zoomj)
        {
            foreach (var f in ListFunction)
            {
                for (double i = zoomistart; i > zoomi; i = i - 0.1)
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
                    DataToChartList.Add((new DataToChart { SeriesID = f, Axis = (double.Parse(Math.Round(i, 1).ToString())), Ayis = (double.Parse(v)) }));
                }
                for (double i = zoomjstart; i < zoomj; i = i + 0.1)
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
        public List<DataToChart> CountYwithXWithUpdataTwoLine(List<string> ListFunction, List<DataToChart> DataToChartList, DataToChart DataToCharts, MainWindow Main, double zoomistart, double zoomjstart, int zoomi, int zoomj, double startminim, double endminim, double countzoom, int roundtozoom)
        {
            foreach (var f in ListFunction)
            {
                for (double i = zoomistart; i > zoomi; i = i - 0.1)
                {
                    string v = "";
                    foreach (var vari in DataToCharts.ListVariablesToChart)
                    {
                        v += f.Replace(vari, "(" + Math.Round(i, 1).ToString() + ")");
                    }
                    v = v.Replace(",", ".");
                    v = Main.AddToNumberDot(v);
                    if (i != 0)
                    {
                        v = Maxima.Eval(v);
                        v = v.Replace(".", ",");
                        DataToChartList.Add((new DataToChart { SeriesID = f, Axis = (double.Parse(Math.Round(i, 1).ToString())), Ayis = (double.Parse(v)) }));
                    }
                }
                for (double i = zoomjstart; i < zoomj; i = i + 0.1)
                {
                    string v = "";
                    foreach (var vari in DataToCharts.ListVariablesToChart)
                    {
                        v += f.Replace(vari, "(" + Math.Round(i, 1).ToString() + ")");
                    }
                    v = v.Replace(",", ".");
                    v = Main.AddToNumberDot(v);
                    if (i != 0)
                    {
                        v = Maxima.Eval(v);
                        v = v.Replace(".", ",");
                        DataToChartList.Add((new DataToChart { SeriesID = f, Axis = (double.Parse(Math.Round(i, 1).ToString())), Ayis = (double.Parse(v)) }));
                    }
                }
                if (endminim > 5E-05 && zoommax == 0)
                {
                    for (double i = startminim; i > endminim; i = i - countzoom)
                    {
                        string v = "";
                        foreach (var vari in DataToCharts.ListVariablesToChart)
                        {
                            v += f.Replace(vari, "(" + Math.Round(i, roundtozoom).ToString() + ")");
                        }
                        v = v.Replace(",", ".");
                        v = Main.AddToNumberDot(v);
                        v = Maxima.Eval(v);
                        v = v.Replace(".", ",");
                        DataToChartList.Add((new DataToChart { SeriesID = f, Axis = (double.Parse(Math.Round(i, roundtozoom).ToString())), Ayis = (double.Parse(v)) }));
                    }
                    for (double i = -startminim; i < -endminim; i = i + countzoom)
                    {
                        string v = "";
                        foreach (var vari in DataToCharts.ListVariablesToChart)
                        {
                            v += f.Replace(vari, "(" + Math.Round(i, roundtozoom).ToString() + ")");
                        }
                        v = v.Replace(",", ".");
                        v = Main.AddToNumberDot(v);
                        v = Maxima.Eval(v);
                        v = v.Replace(".", ",");
                        DataToChartList.Add(new DataToChart { SeriesID = f, Axis = (double.Parse(Math.Round(i, roundtozoom).ToString())), Ayis = (double.Parse(v)) });
                    }
                }
                else
                {
                    zoommax++;
                }
            }
            return DataToChartList;
        }
        public int zoommax { get; set; }
    }
}