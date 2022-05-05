using System;
using System.Collections.Generic;

namespace Calculator
{
    public class Program
    {
        static double result = new double();
        public static void Main(string[] args)
        {
            ConsoleKeyInfo key;            

            Console.WriteLine("Welcome to Calculator.");

            do
            {
                string sum = UserInput();
                
                result = Calculate(sum);

                if (double.IsInfinity(result) || double.IsNaN(result) || double.IsNegativeInfinity(result) ||
                    double.IsPositiveInfinity(result))
                {
                    Console.WriteLine("\nResult is not valid.");
                }
                else
                {
                    Console.WriteLine("\nResult: {0}", result);
                }
                
                Console.WriteLine("\nPress Esc to exit, any key to continue.");
                key = Console.ReadKey();
            } while (key.Key != ConsoleKey.Escape);       
        }

        public static double Calculate(string sum)
        {            
            List<string> formulaList = new List<string>();
            string[] sumArray = sum.Split(' ');

            foreach (var s in sumArray)
            {
                formulaList.Add(s);
            }

            if (formulaList.Contains("("))
            {
                result = BracketCalculation(formulaList);
            }
            else
            {
                result = NormalCalculation(formulaList);
            }            
           
            return result;
        }

        private static double NormalCalculation(List<string> formulaList)
        {          
            for (int i = 0; i < formulaList.Count; i++)
            {
                if (formulaList[i] == "*" || formulaList[i] == "/")
                {
                    result = DoOperatorsCalc(formulaList[i - 1], formulaList[i], formulaList[i + 1]);
                    formulaList.RemoveRange(i - 1, 3);
                    formulaList.Insert(i - 1, result.ToString());
                    i = 0;
                }
            }

            for (int i = 0; i < formulaList.Count; i++)
            {
                if (formulaList[i] == "+")
                {
                    result = DoOperatorsCalc(formulaList[i - 1], formulaList[i], formulaList[i + 1]);
                    formulaList.RemoveRange(i - 1, 3);
                    formulaList.Insert(i - 1, result.ToString());
                    i = 0;
                }
                if (formulaList[i] == "-")
                {
                    result = DoOperatorsCalc(formulaList[i - 1], formulaList[i], formulaList[i + 1]);
                    formulaList.RemoveRange(i - 1, 3);
                    formulaList.Insert(i - 1, result.ToString());
                    i = 0;
                }
            }

            return result;
        }        

        private static double DoOperatorsCalc(string s1, string fOperator, string s2)
        {
            try
            {
                double d1 = Convert.ToDouble(s1);
                double d2 = Convert.ToDouble(s2);

                switch (fOperator)
                {
                    case "*":
                        return d1 * d2;
                    case "/":                           
                        return d1 / d2;
                    case "+":
                        return d1 + d2;
                    case "-":
                        return d1 - d2;
                    default:
                        throw new Exception("Operators not found.");
                }
            }            
           
            catch 
            {
                throw new Exception("Please key in valid formula.");
            }           
        }

        private static double BracketCalculation(List<string> formulaList)
        {
            try
            {
                do
                {
                    int? closeBracket = null;
                    int? openBracket = null;

                    for (int i = 0; i < formulaList.Count; i++)
                    {
                        if (formulaList[i].ToString() == "(")
                        {
                            openBracket = i;
                        }

                        if (formulaList[i].ToString() == ")")
                        {
                            if (!closeBracket.HasValue)
                            {
                                closeBracket = i;
                            }
                        }
                    }

                    List<string> subFormula = formulaList.GetRange(openBracket.Value + 1, closeBracket.Value - openBracket.Value - 1);
                    if (subFormula.Count < 3)
                    {
                        throw;
                    }
                    else
                    {
                        result = NormalCalculation(subFormula);
                        formulaList.RemoveRange(openBracket.Value, closeBracket.Value - openBracket.Value + 1);
                        formulaList.Insert(openBracket.Value, result.ToString());
                        Console.WriteLine("\nformulaList: " + formulaList);
                    }
                } while (formulaList.Contains("("));

                result = NormalCalculation(formulaList);
            }

            catch
            {
                throw new Exception("Please key in valid formula.");
            }

            return result;
        }

        private static string UserInput()
        {
            Console.WriteLine("\nPlease key in your input: ");
            string sum = Console.ReadLine();            

            return sum;
        }
    }
}