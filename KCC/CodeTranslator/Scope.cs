using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTranslator
{
    /// <summary>
    /// Segments a new memory scope for instance data and data structures
    /// </summary>
    class Scope
    {
        public Scope(string scopeSymbol)
        {
            ScopeSymbol = scopeSymbol;
            SymbolTable = new SymbolTable();
            DataTypeTable = new DataTypeTable();
        }
        public SymbolTable SymbolTable { get; internal set; }
        public DataTypeTable DataTypeTable { get; internal set; }

        public string ScopeSymbol { get; internal set; }
    }
}
