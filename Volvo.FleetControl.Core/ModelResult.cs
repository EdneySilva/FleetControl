using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Volvo.FleetControl.Core
{

    public struct ModelResult<T>
    {
        public ModelResult(T model)
        {
            IsSuccess = true;
            ValidationResult = Enumerable.Empty<Validation>();
            Model = model;
        }

        public ModelResult(IEnumerable<Validation> validationResult)
        {
            IsSuccess = validationResult.All(a => a.IsValid);
            ValidationResult = validationResult;
            Model = default(T);
        }

        public T Model { get; }

        public bool IsSuccess { get; }

        public bool IsFail => !IsSuccess;

        public IEnumerable<Validation> ValidationResult { get; }
    }
}
