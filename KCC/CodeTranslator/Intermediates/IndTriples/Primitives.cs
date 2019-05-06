using System.Collections.Generic;
using System.Security.Policy;
using CommonLangLib;

namespace CodeTranslator
{
    public enum Primitives
    {
        PInt,
        PString,
        PVoid
    }

    public class DataTypeTable
    {
        private static Dictionary<string, DataType> _typeTable;
        private static uint _typeCounter;

        /// <summary>
        /// Initialize supported primitives
        /// </summary>
        private DataTypeTable()
        {
            _typeTable = new Dictionary<string, DataType>();
            _typeCounter = 1;

            _typeTable.Add("asm", new DataType(0, ref _typeCounter, "asm"));

            _typeTable.Add("void", new DataType(0, ref _typeCounter, "void"));
            _typeTable.Add("int", new DataType(4, ref _typeCounter, "int"));
            _typeTable.Add("uint", new DataType(4, ref _typeCounter, "uint"));
            _typeTable.Add("short", new DataType(2, ref _typeCounter, "short"));
            _typeTable.Add("ushort", new DataType(2, ref _typeCounter, "ushort"));
            _typeTable.Add("long", new DataType(8, ref _typeCounter, "long"));
            _typeTable.Add("ulong", new DataType(8, ref _typeCounter, "ulong"));
            _typeTable.Add("float", new DataType(4, ref _typeCounter, "float"));
            _typeTable.Add("double", new DataType(8, ref _typeCounter, "double"));
            _typeTable.Add("char", new DataType(1, ref _typeCounter, "char"));
            _typeTable.Add("uchar", new DataType(1, ref _typeCounter, "uchar"));
            _typeTable.Add("string", new DataType(0, ref _typeCounter, "string"));
            _typeTable.Add("cstring", new DataType(0, ref _typeCounter, "cstring"));
            _typeTable.Add("array", new DataType(0, ref _typeCounter, "array"));
        }
        private static DataTypeTable _self;

        public static DataTypeTable GetInstance()
        {
            return _self ?? (_self = new DataTypeTable());
        }

        public DataType GetPrimitive(string sym)
        {
            _typeTable.TryGetValue(sym, out var ret);
            return ret;
        }
    }

    public class DataType
    {
        public ushort Width { get; }
        public uint TypeId { get; }
        public string SymbolId { get; }
        public List<DataType> Body { get; }
        public SymbolAddrTable Scope { get; internal set; }

        public DataType(ushort width, ref uint typeId, string symbolId, SymbolAddrTable scope=null, List<DataType> body=null)
        {
            Width = width;
            TypeId = typeId++;
            SymbolId = symbolId;
            Body = body;
            Scope = scope;

            Debug.PrintDbg($"RegVar {width} {typeId} {symbolId} {body?.Count}");
        }
    }
}