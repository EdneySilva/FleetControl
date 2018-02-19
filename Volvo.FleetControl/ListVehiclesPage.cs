using System;
using System.Collections.Generic;
using System.Text;
using Volvo.FleetControl.Abstractions;
using Volvo.FleetControl.Core.Domain;
using Volvo.FleetControl.Core.Domain.Serivces;

namespace Volvo.FleetControl
{
    class ListVehiclesPage : Page
    {
        public ListVehiclesPage(FleetManager fleet)
            : base(fleet)
        {
        }

        public override void Display(string parentMenu)
        {            
            while (true)
            {
                Console.Clear();
                Console.WriteLine(parentMenu + " List all vehicle");
                Console.WriteLine();
                Console.WriteLine("Press [esc] any time to go back");
                Console.Write("Vehicle".PadRight(10) + "\t");
                Console.Write("Chassis Number".PadRight(16) + "\t");
                Console.Write("Chassis Series".PadRight(16) + "\t");
                Console.Write("Number of Passagers".PadRight(21) + "\t");
                Console.Write("Color".PadRight(10));
                Console.WriteLine();
                //Console.WriteLine("Vehicle\t\t\t\tChassis Number\t\t\t\tChassis Series\t\t\t\tNumber of passagers\t\t\t\tColor");                
                foreach (var item in FleetManager.ListAllVehicles())
                {
                    Console.Write(item.Type.ToString().PadRight(10) + "\t");
                    Console.Write(item.ChassisId.ChassisNumber.ToString().PadRight(16) + "\t");
                    Console.Write(item.ChassisId.ChassisSeries.PadRight(16) + "\t");
                    Console.Write(item.NumberOfPassagers.ToString().PadRight(21) + "\t");
                    Console.Write(item.Color.PadRight(10) + "\t");
                    Console.WriteLine();
                }
                var key = Console.ReadKey().Key;
                if (key == ConsoleKey.Escape)
                    break;
            }
        }

        private string Type(VehicleType vehicle)
        {
            return vehicle.ToString();
        }
    }
}
