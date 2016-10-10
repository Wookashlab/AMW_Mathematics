using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using MaximaSharp;

namespace AMW_Mathematics.ModelView
{
    public class ViewPlot : INotifyPropertyChanged
    {
        private Expreson phrase = new Expreson();
        private PlotModel plotModel;
        public PlotModel PlotModel                                          //użycie akceleratorów get; set; w celu uzyskania dostępu do prywatnego pola plotMode. Wlłaściwość PlotModel jest publiczna dzieki temu możeby bindować w XAML prawatną zmienną plotMode                             // 
        {                                                                   //czyli możemy wyświetlić wykres z jego wszystkimi parametrami ustawianymi w następnych krokach #M
            get { return plotModel; }
            set { plotModel = value; OnPropertyChanged("PlotModel"); }
        }
        public ViewPlot(List<DataToChartsLine> ReturFunctionValueToChart)        //konstruktor klasy ViewPlot wykona się podczas stworzenia nowego obiektu kalsy ViewPlot #M
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
        private void LoadData(List<DataToChartsLine> DataToChart)                                    //Metoda odpowiedzialna za: załadowanie danych do wykresu, ustawienie koloru wykresu jego markerów i legendy #M
        {
            int i = 0;
            var dataPerSeries = DataToChart.GroupBy(m => m.SeriesID).GroupBy(k => k.Key.Replace("#", "")).ToList();                  //grupowanie listy DataToChart po Seri #M
            foreach (var f in dataPerSeries)
            {
                foreach (var series in f)
                {
                    var lineSerie = new LineSeries
                    {
                        StrokeThickness = 2,
                        MarkerSize = 1,
                        MarkerStroke = colors[i],
                        MarkerType = MarkerType.None,
                        CanTrackerInterpolatePoints = false,
                        Smooth = false,
                    };
                    series.ToList().ForEach(d => lineSerie.Points.Add(new DataPoint(d.Axis, d.Ayis)));
                    lineSerie.Color = colors[i];                                                    //Dodajemy kolor do serii wybrany z listy color w zależności od obiegu pętli #M
                    PlotModel.Series.Add(lineSerie);                                                 //Dodanie do modelu wykresu nowej serii #M
                }
                PlotModel.Series[PlotModel.Series.Count - 1].Title = string.Format(f.Key);
                i++;
            }
        }
        public List<string> ReturnY(List<string> listy,string function, IGrouping<string,IGrouping<string,DataToChartsLine>> f,bool start)
        {
            if (start == true)
            {
                function = f.Key;
                function = "y = " + function.Replace("#", "");
                function = Maxima.Eval("solve(" + function + ",x" + ")");
            }
            if (function.Contains(",") == true)
            {
                string fa = function.Substring(1, function.IndexOf(",")-1);
                fa = fa.Replace("x", "");
                fa = fa.Replace(" ", "");
                int index = fa.LastIndexOf("=");
                fa = fa.Substring(index + 1, fa.Length - index - 1);
                listy.Add(fa);
                listy.Add(fa);
                function = function.Remove(1, function.IndexOf(","));
                ReturnY(listy,function, f, false);
            }
            else
            {
                function = function.Replace("]", "");
                function = function.Replace("x", "");
                function = function.Replace(" ", "");  
                function = function.Replace("[", "");
                int index = function.LastIndexOf("=");
                function = function.Substring(index + 1, function.Length - index - 1);
                listy.Add(function);
                listy.Add(function);
                return listy;
            }
            return listy;
        }
        public List<DataToChartsLine> UpdateModelZoomIN(List<DataToChartsLine> DataToChart)              //Metoda odpowiedzialna za aktualizacje danych na wykresie #M
        {
            int i = 0;
            int j = 0;
            var dataPerSeries = DataToChart.GroupBy(m => m.SeriesID).OrderBy(z => z.Key.Length).GroupBy(k => k.Key.Replace("#", "")).ToList();                  //grupowanie listy DataToChart po Seri #M
            foreach (var f in dataPerSeries)
            {
                List<string> listy = ReturnY(new List<string>(),"", f, true);
                foreach (var series in f)
                {
                    var index = series.Select(k => Math.Abs(k.Ayis)).ToList();
                    int ix = index.IndexOf(index.Min());
                    if(ix != 0 && ix != index.Count - 1 || ix != index.Count-1 && ix != 0)
                    {
                        double first = series.First().Ayis;
                        double last = series.Last().Ayis;
                        if (first < 0) first = first -10;
                        else first = first + 10;
                        if (last < 0) last = last - 10;
                        else last = last + 10;
                        var function = listy[i];
                        function = Maxima.Eval(phrase.AddToNumberDot(phrase.AddToNumberDot(function.Replace("y", ("(" + first.ToString() + ")")))));
                        var lineSerie = PlotModel.Series[j] as LineSeries;
                        function = function.Replace(".", ",");
                        lineSerie.Points.Insert(0,(new DataPoint(double.Parse(function), first)));
                        i++;
                        DataToChart.Insert(0,((new DataToChartsLine { SeriesID = series.Key, Axis = double.Parse(function), Ayis = first })));
                        function = listy[i];
                        function = Maxima.Eval(phrase.AddToNumberDot(function.Replace("y", ("(" + last.ToString() + ")"))));
                        lineSerie = PlotModel.Series[j] as LineSeries;
                        function = function.Replace(".", ",");
                        lineSerie.Points.Add(new DataPoint(double.Parse(function), last));
                        i++;
                        DataToChart.Add((new DataToChartsLine { SeriesID = series.Key, Axis = double.Parse(function), Ayis = last }));
                    }
                    //function = Maxima.Eval(phrase.AddToNumberDot(function.Replace("y", ("(" + last.ToString() + ")"))));
                    //var lineSerie = PlotModel.Series[i] as LineSeries;
                    //function  = function.Replace(".", ",");
                    //lineSerie.Points.Add(new DataPoint(double.Parse(function), last));
                    //var listpom = lineSerie.Points.OrderBy(m => m.X).ToList();
                    //lineSerie.Points.RemoveRange(0, lineSerie.Points.Count);                                   
                    //listpom.ToList().ForEach(d => lineSerie.Points.Add(new DataPoint(d.X, d.Y)));
                    //i++;
                    //DataToChart.Add((new DataToChartsLine { SeriesID = series.Key, Axis = double.Parse(function), Ayis =  last }));
                    i++;
                    j++;
                }
            }
            return DataToChart;
        }
        public void UpdateModelZoomOUT(List<DataToChartsLine> DataToChart, int max, int min, double zoommin)              //Metoda odpowiedzialna za aktualizacje danych na wykresie #M
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