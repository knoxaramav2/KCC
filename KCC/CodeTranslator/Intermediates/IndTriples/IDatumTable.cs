namespace CodeTranslator
{
    public interface IDatumTable <T>
    {
        IDatumTable<T> AddTable(string id, string type);
        T AddRecord(T t);
        void ClearTable();
        T SearchRecord(string id);
    }
}