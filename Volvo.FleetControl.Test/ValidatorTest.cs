
using System;
using System.Linq;
using Volvo.FleetControl.Core.Domain.Abstractions;
using Volvo.FleetControl.Core.Domain.Serivces;
using Volvo.FleetControl.Test.Mokes;
using Xunit;

namespace Volvo.FleetControl.Test
{
    public class ValidatorTest
    {
        VehicleValidatorCollection Collection = new VehicleValidatorCollection();
        public ValidatorTest()
        {
            Collection = Collection.ChassisIsRequired().CheckNumberOfPassagers().ColorIsRequired();
        }

        [Fact]
        public void When_add_vehicle_without_chassis_return_fail()
        {
            var result = Collection.Result(new CustomVehicle()
            {
                Color = "Red",
                ChassisId = null,
                Type = Core.Domain.VehicleType.Bus,
                NumberOfPassagers = 42
            });
            var error = result.FirstOrDefault();
            Assert.Contains("chassis id is required", error.Error);
        }

        [Fact]
        public void When_add_vehicle_without_color_return_fail()
        {
            var result = Collection.Result(new CustomVehicle()
            {
                Color = null,
                ChassisId = new Core.Domain.Chassis() { ChassisNumber = 123, ChassisSeries = "abc123" },
                Type = Core.Domain.VehicleType.Bus,
                NumberOfPassagers = 42
            });
            var error = result.FirstOrDefault();
            Assert.Contains("color is required", error.Error);
        }

        [Fact]
        public void When_add_vehicle_with_invalid_number_of_passagers_return_fail()
        {
            var result = Collection.Result(new CustomVehicle()
            {
                Color = "Red",
                ChassisId = new Core.Domain.Chassis() { ChassisNumber = 123, ChassisSeries = "abc123" },
                Type = Core.Domain.VehicleType.Bus,
                NumberOfPassagers = 100
            });
            var error = result.FirstOrDefault();
            Assert.Contains("number of passagers", error.Error);
        }

        [Fact]
        public void When_add_custom_validator_should_be_executed()
        {
            bool executed = false;
            Collection = Collection.AddCustomValidator((vehicle) =>
            {
                executed = true;
                return Core.Validation.Success;
            });
            var result = Collection.Result(new CustomVehicle()
            {
                Color = "Red",
                ChassisId = new Core.Domain.Chassis() { ChassisNumber = 123, ChassisSeries = "abc123" },
                Type = Core.Domain.VehicleType.Bus,
                NumberOfPassagers = 42
            });
            Assert.True(executed, "The custom validator was not executed");
        }
    }
}
