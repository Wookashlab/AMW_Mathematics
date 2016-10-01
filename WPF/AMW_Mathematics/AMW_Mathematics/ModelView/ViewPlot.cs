using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace AMW_Mathematics.ModelView
{
    public class ViewPlot : INotifyPropertyChanged
    {
        private PlotModel plotModel;
        public PlotModel PlotModel                                          //użycie akceleratorów get; set; w celu uzyskania dostępu do prywatnego pola plotMode. Wlłaściwość PlotModel jest publiczna dzieki temu możeby bindować w XAML prawatną zmienną plotMode                             // 
        {                                                                   //czyli możemy wyświetlić wykres z jego wszystkimi parametrami ustawianymi w następnych krokach #M
            get { return plotModel; }
            set { plotModel = value; OnPropertyChanged("PlotModel"); }
        }
        public ViewPlot(List<DataToChart> ReturFunctionValueToChart) //konstruktor klasy ViewPlot wykona się podczas stworzenia nowego obiektu kalsy ViewPlot #M
        {
            PlotModel = new PlotModel();                            //Stworznie nowego obiektu kalsy PlotModel #M
            SetUpModel();                                           //metoda odpowiada za ustawienie podstawoych parametrów wykresu #M
            LoadData(ReturFunctionValueToChart);
        }
        private readonly List<OxyColor> colors = new List<OxyColor>//Lista kolorów będąca wykorzystystana do wyświetlenia przebiegu różnych funkcji na jednym wykresie #M
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
        private void SetUpModel()                                   //ustawienie parametrów wykresu takich jak: (legenda ramka, tło, oś x, oś y ), dla obiektu klasy PlotModel #M
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
        private void LoadData(List<DataToChart> DataToChart)                                    //Metoda odpowiedzialna za: załadowanie danych do wykresu, ustawienie koloru wykresu jego markerów i legendy #M
        {
            int i = 0;
            var dataPerSeries = DataToChart.GroupBy(m => m.SeriesID).ToList();                  //grupowanie listy DataToChart po Seri #M
            foreach (var data in dataPerSeries)
            {
                var lineSerie = new LineSeries
                {
                    StrokeThickness = 2,
                    MarkerSize = 1,
                    MarkerStroke = colors[i],
                    MarkerType = MarkerType.None,
                    CanTrackerInterpolatePoints = false,
                    Title = string.Format(data.Key),
                    Smooth = false,
                };
                var lineSeries1 = new LineSeries
                {
                    StrokeThickness = 2,
                    MarkerSize = 1,
                    MarkerStroke = colors[i],
                    MarkerType = MarkerType.None,
                    CanTrackerInterpolatePoints = false,
                    Smooth = false,
                };
                foreach (var d in data)
                {
                    if (d.Axis >= 0)
                    {
                       // if(d.Ayis > 0)
                      //  {
                            lineSerie.Points.Add(new DataPoint(d.Axis, d.Ayis));
                        //}
                    }
                }
                foreach (var d in data)
                {
                    if (d.Axis <= 0)
                    {
                       // if(d.Ayis < 0)
                        lineSeries1.Points.Add(new DataPoint(d.Axis, d.Ayis));
                    }
                }
                lineSerie.Color = colors[i];
                lineSeries1.Color = colors[i];
                PlotModel.Series.Add(lineSeries1);
                PlotModel.Series.Add(lineSerie);                                                 //Dodanie do modelu wykresu nowej seri #M
                i++;
            }
        }
        public int UpdateModelZoomIN(List<DataToChart> DataToChart, int i)              //Metoda odpowiedzialna za aktualizacje danych na wykresie #M
        {
            var dataPerSeries = DataToChart.GroupBy(m => m.SeriesID).ToList(); //grupowanie listy DataToChart po Seri #M
            foreach (var data in dataPerSeries)
            {
                var lineSerie = PlotModel.Series[i] as LineSeries;
                var lineSeres1 = PlotModel.Series[i + 1] as LineSeries;
                if (lineSerie != null)
                {
                    foreach (var d in data)
                    {
                        if (d.Axis < 0)
                        {
                            lineSerie.Points.Add(new DataPoint(d.Axis, d.Ayis));
                        }
                    }
                    //data.ToList().ForEach(d => lineSerie.Points.Add(new DataPoint(d.Axis, d.Ayis))); //dodanie do parametru Points w klasie LineSeres punktów x, y odpowiadającym danej funckji #M
                    var cos = lineSerie.Points.OrderBy(m => m.X).ToList();

                    lineSerie.Points.RemoveRange(0, lineSerie.Points.Count);
                    cos.ToList().ForEach(d => lineSerie.Points.Add(new DataPoint(d.X, d.Y)));

                }
                if (lineSeres1 != null)
                {
                    foreach (var d in data)
                    {
                        if (d.Axis > 0)
                        {
                            lineSeres1.Points.Add(new DataPoint(d.Axis, d.Ayis));
                        }
                    }
                    // data.ToList().ForEach(d => lineSerie.Points.Add(new DataPoint(d.Axis, d.Ayis))); //dodanie do parametru Points w klasie LineSeres punktów x, y odpowiadającym danej funckji #M
                    var cos1 = lineSeres1.Points.OrderBy(m => m.X).ToList();
                    lineSeres1.Points.RemoveRange(0, lineSeres1.Points.Count);
                    cos1.ToList().ForEach(k => lineSeres1.Points.Add(new DataPoint(k.X, k.Y)));
                }
                i = i + 2;
            }
            return i;
        }
        public void UpdateModelZoomOUT(List<DataToChart> DataToChart, int max, int min, double zoommin)              //Metoda odpowiedzialna za aktualizacje danych na wykresie #M
        {
            int i = 0;
            var dataPerSeries = DataToChart.GroupBy(m => m.SeriesID).ToList(); //grupowanie listy DataToChart po Seri #M
            foreach (var data in dataPerSeries)
            {
                var lineSerie = PlotModel.Series[i] as LineSeries;
                var lineSeres1 = PlotModel.Series[i + 1] as LineSeries;
                if (lineSerie != null)
                {
                    // data.ToList().ForEach(d => lineSerie.Points.Add(new DataPoint(d.Axis, d.Ayis))); //dodanie do parametru Points w klasie LineSeres punktów x, y odpowiadającym danej funckji #M
                    var cos = lineSerie.Points.OrderBy(m => m.X).ToList();
                    for (int j = 0; j < cos.Count; j++)
                    {
                        if (cos[j].X < min)
                        {
                            cos.Remove(cos[j]);
                            j = -1;
                        }
                    }
                    for (int j = 0; j < cos.Count; j++)
                    {
                        if (cos[j].X > -zoommin)
                        {
                            cos.Remove(cos[j]);
                            j = -1;
                        }
                    }
                    lineSerie.Points.RemoveRange(0, lineSerie.Points.Count);
                    cos.ToList().ForEach(d => lineSerie.Points.Add(new DataPoint(d.X, d.Y)));
                }
                if (lineSeres1 != null)
                {
                    //data.ToList().ForEach(d => lineSeres1.Points.Add(new DataPoint(d.Axis, d.Ayis))); //dodanie do parametru Points w klasie LineSeres punktów x, y odpowiadającym danej funckji #M
                    var cos1 = lineSeres1.Points.OrderBy(m => m.X).ToList();
                    for (int j = 0; j < cos1.Count; j++)
                    {
                        if (cos1[j].X > max)
                        {
                            cos1.Remove(cos1[j]);
                            j = -1;
                        }
                    }
                    for (int j = 0; j < cos1.Count; j++)
                    {
                        if (cos1[j].X < zoommin)
                        {
                            cos1.Remove(cos1[j]);
                            j = -1;
                        }
                    }
                    lineSeres1.Points.RemoveRange(0, lineSeres1.Points.Count);
                    cos1.ToList().ForEach(d => lineSeres1.Points.Add(new DataPoint(d.X, d.Y)));
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