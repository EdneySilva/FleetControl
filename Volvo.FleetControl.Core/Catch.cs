using System;

namespace Volvo.FleetControl.Core
{
    public struct Catch<TSuccess, TFail>
    {
        private Try<TSuccess, TFail> Context { get; }

        private Action<TFail> FailHandler { get; }

        internal Catch(Try<TSuccess, TFail> context)
        {
            this.Context = context;
            this.FailHandler = (Action<TFail>)(fail => { });
        }

        internal Catch(Try<TSuccess, TFail> context, Action<TFail> failHandler)
        {
            this.Context = context;
            this.FailHandler = failHandler;
        }

        public TSuccess Result()
        {
            if (!this.Context.IsFail)
                return this.Context.Success;
            this.FailHandler(this.Context.Fail);
            return default(TSuccess);
        }
    }
}