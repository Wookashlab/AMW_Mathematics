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
        private TriangleView triangleview = new TriangleView();

        private TriangleShowBoxView trangleshowboxview = new TriangleShowBoxView();

        private Polygon Triangle;

        private List<Label> ListLabel;

        private List<Label> ListLabel2;

        private bool drawlabel = false;

        public TriangleSolver()
        {
            InitializeComponent();
        }
        public Dictionary<string,double> SolveAfterAngles(Dictionary<string, double> LenghtSides, double?  AngleA, double ? AngleB, double ? AngleC, double ? ValueC, double ?  ValueB, double ? ValueA )
        {
            bool solve = false;
            if (AngleA != null && AngleB != null && AngleC != null) solve = true;
            if (AngleA == null && AngleB != null && AngleC != null)
            {
                AngleA = Math.Abs(((double)AngleB + (double)AngleC)-180);
                solve = true;
            }
            if (AngleA != null && AngleB == null && AngleC != null)
            {
                AngleB = Math.Abs(((double)AngleB + (double)AngleC)-180);
                solve = true;
            }      
            if (AngleA != null && AngleB != null && AngleC == null)
            {
                AngleC = Math.Abs(((double)AngleB + (double)AngleA)-180);
                solve = true;
            }
                
            double Alfa = 0;
            double Beta = 0;
            double Gamma = 0;
            if (solve == true)
            {
                Alfa = ((double)AngleA * Math.PI) / 180;
                Beta = ((double)AngleB * Math.PI) / 180;
                Gamma = ((double)AngleC * Math.PI) / 180;
                LenghtSides.Add("AngleA", (double)AngleA);
                LenghtSides.Add("AngleB", (double)AngleB);
                LenghtSides.Add("AngleC", (double)AngleC);
            }
            if(ValueC == null)
            {
                ValueC = 10;             
                ValueA = Math.Round((((double)ValueC * Math.Sin(Beta)) / (Math.Sin(Alfa))), 2);
                ValueB = Math.Round((((double)ValueC * Math.Sin(Gamma)) / (Math.Sin(Alfa))), 2);
                LenghtSides.Add("TValuec", (double)ValueB);
                LenghtSides.Add("TValuea", (double)ValueC);
                LenghtSides.Add("TValueb", (double)ValueA);
            }
            
            return LenghtSides;
        }
        private void SolveTriangel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Dictionary<string, double> SolvingDictionary = new Dictionary<string, double>();
                double diffrent = 0;
                double val;
                SolvingDictionary = SolveAfterAngles(SolvingDictionary,
                    double.TryParse(AngleA.Text, out val) ? (double?)val : null,
                    double.TryParse(AngleB.Text, out val) ? (double?)val : null,
                    double.TryParse(AngleC.Text, out val) ? (double?)val : null,
                    double.TryParse(TValuec.Text, out val) ? (double?)val : null,
                    double.TryParse(TValueb.Text, out val) ? (double?)val : null,
                    double.TryParse(TValuea.Text, out val) ? (double?)val : null
                    );
                bool checkclear = false;
                bool Checkanglessum = true;
                if (TValuec.Text == "" && TValueb.Text == "" && TValuea.Text == "" )
                {
                    checkclear = true;
                }
                if (SolvingDictionary.Count != 0)
                {
                    var sol = ShowSolverBox;
                    foreach (var item in SolvingDictionary)
                    {
                        TextBox searched = sol.FindName(item.Key) as TextBox;
                        searched.Text = item.Value.ToString();
                    }
                    if (double.Parse(AngleA.Text) + double.Parse(AngleB.Text) + double.Parse(AngleC.Text) != 180) Checkanglessum = false;
                }
                double ValueC = double.Parse(TValuec.Text);
                double ValueA = double.Parse(TValuea.Text);
                double ValueB = double.Parse(TValueb.Text);
                if (checkclear == true)
                {
                    TValuec.Text = "";
                    TValuea.Text = "";
                    TValueb.Text = "";
                }

                if (ValueA + ValueB > ValueC && ValueA + ValueC > ValueB && ValueC + ValueB > ValueA && Checkanglessum == true)
                {
                    SolveTriangelProperties.IsEnabled = true;
                    Triangle = triangleview.DrawTriangel(TriangeImg, Triangle, TValuea, TValueb, TValuec, AngleA, AngleB, AngleC, diffrent, ValueC, ValueA, ValueB);
                    SolveTriangelProperties.SelectedIndex = 0;
                    drawlabel = true;
                }
                else
                {
                    TValuea.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 182, 16, 16));
                    TValueb.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 182, 16, 16));
                    TValuec.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 182, 16, 16));
                    AngleA.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 182, 16, 16));
                    AngleB.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 182, 16, 16));
                    AngleC.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 182, 16, 16));
                    try
                    {
                        TriangeImg.Children.RemoveRange(0, TriangeImg.Children.Count);
                    }
                    catch { }
                    drawlabel = false;
                }
            }
            catch { } 
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
            triangleview.EnterToBoxSide(sender, ref ListLabel2);
        }
 
        private void SolverLabel_MouseLeave(object sender, MouseEventArgs e)
        {
            triangleview.LeaveBoxSide(sender, ref ListLabel2);
        }

        private void SolverLabelAngle_MouseEnter(object sender, MouseEventArgs e)
        {
            triangleview.EnterToBoxAngle(sender, ref ListLabel);
        }

        private void SolverLabelAngle_MouseLeave(object sender, MouseEventArgs e)
        {
            triangleview.LeaveBoxAngle(sender, ref ListLabel);
        }

        private void SolveTriangelProperties_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            trangleshowboxview.ShowSolverBox_Window(SolveTriangelProperties.SelectedIndex, ShowSolverBox,TypeSolving, AngleA, AngleB, AngleC, TValuea, TValueb, TValuec);
        }

        private void TypeSolving_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SolveTriangelProperties.SelectedIndex = -1;
            ShowSolverBox.Items.Clear();
            SolveTriangelProperties.IsEnabled = false;
            TypeSolvingGrid.IsEnabled = true;
            TriangeImg.Children.RemoveRange(0, TriangeImg.Children.Count);
            int index = TypeSolving.SelectedIndex;
            switch(index)
            {
                case 0:
                    AngleA.Clear();
                    AngleB.Clear();
                    AngleC.Clear();
                    TValuea.IsEnabled = true;
                    TValueb.IsEnabled = true;
                    TValuec.IsEnabled = true;
                    AngleA.IsEnabled = false;
                    AngleB.IsEnabled = false;
                    AngleC.IsEnabled = false;
                    break;
                case 1:
                    TValuea.Clear();
                    TValueb.Clear();
                    TValuec.Clear();
                    AngleA.IsEnabled = true;
                    AngleB.IsEnabled = true;
                    AngleC.IsEnabled = true;
                    TValuea.IsEnabled = false;
                    TValueb.IsEnabled = false;
                    TValuec.IsEnabled = false;
                    break;
            }
        }
    }
}
