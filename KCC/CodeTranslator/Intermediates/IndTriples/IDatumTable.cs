namespace CodeTranslator
{
    public interface IDatumTable <T>
    {
        IDatumTable<T> AddTable();
        T AddRecord(T t);
        void ClearTable();
        T SearchRecord(string id);
    }
}