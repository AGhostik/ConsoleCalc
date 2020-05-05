using System;
using System.Linq;
using ConsoleCalc.Extensions;
using ConsoleCalc.Models;

namespace ConsoleCalc
{
    public class ExpressionEvaluator
    {
        public decimal Evaluate(string input)
        {
            var unexpectedCharacter = InputValidationService.FindUnexpectedCharacters(input).FirstOrDefault();
            if (unexpectedCharacter != null)
                throw new Exception($"Unexpected character: \'{unexpectedCharacter.Character}\', position: \'{unexpectedCharacter.Index}\'");

            var invalidBracket = InputValidationService.FindInvalidBracket(input).FirstOrDefault();
            if (invalidBracket != null)
                throw new Exception($"Invalid bracket: \'{invalidBracket.Character}\', position: \'{invalidBracket.Index}\'");

            var normalizedInput = input.RemoveExcessSpacebar().RemoveExcessLeadingSign().AddSpacebars().AddBracers();

            if (ExpressionUnit.TryParse(normalizedInput, out var expressionUnit))
                return expressionUnit.GetResult();

            throw new Exception("Parsing Error");
        }
    }
}
