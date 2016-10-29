using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMW_Mathematics.E_ModelView
{
    class ComboListEquationView
    {
        public List<string> Source { get; set; }
        public ComboListEquationView()
        {
            Source = new List<string>
            {
               "Solve 1 Equation",
               "Solve a System of 2 Equations",
               "Solve a System of 3 Equations",
               "Solve a System of 4 Equations",
               "Solve a System of 5 Equations",
               "Solve a System of 6 Equations"
            };
        }
    }
}
