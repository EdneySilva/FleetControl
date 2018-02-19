﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Volvo.FleetControl.Core.Domain.Abstractions;
using Volvo.FleetControl.Core.Infraestructure.Abstractions;

namespace Volvo.FleetControl.Core.Infraestructure
{
    class MemoryRepository : IRepository
    {
        List<IVehicle> Vehicles = new List<IVehicle>();
        
        public IQueryable<IVehicle> GetVehicles()
        {
            return Vehicles.AsQueryable();
        }

        public Result Store(IVehicle vehicle)
        {
            Vehicles.Add(vehicle);
            return new Result(Enumerable.Empty<Validation>());
        }

        public Result Update(IVehicle vehicle)
        {
            var index = Vehicles.IndexOf(vehicle);
            Vehicles[index] = vehicle;
            return new Result(Enumerable.Empty<Validation>());
        }

        public Result Delete(IVehicle vehicle)
        {
            Vehicles.Remove(vehicle);
            return new Result(Enumerable.Empty<Validation>());
        }
    }
}
