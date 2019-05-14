using CommonLangLib;
using KCC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace UnitTests
{
    class Utils
    {
        public static string CallSystem(string command, string args)
        {
            string shell, exec;

            if (CliOptions.Arch.OS == OS.Linux)
            {
                shell = "/bin/bash";
                exec = "-c";
            }
            else
            {
                shell = "CMD.exe";
                exec = "/C";
            }

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = shell,
                    Arguments = $"{exec} \"{command} {args}",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            CommonLangLib.Debug.PrintDbg(result);

            return result;
        }
    }
}
