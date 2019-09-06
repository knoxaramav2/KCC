using System;
using System.Collections.Generic;
using System.Text;

namespace TargetPluginSDK
{
    class Stack
    {
        static Stack _self;

        public Stack GetInstance()
        {
            if (_self==null)
            {
                _self = new Stack();
            }

            return _self;
        }
    }

    class Frame
    {
        public string Name;
        public int Width;
    }

    class DCell
    {
        public string Name;
        public int Width;
        public int Offset;
    }
}
