using System;
using System.Linq;
using ConsoleCalc.Extensions;
using ConsoleCalc.Models;

namespace ConsoleCalc
{
    public class ExpressionEvaluator
    {
        /// <summary>
        /// Высчитывает ответ из текстового математического выражения
        /// <para>Допустимые символы:</para>
        /// <para>Цифры [0 - 9];</para>
        /// <para>Знак точки для обозначения дробной части</para>
        /// <para>Знаки арифметических операций: + - * / %</para>
        /// <para>Круглые скобочки для обозначения приоритета одной операции над другой</para>
        /// </summary>
        /// <param name="input"></param>
        /// <exception cref="Exception"></exception>
        /// <returns></returns>
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
