using System;
using System.Collections.Generic;
using System.Text;

namespace Volvo.FleetControl.Core
{
    public struct Validation
    {
        public static implicit operator Validation(string error) => new Validation(false, error);

        public static implicit operator Validation(Exception exception) => new Validation(false, exception.Message);

        public static Validation Success { get; } = new Validation(true, null);

        public Validation(bool isValid, string error)
        {
            IsValid = isValid;
            Error = error;
        }

        public bool IsValid { get; private set; }
        public string Error { get; private set; }
    }
}
