using System;
using System.Collections.Generic;
using System.Text;
using Volvo.FleetControl.Core.Domain.Abstractions;

namespace Volvo.FleetControl.Core.Infraestructure.Abstractions
{
    public interface IPreferencesConfig
    {
        IPreferencesConfig Paint(string color);

        IVehicle Apply(IVehicle vehicle);
    }
}
