using System.Collections.Generic;
using CommonLangLib;

namespace CodeTranslator
{
    public class SymbolAddrTable : IDatumTable<SymbolEntry>
    {
        private SymbolAddrTable _header;
        private uint _offset;
        private static ushort _blockSize;

        public string Id { get; }

        private readonly Dictionary<string, SymbolEntry> _entries;

        public SymbolAddrTable(string id, SymbolAddrTable previous, ushort blockSize)
        {
            Id = id;

            _header = previous;
            _offset = 0;
            _blockSize = blockSize;

            _entries = new Dictionary<string, SymbolEntry>();
        }

        public IDatumTable<SymbolEntry> AddTable()
        {
            throw new System.NotImplementedException();
        }

        public SymbolEntry AddRecord(SymbolEntry t)
        {
            if (!_entries.TryGetValue(t.Id, out var e))
            {
                ErrorReporter.GetInstance().Add($"{t.Id} already exists in scope", ErrorCode.SymbolRedefinitino);
                return null;
            }

            //TODO Better alignment
            var dblk = _offset % _blockSize;
            if (e.Width > dblk)
            {
                _offset += dblk;
            }

            _offset += e.Width;

            e.SetScope(this);
            e.Offset = _offset;
            _entries.Add(e.Id, e);

            return t;
        }

        public void ClearTable()
        {
            _entries.Clear();
        }

        public SymbolEntry SearchRecord(string id)
        {
            return !_entries.TryGetValue(id, out var e) ? _header?.SearchRecord(id) : e;
        }
    }

    public class SymbolEntry
    {
        private SymbolAddrTable _scope;
        public uint Width { get; }
        public string Id { get; }
        public uint Offset { get; internal set; }

        public SymbolEntry(string id, uint width)
        {
            Width = width;
            Id = id;
        }

        internal void SetScope(SymbolAddrTable scope)
        {
            _scope = scope;
        }

    }
}