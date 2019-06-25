using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLangLib
{
    class PrintUtils
    {
        private static VERBOSE_LEVEL _verboseLevel = VERBOSE_LEVEL.Low;

        public static void Init(VERBOSE_LEVEL verboseLevel)
        {
            _verboseLevel = verboseLevel;
        }

        public static void PrintVerbose(string msg, string shortMessage, string longMessage)
        {
            switch (_verboseLevel)
            {
                case VERBOSE_LEVEL.Low:
                    break;
                case VERBOSE_LEVEL.Mid: ColorIO.WriteLineClr(shortMessage, ConsoleColor.Black);
                    break;
                case VERBOSE_LEVEL.High: ColorIO.WriteLineClr(shortMessage, ConsoleColor.Black);
                    break;
            }
        }
    }

    enum VERBOSE_LEVEL
    {
        Low,
        Mid,
        High
    }
}
