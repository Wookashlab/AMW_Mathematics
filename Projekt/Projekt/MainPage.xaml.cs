using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Projekt.Resources;
using System.IO;
using System.Text.RegularExpressions;
namespace Projekt
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

/*        private void button_Click(object sender, RoutedEventArgs e)                     //Zakomentowałem to bo narazie nie używamy tego 
        {
            var webClient = new WebClient();
            webClient.OpenReadAsync(new Uri("http://hein.bluequeen.tk/select.php"));
            webClient.OpenReadCompleted += new OpenReadCompletedEventHandler(przystanki);
        }
        void przystanki(object sender, OpenReadCompletedEventArgs e)
        {
                using (var reader = new StreamReader(e.Result))
                {
                string value = reader.ReadToEnd();
                string[] response = Regex.Split(value, "</br>");
                int i = 0;
                    while ( i < response.Length - 1)
                    {
                        List.Items.Add(response[i]);
                        i++;
                    }
             }
        } */
    }
}