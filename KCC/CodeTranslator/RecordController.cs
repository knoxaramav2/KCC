using System;
using System.Collections.Generic;

namespace CodeTranslator
{
    public enum RecordOperationResult
    {
        Ok,
        RecordDoesNotExist,
        RecordDuplicate,
        RecordSaveError
    }

    public interface IRecordController
    {
        RecordOperationResult Add(String key, object value);
        object GetRecord(string recordName);
    }

    public abstract class RecordController <T> : IRecordController
    {
        

        private readonly Dictionary<string, T> _records;
        

        protected RecordController ()
        {
            _records = new Dictionary<string, T>();
        }

        public virtual RecordOperationResult Add(string key, object value)
        {
            if (_records.ContainsKey(key) == false)
            {
                return RecordOperationResult.RecordDuplicate;
            }

            try
            {
                _records.Add(key, (T)value);
            }
            catch (Exception)
            {
                return RecordOperationResult.RecordSaveError;
            }
            

            return RecordOperationResult.Ok;
        }

        public virtual object GetRecord(string recordName)
        {
            T ret;

            try
            {
                ret = _records[recordName];
            }
            catch (Exception)
            {
                return default(T);
            }


            return ret;
        }
    }

    public enum RecordType
    {
        Class,
        Method,
        Variable,
        Type,
        Instruction
    }

    /// <summary>
    /// Basic Record
    /// Contains a (string) name and a (long) RecordId
    /// </summary>
    public class Record
    {
        public RecordType Type { get; internal set; }
        public string Name { get; internal set; }
        public long RecordId { get; internal set; }
        public TypeForeignReference ForeignReference { get; internal set; }
    }

    /// <summary>
    /// Stores data required to resolve a reference
    /// </summary>
    public class TypeForeignReference
    {
        public string DataTypeName { get; internal set; }
        public long DataTypeId { get; internal set; }
    }
}