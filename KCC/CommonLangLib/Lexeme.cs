using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLangLib
{
    public enum Lexeme
    {
        //Misc Management

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
        DecPost,

        //Enclosures
        OpenBrace,
        OpenBracket,
        OpenParenth,

        CloseBrace,
        CloseBracket,
        CloseParenth
    }

    public static class LexemeHelpter
    {
        public static bool isCloseGroup(Lexeme lexeme)
        {
            return lexeme >= Lexeme.CloseBrace && lexeme <= Lexeme.CloseParenth;
        }

        public static bool isOpenGroup(Lexeme lexeme)
        {
            return lexeme >= Lexeme.OpenBrace && lexeme <= Lexeme.OpenParenth;
        }
    }
}
