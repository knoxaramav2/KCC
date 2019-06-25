using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using CommonLangLib;

namespace CodeTranslator
{
    public class GlobalMethodTable
    {
        private static GlobalMethodTable _self;
        private static TypeTable _typeTable;
        private static GlobalSymbolTable _symbolTable;
        private static Dictionary<ulong, MethodTable> _tables;

        private GlobalMethodTable()
        {
            _tables = new Dictionary<ulong, MethodTable>();
            _typeTable = TypeTable.GetInstance();
            _symbolTable = GlobalSymbolTable.GetInstance();
        }

        public static GlobalMethodTable GetInstance()
        {
            return _self ?? (_self = new GlobalMethodTable());
        }

        public void AddTable(ulong scopeId, ulong parentScopeId)
        {
            _tables.Add(scopeId, new MethodTable(parentScopeId));
        }

        public MethodRecord GetMethod(ulong scopeId, ulong record)
        {
            var table = _tables[scopeId];
            MethodRecord r;

            if (table == null || (r = table.GetRecord(record))==null)
            {
                Debug.PrintDbg($"Could not resolve method {record} @{scopeId}");
            }

            return table?.GetRecord(record);
        }

        public bool AddRecord(ulong scopeId, ulong refId, string symbol, uint returnType,
            List<uint> types=null, List<uint> rf=null)
        {
            var table = _tables[scopeId];
            if (table == null)
            {
                Debug.PrintDbg($"Could not resolve method {returnType} {scopeId}::{symbol} @{refId}");
                return false;
            }

            table.AddFunction(refId, scopeId, symbol, returnType, null, null);

            return true;
        }

        public bool AddRecord(ulong scopeId, ulong refId, string symbol, string returnType,
            List<uint> types = null, List<uint> rf = null)
        {
            var type = _typeTable.GetRecord(scopeId, returnType);
            if (type == null)
            {
                Debug.PrintDbg($"Could not resolve method {returnType} {scopeId}::{symbol} @{refId}");
                return false;
            }

            return AddRecord(refId, scopeId, symbol, type.RefId, types, rf);
        }
    }

    public class MethodTable
    {
        private Dictionary<ulong, MethodRecord> _records;
        internal ulong ParentId { get; }

        private MethodRecord _trackedRecord;

        public MethodTable(ulong parentScopeId)
        {
            _records = new Dictionary<ulong, MethodRecord>();
            ParentId = parentScopeId;
        }

        internal MethodRecord GetRecord(ulong refId)
        {
            return _records[refId];
        }

        internal MethodRecord GetRecord(string symbol)
        {
            return (from r in _records.Select((entry, index) => new { entry, index })
                where r.entry.Value.Symbol == symbol
                select r.entry.Value).FirstOrDefault();
        }

        internal void AddFunction(ulong refId, ulong scopeId, string symbol, uint returnType,
            List<uint> types, List<uint> refs)
        {
            Debug.PrintDbg($"+Method {returnType} {scopeId}::{refId} {symbol} [{types}] [{refs}]");

            var method = new MethodRecord(refId, symbol, returnType, types, refs);
            _records.Add(refId, method);
            _trackedRecord = method;
        }

        internal void CloseFunction()
        {
            _trackedRecord = null;
        }

        internal bool AddInstruction(InstOp op, int arg0, int arg1, int spcl0, int spcl1, OpModifier modifier)
        {
            if (_trackedRecord == null)
            {
                return false;
            }

            _trackedRecord.AddInstruction(new Instruction(
                op, arg0, arg1, spcl0, spcl1, modifier));

            return true;
        }
    }

    public class MethodRecord
    {
        public ulong RefId { get; }
        public string Symbol { get; }
        public uint ReturnType { get; }
        public List<uint> ArgTypes { get; }
        public List<uint> ArgRefs { get; }

        private List<Instruction> _instructions;

        public MethodRecord(ulong refId, string symbol, uint returnType, 
            List<uint> argTypes=null, List<uint> argRefs=null)
        {
            RefId = refId;
            Symbol = symbol;
            ReturnType = returnType;
            ArgTypes = argTypes;
            ArgRefs = argRefs;

            _instructions = new List<Instruction>();
        }

        public void AddInstruction(Instruction i)
        {
            Debug.PrintDbg($"+Inst {i.Op} {i.Arg0} {i.Arg1}");

            _instructions.Add(i);
        }
    }

    public class Instruction
    {
        public InstOp Op { get; }
        public int Arg0 { get; }
        public int Arg1 {get;}
        public int Spcl0 { get; }
        public int Spcl1 { get; }
        public OpModifier Modifier;

        public Instruction(InstOp op, int arg0, int arg1, int spcl0, int spcl1, OpModifier modifier)
        {
            Op = op;
            Arg0 = arg0;
            Arg1 = arg1;
            Spcl0 = spcl0;
            Spcl1 = spcl1;
            Modifier = modifier;
        }
    }

}