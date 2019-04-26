namespace CodeTranslator
{
    public class TypeTable
    {
        private static TypeTable _self;
        private static uint _recordCounter;

        private TypeTable()
        {
            _recordCounter = 1;
        }

        public static TypeTable GetInstance()
        {
            return _self ?? (_self = new TypeTable());
        }

        public bool AddTypeDefinition()
        {


            return true;
        }
    }
}