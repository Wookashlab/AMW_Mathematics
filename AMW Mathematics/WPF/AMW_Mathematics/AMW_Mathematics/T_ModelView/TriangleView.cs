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

namespace AMW_Mathematics.T_ModelView
{
    class TriangleView
    {

        double licz_kat(double a, double b, double c)
        {
            double cos_kat;
            cos_kat = (b * b + c * c - a * a) / (2 * b * c); 
            return (Math.Acos(cos_kat) * 180 / 3.14159265);
        }

        public Polygon DrawTriangel(Grid TriangeImg, Polygon Triangle, TextBox TValuea, TextBox TValueb, TextBox TValuec, TextBox AngleA, TextBox AngleB, TextBox AngleC, double diffrent, double ValueC, double ValueA, double ValueB, SolidColorBrush themeColor)      //metoda odpowadająca za rystowanie trójkąta #M
        {
            TValuea.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 204, 204, 204));                                                                                                                                                                              //ustawienie koloru TextBoxów na domyślny światczy to o tym że dane zostały wprowadzone prawidłowo #M
            TValueb.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 204, 204, 204));
            TValuec.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 204, 204, 204));
            AngleA.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 204, 204, 204));
            AngleB.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 204, 204, 204));
            AngleC.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 204, 204, 204));
            if (TValuec.Text != "" && TValueb.Text != "" && TValuea.Text != "")                                                                                                                                                                                         //sprawdzenie TextBoxy nie są puste dzięki temu nie wystąpią błędy podczas obliczeń #M
            {
                AngleA.Text = Math.Round(licz_kat(ValueA, ValueB, ValueC), 2).ToString();                                                                                                                                                                               //obliczenie kąta na podstawie boku #M
                AngleB.Text = Math.Round(licz_kat(ValueB, ValueA, ValueC), 2).ToString();
                AngleC.Text = Math.Round(licz_kat(ValueC, ValueB, ValueA), 2).ToString();
            }
            Triangle = new Polygon                                                                                                                                                                                                                                      //utworznie instancji klasy Polyglon oraz ustawienie u odpowiednich parametrów #M
            {
                Name = "Triangle"                                                                                                                                                                                                                                       //podanie nazwy #M
              ,
                StrokeThickness = 3                                                                                                                                                                                                                                     //gubość lini #M
              ,
                Fill = themeColor
              ,
                Opacity = 1                                                                                                                                                                                                                                             //przezroczystośc #M
              ,
                Margin = new Thickness(10, 10, 0, 0)                                                                                                                                                                                                                    //margines #M
              ,
                VerticalAlignment = VerticalAlignment.Top
              ,
                HorizontalAlignment = HorizontalAlignment.Center
              ,         
                Height = 107                                                                                                                                                                                                                                            //wysokość #M
              ,         
                Width = 280                                                                                                                                                                                                                                             //szerokość #M
              ,
                Stroke = Brushes.Black
              ,
                Stretch = Stretch.Uniform
            };                                                                                              
            if (ValueC < 150)                                                                                                                                                                                                                                           //obliczenie długości boków które zostaną wygenerowane na rysunku w momencie gdy długość boku c jest mniejsza od 150 #M
            {
                diffrent = 150 / ValueC;
                ValueC = ValueC * diffrent;
                ValueA = ValueA * diffrent;
                ValueB = ValueB * diffrent;
            }
            if (ValueC > 150)                                                                                                                                                                                                                                           //obliczenie długości boków które zostaną wygenerowane na rysunku w momencie gdy długość boku c jest większa od 150 #M
            {
                diffrent = ValueC / 150;
                ValueC = 150;
                ValueA = ValueA / diffrent;
                ValueB = ValueB / diffrent;
            }
            Point[] TrianglePoint = new Point[3];                                                                                                                                                                                                                       //ustalenie współrzędnych dwóch punktów trójkąta #M
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
            double P4 = Math.Sqrt(cA * cB + cB * cC + cC * cA);                                                                                                                                                                                                         //obliczenie pola trójkąta #M
            double X = (TrianglePoint[0].X * cB + TrianglePoint[0].Y * P4 + TrianglePoint[1].X * cA - TrianglePoint[1].Y * P4) / (cA + cB);                                                                                                                             //obliczenie 3 współrzędnej trójkąta #M
            double Y = (TrianglePoint[0].X * P4 + TrianglePoint[0].Y * cB + TrianglePoint[1].X * P4 + TrianglePoint[1].Y * cA) / (cB + cA);
            X = Math.Round(X, 0) + 100;
            Y = 100 - Math.Round(Y, 0);
            for (int i = 0; i < 2; i++)
            {
                TrianglePoint[i].X = TrianglePoint[i].X + 100;                                                                                                                                                                                                          //przeskalowanie wcześniej ustalonych punktów trójąta #M
                TrianglePoint[i].Y = TrianglePoint[i].Y + 100;
            }
            TrianglePoint[2].X = X;                                                                                                                                                                                                                                     //dodanie do tablicy obliczonych współrzędnych trzeciego punktu trójkąta #M
            TrianglePoint[2].Y = Y;
            PointCollection polygonPoints = new PointCollection();                                                                                                                                                                                                      //stworzenie kolekcji punktów w celu dodania ich do obiektu Traingel #M
            foreach (var item in TrianglePoint)
            {
                Triangle.Points.Add(item);                                                                                                                                                                                                                              //dodanie rysunków do obiektu traingel #M

            }
            try
            {
                TriangeImg.Children.RemoveRange(0, TriangeImg.Children.Count);                                                                                                                                                                                          //usunięcie wszystkich dzieci grida w którym znajduje się Triangel w celu ponownego jego narysowania #M
            }
            catch
            {
            };
            TriangeImg.Children.Add(Triangle);                                                                                                                                                                                                                          //dodanie do Grida obiektu Triangel 
            return Triangle;                                                                                                                                                                                                                                            //zwrócenie obietu Triangel;
        }

        public void DrawLabel(Grid TriangeImg, Polygon Triangle, ref List<Label> ListLabel,ref List<Label> ListLabel2)  //metoda odpowiedzialna za dodanie labelek do rysunku trójkąta #M
        {
            try
            {
                ListLabel = new List<Label> { new Label(), new Label(), new Label() };                                  //stworzenie listy labelek #M
                ListLabel[0].Content = "α";
                ListLabel[1].Content = "β";
                ListLabel[2].Content = "γ";
                ListLabel2 = new List<Label> { new Label(), new Label(), new Label() };                                 //stworzenie listy labelek #M
                ListLabel2[0].Content = "c";
                ListLabel2[1].Content = "b";
                ListLabel2[2].Content = "a";
                var transform = Triangle.RenderedGeometry.Transform;                                                    //wyśrodkowanie trójkąta względem grida #M
                int i = 0;
                var polygonGeometryTransform = Triangle.RenderedGeometry.Transform;                        
                var polygonToGridTransform = Triangle.TransformToAncestor(TriangeImg);                                  //przypisanie do zmiennej zawartości wyśrodkowanego grida #M
                for (int j = 0; j < Triangle.Points.Count; j++)
                {
                    var transformedPoint = polygonToGridTransform.Transform(
                                          polygonGeometryTransform.Transform(Triangle.Points[j]));                      //pobranie współrzednej punktu trójkąta #M
                    Point transformedPoint1 = new Point();
                    if (i != 2)
                    {
                        transformedPoint1 = polygonToGridTransform.Transform(
                                            polygonGeometryTransform.Transform(Triangle.Points[j + 1]));
                    }

                    if (i == 0)                                                                                         //ustalenie położenia pierwszej labelki z list ListLabel i Listlabel2  #M
                    {
                        ListLabel[i].Margin = new Thickness(double.Parse(transformedPoint.X.ToString()), double.Parse(transformedPoint.Y.ToString()) - 5, 0, 0);    
                        double labela = Math.Abs(double.Parse(transformedPoint.X.ToString()) - double.Parse(transformedPoint1.X.ToString()));
                        labela = labela / 2;
                        labela = labela + double.Parse(transformedPoint1.X.ToString());
                        ListLabel2[i].Margin = new Thickness(labela - 5, double.Parse(transformedPoint.Y.ToString()), 0, 0);
                    }
                    if (i == 1)                                                                                        //ustalenie położenia drugiej labelki z list ListLabel i Listlabel2  #M
                    {
                        ListLabel[i].Margin = new Thickness(double.Parse(transformedPoint.X.ToString()) - 18, double.Parse(transformedPoint.Y.ToString()) - 5, 0, 0);
                        if (double.Parse(transformedPoint.X.ToString()) < double.Parse(transformedPoint1.X.ToString()))
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
                    if (i == 2)                                                                                         //ustalenie położenia trzeciel labelki z list ListLabel i Listlabel2  #M
                    {
                        ListLabel[i].Margin = new Thickness(double.Parse(transformedPoint.X.ToString()) - 7, double.Parse(transformedPoint.Y.ToString()) - 25, 0, 0);
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
                            ListLabel2[i].Margin = new Thickness(labelbX, labelbY, 0, 0);
                        }


                    }
                    TriangeImg.Children.Add(ListLabel[i]);                                                              //dodanie labelek do grida #M
                    TriangeImg.Children.Add(ListLabel2[i]);                                                             //dodanie labelek do grida #M
                    i++;
                }
            }
            catch { }
        }

        public void EnterToBoxSide(object sender, ref List<Label> ListLabel2)
        {
            try
            {
                var name = (TextBox)sender;
                switch (name.Name)
                {
                    case "TValuec":
                        ListLabel2[0].Foreground = new SolidColorBrush(Color.FromArgb(255, 182, 16, 16));
                        ListLabel2[0].FontSize = 16;
                        ListLabel2[0].FontWeight = FontWeights.Bold;
                        ListLabel2[0].Content = ListLabel2[0].Content + "!";
                        break;
                    case "TValuea":
                        ListLabel2[1].Foreground = new SolidColorBrush(Color.FromArgb(255, 182, 16, 16));
                        ListLabel2[1].FontSize = 16;
                        ListLabel2[1].FontWeight = FontWeights.Bold;
                        ListLabel2[1].Margin = new Thickness(ListLabel2[1].Margin.Left - 8, ListLabel2[1].Margin.Top, 0, 0);
                        ListLabel2[1].Content = "!" + ListLabel2[1].Content;
                        break;
                    case "TValueb":
                        ListLabel2[2].Foreground = new SolidColorBrush(Color.FromArgb(255, 182, 16, 16));
                        ListLabel2[2].FontSize = 16;
                        ListLabel2[2].FontWeight = FontWeights.Bold;
                        ListLabel2[2].Content = "!" + ListLabel2[2].Content;
                        break;
                }
            }
            catch { };
        }

        public void LeaveBoxSide(object sender, ref List<Label> ListLabel2)
        {
            try
            {
                var name = (TextBox)sender;
                switch (name.Name)
                {
                    case "TValuec":
                        ListLabel2[0].ClearValue(Label.ForegroundProperty);
                        ListLabel2[0].ClearValue(Label.FontSizeProperty);
                        ListLabel2[0].ClearValue(Label.FontWeightProperty);
                        ListLabel2[0].Content = ListLabel2[0].Content.ToString().Replace("!", "");
                        break;
                    case "TValuea":
                        ListLabel2[1].ClearValue(Label.ForegroundProperty);
                        ListLabel2[1].ClearValue(Label.FontSizeProperty);
                        ListLabel2[1].ClearValue(Label.FontWeightProperty);
                        ListLabel2[1].Margin = new Thickness(ListLabel2[1].Margin.Left + 8, ListLabel2[1].Margin.Top, 0, 0);
                        ListLabel2[1].Content = ListLabel2[1].Content.ToString().Replace("!", "");
                        break;
                    case "TValueb":
                        ListLabel2[2].ClearValue(Label.ForegroundProperty);
                        ListLabel2[2].ClearValue(Label.FontSizeProperty);
                        ListLabel2[2].ClearValue(Label.FontWeightProperty);
                        ListLabel2[2].Content = ListLabel2[2].Content.ToString().Replace("!", "");
                        break;
                }
            }
            catch { }
        }

        public void EnterToBoxAngle(object sender, ref List<Label> ListLabel)
        {
            try
            {
                var name = (TextBox)sender;
                switch (name.Name)
                {
                    case "AngleC":
                        ListLabel[2].Foreground = new SolidColorBrush(Color.FromArgb(255, 182, 16, 16));
                        ListLabel[2].FontSize = 16;
                        ListLabel[2].FontWeight = FontWeights.Bold;
                        ListLabel[2].Margin = new Thickness(ListLabel[2].Margin.Left, ListLabel[2].Margin.Top - 5, 0, 0);
                        ListLabel[2].Content = ListLabel[2].Content + "!";
                        break;
                    case "AngleB":
                        ListLabel[1].Foreground = new SolidColorBrush(Color.FromArgb(255, 182, 16, 16));
                        ListLabel[1].FontSize = 16;
                        ListLabel[1].FontWeight = FontWeights.Bold;
                        ListLabel[1].Content = "!" + ListLabel[1].Content;
                        break;
                    case "AngleA":
                        ListLabel[0].Foreground = new SolidColorBrush(Color.FromArgb(255, 182, 16, 16));
                        ListLabel[0].FontSize = 16;
                        ListLabel[0].FontWeight = FontWeights.Bold;
                        ListLabel[0].Content = "!" + ListLabel[0].Content;
                        break;
                }
            }
            catch { };
        }

        public void LeaveBoxAngle(object sender, ref List<Label> ListLabel)
        {
            try
            {
                var name = (TextBox)sender;
                switch (name.Name)
                {
                    case "AngleC":
                        ListLabel[2].ClearValue(Label.ForegroundProperty);
                        ListLabel[2].ClearValue(Label.FontSizeProperty);
                        ListLabel[2].ClearValue(Label.FontWeightProperty);
                        ListLabel[2].Margin = new Thickness(ListLabel[2].Margin.Left, ListLabel[2].Margin.Top + 5, 0, 0);
                        ListLabel[2].Content = ListLabel[2].Content.ToString().Replace("!", "");
                        break;
                    case "AngleB":
                        ListLabel[1].ClearValue(Label.ForegroundProperty);
                        ListLabel[1].ClearValue(Label.FontSizeProperty);
                        ListLabel[1].ClearValue(Label.FontWeightProperty);
                        ListLabel[1].Content = ListLabel[1].Content.ToString().Replace("!", "");
                        break;
                    case "AngleA":
                        ListLabel[0].ClearValue(Label.ForegroundProperty);
                        ListLabel[0].ClearValue(Label.FontSizeProperty);
                        ListLabel[0].ClearValue(Label.FontWeightProperty);
                        ListLabel[0].Content = ListLabel[0].Content.ToString().Replace("!", "");
                        break;
                }
            }
            catch { }
        }
    }
}
