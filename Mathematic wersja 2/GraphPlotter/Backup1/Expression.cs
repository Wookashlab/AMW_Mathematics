using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionPlotterControl
{
    public class Expression : IEvaluatable
    {
        string text = "";
        string textInternal = "";
        bool isValid = false;
        char charX = 'x';
        System.Collections.Generic.Dictionary<string, double> constants;

        public Expression(string expressionText)
        {
            this.constants = new Dictionary<string, double>();
            this.constants.Add("pi", Math.PI);
            this.constants.Add("e", Math.E);
            this.ExpressionText = expressionText;
        }

        #region Public Properties for IEvaluatable


        public string ExpressionText
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
                this.textInternal = "(" + value + ")";
                this.textInternal = InsertPrecedenceBrackets().Trim();
                this.Validate();
            }
        }

        public bool IsValid
        {
            get
            {
                return this.isValid;
            }
        }
        #endregion

        #region Public Methods for IEvaluatable

        public double Evaluate(double dvalueX)
        {
            if (this.isValid == false)
                return Double.NaN;
            int temp;
            return EvaluateInternal(dvalueX, 0, out temp);
        }

        #endregion

        #region Private Methods

        private void Validate()
        {
            try
            {
                int temp;
                //if expression does not throw an exception when evaluated at "1", we assume it to be valid
                EvaluateInternal(1, 0, out temp);
                this.isValid = true;
            }
            catch (FormatException)
            {
                this.isValid = false;
            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                this.isValid = false;
            }

        }

        static bool IsOperator(char character)
        {
            if (character == '+' || character == '-' || character == '*'
                || character == '/' || character == '^' || character == '%')
                return true;
            return false;

        }

        public double EvaluateInternal(double dvalueX, int startIndex, out int endIndex)
        {
            //exceptions are bubbled up

            //dAnswer is the running total
            double dAnswer = 0, dOperand = 0;
            char chCurrentChar, chOperator = '+';
            string strAngleOperator;

            for (int i = startIndex + 1; i < textInternal.Length; i++)
            {
                startIndex = i;
                chCurrentChar = textInternal[startIndex];

                // if found a number, update dOperand
                if (char.IsDigit(chCurrentChar))
                {
                    while (char.IsDigit(textInternal[i]) || textInternal[i] == '.')
                        i++;
                    dOperand = Convert.ToDouble(textInternal.Substring(startIndex, i - startIndex));
                    i--;
                }
                //if found an operator
                else if (IsOperator(chCurrentChar))
                {
                    dAnswer = DoOperation(dAnswer, dOperand, chOperator);
                    chOperator = chCurrentChar;
                }
                //if found independent variable
                else if (char.ToLower(chCurrentChar) == charX)
                {
                    dOperand = dvalueX;
                }
                //if found a bracket, solve it first
                else if (chCurrentChar == '(')
                {
                    dOperand = EvaluateInternal(dvalueX, i, out endIndex);
                    i = endIndex;
                }
                //if found closing bracket, return result
                else if (chCurrentChar == ')')
                {
                    dAnswer = DoOperation(dAnswer, dOperand, chOperator);
                    endIndex = i;
                    return dAnswer;
                }

                else //could be any function e.g. "sin" or any constant e.g "pi"
                {
                    while (char.IsLetter(textInternal[i]))
                        i++;
                    //if we got letters followed by "(", we've got a function else a constant
                    if (textInternal[i] == '(')
                    {
                        strAngleOperator = textInternal.Substring(startIndex, i - startIndex).ToLower();
                        dOperand = EvaluateInternal(dvalueX, i, out endIndex);
                        i = endIndex;
                        dOperand = DoAngleOperation(dOperand, strAngleOperator);
                    }
                    else //constant
                    {
                        dOperand = this.constants[textInternal.Substring(startIndex, i - startIndex).ToLower()];
                        i--;
                    }
                }
                if (double.IsNaN(dAnswer) || double.IsNaN(dOperand))
                {
                    endIndex = i;
                    return double.NaN;
                }
            }
            endIndex = textInternal.Length;
            return 0;
        }

        //this function contains definitions for supported functions, we can add more also.
        static double DoAngleOperation(double dOperand, string strOperator)
        {
            strOperator = strOperator.ToLower();
            switch (strOperator)
            {
                case "abs":
                    return Math.Abs(dOperand);
                case "sin":
                    return Math.Sin(dOperand);
                case "cos":
                    return Math.Cos(dOperand);
                case "tan":
                    return Math.Tan(dOperand);
                case "sec":
                    return 1.0 / Math.Cos(dOperand);
                case "cosec":
                    return 1.0 / Math.Sin(dOperand);
                case "cot":
                    return 1.0 / Math.Tan(dOperand);
                case "arcsin":
                    return Math.Asin(dOperand);
                case "arccos":
                    return Math.Acos(dOperand);
                case "arctan":
                    return Math.Atan(dOperand);
                case "exp":
                    return Math.Exp(dOperand);
                case "ln":
                    return Math.Log(dOperand);
                case "log":
                    return Math.Log10(dOperand);
                case "antilog":
                    return Math.Pow(10, dOperand);
                case "sqrt":
                    return Math.Sqrt(dOperand);

                case "sinh":
                    return Math.Sinh(dOperand);
                case "cosh":
                    return Math.Cosh(dOperand);
                case "tanh":
                    return Math.Tanh(dOperand);
                case "arcsinh":
                    return Math.Log(dOperand + Math.Sqrt(dOperand * dOperand + 1));
                case "arccosh":
                    return Math.Log(dOperand + Math.Sqrt(dOperand * dOperand - 1));
                case "arctanh":
                    return Math.Log((1 + dOperand) / (1 - dOperand)) / 2;
                default:
                    //throw new ArgumentException("InvalidAngleOperatorException");
                    return double.NaN;
            }
        }

        // returns dOperant1 (op) dOperand2
        static double DoOperation(double dOperand1, double dOperand2, char chOperator)
        {
            switch (chOperator)
            {
                case '+':
                    return dOperand1 + dOperand2;
                case '-':
                    return dOperand1 - dOperand2;
                case '*':
                    return dOperand1 * dOperand2;
                case '/':
                    return dOperand1 / dOperand2;
                case '^':
                    return Math.Pow(dOperand1, dOperand2);
                case '%':
                    return dOperand1 % dOperand2;
            }
            return double.NaN;
        }

        //insert brackets at appropriate positions since the evaluation function 
        // only evaluates from Left To Right considering only bracket's precedence
        string InsertPrecedenceBrackets()
        {
            int i = 0, j = 0;
            int iBrackets = 0;
            bool bReplace = false;
            int iLengthExpression;
            string strExpression = this.textInternal;

            //Precedence for * && /
            i = 1;
            iLengthExpression = strExpression.Length;
            while (i <= iLengthExpression)
            {
                if (strExpression.Substring(-1 + i, 1) == "*" || strExpression.Substring(-1 + i, 1) == "/")
                {
                    for (j = i - 1; j > 0; j--)
                    {
                        if (strExpression.Substring(-1 + j, 1) == ")")
                            iBrackets = iBrackets + 1;
                        if (strExpression.Substring(-1 + j, 1) == "(")
                            iBrackets = iBrackets - 1;
                        if (iBrackets < 0)
                            break;
                        if (iBrackets == 0)
                        {
                            if (strExpression.Substring(-1 + j, 1) == "+" || strExpression.Substring(-1 + j, 1) == "-")
                            {
                                strExpression = strExpression.Substring(-1 + 1, j) + "(" + strExpression.Substring(-1 + j + 1);
                                bReplace = true;
                                i = i + 1;
                                break;
                            }
                        }
                    }
                    iBrackets = 0;
                    j = i;
                    i = i + 1;
                    while (bReplace == true)
                    {
                        j = j + 1;
                        if (strExpression.Substring(-1 + j, 1) == "(")
                            iBrackets = iBrackets + 1;
                        if (strExpression.Substring(-1 + j, 1) == ")")
                        {
                            if (iBrackets == 0)
                            {
                                strExpression = strExpression.Substring(-1 + 1, j - 1) + ")" + strExpression.Substring(-1 + j);
                                bReplace = false;
                                i = i + 1;
                                break;
                            }
                            else
                                iBrackets = iBrackets - 1;
                        }                        
                        if (strExpression.Substring(-1 + j, 1) == "+" || strExpression.Substring(-1 + j, 1) == "-")
                        {
                            strExpression = strExpression.Substring(-1 + 1, j - 1) + ")" + strExpression.Substring(-1 + j);
                            bReplace = false;
                            i = i + 1;
                            break;
                        }
                    }
                }

                iLengthExpression = strExpression.Length;
                i = i + 1;
            }


            //Precedence for ^ && %
            i = 1;
            iLengthExpression = strExpression.Length;
            while (i <= iLengthExpression)
            {
                if (strExpression.Substring(-1 + i, 1) == "^" || strExpression.Substring(-1 + i, 1) == "%")
                {
                    for (j = i - 1; j > 0; j--)
                    {
                        if (strExpression.Substring(-1 + j, 1) == ")")
                            iBrackets = iBrackets + 1;
                        if (strExpression.Substring(-1 + j, 1) == "(")
                            iBrackets = iBrackets - 1;
                        if (iBrackets < 0)
                            break;
                        if (iBrackets == 0)
                        {
                            if (strExpression.Substring(-1 + j, 1) == "+"
                                || strExpression.Substring(-1 + j, 1) == "-"
                                || strExpression.Substring(-1 + j, 1) == "*"
                                || strExpression.Substring(-1 + j, 1) == "/")
                            {
                                strExpression = strExpression.Substring(-1 + 1, j) + "(" + strExpression.Substring(-1 + j + 1);
                                bReplace = true;
                                i = i + 1;
                                break;
                            }
                        }
                    }
                    iBrackets = 0;
                    j = i;
                    i = i + 1;
                    while (bReplace == true)
                    {
                        j = j + 1;
                        if (strExpression.Substring(-1 + j, 1) == "(")
                            iBrackets = iBrackets + 1;
                        if (strExpression.Substring(-1 + j, 1) == ")")
                        {
                            if (iBrackets == 0)
                            {
                                strExpression = strExpression.Substring(-1 + 1, j - 1) + ")" + strExpression.Substring(-1 + j);
                                bReplace = false;
                                i = i + 1;
                                break;
                            }
                            else
                                iBrackets = iBrackets - 1;
                        }
                        if (strExpression.Substring(-1 + j, 1) == "+" || strExpression.Substring(-1 + j, 1) == "-"
                            || strExpression.Substring(-1 + j, 1) == "*" || strExpression.Substring(-1 + j, 1) == "/")
                        {
                            strExpression = strExpression.Substring(-1 + 1, j - 1) + ")" + strExpression.Substring(-1 + j);
                            bReplace = false;
                            i = i + 1;
                            break;
                        }
                    }
                }
                iLengthExpression = strExpression.Length;
                i = i + 1;
            }
            return strExpression;
        }

        static double GetR(double X, double Y)
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        static double GetTheta(double X, double Y)
        {
            double dTheta;
            if (X == 0)
            {
                if (Y > 0)
                    dTheta = Math.PI / 2;
                else
                    dTheta = -Math.PI / 2;
            }
            else
                dTheta = Math.Atan(Y / X);

            //actual range of theta is from 0 to 2PI
            if (X < 0)
                dTheta = dTheta + Math.PI;
            else if (Y < 0)
                dTheta = dTheta + 2 * Math.PI;
            return dTheta;
        }

        #endregion

    }
}
