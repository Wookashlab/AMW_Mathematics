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
using AMW_Mathematics.T_ModelView;

namespace AMW_Mathematics.Windows
{
    /// <summary>
    /// Interaction logic for TriangleSolver.xaml
    /// </summary>

    public partial class TriangleSolver : MetroWindow
    {
        public TriangleSolver()
        {
            InitializeComponent();
        }
        TriangleView triangleview = new TriangleView();
        Polygon Triangle;
        List<Label> ListLabel;
        List<Label> ListLabel2;
        bool drawlabel = false;
       
        private void ShowSolverBox_Window(int index)
        {
            try
            {
                switch (index)
                {
                    case 0:
                        ShowSolverBox.Items.Clear();
                        ShowSolverBox.Items.Add("A: Law of Cosines: cos(α)=(c²+b²-a²)÷(2·b·a)");
                        ShowSolverBox.Items.Add("B: Law of Cosines: cos(β)=(c²-b²+a²)÷(2·b·a)");
                        ShowSolverBox.Items.Add("B: Law of Cosines: cos(γ)=(b²+a²-c²)÷(2·b·a)");
                        ShowSolverBox.Items.Add("C: Sum of triangle's angles measures A+B+C=1");
                        break;
                    case 1:
                        ShowSolverBox.Items.Clear();
                        if (AngleA.Text == AngleB.Text && AngleA.Text == AngleC.Text && AngleB.Text == AngleC.Text)
                            ShowSolverBox.Items.Add("Equailaterial triangle (all angiels congruent, all sides congruent)");
                        if (AngleA.Text == AngleB.Text || AngleB.Text == AngleC.Text || AngleA.Text == AngleC.Text)
                            ShowSolverBox.Items.Add("Isosceles triangle (two sides of equal lenght)");
                        if (AngleA.Text == "90" || AngleB.Text == "90" || AngleC.Text == "90")
                            ShowSolverBox.Items.Add("Right triangle (one 90 degree angle)");
                        if (AngleA.Text != AngleB.Text && AngleA.Text != AngleC.Text && AngleB.Text != AngleC.Text)
                            ShowSolverBox.Items.Add("Scalene triangle (all three sides of diffrent lenght");
                        if (double.Parse(AngleA.Text) < 90 && double.Parse(AngleB.Text) < 90 && double.Parse(AngleC.Text) < 90)
                            ShowSolverBox.Items.Add("Acute Triangle (all three angles less than 90 degrees)");
                        if (double.Parse(AngleA.Text) > 90 || double.Parse(AngleB.Text) > 90 || double.Parse(AngleC.Text) > 90)
                            ShowSolverBox.Items.Add("Obtuse triangle (one angle greater than 90 degrees)");
                        break;
                    case 2:
                        ShowSolverBox.Items.Clear();
                        double p = 0.5 * (double.Parse(TValuea.Text) + double.Parse(TValueb.Text) + double.Parse(TValuec.Text));
                        double P = Math.Sqrt(p * (p - double.Parse(TValuea.Text)) * (p - double.Parse(TValueb.Text)) * (p - double.Parse(TValuec.Text)));
                        double w1 = (P / double.Parse(TValuea.Text)) * 2;
                        double w2 = (P / double.Parse(TValueb.Text)) * 2;
                        double w3 = (P / double.Parse(TValuec.Text)) * 2;
                        ShowSolverBox.Items.Add("Area" + P.ToString());
                        ShowSolverBox.Items.Add("Altitude A: " + w1.ToString());
                        ShowSolverBox.Items.Add("Altitude B: " + w2.ToString());
                        ShowSolverBox.Items.Add("Altitude C: " + w3.ToString());
                        break;
                }
            }catch { };
        }
        private void SolveTriangel_Click(object sender, RoutedEventArgs e)
        {
            double diffrent = 0;
            double ValueC = double.Parse(TValuec.Text);
            double ValueA = double.Parse(TValuea.Text);
            double ValueB = double.Parse(TValueb.Text);
            if (ValueA + ValueB > ValueC && ValueA + ValueC > ValueB && ValueC + ValueB > ValueA)
            {
                Triangle = triangleview.DrawTriangel(TriangeImg, Triangle, TValuea, TValueb, TValuec, AngleA, AngleB, AngleC, diffrent, ValueC, ValueA, ValueB);
                SolveTriangelProperties.SelectedIndex = 0;
                drawlabel = true;
            }
            else
            {
                TValuea.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 182, 16, 16));
                TValueb.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 182, 16, 16));
                TValuec.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 182, 16, 16));
                AngleA.Text = "";
                AngleB.Text = "";
                AngleC.Text = "";
                try
                {
                    TriangeImg.Children.RemoveRange(0, TriangeImg.Children.Count);
                }
                catch { }
                drawlabel = false;
            }
        }

        private void SolveTriangelClear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TriangelSolve_MouseLeave(object sender, MouseEventArgs e)
        {
            if (drawlabel == true)
            {
                triangleview.DrawLabel(TriangeImg, Triangle,ref ListLabel,ref ListLabel2);
            }
            drawlabel = false;
        }

        private void SolverLabel_MouseEnter(object sender, MouseEventArgs e)
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
        private void SolverLabel_MouseLeave(object sender, MouseEventArgs e)
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

        private void SolverLabelAngle_MouseEnter(object sender, MouseEventArgs e)
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
                        ListLabel[2].Margin = new Thickness(ListLabel[2].Margin.Left, ListLabel[2].Margin.Top-5, 0, 0);
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
        private void SolverLabelAngle_MouseLeave(object sender, MouseEventArgs e)
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

        private void SolveTriangelProperties_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowSolverBox_Window(SolveTriangelProperties.SelectedIndex);
        }
    }
}
