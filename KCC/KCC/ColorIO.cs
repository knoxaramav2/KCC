using System;
using System.Collections.Generic;
using System.Text;

namespace KCC
{

    static class ColorIO
    {
        public static void WriteLineClr(string msg, ConsoleColor clr)
        {
            Console.ForegroundColor = clr;
            Console.WriteLine(msg);
            Console.ResetColor();
        }

        public static void WriteClr(string msg, ConsoleColor clr)
        {
            Console.ForegroundColor = clr;
            Console.Write(msg);
            Console.ResetColor();
        }

        public static void WriteLineWarning(string msg)
        {
            WriteLineClr(msg, ConsoleColor.Yellow);
        }

        public static void WriteLineError(string msg)
        {
            WriteLineClr(msg, ConsoleColor.Red);
        }

        public static void WriteLineOK(string msg)
        {
            WriteLineClr(msg, ConsoleColor.Green);
        }

        public static void WriteWarning(string msg)
        {
            WriteClr(msg, ConsoleColor.Yellow);
        }

        public static void WriteError(string msg)
        {
            WriteClr(msg, ConsoleColor.Red);
        }

        public static void WriteOK(string msg)
        {
            WriteClr(msg, ConsoleColor.Green);
        }
    }
}
