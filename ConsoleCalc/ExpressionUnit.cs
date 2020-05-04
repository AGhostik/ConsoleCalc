using System;
using System.Text.RegularExpressions;

namespace ConsoleCalc
{
    /// <summary>
    /// Под этим понимается выражение вида: X [операция] Y
    /// </summary>
    public class ExpressionUnit : IValueUnit
    {
        public ExpressionUnit(IValueUnit firstValue, Operation operation, IValueUnit secondValue)
        {
            FirstValue = firstValue;
            SecondValue = secondValue;
            Operation = operation;
        }

        public IValueUnit FirstValue { get; }
        public IValueUnit SecondValue { get; }
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
                case Operation.Unknow:
                    throw new Exception("unknown operation");
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static bool TryParse(string value, out ExpressionUnit expressionUnit)
        {
            expressionUnit = null;

            var regex = new Regex("[(]{0,1}(?<firstValue>[-+*/% \\d]+)[)]{0,1} (?<operation>[-+*/%]) [(]{0,1}(?<secondValue>[-+*/% \\d]+)[)]{0,1}");
            var match = regex.Match(value);

            if (match.Success)
            {
                if (!match.Groups.TryGetValue("firstValue", out var firstValueGroup))
                    throw new Exception("MatchGroup.TryGetValue failed - firstValueGroup");
                
                if (!match.Groups.TryGetValue("operation", out var operationGroup))
                    throw new Exception("MatchGroup.TryGetValue failed - operationGroup");
                
                if (!match.Groups.TryGetValue("secondValue", out var secondValueGroup))
                    throw new Exception("MatchGroup.TryGetValue failed - secondValueGroup");

                if (!TryParseValueUnit(firstValueGroup.Value, out var firstValue))
                    return false;

                if (!TryParseOperation(operationGroup.Value, out var operation))
                    return false;
                
                if (!TryParseValueUnit(secondValueGroup.Value, out var secondValue))
                    return false;

                expressionUnit = new ExpressionUnit(firstValue, operation, secondValue);

                return true;
            }
            else
            {
                throw new Exception("regex match is unsuccessfull");
            }
        }

        private static bool TryParseValueUnit(string value, out IValueUnit result)
        {
            if (ValueUnit.TryParse(value, out var valueUnit))
            {
                result = valueUnit;
                return true;
            }
            if (ExpressionUnit.TryParse(value, out var expressionUnit))
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
    }
}