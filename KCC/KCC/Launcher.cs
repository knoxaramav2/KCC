using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using CommonLangLib;

namespace KCC
{
    internal class Launcher
    {
        private static int Main(string[] args)
        {
            //Parse CLI options
            var cliOptions = new CliOptions(args);

            //Initialize
            Debug.Init(cliOptions.EnableDebugMessages);
            KCCEnv.Init();
    
            //TODO replace with ErrorReporter.FatalError?
            if (!cliOptions.IsValid())
            {
                ColorIO.WriteLineError("Fatal Errors Found: Cannot Continue");
                return -1;
            }

            //Load initial files
            InitFiles(cliOptions);

            return 0;
        }

        private static void InitFiles(CliOptions cliOptions)
        {
            foreach (var file in cliOptions.InputFiles)
            {
                PageDistro.GetInstance().LoadFile(file);
            }
        }
    }
}
