namespace CodeTranslator
{
    public class ClassTable : RecordController<ClassRecord>
    {
        private AssemblyRegistry _assemblyRegistry;

        public ClassTable()
        {
            _assemblyRegistry = AssemblyRegistry.GetInstance();
        }

        public override RecordOperationResult Add(string key, object value)
        {
            base.Add(key, (ClassRecord) value);

            return RecordOperationResult.Ok;
        }

    }

    public class ClassRecord : Record
    {
        public ClassRecord()
        {

        }

        public TypeForeignReference ForeignReference { get; internal set; }
    }
}