using CodeTranslator;

namespace Compiler
{
    /// <summary>
    /// Creates assembly files from data
    /// </summary>
    public class Converter
    {
        Db _db;

        public Converter()
        {
            
        }

        public void CreateAssembly(Db db)
        {
            this._db = db;

            //TODO Choose target architect/OS
        }

        public void TargetWin10AMD()
        {

        }
    }


}