using System;
using System.Collections.Generic;
using System.Text;
using Volvo.FleetControl.Core.Domain;
using Volvo.FleetControl.Core.Domain.Abstractions;
using Volvo.FleetControl.Core.Infraestructure.Abstractions;
using Volvo.FleetControl.Test.Mokes;

namespace Volvo.FleetControl.Test.Extensions
{
    static class SimpleDataMokeExtension
    {
        public static IRepository AddMokedData(this IRepository repository)
        {
            repository.Store(Bus("RED", 1, Guid.NewGuid().ToString()));
            repository.Store(Bus("GREEN", 2, Guid.NewGuid().ToString()));
            repository.Store(Bus("ORANGE", 3, Guid.NewGuid().ToString()));
            repository.Store(Bus("GRAY", 4, Guid.NewGuid().ToString()));
            repository.Store(Bus("WITHE", 5, Guid.NewGuid().ToString()));
            repository.Store(Bus("RED", 6, Guid.NewGuid().ToString()));

            repository.Store(Car("RED", 7, Guid.NewGuid().ToString()));
            repository.Store(Car("GREEN", 8, Guid.NewGuid().ToString()));
            repository.Store(Car("ORANGE", 9, Guid.NewGuid().ToString()));
            repository.Store(Car("GRAY", 10, Guid.NewGuid().ToString()));
            repository.Store(Car("WITHE", 11, Guid.NewGuid().ToString()));
            repository.Store(Car("RED", 12, Guid.NewGuid().ToString()));

            repository.Store(Truck("RED", 13, Guid.NewGuid().ToString()));
            repository.Store(Truck("GREEN", 14, Guid.NewGuid().ToString()));
            repository.Store(Truck("ORANGE", 15, Guid.NewGuid().ToString()));
            repository.Store(Truck("GRAY", 16, Guid.NewGuid().ToString()));
            repository.Store(Truck("WITHE", 17, Guid.NewGuid().ToString()));
            repository.Store(Truck("RED", 18, Guid.NewGuid().ToString()));

            return repository;
        }

        static IVehicle Bus(string color, uint chassisNumber, string chassisSeries)
        {
            return new CustomVehicle()
            {
                Type = Core.Domain.VehicleType.Bus,
                Color = color,
                NumberOfPassagers = 42,
                ChassisId = new Chassis()
                {
                    ChassisNumber = chassisNumber,
                    ChassisSeries = chassisSeries
                }
            };
        }

        static IVehicle Car(string color, uint chassisNumber, string chassisSeries)
        {
            return new CustomVehicle()
            {
                Type = Core.Domain.VehicleType.Car,
                Color = color,
                NumberOfPassagers = 4,
                ChassisId = new Chassis()
                {
                    ChassisNumber = chassisNumber,
                    ChassisSeries = chassisSeries
                }
            };
        }

        static IVehicle Truck(string color, uint chassisNumber, string chassisSeries)
        {
            return new CustomVehicle()
            {
                Type = Core.Domain.VehicleType.Truck,
                Color = color,
                NumberOfPassagers = 1,
                ChassisId = new Chassis()
                {
                    ChassisNumber = chassisNumber,
                    ChassisSeries = chassisSeries
                }
            };
        }
    }
}
