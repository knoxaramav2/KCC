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
            var cliOptions = CliOptions.GetInstance();
            cliOptions.ParseCli(args);

            //Initialize
            Debug.Init(cliOptions.EnableDebugMessages);
            KCCEnv.Init();

            var errorReporter = ErrorReporter.GetInstance();
            var pageDistro = PageDistro.GetInstance();

            errorReporter.ValidateAndFlush();

            //TODO replace with ErrorReporter.FatalError?
            if (!cliOptions.IsValid())
            {
                ColorIO.WriteLineError("Fatal Errors Found: Cannot Continue");
                return -1;
            }

            if (cliOptions.Src == null)
            {
                return -1;
            }

            var preProcessor = new PreProcessor.PreProcessor();
            preProcessor.PreCompileProject(cliOptions.Src);

            return 0;
        }
    }
}
