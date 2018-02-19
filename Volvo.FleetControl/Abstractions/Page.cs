using System;
using System.Collections.Generic;
using System.Text;
using Volvo.FleetControl.Core.Domain.Serivces;

namespace Volvo.FleetControl.Abstractions
{
    abstract class Page
    {
        public Page(FleetManager fleetManager)
        {
            FleetManager = fleetManager;
        }
        protected FleetManager FleetManager { get; set; }
        public abstract void Display(string parentMenu);
    }
}
