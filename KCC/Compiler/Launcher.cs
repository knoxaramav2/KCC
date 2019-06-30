using CodeTranslator;
using CommonLangLib;
using KCC;

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
                ColorIO.WriteLineError("Fatal Errors Found: Cannot Continue");
                return -1;
            }

            if (cliOptions.Src == null)
            {
                ColorIO.WriteLineError("Source file is either unspecified or unable to be read");
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

            converter.Build();

            return 0;
        }
    }
}
