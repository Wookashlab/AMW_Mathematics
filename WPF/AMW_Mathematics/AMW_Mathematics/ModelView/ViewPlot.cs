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
        public PlotModel PlotModel
        {
            get { return plotModel; }
            set { plotModel = value; OnPropertyChanged("PlotModel"); }
        }
        public ViewPlot()
        {
            PlotModel = new PlotModel();
            SetUpModel();
            LoadData();
        }

        private readonly List<OxyColor> colors = new List<OxyColor>
                                            {
                                                OxyColors.Green,
                                                OxyColors.IndianRed,
                                                OxyColors.Coral,
                                                OxyColors.Chartreuse,
                                                OxyColors.Azure
                                            };

        private readonly List<MarkerType> markerTypes = new List<MarkerType>
                                                   {
                                                       MarkerType.Plus,
                                                       MarkerType.Star,
                                                       MarkerType.Diamond,
                                                       MarkerType.Triangle,
                                                       MarkerType.Cross
                                                   };


        private void SetUpModel()
        {
            PlotModel.LegendTitle = "Legend";
            PlotModel.LegendOrientation = LegendOrientation.Horizontal;
            PlotModel.LegendPlacement = LegendPlacement.Outside;
            PlotModel.LegendPosition = LegendPosition.TopRight;
            PlotModel.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
            PlotModel.LegendBorder = OxyColors.Black;
            var dateAxis = new LinearAxis() { Position = AxisPosition.Bottom, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, IntervalLength = 80 };
            PlotModel.Axes.Add(dateAxis);
            var valueAxis = new LinearAxis() { Position = AxisPosition.Left, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Title = "Value" };
            PlotModel.Axes.Add(valueAxis);
        }

        private void LoadData()
        {
            List<DataToChart> DataToChart = new List<DataToChart>();
            DataToChart.Add(new DataToChart { Axis = 0.21, Ayis = 0.12 });
            DataToChart.Add(new DataToChart { Axis = 0.21, Ayis = 1.12 });
            DataToChart.Add(new DataToChart { Axis = 0.21, Ayis = 2.12 });
            DataToChart.Add(new DataToChart { Axis = 0.21, Ayis = 3.12 });
            DataToChart.Add(new DataToChart { Axis = 0.21, Ayis = 4.12 });
            var lineSerie = new LineSeries
            {
                StrokeThickness = 2,
                MarkerSize = 3,
                MarkerStroke = colors[0],
                MarkerType = markerTypes[0],
                CanTrackerInterpolatePoints = false,
                Title = string.Format("Detector {0}", 0),
                Smooth = false,
            };
            DataToChart.ToList().ForEach(d => lineSerie.Points.Add(new DataPoint(d.Axis, d.Ayis)));
            PlotModel.Series.Add(lineSerie);
        }
        public void UpdateModel()
        {
            List<DataToChart> DataToChart = new List<DataToChart>();
            DataToChart.Add(new DataToChart { Axis = 0.21, Ayis = 5.12 });
            DataToChart.Add(new DataToChart { Axis = 0.21, Ayis = 6.12 });
            DataToChart.Add(new DataToChart { Axis = 0.21, Ayis = 7.12 });
            DataToChart.Add(new DataToChart { Axis = 0.21, Ayis = 8.12 });
            DataToChart.Add(new DataToChart { Axis = 0.21, Ayis = 9.12 });
            var lineSerie = new LineSeries
            {
                StrokeThickness = 2,
                MarkerSize = 3,
                MarkerStroke = colors[0],
                MarkerType = markerTypes[0],
                CanTrackerInterpolatePoints = false,
                Title = string.Format("Chart {0}", 0),
                Smooth = false,
            };
            DataToChart.ToList().ForEach(d => lineSerie.Points.Add(new DataPoint(d.Axis, d.Ayis)));
            PlotModel.Series.Add(lineSerie);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
