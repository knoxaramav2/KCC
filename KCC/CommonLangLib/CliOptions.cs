using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CommonLangLib;

namespace KCC
{
    public class CliOptions
    {
        private static CliOptions _self;
        public static AutoArch Arch { get; internal set; }

        private bool _canContinue;

        //Start Switches
        private bool ReadHelpDoc { get; set; }
        public bool EnableDebugMessages;
        public bool OutputInternals { get; internal set; }

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
            OutputInternals = false;

            Libraries = new List<string>();
            Src = null;

            OptimizeLevel = 0;
            VerboseLevel = Verbosity.Basic;

            OutputName = "out";

            Arch = new AutoArch();
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
                            case 'd':
                                EnableDebugMessages = true;
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
                                reporter.Add("Previous source file " + Src + " will be ignored", ErrorCode.FileRedundant);
                            }

                            var ext = Path.GetExtension(value);
                            if (ext.ToLower() != ".kcc")
                            {
                                reporter.Add($"Warning: {value} may not be a recognized file", ErrorCode.BadFileExtension);
                            }

                            Src = value;
                            OutputName = $@"{Path.ChangeExtension(Src, "")}";
                            OutputName = OutputName.Remove(OutputName.Length - 1, 1);

                            break;
                        case "--pintern":
                            OutputInternals = true;
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

        public void ReadHelpFile()
        {
            if (!ReadHelpDoc) return;

            var lines = File.ReadAllLines(KCCEnv.ExeUri+"//cli.txt");
            foreach (var line in lines)
            {
                ColorIO.WriteLineClr(line, ConsoleColor.Yellow);
            }

            Console.Out.Flush();
        }

        public void Refresh(string [] args)
        {
            _self = new CliOptions();
            ParseCli(args);
        }
    }

    public enum Verbosity
    {
        None,
        Basic,
        Detailed
    }
}
