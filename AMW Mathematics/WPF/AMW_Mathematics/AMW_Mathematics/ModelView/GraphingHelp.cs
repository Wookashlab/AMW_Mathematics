using System;
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

        public GraphingHelp(int a)
        {
            if (a == 1) this.RownaniaFunkcje2d();
            else this.ZestawDanych();
        }
        public void RownaniaFunkcje2d()
        {
            Title = "Help for Graphing Tab";
            SubTitle = "Equation and Function 2D";
            MainHelp = "Enter the equation or function you want to plot.\n\nExamples\ny = x + 3\ny = sqrt(x^2 + 2)\ny = x ^ 2 + 2x - 1 - 2";
        }
        public void ZestawDanych()
        {
            Title = "Help for Graphing Tab";
            SubTitle = "Data Sets - 2D";
            MainHelp = "Enter the data set that you want to plot.\n\nTips:\nClick the \"Insert Data set\" button for an easy way to enter data\n\nExamples\n{{1,1}, {- 1,1}, {- 3,9}, {2,4}}";
        }
    }
}