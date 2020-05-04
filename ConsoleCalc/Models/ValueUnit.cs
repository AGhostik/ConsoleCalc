﻿using System.Globalization;

namespace ConsoleCalc.Models
{
    /// <summary>
    ///     Под этим понимается значение слева или справа выражения, X или Y
    /// </summary>
    public class ValueUnit : IUnit
    {
        private readonly decimal _value;

        public ValueUnit(decimal value)
        {
            _value = value;
        }

        public decimal GetResult()
        {
            return _value;
        }

        public static bool TryParse(string value, out ValueUnit valueUnit)
        {
            if (decimal.TryParse(value, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign,
                CultureInfo.InvariantCulture,
                out var result))
            {
                valueUnit = new ValueUnit(result);
                return true;
            }

            valueUnit = null;
            return false;
        }

        public override string ToString()
        {
            return _value.ToString(CultureInfo.InvariantCulture);
        }
    }
}