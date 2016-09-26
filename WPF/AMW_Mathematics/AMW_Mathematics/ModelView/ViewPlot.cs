using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace AMW_Mathematics.ModelView
{
    public class ViewPlot: INotifyPropertyChanged
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
                    MarkerSize = 3,
                    MarkerStroke = colors[i],
                    MarkerType = MarkerType.None,
                    CanTrackerInterpolatePoints = false,
                    Title = string.Format("Seria " + i),
                    Smooth = false,
                };
                data.ToList().ForEach(d => lineSerie.Points.Add(new DataPoint(d.Axis, d.Ayis))); //dodanie do parametru Points w klasie LineSeres punktów x, y odpowiadającym danej funckji #M
                PlotModel.Series.Add(lineSerie);                                                 //Dodanie do modelu wykresu nowej seri #M
                i++;
            }
        }
        public void UpdateModel(List<DataToChart> DataToChart)              //Metoda odpowiedzialna za aktualizacje danych na wykresie #M
        {
            int i = 0;
            var dataPerSeries = DataToChart.GroupBy(m => m.SeriesID).ToList(); //grupowanie listy DataToChart po Seri #M
            foreach (var data in dataPerSeries)
            {
                var lineSerie = new LineSeries
                {
                    StrokeThickness = 2,
                    MarkerSize = 3,
                    MarkerStroke = colors[i],
                    MarkerType = MarkerType.None,
                    CanTrackerInterpolatePoints = false,
                    Title = string.Format("Seria " + i),
                    Smooth = false,
                };
                data.ToList().ForEach(d => lineSerie.Points.Add(new DataPoint(d.Axis, d.Ayis))); //dodanie do parametru Points w klasie LineSeres punktów x, y odpowiadającym danej funckji #M
                PlotModel.Series.Add(lineSerie); // Dodanie do modelu wykresu nowej seri #M
                i++;
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
