using System;
using System.Collections.Generic;
using System.Text;
using Volvo.FleetControl.Abstractions;
using Volvo.FleetControl.Core.Domain.Abstractions;
using Volvo.FleetControl.Core.Domain.Serivces;
using Volvo.FleetControl.Extensions;
using Volvo.FleetControl.Infraestructure;

namespace Volvo.FleetControl
{
    class VehicleDetailsPage : Page
    {
        public VehicleDetailsPage(FleetManager fleetManager) : base(fleetManager)
        {
        }

        public override void Display(string parentMenu)
        {
            this.MonitorExitKey(() =>
            {
                while (true)
                {
                    DefaultMessages(parentMenu);
                    Console.Write("Enter the chassis number: ");
                    var chassisNumber = Input.ReadLine(ConsoleKey.Escape);
                    var result = FleetManager
                        .FindVehicleByChassisId(new Core.Domain.Chassis() { ChassisNumber = chassisNumber.ToUint() })
                        .HandlerErrors()
                        .Result();
                    DefaultMessages(parentMenu);
                    if (result.IsFail)
                    {
                        Console.WriteLine("Some errors occured");
                        this.ShowValidationErrors(result.ValidationResult);
                        continue;
                    }
                    PrintVehicleData(result.Model);
                }
            });
        }

        private void PrintVehicleData(IVehicle vehicle)
        {
            Console.WriteLine("Vehicle Type:".PadRight(20, '\0') + $"\t{vehicle.Type.ToString()}");
            Console.WriteLine("Chassis Number:".PadRight(20) + $"\t{vehicle.ChassisId.ChassisNumber}");
            Console.WriteLine("Chassis Series:".PadRight(20) + $"\t{vehicle.ChassisId.ChassisSeries}");
            Console.WriteLine($"Number of passagers:\t{vehicle.NumberOfPassagers}");
            Console.WriteLine("Color:".PadRight(20) + $"\t{vehicle.Color}");
            Console.WriteLine("Press [enter] to refresh");
            Console.ReadKey();
        }

        private void DefaultMessages(string parentMenu)
        {
            Console.Clear();
            Console.WriteLine(parentMenu + " Vehicle details");
            Console.WriteLine();
            Console.WriteLine("Press [esc] any time to go back!");
            Console.WriteLine();
        }
    }
}
