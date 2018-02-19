using System;
using System.Collections.Generic;
using System.Text;
using Volvo.FleetControl.Core.Domain.Abstractions;

namespace Volvo.FleetControl.Core.Domain
{
    class Truck : IVehicle
    {
        public Truck(Chassis chassis)
        {
            ChassisId = chassis;
        }
        
        public VehicleType Type => VehicleType.Truck;

        public byte NumberOfPassagers => 1;

        public string Color { get; set; }

        public Chassis ChassisId { get; }
    }
}
