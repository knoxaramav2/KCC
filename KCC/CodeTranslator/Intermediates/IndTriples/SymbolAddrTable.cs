using System.Collections.Generic;
using CommonLangLib;
using System.Linq;
using System;

namespace CodeTranslator
{
    public class SymbolAddrTable : IDatumTable<SymbolEntry>
    {
        private SymbolAddrTable _header;//parent address table
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

        IDatumTable<SymbolEntry> IDatumTable<SymbolEntry>.AddTable(string id, string type)
        {
            throw new System.NotImplementedException();
        }

        SymbolEntry IDatumTable<SymbolEntry>.AddRecord(SymbolEntry t)
        {
            throw new System.NotImplementedException();
        }

        void IDatumTable<SymbolEntry>.ClearTable()
        {
            throw new System.NotImplementedException();
        }

        SymbolEntry IDatumTable<SymbolEntry>.SearchRecord(string id)
        {
            throw new System.NotImplementedException();
        }

        public string GetFormattedLog(int maxWidth)
        {
            var hWidth = maxWidth / 2;
            var symLen = _entries.Count;
            var instLen = Instructions.Inst.Count;

            var headerSymbol    = "Symbol |";
            var headerInstruct  = "Instruction ";
            var symId           = "ID     |";
            var symType         = "Type   |";
            var InstOp          = "Op";
            var InstArg0        = "Arg0";
            var InstArg1        = "Arg1";
            var InstMod         = "Mod";
            var header = headerSymbol.PadLeft(hWidth) + headerInstruct.PadLeft(hWidth);



            string id, type, op, arg0, arg1, mod;
            string underscore = new string('_', maxWidth);
            string ret = header + System.Environment.NewLine + underscore + System.Environment.NewLine;
            

            for (int i = 0; (i < symLen) || (i < instLen); ++i)
            {
                id = type = op = arg0 = arg1 = mod = "";

                if (i < instLen)
                {
                    var inst = Instructions.Inst[i];
                    op = inst.Op.ToString();
                    arg0 = inst.Arg0;
                    arg1 = inst.Arg1;
                    mod = inst.OpModifier.ToString();
                }

                if (i < symLen)
                {
                    var entry = _entries.Values.ElementAt(i);
                    id = entry.Id;
                    type = entry.Type.SymbolId;
                }

                ret += $"{id} | {type} | {op} | {arg0} | {arg1} | {mod} "+System.Environment.NewLine;
            }

            foreach (var e in _entries.Values)
            {
                if (e.Target == null) continue;
                ret += underscore;
                ret += e?.Target.GetFormattedLog(maxWidth) + Environment.NewLine;
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