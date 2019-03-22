using System.Collections;

namespace CodeTranslator
{
    /// <summary>
    /// Records all data structure declarations within a scope
    /// </summary>
    public class DataTypeTable : RecordController<DataTypeRecord>
    {

    }


    /// <summary>
    /// Records an entry of a declared data type
    /// </summary>
    public class DataTypeRecord
    {
        public string RecordName { get; }
        public int Size { get; }
        public long RecorId { get; }
    }
}