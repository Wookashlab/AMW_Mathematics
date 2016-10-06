﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMW_Mathematics.ModelView
{
    public class GraphingHelp
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string MainHelp { get; set; }

       public void RownaniaFunkcje2d ()
        {
            Title = "Pomoc do zakładki \"Wyrkesy\"";
            SubTitle = "Wzory i równania 2D - układ kartezjański";
            MainHelp = "Wprowadź równanie lub funkcj, którą chcesz narysować.\n\nPrzykłady\ny = x + 3\ny = sqrt(x^2 + 2)\ny = x ^ 2 + 2x - 1 - 2";
        }
    }
}