using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using CodeTranslator;
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

            //TODO Choose target architect/OS
            TargetWin10AMD();
        }

        public void TargetWin10AMD()
        {
            _asm.Add(_fragments.GetLocaleDataHeader());
            _asm.Add(_fragments.GetGlobalData());
            _asm.Add(_fragments.GetFunctionDefs());
        }
    }


}