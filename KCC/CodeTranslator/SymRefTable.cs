using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTranslator
{
    class SymRefTable
    {
        public List<SymRefEntry> Entries;
    }

    public class SymRefEntry
    {
        private ulong id;
        private SymInfo typeId;
    }
}
