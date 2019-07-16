using CodeTranslator.Intermediates.IndTriples;
using CommonLangLib;
using System.Collections.Generic;
using System.Linq;

namespace CodeTranslator
{
    public class IndInstTable : IDatumTable<InstEntry>
    {
        public List<InstEntry> Inst { get; internal set; }
        public List<InstEntry> TempStack { get; internal set; }
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
            Inst.Add(t);

            return t;
        }
        public InstEntry GetLastInstruction()
        {
            return Inst.LastOrDefault();
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

        //Temporary Stack
        public void AddTempEntry(InstEntry entry)
        {
            TempStack.Add(entry);
        }

        public InstEntry PopTempEntry()
        {
            if (TempStack.Count == 0)
            {
                ErrorReporter.GetInstance().Add("Temporary Inst Stack Empty", ErrorCode.TempInstStackEmpty);
                return null;
            }

            var ret = TempStack[TempStack.Count-1];
            TempStack.Remove(ret);
            return ret;
        }
    }

    public class InstEntry
    {
        public short EntryNo {get; private set;}
        public InstOp Op;
        //public string Arg0, Arg1;       //For literals, variables
        public SymbolEntry Arg0 { get; internal set; }  //For literals, variables
        public SymbolEntry Arg1 {get; internal set;}      
        public InstEntry tArg0 { get; internal set; }   //For tracking temporary results of previous operations
        public InstEntry tArg1 { get; internal set; }      
        public OpModifier OpModifier;       //Special modifiers

        public InstEntry(InstOp op, short entryNo, OpModifier opModifier=OpModifier.None)
        {
            EntryNo = entryNo;
            Op = op;
            OpModifier = opModifier;

            tArg0 = tArg1 = null;
            Arg0 = Arg1 = null;
        }
    }
}