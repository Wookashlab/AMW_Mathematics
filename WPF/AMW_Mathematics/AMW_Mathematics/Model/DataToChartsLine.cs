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
        private Expression phrase = new Expression();
        public DataToChartsLine()
        {
            ListVariablesToChart = new List<string>();
            ListVariablesToChart.Add("x");
        }
        public string SeriesID { get; set; }//akcelerator get; set; umożliwia dostep do właściwości Ayis w klasie DataToChart potrzebnej do ładowania danych do wykresu #M
        public double Ayis { get; set; } //akcelerator get; set; umożliwia dostep do właściwości Ayis w klasie DataToChart potrzebnej do ładowania danych do wykresu #M
        public double Axis { get; set; } //akcelerator get; set; umożliwia dostep do właściwości Axis w klasie DataToChart potrzebnej do ładowania danych do wykresu #M
        public List<DataToChartsLine> CountYwithX(List<string> ListFunction1, List<string> ListFunction, List<DataToChartsLine> DataToChartList, DataToChartsLine DataToCharts, MainWindow Main, int zoomi, int zoomj)
        {
            bool IFunction1 = false;                                                                                                                        //deklaracja zmiennej sprawdzającej do ktorej listy mają zostać dodane funkcje 
            for (int j = 0; j < ListFunction.Count; j++)                                                                                                    //Pelta sprawdza czy wykres posiada miejsca zerowe czy też nie, w zależności czy posia czy nie funkcja ladowana jest do odpowiedniej listy #M
            {

                for (double i = zoomi; i < zoomj; i = i + 0.1)                                                                                              //pętla sprawdzająca czy funcja nie posada miejsc zerowcyh #M
                {
                    string v = "";
                    foreach (var vari in DataToCharts.ListVariablesToChart)                                                                                 //Wprowadzenie do ciągu znaków wartości zmiennej X #M
                    {
                        v += ListFunction[j].Replace(vari, "(" + Math.Round(i, 1).ToString() + ")");
                    }
                    v = v.Replace(",", ".");
                    v = phrase.AddToNumberDot(v);
                    int index = v.IndexOf("/");
                    string check = v.Substring(index + 1, v.Length - index - 1);                                                                            //operacje umożliwające zamianę ciągu znaków w taki sposób aby mógł być wprowadzony do maximy, tak zeby program się nei zawiesil #M
                    //if (check.Contains("(0.0)^") == false) //sprawdzenie czy jest potęgą nie działa 
                    //{
                    check = Maxima.Eval(check);
                    if (check != "0.0" || v.Contains("/") == false)                                                                                         //warunek sprawdania czy w wyrażenie nei jest 0 jeśli jest oznacza to że funkcja nie przecina osi X i trzeba ją dodać do funlcji w liście ListFunciton1 #M
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
                    ListFunction.RemoveAt(j);                                                                                                               //usunięci z listy funkcj która nie przecina osi X #M
                    j = j - 1;
                    IFunction1 = false;
                }
            }
            foreach (var f in ListFunction1)                                                                                                                //po rozdzieleniu funkcji na przecinające oś X i nie przecinające osi X ustalenie ich punktów #M
            {
                for (double i = zoomi; i < zoomj; i = i + 0.1)                                                                                              //ustalenie zakresu generowania punktów #M
                {
                    string v = "";
                    foreach (var vari in DataToCharts.ListVariablesToChart)                                                                                 //przygotowanie stringa tak aby wprowadzić go do maximy #M
                    {
                        v += f.Replace(vari, "(" + Math.Round(i, 1).ToString() + ")");
                    }
                    v = v.Replace(",", ".");
                    v = phrase.AddToNumberDot(v);
                    int index = v.IndexOf("/");
                    string check = v.Substring(index + 1, v.Length - index - 1);
                    //if (check.Contains("(0.0)^") == false)
                    //{
                    check = Maxima.Eval(check);
                    if (check != "0.0" || v.Contains("/") == false)
                    {
                        v = Maxima.Eval(v);
                        v = v.Replace(".", ",");
                        DataToChartList.Add(new DataToChartsLine { SeriesID = f, Axis = (double.Parse(Math.Round(i, 1).ToString())), Ayis = (double.Parse(v)) }); //dodanie do listy wygenerowanego punktu 
                    }
                    //}
                }
            }
            foreach (var f in ListFunction)                                                                                                                 //procedura analogiczna to powyższej #M
            {
                for (double i = zoomi; i < zoomj; i = i + 0.1)
                {
                    string v = "";
                    foreach (var vari in DataToCharts.ListVariablesToChart)
                    {
                        v += f.Replace(vari, "(" + Math.Round(i, 1).ToString() + ")");
                    }
                    v = v.Replace(",", ".");
                    v = phrase.AddToNumberDot(v);
                    int index = v.IndexOf("/");
                    v = Maxima.Eval(v);
                    v = v.Replace(".", ",");
                    DataToChartList.Add(new DataToChartsLine { SeriesID = f, Axis = (double.Parse(Math.Round(i, 1).ToString())), Ayis = (double.Parse(v)) });
                }
            }
            return DataToChartList;                                                                                                                         //zwrócenie listy punktów obliczonyych funkcji wraz z nazwami funkcji gdzie później są one grupowane #M
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