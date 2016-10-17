using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMW_Mathematics.Function
{
    class ExpresionComplexPart
    {
        public double ExpresionPartR { get; set; }
        public double ExpresionPartC { get; set; }
        public string Operator { get; set; }
    }
    class ComplexNumber
    {

        public ComplexNumber()
        {
            string expression = "2-3i+2+5i-2+62i";
            List<ExpresionComplexPart> expresioncomplexpartlist = new List<ExpresionComplexPart>();
            expresioncomplexpartlist = ExpandComples(expresioncomplexpartlist, expression);
            expresioncomplexpartlist = SumComplex(expresioncomplexpartlist[0].ExpresionPartC, expresioncomplexpartlist[0].ExpresionPartR, expresioncomplexpartlist[1].ExpresionPartC, expresioncomplexpartlist[1].ExpresionPartR, 0, expresioncomplexpartlist);
        }
        private List<ExpresionComplexPart> ExpandComples(List<ExpresionComplexPart> ExpresionPart, string expression)
        {
            expression.IndexOf("i");
            string exp = expression.Substring(0, expression.IndexOf("i"));
            double ExpresionPartR = 0;
            try
            {
                ExpresionPartR = double.Parse(exp.Substring(0, FindIndexperator(exp)));
            }
            catch
            {
                expression = expression.Remove(0, 1);
                expression.IndexOf("i");
                exp = expression.Substring(0, expression.IndexOf("i"));
                ExpresionPartR = double.Parse(exp.Substring(0, FindIndexperator(exp)));
            }
            string Operator = "";
            try
            {
                Operator = expression.Substring(expression.IndexOf("i") + 1, 1);
            }
            catch
            {
            }
            exp = exp.Remove(0, FindIndexperator(exp));
            double ExpresionPartC = double.Parse(exp.Substring(0, exp.Length));
            expression = expression.Remove(0, expression.IndexOf("i") + 1);
            ExpresionPart.Add(new ExpresionComplexPart { ExpresionPartR = ExpresionPartR, ExpresionPartC = ExpresionPartC, Operator = Operator });
            if (expression != "") ExpandComples(ExpresionPart, expression);
            return ExpresionPart;
        }
        private int FindIndexperator(string exp)
        {
            if (exp.Contains("+") == true)
            {
                return exp.IndexOf("+");
            }
            if (exp.Contains("-") == true)
            {
                return exp.IndexOf("-");
            }
            if (exp.Contains("/") == true)
            {
                return exp.IndexOf("/");
            }
            if (exp.Contains("*") == true)
            {
                return exp.IndexOf("*");
            }
            return 0;
        }
        private string FindOperator(string exp)
        {
            if (exp.Contains("+") == true)
            {
                return "+";
            }
            if (exp.Contains("-") == true)
            {
                return "-";
            }
            if (exp.Contains("/") == true)
            {
                return "/";
            }
            if (exp.Contains("*") == true)
            {
                return "*";
            }
            return "";
        }
        private List<ExpresionComplexPart> SumComplex(double partcone, double partrone, double partctwo, double partrtwo, int index, List<ExpresionComplexPart> expresioncomplexpartlist)
        {
            var expresionone = expresioncomplexpartlist.FindLast(m => m.Operator == "+");
            int ind = expresioncomplexpartlist.IndexOf(expresionone);
            var expresiontwo = expresioncomplexpartlist[ind + 1];
            double complexc = expresionone.ExpresionPartC + expresiontwo.ExpresionPartC;
            double complexr =
            //double complexc = partcone + partctwo;
           // double complexr = partrone + partrtwo;
            expresioncomplexpartlist.Remove(expresioncomplexpartlist[index]);
            expresioncomplexpartlist[index].ExpresionPartC = complexc;
            expresioncomplexpartlist[index].ExpresionPartR = complexr;
            return expresioncomplexpartlist;
        }
    }
}