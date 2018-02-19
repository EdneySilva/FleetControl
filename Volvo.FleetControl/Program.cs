using System;
using System.Collections.Generic;
using Volvo.FleetControl.Abstractions;
using Volvo.FleetControl.Core.Domain.Serivces;
using Volvo.FleetControl.Infraestructure;

namespace Volvo.FleetControl
{
    class Program
    {
        static void Main(string[] args)
        {
            RepositoryService.RegisterDefaultRepository();
            var validators = new VehicleValidatorCollection().ChassisIsRequired().CheckNumberOfPassagers().ColorIsRequired();
            FleetManager fleetManager = new FleetManager(validators);
            Menu menu = new Menu();
            menu.Add("Insert a new vehicle", (context) => { context.NavigateTo(new InsertVehiclePage(fleetManager)); });
            menu.Add("Edit an existing vehicle", (context) => { context.NavigateTo(new EditVehiclePage(fleetManager)); });
            menu.Add("Delete an existing vehicle", (context) => { context.NavigateTo(new DeleteVehiclePage(fleetManager)); });
            menu.Add("List all vehicles", (context) => { context.NavigateTo(new ListVehiclesPage(fleetManager)); });
            menu.Add("Find vehicle by chassis id", (context) => { context.NavigateTo(new VehicleDetailsPage(fleetManager)); });
            menu.Add("Exit", (context) => { context.Exit(); });
            menu.Display();
        }
    }

}
