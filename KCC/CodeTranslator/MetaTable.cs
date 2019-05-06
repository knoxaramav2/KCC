using System.Collections.Generic;
using CommonLangLib;

namespace CodeTranslator
{
    public class MetaTable
    {
        private MetaEntry _current;
        private List<MetaEntry> _entries;
        private static ushort _lcCounter;

        public MetaTable()
        {
           _entries = new List<MetaEntry>();
        }

        private void DirDataCheck(Directives d, ref string info)
        {
            if (d == Directives.Lc)
            {
                info = ""+_lcCounter++;
            }
        }

        public void AddDirective(Directives d, string info)
        {
            DirDataCheck(d, ref info);

            Debug.PrintDbg($"@{d}:{info}");

            _current = new MetaEntry(d, info);
            _entries.Add(_current);
        }
        public void AddNestedDirective(Directives d, string info)
        {
            if (_entries == null)
            {
                Debug.PrintDbg($"Invalid nested directive with {info}");
                return;
            }

            DirDataCheck(d, ref info);

            Debug.PrintDbg($"@  {d}:{info}");

            _current.AddNested(new MetaEntry(d, info));
        }

        public List<MetaEntry> GetDirectives()
        {
            return _entries;
        }

        public static ushort GetLcCounter()
        {
            return _lcCounter;
        }
    }

    public class MetaEntry
    {
        public List<MetaEntry> Nested { get; internal set; }

        public Directives Directive { get; }
        public string Info { get; }

        public MetaEntry(Directives d, string info)
        {
            Nested = new List<MetaEntry>();

            Directive = d;
            Info = info;
        }

        internal void AddNested(MetaEntry m)
        {
            Nested.Add(m);
        }
    }
}