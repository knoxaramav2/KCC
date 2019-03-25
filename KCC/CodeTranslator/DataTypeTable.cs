using System.Collections;

namespace CodeTranslator
{
    /// <summary>
    /// Records all data structure declarations within a scope
    /// </summary>
    public class DataTypeTable : RecordController<DataTypeRecord>
    {
        private AssemblyRegistry _assemblyRegistry;

        public DataTypeTable()
        {
            _assemblyRegistry = AssemblyRegistry.GetInstance();
        }

        public override RecordOperationResult Add(string key, object value)
        {

            base.Add(key, (DataTypeRecord) value);
            RegPrinter.PrintRegUpdate(_assemblyRegistry.TargetAssembly.ScopeSymbol,
                key, value.ToString(), "Add");
            return RecordOperationResult.Ok;
        }
    }


    /// <summary>
    /// Records an entry of a declared data type
    /// </summary>
    public class DataTypeRecord : Record
    {
        public Partition[] Partition { get; }
        public int Size { get; }
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