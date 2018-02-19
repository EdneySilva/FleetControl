using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Volvo.FleetControl.Core.Domain.Abstractions;

namespace Volvo.FleetControl.Core.Infraestructure.Abstractions
{
    public interface IRepository
    {
        IQueryable<IVehicle> GetVehicles();

        Result Store(IVehicle vehicle);

        Result Update(IVehicle vehicle);

        Result Delete(IVehicle vehicle);
    }
}
