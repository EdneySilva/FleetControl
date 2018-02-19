using Volvo.FleetControl.Core.Domain.Abstractions;
using Volvo.FleetControl.Core.Infraestructure.Abstractions;

namespace Volvo.FleetControl.Core.Domain.Serivces
{
    delegate IVehicle VehicleProvider(Chassis chassis);

    public delegate IPreferencesConfig PreferencesProvider(IPreferencesConfig vehiclePreferences);

    public delegate Validation VehicleValiator(IVehicle vehicle);
}