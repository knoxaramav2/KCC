using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLangLib;

namespace KCC
{
    public class CliOptions
    {
        private static CliOptions _self;

        private bool _canContinue;

        //Start Switches
        public bool ReadHelpDoc;
        public bool EnableDebugMessages;

        //Function Switches

        //String Options
        public string OutputName;
        public List<string> Libraries;
        public string Src { get; private set; }

        //Level Options
        public int OptimizeLevel;
        public Verbosity VerboseLevel;

        private CliOptions()
        {
            _canContinue = true;

            ReadHelpDoc = false;
            EnableDebugMessages = false;

            Libraries = new List<string>();
            Src = null;

            OptimizeLevel = 0;
            VerboseLevel = Verbosity.Basic;

            OutputName = "out.exe";
        }

        public static CliOptions GetInstance()
        {
            return _self ?? (_self = new CliOptions());
        }

        public void ParseCli(IReadOnlyList<string> args)
        {
            var argSize = args.Count;
            var reporter = ErrorReporter.GetInstance();

            for (var i = 0; i < argSize; ++i)
            {
                var arg = args[i];

                if (arg[1] != '-')
                {
                    for (var j = 1; j < arg.Length; ++j)
                    {
                        switch (arg[j])
                        {
                            case 'h':
                                ReadHelpDoc = true;
                                break;
                            case 'v':
                                var val = DetectOptionValue(arg, j);
                                if (val == null)
                                {
                                    VerboseLevel = Verbosity.Detailed;
                                }
                                else
                                {
                                    switch (val)
                                    {
                                        case "none":
                                        case "silent":
                                            VerboseLevel = Verbosity.None;
                                            break;
                                        case "basic":
                                            VerboseLevel = Verbosity.Basic;
                                            break;
                                        case "detailed":
                                        case "loud":
                                            VerboseLevel = Verbosity.Detailed;
                                            break;
                                        default:
                                            reporter.Add("Unknown verbose level", ErrorCode.Error);
                                            break;
                                    }

                                    j = arg.Length;
                                }
                                break;

                            default:
                                reporter.Add("Unrecognized option '"+arg[j]+"'", ErrorCode.Error);
                                break;
                        }
                    }
                }
                else
                {
                    var value = DetectOptionValue(arg, 0);
                    var statement = arg;

                    if (value != null)
                    {
                        statement = statement.Substring(0, statement.Length-value.Length-1);
                    }

                    switch (statement)
                    {
                        case "--exe":
                            if (value == null)
                            {
                                reporter.Add(statement+": Must supply a value", ErrorCode.Error);
                                _canContinue = false;
                                break;
                            }
                            OutputName = value;              
                            break;
                        case "--intdbg":
                            EnableDebugMessages = true;
                            break;
                        case "--src":
                            if (value == null)
                            {
                                reporter.Add(statement + ": Must supply a value", ErrorCode.Error);
                                _canContinue = false;
                                break;
                            } else if (Src != null)
                            {
                                reporter.Add("Previous source file " + Src + " will be ignored", ErrorCode.Warning);
                            }

                            Src = value;

                            break;
                        default:
                            _canContinue = false;
                            reporter.Add("Unrecognized option '" + statement + "'", ErrorCode.Error);
                            break;
                    }
                }
            }
        }

        public bool IsValid()
        {
            return _canContinue;
        }


        //Detect if current option at 'start' index of 'item' sets a value
        private static string DetectOptionValue(string item, int start)
        {
            var idx = item.IndexOf('=', start);
            return idx == -1 ? null : item.Substring(idx + 1);
        }
    }

    public enum Verbosity
    {
        None,
        Basic,
        Detailed
    }
}
