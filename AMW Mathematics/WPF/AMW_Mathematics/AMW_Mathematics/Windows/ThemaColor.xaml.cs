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
using MahApps.Metro.Controls;
using MahApps.Metro;
using System.Windows.Shapes;

namespace AMW_Mathematics.Windows
{
    /// <summary>
    /// Interaction logic for ThemaColor.xaml
    /// </summary>
    public partial class ThemaColor : MetroWindow
    {
        public ThemaColor()
        {
            InitializeComponent();
        }

        
        private void button_Click(object sender, RoutedEventArgs e)
        {
            Tuple<AppTheme, Accent> appStyle = ThemeManager.DetectAppStyle(Application.Current);
            ThemeManager.ChangeAppStyle(Application.Current,
                                  ThemeManager.GetAccent("Black"),
                                  ThemeManager.GetAppTheme("BaseDark")); // or appStyle.Item1
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Tuple<AppTheme, Accent> appStyle = ThemeManager.DetectAppStyle(Application.Current);
            ThemeManager.ChangeAppStyle(Application.Current,
                                  ThemeManager.GetAccent("Green"),
                                  ThemeManager.GetAppTheme("BaseDark")); // or appStyle.Item1
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Tuple<AppTheme, Accent> appStyle = ThemeManager.DetectAppStyle(Application.Current);
            ThemeManager.ChangeAppStyle(Application.Current,
                                  ThemeManager.GetAccent("Green"),
                                  ThemeManager.GetAppTheme("BaseDark")); // or appStyle.Item1
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Tuple<AppTheme, Accent> appStyle = ThemeManager.DetectAppStyle(Application.Current);
            ThemeManager.ChangeAppStyle(Application.Current,
                                  ThemeManager.GetAccent("Green"),
                                  ThemeManager.GetAppTheme("BaseLight")); // or appStyle.Item1
        }
    }
}
