using CommonLangLib;
using KCC;

namespace CodeTranslator
{
    public class LocaleArchFrags
    {
        enum Mode
        {
            InactMode,
            SectData
        }

        private Mode _mode;
        private ArchId _arch;
        private CliOptions _cli;

        public LocaleArchFrags(ArchId arch)
        {
            _arch = arch;
            _mode = Mode.InactMode;
            _cli = CliOptions.GetInstance();
        }

        public string GetLocaleDataHeader()
        {
            var header = $"    .file \"{_cli.OutputName}\"" +
                         $"    " +
                         $"    .text";
            return header;
        }

        public string GetGlobalData()
        {
            string ret = null;


            return ret;
        }

        public string GetFunctionDefs()
        {
            string ret = null;

            return ret;
        }
    }
}