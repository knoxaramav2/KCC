using System;
using CommonLangLib;

namespace Compiler
{
    static class Debug
    {
        private static bool _enabled = false;

        public static void Init(bool enable)
        {
            _enabled = enable;
        }

        public static void PrintDbg(string msg)
        {
            if (!_enabled) { return; }

            ColorIO.WriteLineClr(msg, ConsoleColor.DarkMagenta);
        }
    }
}
