using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLangLib
{
    public enum BitCntr{
        B8,
        BL8,
        BH8,
        B16,
        B32,
        B64
    }
    public static class AsmRef
    {
        public static string GetAccu(BitCntr b, bool hi = false)
        {
            switch (b)
            {
                case BitCntr.BL8: return "al";
                case BitCntr.BH8: return "ah";
                case BitCntr.B16: return "ax";
                case BitCntr.B32: return "eax";
                case BitCntr.B64: return "rax";
            }

            return null;
        }

        public static string GetCounter(BitCntr b)
        {
            switch (b)
            {
                case BitCntr.BL8: return "cl";
                case BitCntr.BH8: return "ch";
                case BitCntr.B16: return "cx";
                case BitCntr.B32: return "ecx";
                case BitCntr.B64: return "rcx";
            }

            return null;
        }

        public static string GetData(BitCntr b)
        {
            switch (b)
            {
                case BitCntr.BL8: return "dl";
                case BitCntr.BH8: return "dh";
                case BitCntr.B16: return "dx";
                case BitCntr.B32: return "edx";
                case BitCntr.B64: return "rdx";
            }

            return null;
        }

        public static string GetBase(BitCntr b)
        {
            switch (b)
            {
                case BitCntr.BL8: return "bl";
                case BitCntr.BH8: return "bh";
                case BitCntr.B16: return "bx";
                case BitCntr.B32: return "ebx";
                case BitCntr.B64: return "rbx";
            }

            return null;
        }

        public static string GetStackPntr(BitCntr b)
        {
            switch (b)
            {
                case BitCntr.B8: return "spl";
                case BitCntr.B16: return "sp";
                case BitCntr.B32: return "esp";
                case BitCntr.B64: return "rsp";
            }

            return null;
        }

        public static string GetBaseStackPntr(BitCntr b)
        {
            switch (b)
            {
                case BitCntr.B8: return "bpl";
                case BitCntr.B16: return "bp";
                case BitCntr.B32: return "ebp";
                case BitCntr.B64: return "rbp";
            }

            return null;
        }

        public static string GetSource(BitCntr b)
        {
            switch (b)
            {
                case BitCntr.B8: return "sil";
                case BitCntr.B16: return "s";
                case BitCntr.B32: return "esi";
                case BitCntr.B64: return "rsi";
            }

            return null;
        }

        public static string GetDestination(BitCntr b)
        {
            switch (b)
            {
                case BitCntr.B8: return "dil";
                case BitCntr.B16: return "di";
                case BitCntr.B32: return "edi";
                case BitCntr.B64: return "rdi";
            }

            return null;
        }

        public static string GetInstructionPointer(BitCntr b)
        {
            switch (b)
            {
                case BitCntr.B8: return "ipl";
                case BitCntr.B16: return "ip";
                case BitCntr.B32: return "eip";
                case BitCntr.B64: return "rip";
            }

            return null;
        }
    }
}
