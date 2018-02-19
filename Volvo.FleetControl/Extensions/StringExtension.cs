using System;
using System.Collections.Generic;
using System.Text;

namespace Volvo.FleetControl.Extensions
{
    static class StringExtension
    {
        public static uint ToUint(this string value)
        {
            uint safe = 0;
            uint.TryParse(value, out safe);
            return safe;
        }
    }
}
