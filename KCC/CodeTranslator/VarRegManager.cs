using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTranslator
{
    class VarRegManager
    {
        private List<Register> _registers;

        public VarRegManager()
        {
            _registers = new List<Register>();
        }

        public bool AddRegister(string name, int width)
        {
            foreach (var r in _registers)
            {
                if (name == r.Name)
                {
                    return false;
                }
            }

            _registers.Add(new Register(name, width));

            return true;
        }
    }

    class Register
    {
        public int Width { get; internal set; }
        public string Name { get; internal set; }

        public Register(string name, int width)
        {
            Name = name;
            Width = width;
        }
    }

    class RegSegment
    {
        public int Width { get; internal set; }
        public string Name { get; internal set; }
        
    }
}
