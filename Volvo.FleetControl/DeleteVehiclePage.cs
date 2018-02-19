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
    class DeleteVehiclePage : Page
    {
        public DeleteVehiclePage(FleetManager fleetManager) : base(fleetManager)
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
                    DeleteVehicle(result.Model);
                }
            });
        }

        private void DeleteVehicle(IVehicle vehicle)
        {
            var result = FleetManager.DeleteVehicle(vehicle, () =>
            {
                Console.WriteLine("Do you want to delete this vehicle?");
                Console.WriteLine("Enter [y] to yes or any other key to no");
                return Console.ReadKey().Key == ConsoleKey.Y;
            }).HandlerErrors().Result();
            Console.WriteLine();
            if (result.IsFail)
            {
                Console.WriteLine("Some errors occured");
                this.ShowValidationErrors(result.ValidationResult);
            }
            else
                this.ShowSuccessMessage();
        }

        private void DefaultMessages(string parentMenu)
        {
            Console.Clear();
            Console.WriteLine(parentMenu + " Delete Vehicle");
            Console.WriteLine();
            Console.WriteLine("Press [esc] any time to go back!");
            Console.WriteLine();
        }
    }
}
