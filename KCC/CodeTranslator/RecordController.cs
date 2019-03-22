using System;
using System.Collections.Generic;

namespace CodeTranslator
{
    public abstract class RecordController <T>
    {
        public enum RecordOperationResult
        {
            Ok,
            RecordDoesNotExist,
            RecordDuplicate,
            RecordSaveError
        }

        private readonly Dictionary<string,T> _records;
        

        protected RecordController ()
        {
            _records = new Dictionary<string, T>();
        }

        public virtual RecordOperationResult Add(string key, T value)
        {
            if (_records.ContainsKey(key) == false)
            {
                return RecordOperationResult.RecordDuplicate;
            }

            try
            {
                _records.Add(key, value);
            }
            catch (Exception)
            {
                return RecordOperationResult.RecordSaveError;
            }
            

            return RecordOperationResult.Ok;
        }

        public virtual T GetRecord(string recordName)
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

    /// <summary>
    /// Basic Record
    /// Contains a (string) name and a (long) RecordId
    /// </summary>
    public class Record
    {
        public string Name { get; internal set; }
        public long RecordId { get; internal set; }
    }
}