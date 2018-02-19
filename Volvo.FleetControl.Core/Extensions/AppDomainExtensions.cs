using System;
using System.Collections.Generic;
using System.Text;
using Volvo.FleetControl.Core.Infraestructure.Abstractions;

namespace Volvo.FleetControl.Core.Extensions
{
    public static class AppDomainExtensions
    {
        private const string REPOSITORY_KEY = "Volvo_FleetControl_Core_Repository";

        public static void SetRepositoryFactory(this AppDomain appDomain, Func<IRepository> repository) => appDomain.SetData(REPOSITORY_KEY, repository);

        public static IRepository Repository(this AppDomain appDomain)
        {
            var factory = appDomain.GetData(REPOSITORY_KEY);
            if (factory == null)
                return null;
            return ((Func<IRepository>)factory).Invoke();
        }
    }
}
