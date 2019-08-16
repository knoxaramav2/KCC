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
        List<string> instructions;


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
        ATT,    //AT&T
        INTEL   //INTEL
    }

    //Size of data
    public enum D_SIZE
    {

    }

    public enum AsmCode
    {
        //movement: based on https://cs.brown.edu/courses/cs033/docs/guides/x64_cheatsheet.pdf
        MOV,        //Move
        PUSH,       //Push to stack
        POP,        //Pop stack
        W_2_DW,     //Word to double word
        DW_2_QW,    //Double word to quad word
        QW_2_OW,    //Quad word to octo word

        //unary: based on https://cs.brown.edu/courses/cs033/docs/guides/x64_cheatsheet.pdf
        INC,        //Increment
        DEC,        //Decrement
        NEG,        //Arithmetic Negation
        NOT,        //Bitwise compliment

        //binary: based on https://cs.brown.edu/courses/cs033/docs/guides/x64_cheatsheet.pdf
        LOAD_ADDR,  //Load address to destination
        ADD,        //Add src to dest
        SUB,        //Sub src from dest
        MULT,       //Multiply dest from src
        XOR,        //Bitwise XOR dest by src
        OR,         //Bitwise OR dest by src
        AND,        //Bitwise AND dest by src

        //shift: based on https://cs.brown.edu/courses/cs033/docs/guides/x64_cheatsheet.pdf
        SHFT_L,     //Shift n bits left
        A_SHFT_R,   //(sar) Shift right. Preserve high order bit 
        H_SHFT_R,   //(shr) Shift right. Set high order bit to 0

        //Special Arithmetic: based on https://cs.brown.edu/courses/cs033/docs/guides/x64_cheatsheet.pdf
        SMULTQ,     //Signed full multiply
        UMULTQ,     //Unsigned full multiply
        SDIVQ,      //Signed divide
        UDIVQ,      //Unsigned divide

        //Comparison: based on https://cs.brown.edu/courses/cs033/docs/guides/x64_cheatsheet.pdf


    }
}
