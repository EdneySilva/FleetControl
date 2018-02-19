using System;
using System.Collections.Generic;
using System.Text;
using Volvo.FleetControl.Core.Domain.Abstractions;

namespace Volvo.FleetControl.Core.Domain
{
    class Bus : IVehicle
    {
        public Bus(Chassis chassis)
        {
            ChassisId = chassis;
        }

        public Chassis ChassisId { get; set; }

        public VehicleType Type => VehicleType.Bus;

        public byte NumberOfPassagers { get; internal set; } = 42;

        public string Color { get; set; }
    }
}
