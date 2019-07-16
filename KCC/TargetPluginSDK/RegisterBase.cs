using System;
using System.Collections.Generic;
using System.Text;

namespace TargetPluginSDK
{
    class RegisterBase
    {
        private List<Register> _registers;

        public RegisterBase()
        {
            _registers = new List<Register>();

            void AddRegister(string name, int width, string half=null, string quarter=null, string eigth=null)
            {
                _registers.Add(new Register(name, width, half, quarter, eigth));
            }
        }
    }

    class Register
    {
        private int _width;

        private string _full;
        private string _half;
        private string _quarter;
        private string _eight;

        internal Register(string name, int width, string half=null, string quarter=null, string eigth=null)
        {
            _width = width;

            _full = name;
            _half = half;
            _quarter = quarter;
            _eight = eigth;
        }

        public string PrepareFunctionArgs()
        {
            var ret = "";


            return ret;
        }
    }

    public enum RegType
    {
        INTEGER,
        LONG,
        FLOATING,
        POINTER,
    }
}
