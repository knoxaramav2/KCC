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
            MethodTable = new MethodTable();
            ClassTable = new ClassTable();
        }

        //Adds a child scope with a matching foreign reference key
        public bool AddScope(Record record)
        {

            return true;
        }

        public bool ChangeScope(string scopeSymbol)
        {
            foreach (var child in Children)
            {
                if (child.ScopeSymbol == scopeSymbol)
                {

                }
            }
            return true;
        }

        public List<Scope> Children { get; internal set; }
        public Scope Parent { get; internal set; }
        public Scope Target { get; internal set; }


        public SymbolTable SymbolTable { get; internal set; }
        public DataTypeTable DataTypeTable { get; internal set; }
        public MethodTable MethodTable { get; internal set; }
        public ClassTable ClassTable { get; internal set; }

        public string ScopeSymbol { get; internal set; }
        public TypeForeignReference ForeignReference { get; internal set; }
    }
}
