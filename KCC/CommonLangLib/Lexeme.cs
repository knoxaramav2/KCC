using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLangLib
{
    public enum Lexeme
    {
        //Data Types
        Int,
        Float,
        Double,
        Char,
        String,
        Function,
        Void,

        //Keywords
        
            //Variable creators
        NewInt,
        NewFloat,
        NewDouble,
        NewChar,
        NewString,
        NewFunction,
        NewVoid,

            //Control
        Continue,
        Break,
        If,
        Else,
        While,
        DoWhile,
        Goto,

        //Operators
            
            //Arithmetic
        Add,
        Sub,
        Mult,
        Div,
        Modulo,
        Power,
        Set,
        SetAdd,
        SetSub,
        SetMult,
        SetDiv,
        SetModulo,
        SetPower,

            //Unary
        IncPre,
        IncPost,
        DecPre,
        DecPost
    }
}
