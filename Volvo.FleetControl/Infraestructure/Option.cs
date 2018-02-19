using System;
using System.Collections.Generic;
using System.Text;

namespace Volvo.FleetControl.Infraestructure
{
    struct Option
    {
        public string Text { get; set; }
        public Action<Menu> Callback { get; set; }

        public Option(string text, Action<Menu> callback)
        {
            Text = text;
            Callback = callback;
        }
    }
}
