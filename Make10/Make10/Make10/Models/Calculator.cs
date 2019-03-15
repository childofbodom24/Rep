using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Make10.Models
{
    public static class Calculator
    {
        private static Dictionary<string, Func<double, double, double>> operatorDictionary;

        public static Dictionary<string, Func<double, double, double>> OperatorDictionary
        {
            get
            {
                if (operatorDictionary == null)
                {
                    operatorDictionary = new Dictionary<string, Func<double, double, double>>()
                    {
                        { "＋", (v1, v2)=> v1 + v2 },
                        { "－", (v1, v2)=> v1 - v2 },
                        { "×", (v1, v2)=> v1 * v2 },
                        { "÷", (v1, v2)=> v1 / v2 }
                    };
                }

                return operatorDictionary;
            }
        }

        public static IEnumerable<int> CreateNumbers(int size)
        {
            var operatorsCombination = CreateOperatorsCombination(size);

            var numbers = new int[size];

            int seed = DateTime.Now.Millisecond;
            Random r = new Random(seed);
            while (true)
            {
                foreach (var i in Enumerable.Range(0, size))
                {
                    numbers[i] = r.Next(1, 10);
                }

                if(ValidateNumbers(numbers, operatorsCombination) != null)
                {
                    return numbers;
                }
            }
        }

        public static IEnumerable<string> ValidateNumbers(IEnumerable<int> numbers, IEnumerable<IEnumerable<string>> operatorsCombination = null)
        {
            if(operatorsCombination == null)
            {
                operatorsCombination = CreateOperatorsCombination(numbers.Count());
            }

            var formula = new List<string>();
            foreach (var operators in operatorsCombination)
            {
                formula.Clear();
                foreach (var i in Enumerable.Range(0, numbers.Count()))
                {
                    if (i != 0)
                    {
                        formula.Add(operators.ElementAt(i - 1));
                    }

                    formula.Add(numbers.ElementAt(i).ToString());
                }

                var answerText = Calculate(formula);
                int n = 0;
                if (int.TryParse(answerText, out n))
                {
                    if (n == 10 && operators.Any(o => o != "＋"))
                    {
                        return formula;
                    }
                }
            }

            return null;
        }

        public static string Calculate(IEnumerable<string> formula)
        {
            double answer = 0;
            string ope = string.Empty;
            foreach (var f in formula)
            {
                if (Calculator.OperatorDictionary.ContainsKey(f))
                {
                    ope = f;
                }
                else
                {
                    int n = 0;
                    if (int.TryParse(f, out n))
                    {
                        if (string.IsNullOrEmpty(ope))
                        {
                            answer = n;
                        }
                        else
                        {
                            answer = Calculator.OperatorDictionary[ope](answer, n);
                            ope = string.Empty;
                        }
                    }
                    else
                    {
                        throw new Exception("bug");
                    }
                }
            }

            var answerText = answer.ToString();
            return (answerText.Contains(".")? answer.ToString("F3") : answerText) + ope;
        }

        private static IEnumerable<IEnumerable<int>> CreateNumbersCombination(IEnumerable<int> numbers)
        {
            var result = new List<IEnumerable<int>>();
            foreach (var n in EnumerateCombination<int>(numbers, numbers.Count(), false))
            {
                result.Add(n);
            }

            return result.ToArray();
        }

        private static IEnumerable<IEnumerable<string>> CreateOperatorsCombination(int numbersCount)
        {
            var result = new List<IEnumerable<string>>();
            foreach (var o in EnumerateCombination<string>(OperatorDictionary.Keys, numbersCount - 1, true))
            {
                result.Add(o);
            }

            return result.ToArray();
        }

        private static IEnumerable<T[]> EnumerateCombination<T>(IEnumerable<T> items, int k, bool withRepetition)
        {
            if (k == 1)
            {
                yield return new T[] { items.First() };
                yield break;
            }

            foreach (var i in Enumerable.Range(0, items.Count()))
            {
                var leftside = new T[] { items.ElementAt(i) };
                var unused = items.ToList();
                if (!withRepetition)
                {
                    unused.RemoveAt(i);
                }

                foreach (var rightside in EnumerateCombination<T>(unused, k - 1, withRepetition))
                {
                    yield return leftside.Concat(rightside).ToArray();
                }
            }
        }
    }
}
