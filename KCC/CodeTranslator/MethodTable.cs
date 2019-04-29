using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace CodeTranslator
{
    public class GlobalMethodTable
    {
        private static MethodTable _self;
        private static TypeTable _typeTable;
        private static GlobalSymbolTable _symbolTable;
        private static Dictionary<ulong, MethodTable> _tables;
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

        internal void AddFunction(ulong refId, string symbol, uint returnType,
            List<uint> types, List<uint> refs)
        {
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
            _instructions.Add(i);
        }
    }

    public class Instruction
    {
        private InstOp _op;
        private int _arg0, _arg1, _spcl0, _spcl1;
        private OpModifier _modifier;

        public Instruction(InstOp op, int arg0, int arg1, int spcl0, int spcl1, OpModifier modifier)
        {
            _op = op;
            _arg0 = arg0;
            _arg1 = arg1;
            _spcl0 = spcl0;
            _spcl1 = spcl1;
            _modifier = modifier;
        }
    }

}