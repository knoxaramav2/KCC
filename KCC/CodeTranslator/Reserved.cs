using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTranslator
{
    static class ReservedKw
    {
        //Declare a variable instance
        public static string Declare = "#DECLARE";
        //Return a value
        public static string Return = "#RETURN";

        //Invoke a non-internal function
        public static string Invoke = "#INVOKE";

        //Operators
        public static string Set = "#SET";

        public static string Not = "#NOT";

        public static string Pre_Inc = "#PRE_INC";
        public static string Post_Inc = "#POST_INC";
        public static string Pre_Dec = "#PRE_DEC";
        public static string Post_Dec = "#POST_DEC";

    }

    static class ReservedMeta
    {
        public static string ResultBuffer = "#TBUFF";
    }
}
