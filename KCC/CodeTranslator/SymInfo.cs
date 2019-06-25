using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTranslator
{
    class SymInfo
    {
        public List<SymInfoEntry> Entries;
    }

    class SymInfoEntry
    {
        public string Id { get; }
        public uint Size { get; }
    }
}
