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
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = false,
                }
            };

            process.EnableRaisingEvents = true;

            string result = "";
            string err = "";

            process.Start();

            while (!process.StandardOutput.EndOfStream)
            {
                result += process.StandardOutput.ReadLine() + "\r\n";
            }

            while (!process.StandardError.EndOfStream)
            {
                err += process.StandardError.ReadLine();
            }

            CommonLangLib.Debug.PrintDbg(result);

            return result;
        }
    }
}
