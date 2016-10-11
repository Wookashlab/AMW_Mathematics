using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace AMW_Mathematics.ModelView
{
    public class ChartPointView : INotifyPropertyChanged
    {
        private PlotModel plotModel;
        public PlotModel PlotModel                                          //użycie akceleratorów get; set; w celu uzyskania dostępu do prywatnego pola plotMode. Wlłaściwość PlotModel jest publiczna dzieki temu możeby bindować w XAML prawatną zmienną plotMode                             // 
        {                                                                   //czyli możemy wyświetlić wykres z jego wszystkimi parametrami ustawianymi w następnych krokach #M
            get { return plotModel; }
            set { plotModel = value; OnPropertyChanged("PlotModel"); }
        }
        public ChartPointView(List<DataToPointChartView> ReturFunctionValueToChart)        //konstruktor klasy ViewPlot wykona się podczas stworzenia nowego obiektu kalsy ViewPlot #M
        {
            PlotModel = new PlotModel();                                    //Stworznie nowego obiektu kalsy PlotModel #M
            SetUpModel();                                                   //metoda odpowiada za ustawienie podstawoych parametrów wykresu #M
            LoadData(ReturFunctionValueToChart);
        }
        private readonly List<OxyColor> colors = new List<OxyColor>         //Lista kolorów będąca wykorzystystana do wyświetlenia przebiegu różnych funkcji na jednym wykresie #M
                                            {
                                                OxyColors.Green,
                                                OxyColors.IndianRed,
                                                OxyColors.Coral,
                                                OxyColors.Chartreuse,
                                                OxyColors.Azure,
                                                OxyColors.Magenta,
                                                OxyColors.Violet,
                                                OxyColors.Yellow
                                            };
        private void SetUpModel()                                           //ustawienie parametrów wykresu takich jak: (legenda ramka, tło, oś x, oś y ), dla obiektu klasy PlotModel #M
        {
            PlotModel.LegendTitle = "Legenda";
            PlotModel.LegendOrientation = LegendOrientation.Horizontal;
            PlotModel.LegendPlacement = LegendPlacement.Outside;
            PlotModel.LegendPosition = LegendPosition.TopRight;
            PlotModel.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
            PlotModel.LegendBorder = OxyColors.Black;
            var dateAxis = new LinearAxis() { Position = AxisPosition.Bottom, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, IntervalLength = 80, Title = "X" };
            PlotModel.Axes.Add(dateAxis);
            var valueAxis = new LinearAxis() { Position = AxisPosition.Left, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Title = "Y" };
            PlotModel.Axes.Add(valueAxis);
        }
        private void LoadData(List<DataToPointChartView> DataToChart)                                    //Metoda odpowiedzialna za: załadowanie danych do wykresu, ustawienie koloru wykresu jego markerów i legendy #M
        {
            int i = 0;
            var dataPerSeries = DataToChart.GroupBy(m => m.SeriesID).ToList();                  //grupowanie listy DataToChart po Seri #M
            foreach (var data in dataPerSeries)                                                 //pętla dodająca dane do seri. Podzielenie danych na dwie serie w zależności od osi X dzięki temu unikniemy wystąpienia lini gdy funkcja nie ma miejsca zerowego.   #M               
            {
                var scatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle };           // (Funkcja powinna dodatkow mieć możliwość rozgraniczenia po Y) #M
                data.ToList().ForEach(d => scatterSeries.Points.Add(new ScatterPoint(d.Axis, d.Ayis, 5, 200)));
                PlotModel.Series.Add(scatterSeries);                                                 //Dodanie do modelu wykresu nowej serii #M
                i++;
            }
        }
        public int UpdateModelZoomIN(List<DataToPointChartView> DataToChart, int i)              //Metoda odpowiedzialna za aktualizacje danych na wykresie #M
        {
            var dataPerSeries = DataToChart.GroupBy(m => m.SeriesID).ToList();          //grupowanie listy DataToChart po Seri #M
            foreach (var data in dataPerSeries)
            {
                var lineSerie = PlotModel.Series[i] as LineSeries;                      //stworzneie obiektu LineSeries sczytanie dwóch serii ponieważ wykres funkcji podzielony jest na dwie serie  #M
                var lineSeres1 = PlotModel.Series[i + 1] as LineSeries;                 //stworzenie obiektu LineSeires 
                if (lineSerie != null)
                {
                    foreach (var d in data)                                             //pętla taka sama jak przy generowaniu wykresu sprawdzajaca, do której serii dany punkt ma pójść #M
                    {
                        if (d.Axis < 0)
                        {
                            lineSerie.Points.Add(new DataPoint(d.Axis, d.Ayis));
                        }
                    }
                    //data.ToList().ForEach(d => lineSerie.Points.Add(new DataPoint(d.Axis, d.Ayis))); //dodanie do parametru Points w klasie LineSeres punktów x, y odpowiadającym danej funckji #M
                    var listpom = lineSerie.Points.OrderBy(m => m.X).ToList();         //przypisanie do zmiennnej listpom seri punktow pogrupowanych po X #M
                    lineSerie.Points.RemoveRange(0, lineSerie.Points.Count);           //usunięci z lineSerie wszystkich punktów #M
                    listpom.ToList().ForEach(d => lineSerie.Points.Add(new DataPoint(d.X, d.Y))); //dodanie do lineSerie punktów z listy listpom które zostały już pogrupowane operacja ta jest potrzebna żeby punkty były wykreślane w dpowiedniej kolejności #M

                }
                if (lineSeres1 != null)                                                //operacja na tej serii analogiczna do operacjii wykonanej na serii powyższej
                {
                    foreach (var d in data)
                    {
                        if (d.Axis > 0)
                        {
                            lineSeres1.Points.Add(new DataPoint(d.Axis, d.Ayis));
                        }
                    }
                    // data.ToList().ForEach(d => lineSerie.Points.Add(new DataPoint(d.Axis, d.Ayis))); //dodanie do parametru Points w klasie LineSeres punktów x, y odpowiadającym danej funckji #M
                    var listpom1 = lineSeres1.Points.OrderBy(m => m.X).ToList();
                    lineSeres1.Points.RemoveRange(0, lineSeres1.Points.Count);
                    listpom1.ToList().ForEach(k => lineSeres1.Points.Add(new DataPoint(k.X, k.Y)));
                }
                i = i + 2;                                                             //zwiekszenei licznika o 2 żeby przejść do wykresu nastepnej funkcji #M
            }
            return i;                                                                  //liczba funkcji ktora została już powiekszona. Powiększanie zaczyna się od funkcji, ktore nie mają miejsc zerowcych i zwracana jest ich ilość po to aby wiedzieć od jakiego elementu zaczać powiększanie funkcji, które mają miejsca zerowe #M 
        }
        public void UpdateModelZoomOUT(List<DataToPointChartView> DataToChart, int max, int min, double zoommin)              //Metoda odpowiedzialna za aktualizacje danych na wykresie #M
        {
            int i = 0;
            var dataPerSeries = DataToChart.GroupBy(m => m.SeriesID).ToList();                                       //grupowanie listy DataToChart po Seri #M
            foreach (var data in dataPerSeries)
            {
                var lineSerie = PlotModel.Series[i] as LineSeries;                                                   //kolejny raz podział na dwie serie #M
                var lineSeres1 = PlotModel.Series[i + 1] as LineSeries;
                if (lineSerie != null)
                {
                    // data.ToList().ForEach(d => lineSerie.Points.Add(new DataPoint(d.Axis, d.Ayis))); //dodanie do parametru Points w klasie LineSeres punktów x, y odpowiadającym danej funckji #M
                    var listpom = lineSerie.Points.OrderBy(m => m.X).ToList();                                      //pogrupowanie punktów danej serii i załadowanie ich do listy  #M
                    for (int j = 0; j < listpom.Count; j++)
                    {
                        if (listpom[j].X < min)                                                                     //pętla zmniejszająca ilość elementów na wyresie o wartość min wartość min jest to wartość osi X jaka ma wystąpić po zmniejszeniu #M
                        {
                            listpom.Remove(listpom[j]);
                            j = -1;
                        }
                    }
                    for (int j = 0; j < listpom.Count; j++)                                                        //pętla zmniejszająca ilość elementów na wyresie o wartość min wartość min jest to wartość osi X jaka ma wystąpić po zmniejszeniu #M
                    {
                        if (listpom[j].X > -zoommin)
                        {
                            listpom.Remove(listpom[j]);
                            j = -1;
                        }
                    }
                    lineSerie.Points.RemoveRange(0, lineSerie.Points.Count);                                    //usunięcie z serii wszystkich elementow #M
                    listpom.ToList().ForEach(d => lineSerie.Points.Add(new DataPoint(d.X, d.Y)));               //dodanie do seri elementów o wcześniej zmniejszonej ilośći #M
                }
                if (lineSeres1 != null)                                                                         //analogiczna operacja jak wyżej dla drugiej cześci wykresy #M
                {
                    //data.ToList().ForEach(d => lineSeres1.Points.Add(new DataPoint(d.Axis, d.Ayis))); //dodanie do parametru Points w klasie LineSeres punktów x, y odpowiadającym danej funckji #M
                    var listpom1 = lineSeres1.Points.OrderBy(m => m.X).ToList();
                    for (int j = 0; j < listpom1.Count; j++)
                    {
                        if (listpom1[j].X > max)
                        {
                            listpom1.Remove(listpom1[j]);
                            j = -1;
                        }
                    }
                    for (int j = 0; j < listpom1.Count; j++)
                    {
                        if (listpom1[j].X < zoommin)
                        {
                            listpom1.Remove(listpom1[j]);
                            j = -1;
                        }
                    }
                    lineSeres1.Points.RemoveRange(0, lineSeres1.Points.Count);
                    listpom1.ToList().ForEach(d => lineSeres1.Points.Add(new DataPoint(d.X, d.Y)));
                }
                i = i + 2;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) //metoda odpowiedzialna jest za wykrycie zdarzenia aktualizacji wykresu #M
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}