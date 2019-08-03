using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TargetSDK2
{
    public class AsmCompat
    {
        private Format _format;
    }

    class InstructionAlias
    {
        public AsmCode Op;
        public string Arg0, Arg1;
        public int Offset0, Offset1;

        internal InstructionAlias(AsmCode op, string arg0, int offset0, string arg1, int offset1)
        {
            Op = op;
            Arg0 = arg0;
            Arg1 = arg1;
            Offset0 = offset0;
            Offset1 = offset1;
        }
    }

    public enum Format
    {
        ATT,//AT&T
        INTEL
    }

    public enum AsmCode
    {

    }
}
