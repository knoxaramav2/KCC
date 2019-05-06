using System.Collections.Generic;
using CommonLangLib;

namespace CodeTranslator
{
    public class SymbolAddrTable : IDatumTable<SymbolEntry>
    {
        private SymbolAddrTable _header;
        public IndInstTable Instructions;//Note: Also acts as a constructor
        private DataTypeTable _typeTable;
        private uint _offset;
        private static ushort _blockSize;

        public BodyType BodyType;

        public string Id { get; }

        //Non-scope specific counters for easier assembly/temp name generation
        public static uint LDCounter { get; internal set; }
        public static List<string> CStrings { get; internal set; }

        private readonly Dictionary<string, SymbolEntry> _entries;

        public SymbolAddrTable(string id, SymbolAddrTable previous, ushort blockSize)
        {
            Id = id;

            _header = previous;
            Instructions = new IndInstTable();
            _offset = 0;
            _blockSize = blockSize;

            _entries = new Dictionary<string, SymbolEntry>();
            _typeTable = DataTypeTable.GetInstance();
        }

        public SymbolAddrTable GetPrevious()
        {
            return _header;
        }

        public IDatumTable<SymbolEntry> AddTable(string id, string type)
        {
            var dt = _typeTable.GetPrimitive(type);

            if (dt == null)
            {
                ErrorReporter.GetInstance().Add($"Type {type} not found", ErrorCode.SymbolUndefined);
                return null;
            }

            var e = new SymbolEntry(id, 0, dt);
            var s = new SymbolAddrTable(id, this, _blockSize);
            e.Target = s;
            AddRecord(e);

            return s;
        }

        public SymbolEntry AddRecord(SymbolEntry t)
        {
            if (_entries.TryGetValue(t.Id, out var e))
            {
                ErrorReporter.GetInstance().Add($"{t.Id} already exists in scope", ErrorCode.SymbolRedefinition);
                return null;
            }

            //TODO Better alignment
            var dblk = _offset % _blockSize;
            if (t.Width > dblk)
            {
                _offset += dblk;
            }

            _offset += t.Width;

            t.SetScope(this);
            t.Offset = _offset;
            _entries.Add(t.Id, t);

            return t;
        }

        public SymbolEntry AddRecord(string id, string type)
        {
            var t = _typeTable.GetPrimitive(type);
            if (t == null)
            {
                ErrorReporter.GetInstance().Add($"Type {type} not found", ErrorCode.SymbolUndefined);
                return null;
            }

            return AddRecord(new SymbolEntry(id, t.Width, t));
        }

        public void SetType(BodyType t)
        {
            BodyType = t;
        }

        //TODO Merge with add record
        public SymbolEntry AddCString(string msg)
        {
            var e = new SymbolEntry($"{LDCounter++}", (uint) msg.Length, _typeTable.GetPrimitive("cstring"));



            return e;
        }

        public void ClearTable()
        {
            _entries.Clear();
        }

        public SymbolEntry SearchRecord(string id)
        {
            return !_entries.TryGetValue(id, out var e) ? _header?.SearchRecord(id) : e;
        }

        public List<SymbolAddrTable> SearchAll()
        {
            var ret = new List<SymbolAddrTable> {this};

            foreach (var e in _entries)
            {
                if (e.Value.Target == null)
                {
                    continue;
                } 

                ret.AddRange(e.Value.Target.SearchAll());
            }

            return ret;
        }

    }

    public class SymbolEntry
    {
        private SymbolAddrTable _scope;
        public SymbolAddrTable Target { get; internal set; }

        public uint Width { get; }
        public string Id { get; }
        public uint Offset { get; internal set; }
        public DataType Type { get; }

        public SymbolEntry(string id, uint width, DataType type)
        {
            Width = width;
            Id = id;
            Type = type;

            Target = null;
        }

        internal void SetScope(SymbolAddrTable scope)
        {
            _scope = scope;
        }

    }

    public enum BodyType
    {
        AnonBlock,
        Asm,
        Function,
        Class
    }
}