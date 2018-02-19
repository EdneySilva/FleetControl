using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Volvo.FleetControl.Abstractions;

namespace Volvo.FleetControl.Infraestructure
{
    class Menu
    {
        bool exit = false;
        Dictionary<ConsoleKey, Option> options = new Dictionary<ConsoleKey, Option>();
        int currentKey = (int)ConsoleKey.NumPad1;
        int currentAlternativeKey = (int)ConsoleKey.D1;
        public Menu Add(string text, Action<Menu> callback)
        {
            options.Add((ConsoleKey)currentKey, new Option(text, callback));
            options.Add((ConsoleKey)currentAlternativeKey, new Option(text, callback));
            currentKey += 1;
            currentAlternativeKey += 1;
            return this;
        }

        public void Display()
        {
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Main menu");
                Console.WriteLine("------------------------------------------------");
                int index = 1;
                foreach (var item in options.Select(s => s.Value.Text).Distinct())
                {
                    Console.WriteLine($"{index++} - " + item);
                }
                Console.WriteLine();
                Console.Write("Choose an option: ");
                var key = Console.ReadKey().Key;
                if (!options.ContainsKey(key))
                {
                    Console.WriteLine("Invalid option! Press enter to continue");
                    Console.ReadLine();
                    continue;
                }
                options[key].Callback(this);
            }
        }

        public void NavigateTo(Page page)
        {
            page.Display("Main menu > ");
        }

        public void Exit() => exit = true;
        
    }    
}
