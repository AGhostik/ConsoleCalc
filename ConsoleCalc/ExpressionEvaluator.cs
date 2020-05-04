using System;
using ConsoleCalc.Extensions;
using ConsoleCalc.Models;

namespace ConsoleCalc
{
    public class ExpressionEvaluator
    {
        public decimal Evaluate(string input)
        {
            var normalizedInput = input.RemoveExcessSpacebar().RemoveExcessLeadingSign().AddSpacebars();

            if (ExpressionUnit.TryParse(normalizedInput, out var expressionUnit))
                return expressionUnit.GetResult();

            throw new Exception("Parsing Error");
        }
    }
}
