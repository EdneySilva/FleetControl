using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Volvo.FleetControl.Core.Domain.Abstractions;
using Volvo.FleetControl.Core.Infraestructure.Abstractions;
using Volvo.FleetControl.Core.Extensions;

namespace Volvo.FleetControl.Core.Domain.Serivces
{
    public class FleetManager
    {
        VehicleValidatorCollection Validators { get; }

        IRepository Repository { get; } = AppDomain.CurrentDomain.Repository();

        public FleetManager(VehicleValidatorCollection validators)
        {
            Validators = validators;
            Validators.AddCustomValidator((vehicle) =>
            {
                if (vehicle.ChassisId == null)
                    return Validation.Success;
                if (Repository.GetVehicles().Any(v => v.ChassisId.ChassisNumber == vehicle.ChassisId.ChassisNumber))
                    return "There are a vehicle stored with the same chassis id!";
                return Validation.Success;
            });
        }

        public Try<Result, Exception> AddVehicle(IVehicle vehicle)
        {
            try
            {
                var result = Validators.Result(vehicle);
                if (result.Any(a => !a.IsValid))
                    return new Result(result);
                return Repository.Store(vehicle);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Try<ModelResult<IVehicle>, Exception> FindVehicleByChassisId(Chassis chassisId)
        {
            try
            {
                if (chassisId == null)
                    throw new ArgumentNullException("O chassis id não pode ser nulo");
                var result = from item in Repository.GetVehicles()
                             where item.ChassisId.ChassisNumber == chassisId.ChassisNumber
                             select item;
                if (!result.Any())
                    return new ModelResult<IVehicle>(new Validation[] { "Vehicle not found for the chassis id provided" });
                return new ModelResult<IVehicle>(result.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Try<Result, Exception> UpdateVehicleRegister(IVehicle vehicle)
        {
            try
            {
                if (!Repository.GetVehicles().Any(a => a.ChassisId.ChassisNumber == vehicle.ChassisId.ChassisNumber))
                    return new Result(new Validation[] { "Vehicle not found for the chassis id provided" });
                return Repository.Update(vehicle);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Try<Result, Exception> DeleteVehicle(IVehicle vehicle, Func<bool> confirmOperation)
        {
            try
            {
                if (!Repository.GetVehicles().Any(a => a.ChassisId.ChassisNumber == vehicle.ChassisId.ChassisNumber))
                    return new Result(new Validation[] { "Vehicle not found for the chassis id provided" });
                if (!confirmOperation.Invoke())
                    return new Result(Enumerable.Empty<Validation>());
                return Repository.Delete(vehicle);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public IEnumerable<IVehicle> ListAllVehicles()
        {
            return Repository.GetVehicles().ToList();
        }
    }
}
