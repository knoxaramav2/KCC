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
                return "Gas64";
            }
        }

        public string Build()
        {
            var ret = "";



            return ret;
        }

        public void Init(CliOptions cli, InstDeclController controller)
        {
            _cli = cli;
            _ctrl = controller;

            Debug.PrintDbg($"Loaded {TargetName}");


        }
    }
}
