using CodeTranslator;
using CommonLangLib;
using KCC;
using System;
using System.IO;

namespace Compiler
{
    internal class Launcher
    {
        private static int Main(string[] args)
        {
            KCCEnv.Init();

            //Parse CLI options
            var cliOptions = CliOptions.GetInstance();
            cliOptions.ParseCli(args);
            cliOptions.ReadHelpFile();

            //Initialize
            Debug.Init(cliOptions.EnableDebugMessages);
            var errorReporter = ErrorReporter.GetInstance();
            var pageDistro = PageDistro.GetInstance();

            errorReporter.ValidateAndFlush();

            //TODO replace with ErrorReporter.FatalError?

            if (!cliOptions.IsValid())
            {
                if (cliOptions.InfoMode)
                {
                    return 0;
                }

                if (cliOptions.Src == null)
                {
                    ColorIO.WriteError("Src file not specified");
                } else
                {
                    ColorIO.WriteError("Unable to continue. Exiting.");
                }

                return -1;
            }

            var preProcessor = new PreProcessor.PreProcessor();
            preProcessor.PreCompileProject(cliOptions.Src);

            var translator = new Translator();

            PageInfo pageInfo;
            while ((pageInfo=pageDistro.GetNextPage()) != null)
            {
                translator.Translate(pageInfo.ToString());
            }

    
            if (errorReporter.ValidateAndFlush())
            {
                ColorIO.WriteLineError("Fatal Errors Found: Cannot Continue");
                return -1;
            }

            var converter = new Converterx.Converter();
            converter.LogInternalTranslation();

            if (errorReporter.ValidateAndFlush())
            {
                ColorIO.WriteLineError("Fatal Errors Found: Cannot Continue");
                return -1;
            }

            try
            {
                converter.Build();
            } catch(Exception e)
            {
                errorReporter.Add(e.Message, ErrorCode.PluginNotFound);
                errorReporter.ValidateAndFlush();
                return -1;
            }
            

            return 0;
        }
    }
}
