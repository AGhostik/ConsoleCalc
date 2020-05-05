﻿using System;
using System.Text.RegularExpressions;

namespace ConsoleCalc.Models
{
    /// <summary>
    ///     Под этим понимается выражение вида: X [операция] Y
    /// </summary>
    public class ExpressionUnit : IUnit
    {
        public ExpressionUnit(IUnit firstValue, Operation operation, IUnit secondValue)
        {
            FirstValue = firstValue;
            SecondValue = secondValue;
            Operation = operation;
        }

        public IUnit FirstValue { get; }
        public IUnit SecondValue { get; }
        public Operation Operation { get; }

        public decimal GetResult()
        {
            var firstvalue = FirstValue.GetResult();
            var secondValue = SecondValue.GetResult();

            switch (Operation)
            {
                case Operation.Plus:
                    return firstvalue + secondValue;
                case Operation.Minus:
                    return firstvalue - secondValue;
                case Operation.Div:
                    return firstvalue / secondValue;
                case Operation.Multiply:
                    return firstvalue * secondValue;
                case Operation.DivRem:
                    return firstvalue % secondValue;
                case Operation.Unknow:
                    throw new Exception("unknown operation");
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static bool TryParse(string value, out ExpressionUnit expressionUnit)
        {
            expressionUnit = null;

            var regex = RegexService.GetRegex_FindExpressionParts();
            var match = regex.Match(value);

            if (match.Success)
            {
                if (!TryParseValueUnit(match.Groups["firstValue"].Value, out var firstValue))
                    return false;

                if (!TryParseOperation(match.Groups["operation"].Value, out var operation))
                    return false;

                if (!TryParseValueUnit(match.Groups["secondValue"].Value, out var secondValue))
                    return false;

                expressionUnit = new ExpressionUnit(firstValue, operation, secondValue);

                return true;
            }

            // лучше заменить на логирование
            throw new Exception($"regex match is unsuccessfull, value: \'{value}\'");
        }

        private static bool TryParseValueUnit(string value, out IUnit result)
        {
            if (ValueUnit.TryParse(value, out var valueUnit))
            {
                result = valueUnit;
                return true;
            }

            if (TryParse(value, out var expressionUnit))
            {
                result = expressionUnit;
                return true;
            }

            result = null;
            return false;
        }

        private static bool TryParseOperation(string value, out Operation result)
        {
            switch (value)
            {
                case "+":
                    result = Operation.Plus;
                    return true;
                case "-":
                    result = Operation.Minus;
                    return true;
                case "*":
                    result = Operation.Multiply;
                    return true;
                case "/":
                    result = Operation.Div;
                    return true;
                case "%":
                    result = Operation.DivRem;
                    return true;
                default:
                    result = Operation.Unknow;
                    return false;
            }
        }

        public override string ToString()
        {
            string operationString;

            switch (Operation)
            {
                case Operation.Plus:
                    operationString = "+";
                    break;
                case Operation.Minus:
                    operationString = "-";
                    break;
                case Operation.Div:
                    operationString = "/";
                    break;
                case Operation.Multiply:
                    operationString = "*";
                    break;
                case Operation.DivRem:
                    operationString = "%";
                    break;
                default:
                    operationString = "?";
                    break;
            }

            return $"({FirstValue} {operationString} {SecondValue})";
        }
    }
}