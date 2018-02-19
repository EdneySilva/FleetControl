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
    class EditVehiclePage : Page
    {
        public EditVehiclePage(FleetManager fleetManager) : base(fleetManager)
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
                    UpdateColor(result.Model);
                }
            });
        }

        private void UpdateColor(IVehicle vehicle)
        {
            Console.WriteLine();
            Console.Write("Enter the vehicle color: ");
            var color = Input.ReadLine(exitKey: ConsoleKey.Escape);
            vehicle.Color = color;
            var result = FleetManager.UpdateVehicleRegister(vehicle).HandlerErrors().Result();
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
