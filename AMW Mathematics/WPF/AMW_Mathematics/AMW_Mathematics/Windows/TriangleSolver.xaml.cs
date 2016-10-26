using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro;
using MahApps.Metro.Controls;

namespace AMW_Mathematics.Windows
{
    /// <summary>
    /// Interaction logic for TriangleSolver.xaml
    /// </summary>

    public partial class TriangleSolver : MetroWindow
    {
        public TriangleSolver()
        {
            this.Visibility = Visibility.Visible;
            InitializeComponent();
          
        }

        private void SolveTriangel_Click(object sender, RoutedEventArgs e)
        {
            var Triangle = new Polygon
            {
                Name = "Triangle"
              ,StrokeThickness = 2
              ,Fill = Brushes.Blue
              ,Opacity = 0.3
              ,Margin = new Thickness(10, 10, 0, 0)
              ,VerticalAlignment = VerticalAlignment.Top
              ,HorizontalAlignment = HorizontalAlignment.Left
              ,Height = 107
              ,Width = 280
              ,Stroke = Brushes.Blue
            };
            double diffrent = 0; 
            double ValueC = double.Parse(TValuec.Text);
            double ValueA = double.Parse(TValuea.Text);
            double ValueB = double.Parse(TValueb.Text);
            if (ValueC < 150)
            {
                diffrent = 150/ ValueC;
                ValueC = ValueC * diffrent;
                ValueA = ValueA * diffrent;
                ValueB = ValueB * diffrent;
            }
            if (ValueC > 150)
            {
                diffrent = ValueC/150;
                ValueC = 150;
                ValueA = ValueA / diffrent;
                ValueB = ValueB/diffrent;
            }
            Point[] TrianglePoint = new Point[3];
            TrianglePoint[0].X = ValueC;
            TrianglePoint[0].Y = 0;
            TrianglePoint[1].X = 0;
            TrianglePoint[1].Y = 0;
            double c = TrianglePoint[0].X;
            double a = ValueA;
            double b = ValueB;
            double cA = Math.Pow(b, 2) + Math.Pow(c, 2) - Math.Pow(a, 2);
            double cB = Math.Pow(a, 2) + Math.Pow(c, 2) - Math.Pow(b, 2);
            double cC = Math.Pow(a, 2) + Math.Pow(b, 2) - Math.Pow(c, 2);
            double P4 = Math.Sqrt(cA * cB + cB * cC + cC * cA);
            double X = (TrianglePoint[0].X * cB + TrianglePoint[0].Y * P4 + TrianglePoint[1].X * cA - TrianglePoint[1].Y * P4) / (cA + cB);
            double Y = (TrianglePoint[0].X * P4 + TrianglePoint[0].Y * cB + TrianglePoint[1].X * P4 + TrianglePoint[1].Y * cA) / (cB + cA);


            X = Math.Round(X, 0) + 100;
            Y = 100 - Math.Round(Y, 0);
            if(Y < 0) //zrobić abs i zmniejszyć x o y 
            {
                diffrent = Math.Abs(((int)Y / double.Parse(TValuec.Text)))* double.Parse(TValuec.Text)*(double.Parse(TValuec.Text)/ double.Parse(TValuea.Text));
                Y = 0;
                if (diffrent > 140)
                {
                    diffrent = 140;
                }
                TrianglePoint[0].X = TrianglePoint[0].X - diffrent;
                X = X - (diffrent / 2);
            }
            for (int i = 0; i < 2; i++)
            {
                TrianglePoint[i].X = TrianglePoint[i].X + 100;
                TrianglePoint[i].Y = TrianglePoint[i].Y + 100;
            }
            TrianglePoint[2].X = X;
            TrianglePoint[2].Y = Y;

            PointCollection polygonPoints = new PointCollection();
            foreach (var item in TrianglePoint)
            {
                Triangle.Points.Add(item);
            }
            try
            {
                GridAll.Children.RemoveAt(4);
            }
            catch {
            };
           
            GridAll.Children.Add(Triangle);
        }
    }
}
