namespace CodeTranslator
{
    /// <summary>
    /// Defines the internal numeric language of KCC
    /// 
    /// This defines the stage in where string representations
    /// of code are converted into numerical properties
    /// 
    /// Instructions are similar to assembly to allow for easier translation
    /// between target platforms
    /// 
    /// Syntax Notes:
    /// 
    /// Instruction with zero to two operands
    /// [Instruction] [Op?] [Op?]
    /// 
    /// Immediate mode instruction (Where I is the modifier
    /// [Instruction]I [Op] [Op?]  
    /// 
    /// 
    /// Modifiers (Note: These are as implemnted only)
    /// [Pre]I   :   Immediate value mode
    /// [Pre]U   :   Unsigned operation (no overflow)
    /// B[OP]    :   Bitwise
    /// B[OP]I   :   Bitwise and Immediate
    /// 
    /// 
    /// 
    /// </summary>

    //Internal instruction codes
    public enum InstOp
    {
        #region MISC_EXPAND
        RgMiscBlock = 1,

        NoOp,                   //Mock operation, no action



        #endregion

        #region ARITHMETIC
        RgArithmetic = 100,

        Add,                    //Numeric Add W Overflow
        AddI,                   //Numeric Immediate Add W Overflow
        Sub,                    //Numeric Subtract W Overflow
        SubI,                   //Numeric Subtract Immediate
        Mult,                   //Numeric Mult W Overflow
        MultI,                  //Numeric Mult Immediate
        Div,                    //Numeric Div W Overflow
        DivI,                   //Numeric Div Immediate

        Set,                    //Set byte
        SetI,                   //Set byte immediate
        SetW,                   //Set word
        SetWI,                  //Set word immediate

        #endregion

        #region MEM_SWAP
        RgMemSwap = 300,
        
        Ld,                     //Load byte
        LdU,                    //Load upper immediate
        LdW,                    //Load word
        MfH,                    //Move from HI
        MfL,                    //Move from LOW

        StB,                    //Store byte
        StW,                    //Store word

        ShL,                    //Shift Left
        ShR,                    //Shift Right

        #endregion

        #region LOGIC
        RgLogic = 500,

        Or,                     //Numeric OR
        And,                    //Numeric AND

        BOr,                    //Bitwise OR
        BAnd,                   //Bitwise AND
        BNot,                   //Bitwise NOT
        BInv,                   //Bitwise Invert
        BXor,                   //Bitwise Xor
        BXorI,                  //Bitwise Xor Immediate

        #endregion

        #region CONTROL
        RgControl = 800,

        Beq,                    //Branch if registers equal
        Blt,                    //Branch less than
        Bgt,                    //Branch greater than
        Bneq,                   //Branch not equal

        Jmp,                    //Jump to

        #endregion

        #region SYSTEM
        RgSystem,

        SysCall,                //Generate software interrupt
        

        #endregion

    }

    public enum OpModifier
    {
        None,
        Immediate
    }
}