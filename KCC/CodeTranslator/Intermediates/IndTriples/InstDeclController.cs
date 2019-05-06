namespace CodeTranslator
{
    public class InstDeclController
    {
        private IndInstTable _instTable;
        private SymbolAddrTable _symTable;

        private InstDeclController _self;

        private InstDeclController()
        {
            _instTable = new IndInstTable();
            _symTable = new SymbolAddrTable();
        }

        public InstDeclController GetInstance()
        {
            return _self ?? (_self = new InstDeclController());
        }

        public void AddTable()
        {

        }

    }
}