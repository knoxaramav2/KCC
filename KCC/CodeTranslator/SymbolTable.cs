namespace CodeTranslator
{
    /// <summary>
    /// Stores all variable records within a scope
    /// </summary>
    public class SymbolTable : RecordController<SymbolRecord>
    {
        
    }

    /// <summary>
    /// Records an instance declaration of a variable
    /// </summary>
    public class SymbolRecord
    {
        public string RecordName { get; }
        public long DataTypeId { get; }
        public Partition [] Partition { get; }
    }

    /// <summary>
    /// Used in class declarations, or other memory compact structures
    /// </summary>
    public class Partition
    {
        public string PartitionName { get; internal set; }
        public string ParitionSize { get; internal set; }
    }
}