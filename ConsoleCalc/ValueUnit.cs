﻿using System.Globalization;

namespace ConsoleCalc
{
    /// <summary>
    /// Под этим понимается значение слева или справа у выражения, X или Y
    /// </summary>
    public class ValueUnit : IValueUnit
    {
        public ValueUnit(decimal value)
        {
            _value = value;
        }

        private readonly decimal _value;

        public decimal GetResult()
        {
            return _value;
        }

        public static bool TryParse(string value, out ValueUnit valueUnit)
        {
            if (decimal.TryParse(value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture,
                out var result))
            {
                valueUnit = new ValueUnit(result);
                return true;
            }

            valueUnit = null;
            return false;
        }
    }
}