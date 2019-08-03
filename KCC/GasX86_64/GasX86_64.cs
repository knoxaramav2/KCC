using System;
using CodeTranslator;
using KCC;
using TargetPluginSDK;
using CommonLangLib;

namespace GasX86_64
{
    public class GasX86_64 : ITarget
    {
        private CliOptions _cli;
        private InstDeclController _ctrl;

        public GasX86_64()
        {

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

                if (e.IsFunction)
                {
                    string prologue = GetPrologue(e);
                    string vars = GetVarDeclarations(e);


                } else if (e.IsLiteral)
                {

                } else
                {

                }
                
            }

            return true;
        }

        private string GetPrologue(SymbolEntry e)
        {
            return null;
        }

        private string GetVarDeclarations(SymbolEntry e)
        {
            return null;
        }

        public void Init(CliOptions cli, InstDeclController controller)
        {
            _cli = cli;
            _ctrl = controller;

            Debug.PrintDbg($"Loaded {TargetName}");
        }
    }
}
