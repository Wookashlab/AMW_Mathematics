﻿using System;
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

namespace AMW_Mathematics
{
    /// <summary>
    /// Interaction logic for VariableWindow.xaml
    /// </summary>
    public partial class VariableWindow : Window
    {
        public string Variable;
        public VariableWindow()
        {
            InitializeComponent();

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Variable = "a";
            this.Close();
        }
    }
}