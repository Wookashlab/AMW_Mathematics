using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaximaSharp;

namespace AMW_Mathematics.ModelView
{
    public class DataToChartsLine //klasa umożiwa ładowanie punktów danej funkcji do wykresu #M
    {
        public List<string> ListVariablesToChart;
        private Expreson phrase = new Expreson();
        public DataToChartsLine()
        {
            ListVariablesToChart = new List<string>();
            ListVariablesToChart.Add("x");
        }
        public Dictionary<string, double> PD { get; set; }
        public string SeriesID { get; set; }//akcelerator get; set; umożliwia dostep do właściwości Ayis w klasie DataToChart potrzebnej do ładowania danych do wykresu #M
        public double Ayis { get; set; } //akcelerator get; set; umożliwia dostep do właściwości Ayis w klasie DataToChart potrzebnej do ładowania danych do wykresu #M
        public double Axis { get; set; } //akcelerator get; set; umożliwia dostep do właściwości Axis w klasie DataToChart potrzebnej do ładowania danych do wykresu #M
        public List<DataToChartsLine> CountYwithX(List<string> ListFunction1, List<string> ListFunction, List<DataToChartsLine> DataToChartList, DataToChartsLine DataToCharts, double zoomi, double zoomj, double step, int round)
        {                                                                  //deklaracja zmiennej sprawdzającej do ktorej listy mają zostać dodane funkcje 
            PD = new Dictionary<string, double>();
            string countlim;
            string series = "#";
            for (int j = 0; j < ListFunction.Count; j++)                                                                                                    //Pelta sprawdza czy wykres posiada miejsca zerowe czy też nie, w zależności czy posia czy nie funkcja ladowana jest do odpowiedniej listy #M
            {
                for (double i = zoomi; i < zoomj; i = i + step)                                                                                              //pętla sprawdzająca czy funcja nie posada miejsc zerowcyh #M
                {
                    countlim = "limit(" + ListFunction[j] + "," + "x" + "," + Math.Round(i, round).ToString().Replace(",", ".") + ")";
                    countlim = phrase.AddToNumberDot(countlim);
                    try
                    {
                        countlim = Maxima.Eval(countlim);
                        countlim = countlim.Replace(".", ",");
                        double countlimd = double.Parse(countlim);

                        DataToChartList.Add((new DataToChartsLine { SeriesID = ListFunction[j] + series, Axis = (double.Parse(Math.Round(i, round).ToString())), Ayis = countlimd }));
                    }
                    catch
                    {
                        PD.Add(ListFunction[j] + series, Math.Round(i, 2));
                        series = series + "#";
                    }
                }
            }
            return DataToChartList;
        }
        public List<DataToChartsLine> CountYwithXWithUpdata(List<string> ListFunction, List<DataToChartsLine> DataToChartList, DataToChartsLine DataToCharts, MainWindow Main, double zoomistart, double zoomjstart, int zoomi, int zoomj)
        {
            foreach (var f in ListFunction)                                                                                                                         //metoda zwiększająca lub zmniejszająca liczbę punktów na wykresie #M
            {
                for (double i = zoomistart; i > zoomi; i = i - 0.1)                                                                                                 //ustalenie zakresu od jakiego do jakiego mają być generowane punkty #M
                {                                                                                                                                                   //wszystkie punkt generowane sa dla wartości dodatnich X #M
                    string v = "";
                    foreach (var vari in DataToCharts.ListVariablesToChart)                                                                                         //przygotwanie stringa tak aby wprwoadzić go do maximy #M
                    {
                        v += f.Replace(vari, "(" + Math.Round(i, 1).ToString() + ")");
                    }
                    v = v.Replace(",", ".");
                    v = phrase.AddToNumberDot(v);
                    v = Maxima.Eval(v);                                                                                                                             //zapisanie wyniku maximi do zmiennej v czyli defakto zapisanie wyniku funcji o zadanym x #M
                    v = v.Replace(".", ",");
                    DataToChartList.Add((new DataToChartsLine { SeriesID = f, Axis = (double.Parse(Math.Round(i, 1).ToString())), Ayis = (double.Parse(v)) }));          //dodanei do listy punktów obliczonej funkcji #M
                }
                for (double i = zoomjstart; i < zoomj; i = i + 0.1)                                                                                                 //analogicznie jak w przypadku powyższym tylko że dla wartości X ujemnych #M                                                                 
                {
                    string v = "";
                    foreach (var vari in DataToCharts.ListVariablesToChart)
                    {
                        v += f.Replace(vari, "(" + Math.Round(i, 1).ToString() + ")");
                    }
                    v = v.Replace(",", ".");
                    v = phrase.AddToNumberDot(v);
                    v = Maxima.Eval(v);
                    v = v.Replace(".", ",");
                    DataToChartList.Add(new DataToChartsLine { SeriesID = f, Axis = (double.Parse(Math.Round(i, 1).ToString())), Ayis = (double.Parse(v)) });
                }
            }
            return DataToChartList;                                                                                                                                 //zwrócenie listy wygenerowanych punktow dla funkcji #M
        }
        public List<DataToChartsLine> CountYwithXWithUpdataTwoLine(List<string> ListFunction, List<DataToChartsLine> DataToChartList, DataToChartsLine DataToCharts, MainWindow Main, double zoomistart, double zoomjstart, int zoomi, int zoomj, double startminim, double endminim, double countzoom, int roundtozoom)
        {                                                                                                                                                           //funkcja zwiekszająca lub zmiejszająca wykresy funkcji, która nie ma przecięcia z osią X #M
            foreach (var f in ListFunction)
            {
                for (double i = zoomistart; i > zoomi; i = i - 0.1)                                                                                                 //ustalenie pętli do wygenerowania określonej liczby punktów #M dla X wiekszego od 0 #M
                {
                    string v = "";
                    foreach (var vari in DataToCharts.ListVariablesToChart)                                                                                         //przygotowanie stringa do maximy #M
                    {
                        v += f.Replace(vari, "(" + Math.Round(i, 1).ToString() + ")");
                    }
                    v = v.Replace(",", ".");
                    v = phrase.AddToNumberDot(v);
                    if (i != 0)
                    {
                        v = Maxima.Eval(v);
                        v = v.Replace(".", ",");
                        DataToChartList.Add((new DataToChartsLine { SeriesID = f, Axis = (double.Parse(Math.Round(i, 1).ToString())), Ayis = (double.Parse(v)) }));      //dodanie wygenerowanego punktu do listy #M
                    }
                }
                for (double i = zoomjstart; i < zoomj; i = i + 0.1)                                                                                                 //ustalenie pętli do wygenerowania określonej liczby punktów #M dla X mniejszego od 0 #M
                {                                                                                                                                                   //pozostała cześć analogiczna do powyższej części metody #M
                    string v = "";
                    foreach (var vari in DataToCharts.ListVariablesToChart)
                    {
                        v += f.Replace(vari, "(" + Math.Round(i, 1).ToString() + ")");
                    }
                    v = v.Replace(",", ".");
                    v = phrase.AddToNumberDot(v);
                    if (i != 0)
                    {
                        v = Maxima.Eval(v);
                        v = v.Replace(".", ",");
                        DataToChartList.Add((new DataToChartsLine { SeriesID = f, Axis = (double.Parse(Math.Round(i, 1).ToString())), Ayis = (double.Parse(v)) }));      //dodanie wygenerowanego punktu do listy #M
                    }
                }
                if (endminim > 5E-05 && zoommax == 0)                                                                                                               //ustalenie minimalnego pułapu X i maxymanlnego pułapu Y #M
                {                                                                                                                                                   //w przypadku gdy zmienna zoomax nie będzie zero to oznacza że pułap minimalny został osiągnięty i teraz może wykonywać się tylko zwiększanie lub zmniejszanie X tak długo aż zmienna zoomax nie bedzie z powrotem 0
                    for (double i = startminim; i > endminim; i = i - countzoom)                                                                                    //generowanie X mniejszefo od 0.1 pozostałe części generowania analogiczne jak powyżej #M
                    {
                        string v = "";
                        foreach (var vari in DataToCharts.ListVariablesToChart)
                        {
                            v += f.Replace(vari, "(" + Math.Round(i, roundtozoom).ToString() + ")");
                        }
                        v = v.Replace(",", ".");
                        v = phrase.AddToNumberDot(v);
                        v = Maxima.Eval(v);
                        v = v.Replace(".", ",");
                        DataToChartList.Add((new DataToChartsLine { SeriesID = f, Axis = (double.Parse(Math.Round(i, roundtozoom).ToString())), Ayis = (double.Parse(v)) }));
                    }
                    for (double i = -startminim; i < -endminim; i = i + countzoom)
                    {
                        string v = "";
                        foreach (var vari in DataToCharts.ListVariablesToChart)
                        {
                            v += f.Replace(vari, "(" + Math.Round(i, roundtozoom).ToString() + ")");
                        }
                        v = v.Replace(",", ".");
                        v = phrase.AddToNumberDot(v);
                        v = Maxima.Eval(v);
                        v = v.Replace(".", ",");
                        DataToChartList.Add(new DataToChartsLine { SeriesID = f, Axis = (double.Parse(Math.Round(i, roundtozoom).ToString())), Ayis = (double.Parse(v)) });
                    }
                }
                else
                {
                    zoommax++;
                }
            }
            return DataToChartList;                                                                                                                                   //zwrócenie listy punktów danej funkcji, które należy usunąć lub dodać #M
        }
        public int zoommax { get; set; }                                                                                                                              //zmienna zabezpiczająca przed zbyt dużym zwiekszaniem się wartości Y na wykresie w momencie gdy jest zbyt duża #M 
    }
}