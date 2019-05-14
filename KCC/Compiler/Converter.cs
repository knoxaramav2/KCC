using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using CodeTranslator;
using CodeTranslator.Targets;
using CommonLangLib;
using KCC;

namespace Compiler
{
    /// <summary>
    /// Creates assembly files from data
    /// </summary>
    public class Converter
    {
        private List<string> _asm;
        private CliOptions _cli;
        private LocaleArchFrags _fragments;
        private InstDeclController _controller;

        public Converter()
        {
            _asm = new List<string>();
            _cli = CliOptions.GetInstance();
            _controller = InstDeclController.GetInstance();
        }

        public void Build()
        {
            //TODO Support other architectures than x86-x64
            CreateAssembly();
        }

        public void CreateAssembly()
        {

            //TODO Choose from list

            IArchAgent agent = new Gas32();
            agent.Init(_controller);

            //TODO support multiple files
            while (true)
            {
                _asm.Add(agent.GetHeader());
                _asm.Add(agent.GetGlobals());
                _asm.Add(agent.GetConstData());
                _asm.Add(agent.GetFunctionDefs());

                string asmPath = $@"{KCCEnv.ExeUri}/{_cli.OutputName}";

                using (var file = new StreamWriter(asmPath, false))
                {
                    foreach (var line in _asm)
                    {
                        file.WriteLine(line);
                    }
                }

                agent.InvokeLocalAssembler(asmPath);

                break;
            }
        }

        public void TargetWin10AMD()
        {
            _asm.Add(_fragments.GetLocaleDataHeader());
            _asm.Add(_fragments.GetGlobalData());
            _asm.Add(_fragments.GetFunctionDefs());
        }
    }


}