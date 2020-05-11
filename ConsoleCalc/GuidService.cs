using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleCalc
{
    /// <summary>
    /// Костыли
    /// </summary>
    public static class GuidService
    {
        public static string NewGuid()
        {
            return Guid.NewGuid().ToString().Replace('-', '_');
        }
    }
}
