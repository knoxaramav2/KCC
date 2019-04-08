using System.Collections.Generic;
using System.IO;
using CodeTranslator;

namespace Compiler
{
    /// <summary>
    /// Creates assembly files from data
    /// </summary>
    public class Converter
    {
        private Db _db;
        private List<string> _asm;

        public Converter()
        {
            _asm = new List<string>();
        }

        public void Build()
        {
            using (var file = new StreamWriter("projects/out.asm"))
            {
                foreach (var line in _asm)
                {
                    file.WriteLine(line);
                }

                file.Close();
            }
        }

        public void CreateAssembly(Db db)
        {
            _db = db;

            //TODO Choose target architect/OS
            TargetWin10AMD();
        }

        public void TargetWin10AMD()
        {
            //General headers
            _asm.Add(".386");
            _asm.Add(".model flat, stdcall");
            _asm.Add("option casemap :none");
            
            _asm.Add("include \\masm32\\include\\kernel32.inc");
            _asm.Add("include \\masm32\\include\\masm32.inc");
            _asm.Add("includelib \\masm32\\lib\\kernel32.lib");
            _asm.Add("includelib \\masm32\\lib\\masm32.lib");

            //Data Section
            _asm.Add(".data");
            _asm.Add(" message db \"Test message\"");

            //Code Section
            _asm.Add(".code");
            _asm.Add("main:");
            _asm.Add(" invoke StdOut, addr message");
            _asm.Add(" invoke ExitProcess, 0");
            _asm.Add("end main");


        }
    }


}