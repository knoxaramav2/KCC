﻿using System.Collections.Generic;
using CommonLangLib;

namespace CodeTranslator
{
    public class InstDeclController
    {
        //private static IndInstTable _instTable;
        private static SymbolAddrTable _symTable;
        private static SymbolAddrTable _currentScope;
        private static InstDeclController _self;
        public static MetaTable Meta { get; internal set; }

        //TODO autoconfig
        private ushort _blocksize = 8;

        private InstDeclController()
        {
            //_instTable = new IndInstTable();
            _symTable = new SymbolAddrTable("#ROOT",null,_blocksize);
            _currentScope = _symTable;
            Meta = new MetaTable();
        }

        public static InstDeclController GetInstance()
        {
            return _self ?? (_self = new InstDeclController());
        }

        public bool AddDirective(Directives d, string info, bool nested=false)
        {
            if (nested)
            {
                Meta.AddNestedDirective(d, info);
            }
            else
            {
                Meta.AddDirective(d, info);
            }

            return true;
        }

        public bool AddInstruction(InstOp op, string arg0, string arg1)
        {
            _currentScope.Instructions.AddRecord(new InstEntry(op, arg0, arg1));
            Debug.PrintDbg($"{op} {arg0} {arg1}");

            return true;
        }

        public bool DeclareVariable(string id, string type)
        {
            if (_currentScope.AddRecord(id,type)==null)
            {
                Debug.PrintDbg($"Could not declare var {id}:{type}");
                return false;
            }

            Debug.PrintDbg($"Declared {id}:{type}");

            return true;
        }

        public bool CreateScope(string id, string type)
        {
            var table = (SymbolAddrTable)_currentScope.AddTable(id, type);

            if (table == null)
            {
                return false;
            }

            _currentScope = table;
            Debug.PrintDbg($"Now pointed to {id}");

            return true;
        }

        public void ExitScope()
        {
            if (_currentScope == null)
            {
                Debug.PrintDbg("No scope to leave");
                return;
            }
            _currentScope = _currentScope?.GetPrevious();
            Debug.PrintDbg($"Now pointed to {_currentScope?.Id}");
        }        

    }
}