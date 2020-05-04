using System.Collections.Generic;
using System.Text;
using ConsoleCalc.Extensions;

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
            var result = input.RemoveExcessSpacebar().RemoveExcessLeadingSign();

            return result;
        }

        public ExpressionUnit Parse(string input)
        {
            if (ExpressionUnit.TryParse(input, out var expressionUnit))
                return expressionUnit;

            return null;
        }

        
    }
}
