using System;
using System.Collections.Generic;
using System.Text;

namespace Volvo.FleetControl.Core.Domain.Abstractions
{
    public interface IVehicle
    {
        Chassis ChassisId { get; }
        VehicleType Type { get; }
        byte NumberOfPassagers { get; }
        string Color { get; set; }
    }
}
