using System;
using System.Collections.Generic;
using System.Text;

namespace Volvo.FleetControl.Infraestructure
{
    class Input
    {
        public static string ReadLine()
        {
            string value = "";
            while (true)
            {
                var keyReaded = Console.ReadKey();
                if (keyReaded.Key == ConsoleKey.Enter)
                    break;
                else if (keyReaded.Key == ConsoleKey.Backspace)
                    continue;
                else if (keyReaded.Key == ConsoleKey.Backspace)
                {
                    value = value.Length > 0 ? value.Substring(0, value.Length - 1) : string.Empty;
                    Backspace();
                }
                else
                    value += keyReaded.KeyChar;
            }
            return value;
        }

        public static string ReadLine(ConsoleKey exitKey)
        {
            string value = "";
            while (true)
            {
                var keyReaded = Console.ReadKey();
                if (keyReaded.Key == ConsoleKey.Enter)
                    break;
                else if (exitKey == keyReaded.Key)
                    throw new InputExitPressedException();
                else if (keyReaded.Key == ConsoleKey.Backspace)
                {
                    value = value.Length > 0 ? value.Substring(0, value.Length - 1) : string.Empty;
                    Backspace();
                }
                else
                    value += keyReaded.KeyChar;
            }
            return value;
        }

        private static void Backspace()
        {
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop);
            Console.Write(" ");
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
        }

    }
}
