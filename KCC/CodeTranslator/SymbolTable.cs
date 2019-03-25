﻿namespace CodeTranslator
{
    /// <summary>
    /// Stores all variable records within a scope
    /// </summary>
    public class SymbolTable : RecordController<SymbolRecord>
    {
        private AssemblyRegistry _assemblyRegistry;

        public SymbolTable()
        {
            _assemblyRegistry = AssemblyRegistry.GetInstance();
        }

        public override RecordOperationResult Add(string key, object value)
        {
            base.Add(key, (SymbolRecord)value);
            RegPrinter.PrintRegUpdate(_assemblyRegistry.TargetAssembly.ScopeSymbol,
                key, value.ToString(), "Add");
            return RecordOperationResult.Ok;
        }
    }

    /// <summary>
    /// Records an instance declaration of a variable
    /// Contains a 'foreign' reference to its respective data type id and name
    /// </summary>
    public class SymbolRecord : Record
    {
        public TypeForeignReference ForeignReference { get; internal set; }
    }


}