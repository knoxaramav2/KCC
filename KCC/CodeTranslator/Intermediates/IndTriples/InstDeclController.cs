using System;
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

        public string AddDirective(Directives d, string info, bool nested=false)
        {
            if (nested)
            {
                return Meta.AddNestedDirective(d, info);
            }
            else
            {
                return Meta.AddDirective(d, info);
            }
        }

        public bool AddInstruction(InstOp op, string arg0, string arg1, string special=null, OpModifier opModifier=OpModifier.None)
        {
            var entry = new InstEntry(op, (short)_currentScope.Instructions.Inst.Count, opModifier);

            switch (opModifier)
            {
                //Use last calculated value
                //Useful for long math and logic operations
                case OpModifier.FromLastTemp:
                    entry.tArg0 = _currentScope.Instructions.PopTempEntry();
                    break;
                //Both inputs are literals or variable names
                //Can be optimized later
                case OpModifier.LRawRRaw:
                    entry.Arg0 = _currentScope.SearchRecord(arg0);
                    entry.Arg1 = _currentScope.SearchRecord(arg1);
                    break;
                case OpModifier.LRawRTemp:
                    entry.Arg0 = _currentScope.SearchRecord(arg0);
                    entry.tArg1 = _currentScope.Instructions.PopTempEntry();
                    break;
                case OpModifier.LTempRRaw:
                    entry.tArg0 = _currentScope.Instructions.PopTempEntry();
                    entry.Arg1 = _currentScope.SearchRecord(arg1);
                    break;
                case OpModifier.LTempRTemp:
                    entry.tArg1 = _currentScope.Instructions.PopTempEntry();
                    entry.tArg0 = _currentScope.Instructions.PopTempEntry();
                    break;
                //No arguments
                case OpModifier.None:

                    break;
                //Assume no inputs
                case OpModifier.NullOrDefault:
                    entry.Arg0 = _currentScope.SearchRecord(arg0);
                    entry.Arg1 = null;//TODO Figure out default scheme
                    break;
                case OpModifier.Immediate:
                    entry.Arg0 = _currentScope.SearchRecord(arg0);
                    break;
            }

            //Check for intermediate command
            if ((op>=InstOp.Add && op <= InstOp.PreDecrement) ||
                (op>= InstOp.ShL && op <= InstOp.IndexAccess) ||
                (op >= InstOp.Or && op <= InstOp.NEqu))
            {
                _currentScope.Instructions.AddTempEntry(entry);
                Debug.PrintDbg($"Temp: {entry.EntryNo}");
            }

            _currentScope.Instructions.AddRecord(entry);

            Debug.PrintDbg($"{op} {arg0} {arg1} {opModifier}");

            return true;
        }

        public bool DeclareHeaderVariable(string id, string type)
        {
            return DeclareVariable(id, type, true);
        }

        public bool DeclareVariable(string id, string type, bool isHeader=false)
        {
            if (_currentScope.AddRecord(id,type)==null)
            {
                Debug.PrintDbg($"Could not declare var {id}:{type}");
                return false;
            }

            var instOp = isHeader ? InstOp.DeclareHeaderVar : InstOp.DeclareVar;

            AddInstruction(instOp, id, type, null, OpModifier.Immediate);

            Debug.PrintDbg($"Declared {id}:{type} :: Header? {isHeader}");

            return true;
        }

        public bool DeclareLiteral(string value, string type)
        {
            var result = _currentScope.AddRecord(_currentScope.AddRecord(value, type, true));

            if (result != null)
            {
                Debug.PrintDbg($"Literal: {value} : {type}");
            }

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
            var cTbl = _root.GetFormattedLog(formattedWidth*(5/2));
            return cTbl;
        }

        public string GetDirectiveLog()
        {
            var ret = "";

            foreach(var e in Meta.Entries)
            {
                ret += $"{e.Directive.ToString().ToUpper()}{e.Info}:"+Environment.NewLine;
                foreach(var m in e.Nested)
                {
                    ret += $"\t{m.Directive.ToString()} {m.Info}"+Environment.NewLine;
                }
            }

            return ret;
        }
    }
}