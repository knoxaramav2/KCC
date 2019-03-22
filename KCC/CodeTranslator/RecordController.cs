using System;
using System.Collections;
using System.Collections.Generic;

namespace CodeTranslator
{
    public class RecordController <T>
    {
        public enum RecordOperationResult
        {
            Ok,
            RecordDoesNotExist,
            RecordDuplicate,
            RecordSaveError
        }

        private Dictionary<string,T> records;

        public RecordController ()
        {
            records = new Dictionary<string, T>();
        }

        public RecordOperationResult Add(string key, T value)
        {
            if (records.ContainsKey(key) == false)
            {
                return RecordOperationResult.RecordDuplicate;
            }

            try
            {
                records.Add(key, value);
            }
            catch (Exception e)
            {
                return RecordOperationResult.RecordSaveError;
            }
            

            return RecordOperationResult.Ok;
        }

        public T GetRecord(string recordName)
        {
            T ret;

            try
            {
                ret = records[recordName];
            }
            catch (Exception)
            {
                return default(T);
            }


            return ret;
        }
    }
}