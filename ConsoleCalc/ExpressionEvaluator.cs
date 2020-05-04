using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleCalc
{
    public class ExpressionEvaluator
    {
        public decimal Evaluate(string expression)
        {
            return 0;
        }

        public string NormalizeInput(string input)
        {
            var result = RemoveExcessSpacebar(input);
            return result;
        }

        public ExpressionUnit Parse(string input)
        {
            if (ExpressionUnit.TryParse(input, out var expressionUnit))
            {
                return expressionUnit;
            }

            return null;
        }
        
        private string RemoveExcessSpacebar(string input)
        {
            var regex = new Regex("[ ]{2,}");
            var result = regex.Replace(input, " ");
            return result;
        }
    }
}
