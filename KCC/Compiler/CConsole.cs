﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class CConsole
    {
        public static void PrintC(string msg, ConsoleColor fg, ConsoleColor bg = ConsoleColor.Black)
        {
            Console.ForegroundColor = fg;
            Console.BackgroundColor = bg;
            Console.Write(msg);
            Console.ResetColor();
        }

        public static void PrintlC(string msg, ConsoleColor fg, ConsoleColor bg = ConsoleColor.Black)
        {
            PrintC(msg + Environment.NewLine, fg, bg);
        }
    }
}
