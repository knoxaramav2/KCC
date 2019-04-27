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

        public LocaleArchFrags(ArchId arch)
        {
            _arch = arch;
            _mode = Mode.InactMode;
        }

        public string GetLocaleDataHeader()
        {
            switch (_arch)
            {
                case ArchId.X86:
                    return "section .data";
                        
                    default:
                        return null;
            }
        }
    }
}