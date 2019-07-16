using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTranslator.Intermediates.IndTriples
{
    class FauxReg
    {
        private List<FauxUnit> _units;

        /// <summary>
        /// Allows a register of a type to be allocated.
        /// If no registers are currently available, a new register is generated and returned.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        FauxUnit RequestRegister(FauxRegType type)
        {
            FauxUnit unit = null;
            short maxId = -1;

            foreach(var fu in _units)
            {
                if (fu.Type != type)
                {
                    continue;
                }

                maxId = fu.TypeIndex;

                if (!fu.MarkedUsed)
                {
                    unit = fu;
                    break;
                }
            }

            if (unit == null)
            {
                unit = new FauxUnit();
                unit.Type = type;
                unit.TypeIndex = ++maxId;
                unit.Set();
            }

            return unit;
        }

        /// <summary>
        /// Mark register as free to use
        /// </summary>
        /// <param name="unit"></param>
        void ReleaseRegister(FauxUnit unit)
        {
            unit.Clear();
        }
    }

    public class FauxUnit
    {
        public FauxRegType Type { get; internal set; }
        public bool MarkedUsed { get; private set; }
        public short TypeIndex { get; internal set; }

        public void Set()
        {
            MarkedUsed = true;
        }
        public void Clear()
        {
            MarkedUsed = false;
        }
    }

    public enum FauxRegType
    {
        BYTE,
        WORD,
        DWORD,
        QWORD,
        DQWORD,
        FLOAT,
        DOUBLE
    }
}
