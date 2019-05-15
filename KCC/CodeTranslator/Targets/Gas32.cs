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

        public void Init(InstDeclController controller)
        {
            _controller = controller;
            _cli = CliOptions.GetInstance();
        }

        public string GetHeader()
        {
            return $"    .file \"{_cli.Src}\"" + Environment.NewLine +
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
            var ret = "    .section .rdata, \"dr\"" + Environment.NewLine;

            var m = InstDeclController.Meta;

            foreach (var d in m.GetDirectives())
            {
                var dr = GetDirectiveString(d.Directive, d.Info) + Environment.NewLine;
                foreach (var n in d.Nested)
                {
                    dr += $"    {GetDirectiveString(n.Directive, n.Info)}"
                        + Environment.NewLine;
                }

                ret += dr + Environment.NewLine;
            }

            ret += "    .text" + Environment.NewLine +
                   "    .globl main" + Environment.NewLine + 
                   "    .def main; .scl 2; .type 32; .endef" + Environment.NewLine +
                   "    .seh_proc main" + Environment.NewLine;

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
                ret += $"{fnc.Id}:" + Environment.NewLine +
                       $"   pushq %rbp" + Environment.NewLine +
                       $"   .seh_pushreg %rbp" + Environment.NewLine +
                       $"   movq %rsp, %rbp" + Environment.NewLine +
                       $"   .seh_setframe %rbp, 0" + Environment.NewLine +
                       $"   subq $32, %rsp" + Environment.NewLine +
                       $"   .seh_stackalloc 32" + Environment.NewLine +
                       $"   .seh_endprologue" + Environment.NewLine;
                if (fnc.Id == "main")
                {
                    ret += "    call __main" + Environment.NewLine;
                }

                foreach (var i in fnc.Instructions.Inst)
                {
                    ret += FormatInstruction(i.Op, i.Arg0, i.Arg1, null, null, OpModifier.None)
                        + Environment.NewLine;
                }

                ret += "    movl $0, %eax" + Environment.NewLine +
                       "    addq $32, %rsp" + Environment.NewLine +
                       "    popq %rbp" + Environment.NewLine +
                       "    ret" +Environment.NewLine +
                       "    .seh_endproc" + Environment.NewLine+
                       "    .ident \"KCC V1\"" + Environment.NewLine+
                       "    .def printf; .scl 2; .type 32; .endef" + Environment.NewLine;

            }

            return ret;
        }

        public string FormatInstruction(InstOp opcode, string arg0, string arg1, string spcl1, string spcl2, OpModifier mode)
        {
            var ret = "";

            switch (opcode)
            {
                case InstOp.Print:
                    ret = $"    leaq .LC{arg0}(%rip), %rcx" + Environment.NewLine +
                          $"    call printf";
                    break;
                case InstOp.Exit:

                    break;
            }

            return ret;
        }

        public bool InvokeLocalAssembler(string asmPath)
        {
            //System.Diagnostics.Process.Start("CMD.exe", $@"/C gcc {asmPath} -o {Path.GetFileNameWithoutExtension(asmPath)}")
            //    .WaitForExit();

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
                stdOut += process.StandardOutput.ReadLine() + Environment.NewLine;
            }

            while (!process.StandardError.EndOfStream)
            {
                stdErr += process.StandardError.ReadLine() + Environment.NewLine;
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