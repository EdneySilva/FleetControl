using System;
using System.Linq;
using Volvo.FleetControl.Core.Domain.Abstractions;
using Volvo.FleetControl.Core.Domain.Serivces;
using Volvo.FleetControl.Core.Extensions;
using Volvo.FleetControl.Core.Infraestructure.Abstractions;
using Volvo.FleetControl.Test.Mokes;
using Volvo.FleetControl.Test.Extensions;
using Xunit;

namespace Volvo.FleetControl.Test
{
    public class FleetTest
    {
        FleetManager fleetManager = null;
        IRepository RepositoryInstance = new TestRepository().AddMokedData();
        public FleetTest()
        {
            AppDomain.CurrentDomain.SetRepositoryFactory(() => RepositoryInstance);
            var validators = new VehicleValidatorCollection().ChassisIsRequired().CheckNumberOfPassagers().ColorIsRequired();
            fleetManager = new FleetManager(validators);
        }

        [Fact]
        public void When_add_vehicle_without_chassis_return_fail()
        {
            VehicleMaker volvoMaker = new VehicleMaker();
            //IVehicle vehicle = volvoMaker.BuildBus(null, (preferences) => preferences.Paint("Red"));
            IVehicle vehicle = new CustomVehicle()
            {
                Type = Core.Domain.VehicleType.Truck,
                NumberOfPassagers = 1,
                ChassisId = null
            };
            var result = fleetManager.AddVehicle(vehicle).HandlerErrors().Result();
            Assert.True(result.IsFail, "The operation didn't fail for a invalid vehicle!");
            var validationResult = result.ValidationResult.FirstOrDefault();
            Assert.Contains("chassis id", validationResult.Error);
        }

        [Fact]
        public void When_add_a_vehicle_with_a_chassis_from_another_vehicle_return_fail()
        {
            var vehicle = RepositoryInstance.GetVehicles().SelectRandomItem();
            var result = fleetManager.AddVehicle(vehicle).HandlerErrors().Result();
            Assert.True(result.IsFail, "The operation didn't fail for a invalid vehicle!");
            var validationResult = result.ValidationResult.FirstOrDefault();
            Assert.Contains("There are a vehicle stored with the same chassis id!", validationResult.Error);            
        }

        [Fact]
        public void When_add_a_new_valid_vehicle_return_success()
        {
            IVehicle vehicle = new CustomVehicle()
            {
                Type = Core.Domain.VehicleType.Truck,
                NumberOfPassagers = 1,
                Color = "BLUE",
                ChassisId = new Core.Domain.Chassis() { ChassisNumber = 100, ChassisSeries = Guid.NewGuid().ToString() }
            };
            var result = fleetManager.AddVehicle(vehicle).HandlerErrors().Result();
            Assert.True(result.IsSuccess, "The operation failed for a valid vehicle!");
            Assert.True(result.ValidationResult.All(a => a.IsValid), "There are fail validation results for a valid vehicle!");
        }

        [Fact]
        public void When_not_find_a_vehicle_by_a_chassis_id_return_a_message_error()
        {
            var result = fleetManager.FindVehicleByChassisId(
                    new Core.Domain.Chassis() {
                        ChassisNumber = 9999,
                        ChassisSeries = "****"
                    }
                ).HandlerErrors((ex) => { }).Result();
            Assert.True(result.IsFail, "The operations didn't fail for an invalid operation!");
            Assert.True(result.ValidationResult.Any(a => !a.IsValid), "No validation generated for an invalid operation!");
        }

        [Fact]
        public void When_find_a_vehicle_by_a_chassis_id_return_the_model_value()
        {
            var randomVehicle = RepositoryInstance.GetVehicles().SelectRandomItem();
            var result = fleetManager.FindVehicleByChassisId(randomVehicle.ChassisId).HandlerErrors().Result();
            Assert.True(result.IsSuccess, "The operation failed for a valid operation!");
            Assert.True(randomVehicle.Equals(result.Model), "The same vehicle was not found when provided the same chassis id!");
        }

        [Fact]
        public void When_update_a_vehicle_not_stored_return_fail()
        {
            IVehicle vehicle = new CustomVehicle()
            {
                Type = Core.Domain.VehicleType.Truck,
                NumberOfPassagers = 1,
                Color = "BLUE",
                ChassisId = new Core.Domain.Chassis() { ChassisNumber = 9999, ChassisSeries = Guid.NewGuid().ToString() }
            };
            var result = fleetManager.UpdateVehicleRegister(vehicle).HandlerErrors().Result();
            Assert.True(result.IsFail, "The operation not failed for an invalid vehicle!");
            Assert.True(result.ValidationResult.Any(a => !a.IsValid), "There aren't fail validation results for an invalid vehicle!");
        }

        [Fact]
        public void When_update_a_valid_vehicle_stored_return_success()
        {
            var validVehicle = RepositoryInstance.GetVehicles().SelectRandomItem();
            var result = fleetManager.UpdateVehicleRegister(validVehicle).HandlerErrors().Result();
            Assert.True(result.IsSuccess, "The operation failed for a valid vehicle!");
            Assert.True(result.ValidationResult.All(a => a.IsValid), "There are fail validation results for a valid vehicle!");
        }

        [Fact]
        public void When_delete_a_vehicle_not_stored_return_fail()
        {
            IVehicle vehicle = new CustomVehicle()
            {
                Type = Core.Domain.VehicleType.Truck,
                NumberOfPassagers = 1,
                Color = "BLUE",
                ChassisId = new Core.Domain.Chassis() { ChassisNumber = 9999, ChassisSeries = Guid.NewGuid().ToString() }
            };
            var result = fleetManager.DeleteVehicle(vehicle, () => true).HandlerErrors().Result();
            Assert.True(result.IsFail, "The operation not failed for an invalid vehicle!");
            Assert.True(result.ValidationResult.Any(a => !a.IsValid), "There aren't fail validation results for an invalid vehicle!");
        }

        [Fact]
        public void When_delete_a_valid_vehicle_stored_return_success()
        {
            var validVehicle = RepositoryInstance.GetVehicles().SelectRandomItem();
            var result = fleetManager.DeleteVehicle(validVehicle, () => true).HandlerErrors().Result();
            Assert.True(result.IsSuccess, "The operation failed for a valid vehicle!");
            Assert.True(result.ValidationResult.All(a => a.IsValid), "There are fail validation results for a valid vehicle!");
        }

        [Fact]
        public void When_not_confirm_delete_operation_the_vehicle_it_does_not_removed_from_repository()
        {
            var validVehicle = RepositoryInstance.GetVehicles().SelectRandomItem();
            var result = fleetManager.DeleteVehicle(validVehicle, () => false).HandlerErrors().Result();
            var vehicle = fleetManager.FindVehicleByChassisId(validVehicle.ChassisId);
            Assert.True(result.IsSuccess, "The operation failed for a valid vehicle!");
            Assert.True(result.ValidationResult.All(a => a.IsValid), "There are fail validation results for a valid vehicle!");
        }
    }
}
