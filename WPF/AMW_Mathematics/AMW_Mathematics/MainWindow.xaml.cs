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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaximaSharp;
namespace AMW_Mathematics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Liczwyraz l;
        string wejscie;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button7_Copy_Click(object sender, RoutedEventArgs e)
        {
            string cos = Maxima.Eval("2+2");
            wejscie = textBox.Text + "=";
            l = new Liczwyraz();
            listBox.Items.Add(wejscie + l.liczwyrazenie(wejscie));
            wejscie = "";
            textBox.Clear();
        }
    }
}
