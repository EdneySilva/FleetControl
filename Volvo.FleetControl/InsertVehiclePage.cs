using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Volvo.FleetControl.Abstractions;
using Volvo.FleetControl.Core.Domain;
using Volvo.FleetControl.Core.Domain.Abstractions;
using Volvo.FleetControl.Core.Domain.Serivces;
using Volvo.FleetControl.Extensions;
using Volvo.FleetControl.Infraestructure;

namespace Volvo.FleetControl
{
    class InsertVehiclePage : Page
    {
        public InsertVehiclePage(FleetManager fleet)
            : base(fleet)
        {
        }

        public override void Display(string parentMenu)
        {
            this.MonitorExitKey(() =>
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine(parentMenu + " Insert Vehicle");
                    Console.WriteLine();
                    Console.WriteLine("Press [esc] any time to go back!");
                    Chassis chassis = CreateChassis();
                    var vehicleMaker = CreateVehicle();
                    var color = CreateColor();
                    var vehicle = vehicleMaker.Invoke(new VehicleMaker(), chassis, (pref) => pref.Paint(color));
                    Console.WriteLine();
                    Console.WriteLine("Press [enter] to save the vehicle");
                    Console.ReadKey();
                    var result = FleetManager.AddVehicle(vehicle).HandlerErrors((ex) =>
                    {
                        Console.WriteLine(ex.Message);
                    }).Result();
                    if (result.IsSuccess)
                    {
                        this.ShowSuccessMessage();
                        break;
                    }
                    this.ShowValidationErrors(result.ValidationResult);
                }
            });
        }

        private Chassis CreateChassis()
        {
            uint conversor = 0;
            Chassis chassis = new Chassis();
            Console.WriteLine();
            Console.Write("Enter the chassis number: ");
            string value = Input.ReadLine(ConsoleKey.Escape);
            uint.TryParse(value, out conversor);
            chassis.ChassisNumber = conversor;
            Console.WriteLine();
            Console.Write("Enter the chassis series: ");
            var series = Input.ReadLine(ConsoleKey.Escape);
            chassis.ChassisSeries = series;
            return chassis;
        }

        Func<VehicleMaker, Chassis, PreferencesProvider, IVehicle> CreateVehicle()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Choose the vehicle type: ");
                Console.WriteLine("1 - to Bus");
                Console.WriteLine("2 - to Car");
                Console.WriteLine("3 - to Truck");
                Console.Write("Choose: ");
                var item = Input.ReadLine(ConsoleKey.Escape);
                switch (item)
                {
                    case "1":
                        return (volvoMaker, chassis, preferences) => volvoMaker.BuildBus(chassis, preferences);
                    case "2":
                        return (volvoMaker, chassis, preferences) => volvoMaker.BuildCar(chassis, preferences);
                    case "3":
                        return (volvoMaker, chassis, preferences) => volvoMaker.BuildTruck(chassis, preferences);
                    default:
                        Console.WriteLine("Invalid option. Try again!");
                        break;
                }

            }

        }

        private string CreateColor()
        {
            Console.WriteLine();
            Console.Write("Enter the color: ");
            return Input.ReadLine(ConsoleKey.Escape);
        }
    }
}
