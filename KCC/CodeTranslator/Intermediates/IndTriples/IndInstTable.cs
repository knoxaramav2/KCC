using System.Collections.Generic;

namespace CodeTranslator
{
    class IndInstTable : IDatumTable<InstEntry>
    {
        private List<InstEntry> _inst;
        private List<KeyValuePair<int, InstEntry>> _ind;

        public IndInstTable()
        {
            _inst = new List<InstEntry>();
            _ind = new List<KeyValuePair<int, InstEntry>>();
        }

        public IDatumTable<InstEntry> AddTable()
        {
            throw new System.NotImplementedException();
        }

        public InstEntry AddRecord(InstEntry t)
        {
            _inst.Add(t);
            _ind.Add(new KeyValuePair<int, InstEntry>(_ind.Count, t));

            return null;
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

    class InstEntry
    {

    }
}