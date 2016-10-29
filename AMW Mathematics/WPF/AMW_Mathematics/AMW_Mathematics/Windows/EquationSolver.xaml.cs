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
            DataContext = new ComboListEquationView();
            ListViewExp.Items.Add(new List<ListExpresionView> {
                                 new ListExpresionView { Exp = "", Watermark = "Equation 1" }
            });
        }
    }
}
