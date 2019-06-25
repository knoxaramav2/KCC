using System.Collections.Generic;
using System.Linq;
using CommonLangLib;

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

        public void AddTable(ulong scopeId, ulong parentScopeId)
        {
            _tables.Add(scopeId, new SymbolTable(parentScopeId));
        }

        public SymbolRecord GetSymbol(ulong scopeId, ulong record)
        {
            var table = _tables[scopeId];
            return table?.GetRecord(record);
        }

        public SymbolRecord GetDeepRecord(ulong scopeId, ulong record)
        {
            SymbolRecord ret = null;
            var table = _tables[scopeId];

            while (table != null)
            {
                ret = table.GetRecord(record);
                if (ret == null)
                {
                    table = _tables[table.ParentId];
                }
                else
                {
                    break;
                }
            }

            return ret;
        }

        public bool AddRecord(ulong scopeId, ulong parentId, ulong record, string symbol, uint typeId)
        {
            var table = _tables[scopeId];

            if (table == null)
            {
                table = new SymbolTable(parentId);
                _tables.Add(parentId, table);
            }

            Debug.PrintDbg($"+Symbol {scopeId}::{record} {symbol} @{typeId}");

            table.AddRecord(record, typeId, symbol);

            return true;
        }

        public bool AddRecord(ulong scopeId, ulong record, string symbol, string typeString)
        {
            var type = _typeTable.GetRecord(scopeId, typeString);
            if (type == null)
            {
                Debug.PrintDbg($"Could not register symbol {typeString}:{symbol} @{scopeId}");
                return false;
            }

            return true;
        }
    }

    public class SymbolTable
    {
        private Dictionary<ulong, SymbolRecord> _records;
        internal ulong ParentId { get; }

        internal SymbolTable(ulong parentScopeId)
        {
            _records = new Dictionary<ulong, SymbolRecord>();
            ParentId = parentScopeId;
        }

        internal SymbolRecord GetRecord(ulong refId)
        {
            return _records[refId];
        }

        //Slow, cache results (refId) to avoid frequent use
        internal SymbolRecord GetRecord(string symbol)
        {
            return (from r in _records.Select((entry, index) => new {entry, index})
                where r.entry.Value.Symbol == symbol
                select r.entry.Value).FirstOrDefault();
        }

        internal void AddRecord(ulong refId, uint typeId, string symbol)
        {
            _records.Add(refId, new SymbolRecord(refId, typeId, symbol));
        }

    }

    public class SymbolRecord
    {
        internal SymbolRecord(ulong refId, uint typeId, string symbol)
        {
            RefId = refId;
            TypeId = typeId;
            Symbol = symbol;

            Defined = false;
        }

        public void SetDefined()
        {
            Defined = true;
        }

        public ulong RefId { get; }
        public uint TypeId { get; }
        public bool Defined { get; private set; }
        public string Symbol { get; }
    }
}