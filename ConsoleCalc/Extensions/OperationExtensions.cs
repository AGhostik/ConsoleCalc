using System;
using ConsoleCalc.Models;

namespace ConsoleCalc.Extensions
{
    public static class OperationExtensions
    {
        public static char ToChar(this Operation operation)
        {
            char result;

            switch (operation)
            {
                case Operation.Plus:
                    result = '+';
                    break;
                case Operation.Minus:
                    result = '-';
                    break;
                case Operation.Div:
                    result = '/';
                    break;
                case Operation.Multiply:
                    result = '*';
                    break;
                case Operation.DivRem:
                    result = '%';
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(operation), operation, null);
            }

            return result;
        }
    }
}