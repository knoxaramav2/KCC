using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLangLib
{
    public static class Debug
    {
        private static bool _enabled = false;

        public static bool DebugEnabled()
        {
            return _enabled;
        }

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
