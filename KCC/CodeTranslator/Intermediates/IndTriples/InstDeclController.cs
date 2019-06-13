using System.Collections.Generic;
using CommonLangLib;

namespace CodeTranslator
{
    public class InstDeclController
    {
        private static SymbolAddrTable _root;
        private static SymbolAddrTable _symTable;
        private static SymbolAddrTable _currentScope;
        private static InstDeclController _self;
        public static MetaTable Meta { get; internal set; }

        //TODO autoconfig
        private ushort _blocksize = 8;

        private InstDeclController()
        {
            _root = new SymbolAddrTable("#ROOT",null,_blocksize);
            _currentScope = _symTable = _root;
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

        public bool AddInstruction(InstOp op, string arg0, string arg1, string special=null, OpModifier opModifier=OpModifier.None)
        {
            _currentScope.Instructions.AddRecord(new InstEntry(op, arg0, arg1, opModifier));
            Debug.PrintDbg($"{op} {arg0} {arg1} {opModifier}");

            return true;
        }

        public bool DeclareVariable(string id, string type)
        {
            if (_currentScope.AddRecord(id,type)==null)
            {
                Debug.PrintDbg($"Could not declare var {id}:{type}");
                return false;
            }

            AddInstruction(InstOp.DeclareVar, id, type);

            Debug.PrintDbg($"Declared {id}:{type}");

            return true;
        }

        public bool CreateScope(string id, string type, BodyType t)
        {
            var table = (SymbolAddrTable)_currentScope.AddTable(id, type);
            table.SetType(t);

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

        public SymbolAddrTable FindFirstOf(string sym)
        {
            _currentScope = _symTable;
            _symTable.SearchRecord(sym);
            return null;
        }

        public List<SymbolAddrTable> FindAll()
        {
            return _symTable.SearchAll();
        }

        public string DumpInternalCode(int formattedWidth)
        {
            var cTbl = _root.GetFormattedLog(formattedWidth);
            return cTbl;
        }
    }
}