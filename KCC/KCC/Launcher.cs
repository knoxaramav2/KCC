﻿using System;
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

            //Load initial files
            InitFiles(cliOptions);

            if (errorReporter.ValidateAndFlush()){return -1;}

            //Begin parsing files
            while (pageDistro.GetNextPage() != null)
            {
                
            }

            return 0;
        }

        private static void InitFiles(CliOptions cliOptions)
        {
            var pageDistro = PageDistro.GetInstance();

            foreach (var file in cliOptions.InputFiles)
            {
                pageDistro.LoadFile(file);
            }
        }
    }
}
