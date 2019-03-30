namespace CodeTranslator
{
    /// <summary>
    /// Stores routine entry data
    /// </summary>
    public class MethodTable : RecordController<MethodRecord>
    {
        private AssemblyRegistry _assemblyRegistry;

        public MethodTable()
        {
            _assemblyRegistry = AssemblyRegistry.GetInstance();
        }

        public override RecordOperationResult Add(string key, object value)
        {
            base.Add(key, (MethodRecord)value);
            RegPrinter.PrintRegUpdate(_assemblyRegistry.TargetAssembly.ScopeSymbol,
                key, value.ToString(), "Add");
            return RecordOperationResult.Ok;
        }
    }

    /// <summary>
    /// Stores return types, parameters, and basic instruction data
    /// </summary>
    public class MethodRecord : Record
    {
        //Argument references
        public TypeForeignReference [] foreignReference { get; internal set; }

        //Reference to return type
        public TypeForeignReference returnReference { get; internal set; }

    }

    /// <summary>
    /// Stores information on atomic instructions
    /// Instructions may make use of higher-level references to be determined
    /// after initial scans
    /// </summary>
    public class Instruction
    {

    }
}