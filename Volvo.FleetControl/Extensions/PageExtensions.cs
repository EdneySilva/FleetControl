using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Volvo.FleetControl.Abstractions;
using Volvo.FleetControl.Core;
using Volvo.FleetControl.Infraestructure;

namespace Volvo.FleetControl.Extensions
{
    static class PageExtensions
    {
        public static void MonitorExitKey(this Page page, Action executionContext)
        {
            try
            {
                executionContext();
            }
            catch (InputExitPressedException)
            {
                return;
            }
        }

        public static void ShowSuccessMessage(this Page page, string message = "Operation completed with success")
        {
            Console.WriteLine(message);
            Thread.Sleep(3000);
        }

        public static void ShowValidationErrors(this Page page, IEnumerable<Validation> validations)
        {
            foreach (var item in validations.Where(w => !w.IsValid))
            {
                Console.WriteLine(item.Error);
            }
            Thread.Sleep(3000);
        }
    }
}
