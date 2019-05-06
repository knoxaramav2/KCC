using System;
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
            return $"    .file \"{_cli.Src}\"" +
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
                case Directives.Lc: return $".LC{info}";
                case Directives.Ascii: return $".ascii \"{info}\\0\"";
                case Directives.Text: return ".text";

                default: return null;
            }
        }

        public string GetConstData()
        {
            var ret = "    .section .rdata, \"dr\"";

            var m = InstDeclController.Meta;

            foreach (var d in m.GetDirectives())
            {
                var dr = GetDirectiveString(d.Directive, d.Info);
                foreach (var n in d.Nested)
                {
                    dr += $"    {GetDirectiveString(n.Directive, n.Info)}";
                }

                ret += Environment.NewLine + dr;
            }

            Debug.PrintDbg($"{ret}");

            return ret;
        }

        public string GetFunctionDefs()
        {
            return "";
        }

        public string FormatInstruction(InstOp opcode, int arg0, int arg1, int spcl1, int spcl2, InstOp mode)
        {
            return "";
        }
    }
}