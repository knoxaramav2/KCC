using System.Collections.Generic;

namespace CodeTranslator
{
    public class GlobalSymbolTable
    {
        private static GlobalSymbolTable _self;
        private static TypeTable _typeTable;
        private static Dictionary<ulong, SymbolTable> _tables;

        private GlobalSymbolTable()
        {
            _tables = new Dictionary<ulong, SymbolTable>();
            _typeTable = TypeTable.GetInstance();
        }

        public static GlobalSymbolTable GetInstance()
        {
            return _self ?? (_self = new GlobalSymbolTable());
        }

        public void AddTable(ulong scopeId, ulong parentScopeID)
        {
            _tables.Add(scopeId, new SymbolTable(parentScopeID));
        }

        public SymbolRecord GetSymbol(ulong scopeId, ulong record)
        {
            var table = _tables[scopeId];
            return table?.GetRecord(record);
        }
    }

    public class SymbolTable
    {
        private Dictionary<ulong, SymbolRecord> _records;
        private ulong _parentId;

        internal SymbolTable(ulong parentScopeId)
        {
            _records = new Dictionary<ulong, SymbolRecord>();
            _parentId = parentScopeId;
        }

        internal SymbolRecord GetRecord(ulong refId)
        {
            return _records[refId];
        }

    }

    public class SymbolRecord
    {
        internal SymbolRecord(ulong refId, uint typeId)
        {
            RefId = refId;
            TypeId = typeId;
            Defined = false;
        }

        public void SetDefined()
        {
            Defined = true;
        }

        public ulong RefId { get; }
        public uint TypeId { get; }
        public bool Defined { get; private set; }
    }
}