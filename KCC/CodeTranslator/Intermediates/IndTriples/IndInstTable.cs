using System.Collections.Generic;

namespace CodeTranslator
{
    public class IndInstTable : IDatumTable<InstEntry>
    {
        public List<InstEntry> Inst { get; internal set; }
        private List<KeyValuePair<int, InstEntry>> _ind;

        public IndInstTable()
        {
            Inst = new List<InstEntry>();
            _ind = new List<KeyValuePair<int, InstEntry>>();
        }

        public IDatumTable<InstEntry> AddTable(string id, string type)
        {
            throw new System.NotImplementedException();
        }

        public InstEntry AddRecord(InstEntry t)
        {
            Inst.Add(t);
            _ind.Add(new KeyValuePair<int, InstEntry>(_ind.Count, t));

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
    }

    public class InstEntry
    {
        public InstOp Op;
        public string Arg0, Arg1;

        public InstEntry(InstOp op, string arg0, string arg1)
        {
            Op = op;
            Arg0 = arg0;
            Arg1 = arg1;
        }
    }
}