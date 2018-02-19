using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Volvo.FleetControl.Core.Domain.Abstractions;

namespace Volvo.FleetControl.Core.Domain.Serivces
{
    public class VehicleValidatorCollection
    {
        private List<VehicleValiator> Validators = new List<VehicleValiator>();

        public VehicleValidatorCollection()
        {
        }

        public VehicleValidatorCollection ChassisIsRequired(string message = "The chassis id is required!")
            => AddCustomValidator((vehicle) =>
            {
                if (vehicle.ChassisId == null)
                    return message;
                if (vehicle.ChassisId.ChassisNumber == 0)
                    return "Chassis number is required";
                if (string.IsNullOrWhiteSpace(vehicle.ChassisId.ChassisSeries))
                    return "Chassis series is required";
                return Validation.Success;
            });
        
        public VehicleValidatorCollection ColorIsRequired(string message = "The color is required!")
            => AddCustomValidator((vehicle) =>
            {
                if (string.IsNullOrWhiteSpace(vehicle.Color))
                    return message;
                return Validation.Success;
            });

        public VehicleValidatorCollection CheckNumberOfPassagers(string message = null)
        {
            short[] allowedPassagerNumber = new short[3];
            allowedPassagerNumber[(int)VehicleType.Bus] = 42;
            allowedPassagerNumber[(int)VehicleType.Car] = 4;
            allowedPassagerNumber[(int)VehicleType.Truck] = 1;

            AddCustomValidator((vehicle) =>
            {
                if (allowedPassagerNumber[(int)vehicle.Type] != vehicle.NumberOfPassagers)
                    return message ?? $"The number of passagers supported is {allowedPassagerNumber[(int)vehicle.Type]}";
                return Validation.Success;
            });
            return this;
        }

        public VehicleValidatorCollection AddCustomValidator(VehicleValiator validator)
        {
            Validators.Add(validator);
            return this;
        }

        public IEnumerable<Validation> Result(IVehicle vehicle) => Validators.Select(s => s.Invoke(vehicle)).Where(w => !w.IsValid).ToArray();
    }
}
