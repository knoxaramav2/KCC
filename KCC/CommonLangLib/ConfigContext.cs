using System.Collections.Generic;

namespace CommonLangLib
{
    //MORE SINGELTONS

    /*
     * Holds information identified by pre-processor
     */
    public class ConfigContext
    {
        private ConfigContext _self;

        public List<Macro> Macros { get; }

        private ConfigContext()
        {
            Macros = new List<Macro>();
        }

        public ConfigContext GetInstance()
        {
            return _self ?? (_self = new ConfigContext());
        }

        public void AddMacro(string alias, string symbol)
        {
            var errorReporter = ErrorReporter.GetInstance();

            foreach (var macro in Macros)
            {
                if (macro.Alias != alias) continue;
                errorReporter.Add("Macro " + alias + "already defined", ErrorCode.MacroRedefine);
                return;
            }

            Macros.Add(new Macro(alias, symbol));
        }
    }

    public class Macro
    {
        public string Alias;    // Macro symbol
        public string Symbol;   // Literal symbol(s)

        public Macro(string alias, string symbol)
        {
            Alias = alias;
            Symbol = symbol;
        }
    }

    public class ImportManifest
    {
        
    }
}
