using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using CodeTranslator;
using CodeTranslator.Targets;
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

        public Converter()
        {
            _asm = new List<string>();
            _cli = CliOptions.GetInstance();
        }

        public void Build()
        {
            //TODO Support other architectures than x86-x64
            CreateAssembly();
        }

        public void CreateAssembly()
        {

            //TODO Choose from list

            IArchAgent agent = new Nasm32();

            while (false)
            {
                _asm.Add(agent.GetHeader());
                _asm.Add(agent.GetGlobals());
                _asm.Add(agent.GetConstData());
                _asm.Add(agent.GetFunctionDefs());
                
                using (var file = new StreamWriter($@".s", false))
                {
                    foreach (var line in _asm)
                    {
                        file.WriteLine(line);
                    }

                    
                }
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