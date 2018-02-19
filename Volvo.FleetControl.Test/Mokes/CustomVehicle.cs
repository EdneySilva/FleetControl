using System;
using System.Collections.Generic;
using System.Text;
using Volvo.FleetControl.Core.Domain;
using Volvo.FleetControl.Core.Domain.Abstractions;

namespace Volvo.FleetControl.Test.Mokes
{
    class CustomVehicle : IVehicle
    {
        public Chassis ChassisId { get; set; }

        public VehicleType Type { get; set; }

        public byte NumberOfPassagers { get; set; }

        public string Color { get; set; }
    }
}
