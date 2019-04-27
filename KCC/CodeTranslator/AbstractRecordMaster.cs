using System.Collections.Generic;


//TODO Finish and implement with GlobalSymbolTable, TypeTable
namespace CodeTranslator
{
    public class AbstractRecordMaster <T>
    {
        private Dictionary<ulong, T> _tables;

        
    }

    public class AbstractRecord
    {
        public uint RecordId { get; }
        public ulong ParentId { get; }

        public AbstractRecord(uint recordId, ulong parentId)
        {
            RecordId = recordId;
            ParentId = parentId;
        }
    }
}