using System;
using System.Collections.Generic;
using System.Text;

namespace KCC
{
    class CliOptions
    {

        private bool canContinue;

        //Start Switches
        public bool ReadHelpDoc;

        //Function Switches

        //String Options
        public string OutputName;
        public List<string> InputFiles;

        //Level Options
        public bool OptimizeLevel;

        public CliOptions(string[] args)
        {
            ReadHelpDoc = false;
            InputFiles = new List<string>();
            OptimizeLevel = false;

            ParseCli(args);
        }

        public void ParseCli(IReadOnlyList<string> args)
        {
            var inputActive = true;
            var argSize = args.Count;

            for (var i = 0; i < argSize; ++i)
            {
                var arg = args[i];

                if (inputActive)
                {
                    if (arg[0] == '-')
                    {
                        inputActive = false;
                    }
                    else
                    {
                        InputFiles.Add(arg);
                        continue;
                    }
                }

                if (arg[1] != '-')
                {
                    for (var j = 1; j < arg.Length; ++j)
                    {
                        switch (arg[j])
                        {
                            case 'h':
                                ReadHelpDoc = true;
                                break;
                        }
                    }
                }
                else
                {
                    var argVal = arg.Split('=');

                    switch (argVal[0])
                    {
                        case "--exe":
                            if (OutputName != null || argVal.Length != 2)
                            {
                                ColorIO.WriteLineError("Must set exactly one value");
                                canContinue = false;
                            }
                            else
                            {
                                OutputName = argVal[0];
                            }
                            break;
                    }
                }
            }

            if (OutputName == null ||
                !canContinue)
            {

            }
        }

        public bool IsValid()
        {
            return canContinue;
        }
    }
}
