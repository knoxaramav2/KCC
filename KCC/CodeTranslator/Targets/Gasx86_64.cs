using System;
using System.Diagnostics;
using System.IO;
using CommonLangLib;
using KCC;

namespace CodeTranslator.Targets
{
    public class Gasx86_64 : IArchAgent
    {
        private InstDeclController _controller;
        private CliOptions _cli;
        private string _nl;

        private int byteWidth; //For bools, chars
        private int shortWidth; //Short integers
        private int intWidth; //integers, floats
        private int longWidth; //longs, doubles
        private int longlongWidth; //long longs, long double

        int bitMode;

        public void Init(InstDeclController controller)
        {
            _controller = controller;
            _cli = CliOptions.GetInstance();
            _nl = Environment.NewLine;

            if (CliOptions.Arch.Arch == System.Reflection.ProcessorArchitecture.Amd64)
            {
                byteWidth = 1;
                shortWidth = 2;
                intWidth = 4;
                longWidth = 8;
                longlongWidth = 16;

                bitMode = 64;
            } else if (CliOptions.Arch.Arch == System.Reflection.ProcessorArchitecture.X86)
            {
                byteWidth = 1;
                shortWidth = 2;
                intWidth = 4;
                longWidth = 8;
                longlongWidth = 16;

                bitMode = 32;
            }
        }

        public string GetHeader()
        {
            return $"\t.file \"{_cli.Src}\"" + _nl +
                   $"\t.def __main; .scl 2; .type {bitMode}; .endef" + _nl;
        }

        public string GetGlobals()
        {
            //TODO Get other globals besides string constants
            string ret;

            return "";
        }

        private string GetDirectiveString(Directives d, string info)
        {
            switch (d)
            {
                case Directives.Lc: return $".LC{info}:";
                case Directives.Ascii: return $".ascii \"{info}\\0\"";
                case Directives.Text: return ".text";

                default: return null;
            }
        }

        public string GetConstData()
        {
            var ret = "\t.section .rdata, \"dr\"" + _nl;

            var m = InstDeclController.Meta;

            foreach (var d in m.GetDirectives())
            {
                var dr = GetDirectiveString(d.Directive, d.Info) + _nl;
                foreach (var n in d.Nested)
                {
                    dr += $"    {GetDirectiveString(n.Directive, n.Info)}"
                        + _nl;
                }

                ret += dr;

                if (d.Nested.Count == 0)
                {
                    ret += _nl;
                }
            }

            ret += "\t.text" + _nl +
                   "\t.globl main" + _nl +
                   $"\t.def main; .scl 2; .type {bitMode}; .endef"+_nl;

            CommonLangLib.Debug.PrintDbg($"{ret}");

            return ret;
        }

        public string GetFunctionDefs()
        {
            var fncs = _controller.FindAll();
            var ret = "";
            foreach (var fnc in fncs)
            {
                if (fnc.BodyType != BodyType.Function) continue;
                ret += fnc.Id + ":" + _nl;
                //Create function prologue
                ret += "\tpushq\t\t%rbp";
            }

            return ret;
        }

        public string FormatInstruction(InstOp opcode, string arg0, string arg1, string spcl1, string spcl2, OpModifier mode)
        {
            var ret = "";

            switch (opcode)
            {
                case InstOp.Print:
                    ret = $"    leaq .LC{arg0}(%{AsmRef.GetInstructionPointer(BitCntr.B64)}), %{AsmRef.GetCounter(BitCntr.B64)}" + _nl +
                          $"    call printf";
                    break;
                case InstOp.Exit:
                    ret = $"    movl %{arg0}, %{AsmRef.GetDestination(BitCntr.B32)}" + _nl +
                           "    call exit";
                    break;
            }

            return ret;
        }

        public bool InvokeLocalAssembler(string asmPath)
        {
            //TODO Watch for required libraries
            string shell, exec;
            string libs = "";

            if (CliOptions.Arch.OS == OS.Linux)
            {
                shell = "/bin/bash";
                exec = "-c";
            } else
            {
                shell = "CMD.exe";
                exec = "/C";
            }

            var process = new Process
            {
                StartInfo = new ProcessStartInfo {
                    FileName = shell,
                    Arguments = $"{exec} \"gcc {asmPath} {libs} -o " +
                    $"{KCCEnv.BaseUri}/{_cli.OutputName}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.EnableRaisingEvents = true;

            string stdOut="", stdErr="";

            process.Start();

            while (!process.StandardOutput.EndOfStream)
            {
                stdOut += process.StandardOutput.ReadLine() + _nl;
            }

            while (!process.StandardError.EndOfStream)
            {
                stdErr += process.StandardError.ReadLine() + _nl;
            }

            CommonLangLib.Debug.PrintDbg(stdOut);
            CommonLangLib.Debug.PrintDbg(stdErr);

            if (stdErr != "")
            {
                return false;
            }

            return true;
        }
    }
}