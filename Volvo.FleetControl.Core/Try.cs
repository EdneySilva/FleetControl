using System;
using System.Collections.Generic;
using System.Text;

namespace Volvo.FleetControl.Core
{
    public struct Try<TSuccess, TFail>
    {
        public static implicit operator Try<TSuccess, TFail>(TFail fail)
        {
            return new Try<TSuccess, TFail>(fail);
        }

        public static implicit operator Try<TSuccess, TFail>(TSuccess success)
        {
            return new Try<TSuccess, TFail>(success);
        }

        public static implicit operator Catch<TSuccess, TFail>(Try<TSuccess, TFail> context)
        {
            return new Catch<TSuccess, TFail>(context);
        }

        internal TFail Fail { get; }

        internal TSuccess Success { get; }

        public bool IsFail { get; }

        public bool IsSuccess
        {
            get
            {
                return !this.IsFail;
            }
        }

        public Catch<TSuccess, TFail> HandlerErrors() => this;

        public Catch<TSuccess, TFail> HandlerErrors(Action<TFail> fail) => new Catch<TSuccess, TFail>(this, fail);

        public Try(TFail fail)
        {
            this.Fail = fail;
            this.Success = default(TSuccess);
            this.IsFail = true;
        }

        public Try(TSuccess success)
        {
            this.Fail = default(TFail);
            this.Success = success;
            this.IsFail = false;
        }
    }
}
