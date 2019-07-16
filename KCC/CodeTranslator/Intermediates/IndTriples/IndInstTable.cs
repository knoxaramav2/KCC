using CodeTranslator.Intermediates.IndTriples;
using System.Collections.Generic;

namespace CodeTranslator
{
    public class IndInstTable : IDatumTable<InstEntry>
    {
        public List<InstEntry> Inst { get; internal set; }
        public SymbolAddrTable SymbolTable { get; internal set; }

        public IndInstTable(SymbolAddrTable symbolAddrTable)
        {
            Inst = new List<InstEntry>();
            SymbolTable = symbolAddrTable;
        }

        public IDatumTable<InstEntry> AddTable(string id, string type)
        {
            throw new System.NotImplementedException();
        }

        public InstEntry AddRecord(InstEntry t)
        {
            //Prevent duplicate argument markers
            Inst.Add(t);

            return t;
        }

        public void ClearTable()
        {
            throw new System.NotImplementedException();
        }

        public InstEntry SearchRecord(string id)
        {
            throw new System.NotImplementedException();
        }

        public string GetFormattedLog(int maxWidth)
        {
            throw new System.NotImplementedException();
        }

        public int GetStackFrameSize()
        {
            var varsInScope = new List<string>();



            return 0;
        }
    }

    public class InstEntry
    {
        public short EntryNo {get; private set;}
        public InstOp Op;
        public string Arg0, Arg1;       //For literals, variables
        public InstEntry tArg0, tArg1;  //
        public OpModifier OpModifier;

        public InstEntry(InstOp op, string arg0, string arg1, short entryNo, OpModifier opModifier=OpModifier.None)
        {
            EntryNo = entryNo;
            Op = op;
            Arg0 = arg0;
            Arg1 = arg1;
            OpModifier = opModifier;

            tArg0 = tArg1 = null;
        }
    }
}