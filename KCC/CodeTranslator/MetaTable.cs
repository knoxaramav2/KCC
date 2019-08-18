using System.Collections.Generic;
using CommonLangLib;
using System.Linq;

namespace CodeTranslator
{
    public class MetaTable
    {
        private MetaEntry _current;
        public List<MetaEntry> Entries { get; internal set; }
        private static ushort _lcCounter;

        public MetaTable()
        {
           Entries = new List<MetaEntry>();
        }

        private void DirDataCheck(Directives d, ref string info)
        {
            if (d == Directives.Lc)
            {
                info = ""+_lcCounter++;
            } else if (d == Directives.Ascii)
            {
                info = info.Replace("\"", "");
            }
        }

        public string AddDirective(Directives d, string info)
        {
            var ret = "";
            if ((ret=FindConstByValue(info)) != null)
            {
                return ret;
            }

            DirDataCheck(d, ref info);

            Debug.PrintDbg($"@{d}:{info}");

            _current = new MetaEntry(d, info);
            Entries.Add(_current);

            return $"{_current.Directive.ToString().ToUpper()}{info}";
        }
        public string AddNestedDirective(Directives d, string info)
        {
            if (Entries == null)
            {
                Debug.PrintDbg($"Invalid nested directive with {info}");
                return null;
            }

            var ret = "";
            if ((ret = FindConstByValue(info)) != null)
            {
                return ret;
            }

            DirDataCheck(d, ref info);

            Debug.PrintDbg($"@  {d}:{info}");

            _current.AddNested(new MetaEntry(d, info));

            return $"{_current.Directive.ToString().ToUpper()}{_current.Info}";
        }

        public List<MetaEntry> GetDirectives()
        {
            return Entries;
        }

        public static ushort GetLcCounter()
        {
            return _lcCounter;
        }

        public SymbolEntry GetDirectiveSymbol(string id)
        {
            var entry = Entries.FirstOrDefault(t=>
                (t.Directive.ToString().ToUpper()+t.Info)==id);
            if (entry == null)
            {
                return null;
            }

            DataType type = DataTypeTable.GetInstance().GetPrimitive(entry.Nested[0].Directive.ToString().ToLower());
            SymbolEntry ret = new SymbolEntry(id, type.Width, type, true);

            return ret;
        }

        private string FindConstByValue(string value)
        {

            value = value.Replace("\"", "");

            foreach (var e in Entries)
            {
                if (e.Directive != Directives.Lc)
                {
                    continue;
                }

                foreach (var v in e.Nested)
                {
                    //TODO Add other constant directives
                    if (v.Directive == Directives.Ascii && v.Info == value)
                    {
                        return $"{e.Directive.ToString().ToUpper()}{e.Info}";
                    }
                }
            }

            return null;
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