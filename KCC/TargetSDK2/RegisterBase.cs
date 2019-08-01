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

            Register AddRegister(string name, ushort width)
            {
                var reg = new Register(name, width);
                _registers.Add(reg);
                return reg;
            }


        }
    }

    class Register
    {
        private string _name;

        public ushort MaxWidth { get; private set; }

        List<Tuple<string, int>> subRange;

        internal Register(string name, ushort width)
        {
            _name = name;
            MaxWidth = width;
        }

        Register AddSegment(string name, ushort width)
        {
            ushort rangeWidth = 0;
            //Check for duplicate segment name
            foreach(var t in subRange)
            {
                rangeWidth += width;
                if (t.Item1 == name)
                {
                    throw new RegisterNameDuplicateException(
                        $"Duplicate register name {name} in {_name}");
                }
            }

            //Check for out of range
            if (rangeWidth > MaxWidth)
            {
                throw new RegisterSegOutOfRangeException(
                    $"Register Segment {name} in {_name} out of range at ({rangeWidth}..{rangeWidth-width}) of ({MaxWidth}..0)"
                    );
            }


            return this;
        }
    }

    public enum RegType
    {
        ACCUMULATOR,
        GENERAL,
        STACK_POINTER,
        BASE_POINTER,
        STREAM,
        FLOATING,
        INSTRUCTION_RETURN,
        COUNTER,
        SOURCE,
        DESTINATION
    }

    //Exceptions

    public class RegisterSegOutOfRangeException : Exception
    {
        public RegisterSegOutOfRangeException(string msg) : base(msg)
        {
            
        }
    }

    public class RegisterNameDuplicateException : Exception
    {
        public RegisterNameDuplicateException(string msg) : base(msg)
        {

        }
    }
}
