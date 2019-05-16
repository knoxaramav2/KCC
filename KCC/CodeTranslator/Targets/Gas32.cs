using System;
using System.Diagnostics;
using System.IO;
using CommonLangLib;
using KCC;

namespace CodeTranslator.Targets
{
    public class Gas32 : IArchAgent
    {
        private InstDeclController _controller;
        private CliOptions _cli;
        private string _nl;

        public void Init(InstDeclController controller)
        {
            _controller = controller;
            _cli = CliOptions.GetInstance();
            _nl = Environment.NewLine;
        }

        public string GetHeader()
        {
            return $"    .file \"{_cli.Src}\"" + _nl +
                   "    .def __main; .scl 2; .type 32; .endef";
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
            var ret = "    .section .rdata, \"dr\"" + _nl;

            var m = InstDeclController.Meta;

            foreach (var d in m.GetDirectives())
            {
                var dr = GetDirectiveString(d.Directive, d.Info) + _nl;
                foreach (var n in d.Nested)
                {
                    dr += $"    {GetDirectiveString(n.Directive, n.Info)}"
                        + _nl;
                }

                ret += dr + _nl;
            }

            ret += "    .text" + _nl +
                   "    .globl main" + _nl + 
                   "    .def main; .scl 2; .type 32; .endef" + _nl +
                   "    .seh_proc main" + _nl;

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
                ret += $"{fnc.Id}:" + _nl +
                       $"   pushq %rbp" + _nl +
                       $"   .seh_pushreg %rbp" + _nl +
                       $"   movq %rsp, %rbp" + _nl +
                       $"   .seh_setframe %rbp, 0" + _nl +
                       $"   subq $32, %rsp" + _nl +
                       $"   .seh_stackalloc 32" + _nl +
                       $"   .seh_endprologue" + _nl;
                if (fnc.Id == "main")
                {
                    ret += "    call __main" + _nl;
                }

                foreach (var i in fnc.Instructions.Inst)
                {
                    ret += FormatInstruction(i.Op, i.Arg0, i.Arg1, null, null, OpModifier.None)
                        + _nl;
                }

                ret += "    movl $0, %eax" + _nl +
                       "    addq $32, %rsp" + _nl +
                       "    popq %rbp" + _nl +
                       "    ret" +_nl +
                       "    .seh_endproc" + _nl+
                       "    .ident \"KCC V1\"" + _nl+
                       "    .def printf; .scl 2; .type 32; .endef" + _nl;
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