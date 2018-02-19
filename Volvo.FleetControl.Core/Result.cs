using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Volvo.FleetControl.Core
{
    public struct Result
    {
        public Result(IEnumerable<Validation> validationResult)
        {
            IsSuccess = validationResult.All(a => a.IsValid);
            ValidationResult = validationResult;
        }

        public bool IsSuccess { get; }

        public bool IsFail => !IsSuccess;

        public IEnumerable<Validation> ValidationResult { get; }
    }
}
