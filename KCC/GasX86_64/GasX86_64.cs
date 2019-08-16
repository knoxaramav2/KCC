using System;
using CodeTranslator;
using KCC;
using TargetPluginSDK;
using CommonLangLib;
using System.Collections.Generic;
using System.Linq;

namespace GasX86_64
{
    public class GasX86_64 : ITarget
    {
        private CliOptions _cli;
        private InstDeclController _ctrl;

        private string _nl;

        private List<string> _functions;
        private List<string> _constants;

        public GasX86_64()
        {
            _nl = Environment.NewLine;

            _functions = new List<string>();
            _constants = new List<string>();
        }

        public string TargetName
        {
            get
            {
                return "GasX86_64";
            }
        }

        public string Build()
        {
            var ret = "";

            var root = _ctrl.GetRoot();
            ExecuteFrame(root);

            return ret;
        }

        public bool ExecuteFrame(SymbolAddrTable table)
        {

            var entries = table._entries;
            foreach (var v in entries)
            {
                var e = v.Value;

                if (e.IsFunction())
                {
                    string prologue = GetPrologue(e.Target);
                    string vars = GetVarDeclarations(e);
                    string epilogue = GetEpilogue(e.Target);

                    _functions.Add(prologue+vars+epilogue+_nl);
                } else if (e.IsLiteral)
                {

                } else if (e.IsAssembly())
                {
                    ExecuteFrame(e.Target);
                } else if (e.IsClass())
                {

                }
                
            }

            return true;
        }

        private string GetPrologue(SymbolAddrTable sat)
        {
            var prologue = "";

            prologue +=$"{sat.Id}:"+_nl;
            prologue += "   pushq   %rbp" + _nl;
            prologue += "   movq    %rsp,   %rbp" + _nl;
            prologue +=$"   subq    ${sat.GetUnoptomizedStackWidth()},    %rsp"+_nl;

            if (sat.Id == "main")
            {
                prologue += "   call    __main"+_nl;
            }

            return prologue;
        }

        private string GetVarDeclarations(SymbolEntry e)
        {
            return null;
        }

        private string GetEpilogue(SymbolAddrTable sat)
        {
            var epilogue = "";

            if (sat.Id == "main")
            {
                epilogue += "   leave" + _nl;
            } else
            {
                epilogue += "   popq    %rbp" + _nl;
            }

            
            epilogue += "   ret"+_nl;

            return epilogue;
        }

        private string GetConstants(InstDeclController ctrl)
        {
            var constants = "";

            foreach(var dcl in InstDeclController.Meta.GetDirectives())
            {

            }

            return constants;
        }

        public void Init(CliOptions cli, InstDeclController controller)
        {
            _cli = cli;
            _ctrl = controller;

            Debug.PrintDbg($"Loaded {TargetName}");
        }

        public string DumpAssembly()
        {

            var asm = "";

            asm += string.Join(_nl, _functions);
            asm += string.Join(_nl, _constants);

            return asm;
        }
    }
}
