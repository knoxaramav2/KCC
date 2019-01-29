using System;
using System.Collections.Generic;
using System.Linq;

namespace KCC
{
    internal class Launcher
    {
        static int Main(string[] args)
        {
            var cliOptions = new CliOptions(args);
            
            if (!cliOptions.IsValid())
            {
                ColorIO.WriteLineError("Fatal Errors Found: Cannot Continue");
                return -1;
            }

            return 0;
        }
    }
}
