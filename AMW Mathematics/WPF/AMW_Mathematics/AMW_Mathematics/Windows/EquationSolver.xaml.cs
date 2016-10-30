using MahApps.Metro.Controls;
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
using AMW_Mathematics.E_ModelView;

namespace AMW_Mathematics.Windows
{
    /// <summary>
    /// Interaction logic for EquationSolver.xaml
    /// </summary>
    public partial class EquationSolver : MetroWindow
    {
        public EquationSolver()
        {
            InitializeComponent();
            ListViewExp.Items.Add(new List<ListExpresionView> {
                                 new ListExpresionView { Exp = "", Watermark = "Equation 1" }
            });
        }

        private void CoEquations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int index = CoEquations.SelectedIndex;
                int count;
                if (ListViewExp.Items.Count <= index && ListViewExp.Items.Count < 6)
                {
                    count = index - ListViewExp.Items.Count +1;
                    for (int i = 1; i <= count; i++)
                    {
                        int val = ListViewExp.Items.Count + 1;
                        ListViewExp.Items.Add(new ListExpresionView { Exp = "", Watermark = "Equation " +  val});
                    }
                }
            }
            catch { }
        }
    }
}
