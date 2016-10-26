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
        Polygon Triangle;
        double licz_kat(double a, double b, double c)
        {
            double cos_kat;
            cos_kat = (b * b + c * c - a * a) / (2 * b * c); // a*a=b*b+c*c-2*b*c*cos(kat) wzor na obliczanie katow //
            return (Math.Acos(cos_kat) * 180 / 3.14159265);
        }
        private void SolveTriangel_Click(object sender, RoutedEventArgs e)
        {
            double diffrent = 0;
            double ValueC = double.Parse(TValuec.Text);
            double ValueA = double.Parse(TValuea.Text);
            double ValueB = double.Parse(TValueb.Text);
            if (ValueA + ValueB > ValueC && ValueA + ValueC > ValueB && ValueC + ValueB > ValueA)
            {
                TValuea.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 204, 204, 204));
                TValueb.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 204, 204, 204));
                TValuec.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 204, 204, 204));
                AngleA.Text = licz_kat(ValueA, ValueB, ValueC).ToString();
                AngleB.Text = licz_kat(ValueB, ValueA, ValueC).ToString();
                AngleC.Text = licz_kat(ValueC, ValueB, ValueA).ToString();
                Triangle = new Polygon
                {
                    Name = "Triangle"
                  ,
                    StrokeThickness = 3
                  ,
                    Fill = new SolidColorBrush(Color.FromArgb(255, 65, 177, 225))
                  ,
                    Opacity = 1
                  ,
                    Margin = new Thickness(10, 10, 0, 0)
                  ,
                    VerticalAlignment = VerticalAlignment.Top
                  ,
                    HorizontalAlignment = HorizontalAlignment.Center
                  ,
                    Height = 107
                  ,
                    Width = 280
                  ,
                    Stroke = Brushes.Black
                  ,
                    Stretch = Stretch.Uniform
                };
                if (ValueC < 150)
                {
                    diffrent = 150 / ValueC;
                    ValueC = ValueC * diffrent;
                    ValueA = ValueA * diffrent;
                    ValueB = ValueB * diffrent;
                }
                if (ValueC > 150)
                {
                    diffrent = ValueC / 150;
                    ValueC = 150;
                    ValueA = ValueA / diffrent;
                    ValueB = ValueB / diffrent;
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
                //if(Y < 0) 
                //{
                //    diffrent = Math.Abs(((int)Y / double.Parse(TValuec.Text))) * double.Parse(TValuec.Text) * (double.Parse(TValuec.Text) / double.Parse(TValuea.Text));
                //    Y = 0;
                //    if (diffrent > 140)
                //    {
                //        diffrent = 140;
                //    }
                //    TrianglePoint[0].X = TrianglePoint[0].X - diffrent;
                //    X = X - (diffrent / 2);
                //}
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
                    TriangeImg.Children.RemoveRange(0, TriangeImg.Children.Count);
                }
                catch
                {
                };
                TriangeImg.Children.Add(Triangle);
            }
            else
            {
                TValuea.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 182, 16, 16));
                TValueb.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 182, 16, 16));
                TValuec.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 182, 16, 16));
                TriangeImg.Children.RemoveAt(0);
            }
        }
        private void SolveTriangelClear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TriangelSolve_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                List<Label> ListLabel = new List<Label> { new Label(), new Label(), new Label() };
                ListLabel[0].Content = "A";
                ListLabel[1].Content = "B";
                ListLabel[2].Content = "C";
                List<Label> ListLabel2 = new List<Label> { new Label(), new Label(), new Label() };
                ListLabel2[0].Content = "c";
                ListLabel2[1].Content = "b";
                ListLabel2[2].Content = "a";
                var transform = Triangle.RenderedGeometry.Transform;
                int i = 0;
                var polygonGeometryTransform = Triangle.RenderedGeometry.Transform;
                var polygonToGridTransform = Triangle.TransformToAncestor(TriangeImg);
                for(int j = 0; j < Triangle.Points.Count;j++)
                {
                    var transformedPoint = polygonToGridTransform.Transform(
                                          polygonGeometryTransform.Transform(Triangle.Points[j]));
                    Point transformedPoint1  = new Point();
                    if (i != 2)
                    {
                       transformedPoint1 = polygonToGridTransform.Transform(
                                           polygonGeometryTransform.Transform(Triangle.Points[j + 1]));
                    }

                    if (i == 0)
                    {
                        ListLabel[i].Margin = new Thickness(double.Parse(transformedPoint.X.ToString()), double.Parse(transformedPoint.Y.ToString()) - 5, 0, 0);
                        double labela = Math.Abs(double.Parse(transformedPoint.X.ToString()) - double.Parse(transformedPoint1.X.ToString()));
                        labela = labela / 2;
                        labela = labela + double.Parse(transformedPoint1.X.ToString());
                        ListLabel2[i].Margin = new Thickness(labela - 5, double.Parse(transformedPoint.Y.ToString()), 0, 0);
                    }
                    if (i == 1)
                    {
                        ListLabel[i].Margin = new Thickness(double.Parse(transformedPoint.X.ToString()) - 18, double.Parse(transformedPoint.Y.ToString()) - 5, 0, 0);
                        if(double.Parse(transformedPoint.X.ToString()) < double.Parse(transformedPoint1.X.ToString()))
                        {
                            double labelbX = Math.Abs(double.Parse(transformedPoint1.X.ToString()) - double.Parse(transformedPoint.X.ToString()));
                            double labelbY = Math.Abs(double.Parse(transformedPoint.Y.ToString()) - double.Parse(transformedPoint1.Y.ToString()));
                            labelbY = labelbY / 2;
                            labelbY = labelbY + double.Parse(transformedPoint1.Y.ToString());
                            labelbX = labelbX / 2;
                            labelbX = labelbX + double.Parse(transformedPoint.X.ToString());
                            ListLabel2[i].Margin = new Thickness(labelbX - 16, labelbY - (labelbY / 2), 0, 0);
                        }
                        else
                        {
                            double labelbX = Math.Abs(double.Parse(transformedPoint.X.ToString()) - double.Parse(transformedPoint1.X.ToString()));
                            double labelbY = Math.Abs(double.Parse(transformedPoint.Y.ToString()) - double.Parse(transformedPoint1.Y.ToString()));
                            labelbY = labelbY / 2;
                            labelbY = labelbY + double.Parse(transformedPoint1.Y.ToString());
                            labelbX = labelbX / 2;
                            labelbX = labelbX + double.Parse(transformedPoint1.X.ToString());
                            ListLabel2[i].Margin = new Thickness(labelbX - 16, labelbY, 0, 0);
                        }
                    }
                    if (i == 2)
                    {
                        ListLabel[i].Margin = new Thickness(double.Parse(transformedPoint.X.ToString()) - 7, double.Parse(transformedPoint.Y.ToString()) - 23, 0, 0);
                        transformedPoint1 = polygonToGridTransform.Transform(
                                           polygonGeometryTransform.Transform(Triangle.Points[j - 2]));
                        if (double.Parse(transformedPoint.X.ToString()) < double.Parse(transformedPoint1.X.ToString()))
                        {
                            double labelbX = Math.Abs(double.Parse(transformedPoint1.X.ToString()) - double.Parse(transformedPoint.X.ToString()));
                            double labelbY = Math.Abs(double.Parse(transformedPoint1.Y.ToString()) - double.Parse(transformedPoint.Y.ToString()));
                            labelbY = labelbY / 2;
                            labelbY = labelbY + double.Parse(transformedPoint.Y.ToString());
                            labelbX = labelbX / 2;
                            labelbX = labelbX + double.Parse(transformedPoint.X.ToString());
                            ListLabel2[i].Margin = new Thickness(labelbX, labelbY - (labelbY / 2), 0, 0);
                        }
                        else
                        {
                            double labelbX = Math.Abs(double.Parse(transformedPoint.X.ToString()) - double.Parse(transformedPoint1.X.ToString()));
                            double labelbY = Math.Abs(double.Parse(transformedPoint.Y.ToString()) - double.Parse(transformedPoint1.Y.ToString()));
                            labelbY = labelbY / 2;
                            labelbY = labelbY + double.Parse(transformedPoint.Y.ToString());
                            labelbX = labelbX / 2;
                            labelbX = labelbX + double.Parse(transformedPoint1.X.ToString());
                            ListLabel2[i].Margin = new Thickness(labelbX, labelbY , 0, 0);
                        }


                    }
                    TriangeImg.Children.Add(ListLabel[i]);
                    TriangeImg.Children.Add(ListLabel2[i]);
                    i++;
                }
            }
            catch { }

            //foreach (var point in Triangle.Points)
            //{
            //    var transformpoint = transform.Transform(point);
            //    ListLabel[i].Margin = new Thickness(double.Parse(transformpoint.X.ToString()), double.Parse(transformpoint.Y.ToString()), 0, 0);
            //    ListLabel[i].HorizontalAlignment = HorizontalAlignment.Stretch;
            //    TriangeImg.Children.Add(ListLabel[i]);
            //    i++;

            //}
        }

        private void SelectionCahanged_Variable(object sender, RoutedEventArgs e)
        {
            var klawisz = (Button)sender;
            switch(klawisz.Name)
            {
                case "TValuec":

                    break;
            }

        }
    }
}
