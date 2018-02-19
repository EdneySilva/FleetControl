using System;
using System.Collections.Generic;
using System.Text;
using Volvo.FleetControl.Core.Domain.Abstractions;
using Volvo.FleetControl.Core.Infraestructure.Abstractions;

namespace Volvo.FleetControl.Core.Infraestructure
{
    public class DefaultPreferencesConfig : IPreferencesConfig
    {
        private Queue<Func<IVehicle, IVehicle>> Configuration = new Queue<Func<IVehicle, IVehicle>>();

        public IPreferencesConfig Paint(string color)
        {
            Configuration.Enqueue((vehicle) =>
            {
                vehicle.Color = color;
                return vehicle;
            });
            return this;
        }

        public IVehicle Apply(IVehicle vehicle)
        {
            while (Configuration.Count > 0)
                vehicle = Configuration.Dequeue().Invoke(vehicle);

            return vehicle;
        }
    }
}
