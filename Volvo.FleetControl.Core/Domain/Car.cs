using System;
using System.Collections.Generic;
using System.Text;
using Volvo.FleetControl.Core.Domain.Abstractions;

namespace Volvo.FleetControl.Core.Domain
{
    class Car : IVehicle
    {
        public Car(Chassis chassis)
        {
            ChassisId = chassis;
        }

        public Chassis ChassisId { get; }

        public VehicleType Type => VehicleType.Car;

        public byte NumberOfPassagers => 4;

        public string Color { get; set; }
    }
}
