using System;
using CodeTranslator;
using KCC;
using TargetPluginSDK;

namespace GasX86_64
{
    public class GasX86_64 : ITarget
    {
        private CliOptions _cli;
        private InstDeclController _ctrl;

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
        }
    }
}
