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
            trangleshowboxview.ShowSolverBox_Window(SolveTriangelProperties.SelectedIndex, ShowSolverBox, AngleA, AngleB, AngleC, TValuea, TValueb, TValuec);
        }
    }
}
