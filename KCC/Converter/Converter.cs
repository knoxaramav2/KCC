using System.Collections.Generic;
using System.IO;
using CodeTranslator;
using CodeTranslator.Targets;
using CommonLangLib;
using KCC;
using TargetPluginSDK;

namespace Converter
{
    /// <summary>
    /// Creates assembly files from data
    /// </summary>
    /// 
    /*
    public class Converter
    {
        private List<string> _asm;
        private CliOptions _cli;
        private LocaleArchFrags _fragments;
        private InstDeclController _controller;
        private ITarget _target;

        public Converter()
        {
            _asm = new List<string>();
            _cli = CliOptions.GetInstance();
            _controller = InstDeclController.GetInstance();
        }

        public void LogInternalTranslation()
        {
            if (!_cli.OutputInternals)
            {
                return;
            }

            string directives = _controller.GetDirectiveLog();
            string log = directives + _controller.DumpInternalCode(System.Console.WindowWidth);

            if (_cli.VerboseLevel == Verbosity.Detailed)
            {
                Debug.PrintDbg(log);
            }

            string logPath = $@"{KCCEnv.BaseUri}/{_cli.OutputName}_log.txt";
            using (var file = new StreamWriter(logPath, false))
            {
                file.Write(log);
            }
        }

        public void Build()
        {
            //TODO Support other architectures than x86-x64
            LoadTargetPlugin();
            CreateAssembly();
        }

        public void CreateAssembly()
        {

            //TODO Choose from list

            IArchAgent agent = new Gasx86_64();
            agent.Init(_controller);

            //TODO support multiple files
            while (true)
            {
                //_asm.Add(agent.GetHeader());
                //_asm.Add(agent.GetGlobals());
                //_asm.Add(agent.GetConstData());
                //_asm.Add(agent.GetFunctionDefs());

                _asm.Add(agent.GetAll());

                string asmPath = $@"{KCCEnv.BaseUri}/{_cli.OutputName}.s";

                using (var file = new StreamWriter(asmPath, false))
                {
                    foreach (var line in _asm)
                    {
                        file.Write(line);
                    }
                }

                agent.InvokeLocalAssembler(asmPath);

                break;
            }
        }

        public void LoadTargetPlugin()
        {
            var target = _cli.TargetOption;

            var plugins = Directory.GetFiles("libs", "*.dll");

        }
    }
    */

}