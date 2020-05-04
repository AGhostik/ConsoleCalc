using System;
using ConsoleCalc.Extensions;
using ConsoleCalc.Models;

namespace ConsoleCalc
{
    public class ExpressionEvaluator
    {
        public decimal Evaluate(string input)
        {
            var normalizedInput = NormalizeInput(input);

            if (ExpressionUnit.TryParse(normalizedInput, out var expressionUnit))
                return expressionUnit.GetResult();

            throw new Exception("Parsing Error");
        }

        public string NormalizeInput(string input)
        {
            var result = input.RemoveExcessSpacebar().RemoveExcessLeadingSign().AddSpacebars();

            return result;
        }
    }
}
