using System;
using System.Collections.Generic;
using CommonLangLib;

namespace CodeTranslator
{
    public class TypeTable
    {
        private static TypeTable _self;
        private static uint _recordCounter;
        private static uint _reserveCounter;
        private List<TypeScopeTable> _tables;

        private const uint RESERVE_TYPE_ID = 30;

        /// <summary>
        /// Loads all built-in data types
        /// </summary>
        private void TypeDataPreload()
        {
            AddTable(ulong.MaxValue, ulong.MaxValue);

            AddTypeDefinition(0, ulong.MaxValue, "int", 4, true);
            AddTypeDefinition(0, ulong.MaxValue, "short", 2, true);
            AddTypeDefinition(0, ulong.MaxValue, "float", 4, true);
            AddTypeDefinition(0, ulong.MaxValue, "double", 8, true);
            AddTypeDefinition(0, ulong.MaxValue, "char", 1, true);
            AddTypeDefinition(0, ulong.MaxValue, "byte", 1, true);
            AddTypeDefinition(0, ulong.MaxValue, "string", 0, true);
        }

        private TypeTable()
        {
            _tables = new List<TypeScopeTable>();
            _recordCounter = RESERVE_TYPE_ID;
            _reserveCounter = 1;

            TypeDataPreload();
        }

        public static TypeTable GetInstance()
        {
            return _self ?? (_self = new TypeTable());
        }

        //TODO Add return codes for: alreadyExists, inheritNotFound, etc...
        public bool AddTypeDefinition(uint inheritId, ulong scopeId, string symbol, uint size, bool isSKw=false)
        {
            //TODO check for inherited type and adjust size

            TypeScopeTable table;
            var newRecId = (isSKw ? _reserveCounter : _recordCounter);

            if ((table = GetTable(scopeId)) == null || 
                 !table.AddRecord(newRecId, inheritId, scopeId, symbol, size))
            {
                Debug.PrintDbg($"Typedef failed: {scopeId}::{symbol} : {size}B");
                return false;
            }

            if (isSKw)
            {
                Debug.PrintDbg($"Define INTERNAL {scopeId}::{symbol} ({_reserveCounter}) : {size}B");
                _reserveCounter++;
            }
            else
            {
                Debug.PrintDbg($"Define USESPACE {scopeId}::{symbol} ({_recordCounter}) : {size}B");
                _recordCounter++;
            }

            return true;
        }

        public TypeDefinition GetRecord(ulong scopeId, uint refId)
        {
            foreach (var t in _tables)
            {
                if (t.ScopeId == scopeId)
                {
                    return t.GetRecord(refId);
                }
            }

            return null;
        }

        public TypeDefinition GetRecord(ulong scopeId, string symbol)
        {
            foreach (var t in _tables)
            {
                if (t.ScopeId == scopeId)
                {
                    return t.GetRecord(symbol);
                }
            }

            return null;
        }

        private TypeScopeTable GetTable(ulong scopeId)
        {
            foreach (var t in _tables)
            {
                if (t.ScopeId == scopeId)
                {
                    return t;
                }
            }

            return null;
        }

        public void AddTable(ulong scopeId, ulong parentScopeId)
        {
            _tables.Add(new TypeScopeTable(scopeId, parentScopeId));
        }
    }

    class TypeScopeTable
    {
        public TypeScopeTable(ulong scopeId, ulong parentId)
        {
            _records = new List<TypeDefinition>();
            ScopeId = scopeId;
            ParentId = parentId;
        }

        public TypeDefinition GetRecord(uint refId)
        {
            foreach (var r in _records)
            {
                if (r.RefId == refId)
                {
                    return r;
                }
            }

            return null;
        }

        public TypeDefinition GetRecord(string symbol)
        {
            foreach (var r in _records)
            {
                if (r.Symbol == symbol)
                {
                    return r;
                }
            }

            return null;
        }


        public bool AddRecord(uint refId, uint inheritId, ulong scopeId, string symbol, uint size)
        {
            if (GetRecord(symbol) != null)
            {
                return false;
            }

            _records.Add(new TypeDefinition(refId, inheritId, scopeId, symbol, size));

            return true;
        }

        private List<TypeDefinition> _records;

        public ulong ScopeId { get; }
        internal ulong ParentId { get; }
    }

    public class TypeDefinition
    {
        public TypeDefinition(uint refId, uint inheritId, ulong scopeId, string symbol, uint size)
        {
            RefId = refId;
            InheritId = inheritId;
            ScopeId = scopeId;
            Symbol = symbol;
            Size = size;
            Defined = false;
        }

        public void SetDefined()
        {
            Defined = true;
        }

        public string Symbol { get; }
        public uint RefId { get; }
        public uint InheritId { get; }
        public ulong ScopeId { get; }
        public uint Size { get; }
        public bool Defined { get; private set; }

    }
}