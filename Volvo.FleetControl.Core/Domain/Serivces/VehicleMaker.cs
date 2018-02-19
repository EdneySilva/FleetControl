using System;
using System.Collections.Generic;
using System.Text;
using Volvo.FleetControl.Core.Domain.Abstractions;
using Volvo.FleetControl.Core.Infraestructure;
using Volvo.FleetControl.Core.Infraestructure.Abstractions;

namespace Volvo.FleetControl.Core.Domain.Serivces
{
    public class VehicleMaker
    {
        Dictionary<VehicleType, VehicleProvider> Factory = new Dictionary<VehicleType, VehicleProvider>
        {
            { VehicleType.Bus, (chassis) => new Bus(chassis) },
            { VehicleType.Car, (chassis) => new Car(chassis) },
            { VehicleType.Truck, (chassis) => new Truck(chassis) }
        };

        public IVehicle BuildBus(Chassis chassis, PreferencesProvider vehiclePreferences) => Build(chassis, VehicleType.Bus, vehiclePreferences);

        public IVehicle BuildCar(Chassis chassis, PreferencesProvider vehiclePreferences) => Build(chassis, VehicleType.Car, vehiclePreferences);

        public IVehicle BuildTruck(Chassis chassis, PreferencesProvider vehiclePreferences) => Build(chassis, VehicleType.Truck, vehiclePreferences);

        IVehicle Build(Chassis chassis, VehicleType type, PreferencesProvider vehiclePreferences)
        {
            var vehicle = Factory[type].Invoke(chassis);
            var preferences = new DefaultPreferencesConfig();
            return vehiclePreferences(preferences).Apply(vehicle);
        }
    }

}
