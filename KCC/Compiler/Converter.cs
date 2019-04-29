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
        private ProgramGraph _graph;
        private CliOptions _cli;
        private LocaleArchFrags _fragments;
        private Db _db;

        public Converter(Db db)
        {
            _asm = new List<string>();
            _cli = CliOptions.GetInstance();
            _db = db;
        }

        public void Build()
        {
            //TODO Support other architectures than x86-x64
            CreateAssembly(_db);
        }

        public void CreateAssembly(Db db)
        {
            _graph = db.Graph;
            _graph.Rewind();
            //_graph.NextAssembly();

            //TODO Choose from list

            IArchAgent agent = new Nasm32();
            agent.Init(_graph);


            while (_graph.NextAssembly())
            {
                _asm.Add(agent.GetHeader());
                _asm.Add(agent.GetGlobals());
                _asm.Add(agent.GetConstData());
                _asm.Add(agent.GetFunctionDefs());
                
                using (var file = new StreamWriter($@"{_graph.GetCurrentAsmName()}.s", false))
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