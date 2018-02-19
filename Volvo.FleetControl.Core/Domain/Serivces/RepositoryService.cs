using System;
using System.Collections.Generic;
using System.Text;
using Volvo.FleetControl.Core.Extensions;
using Volvo.FleetControl.Core.Infraestructure;

namespace Volvo.FleetControl.Core.Domain.Serivces
{
    public class RepositoryService
    {
        static MemoryRepository instance = new MemoryRepository();
        public static void RegisterDefaultRepository()
        {
            AppDomain.CurrentDomain.SetRepositoryFactory(() => instance);
        }
    }
}
