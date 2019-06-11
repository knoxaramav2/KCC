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

        Set,
        SetAdd,
        SetSub,
        SetMult,
        SetDiv,
        SetModulo,
        SetAnd,
        SetOr,
        SetXor,

        Add,
        Sub,
        Mult,
        Div,
        Modulo,
        Power

        PostIncrement,
        PostDecrement,
        PreIncrement,
        PreDecrement,
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

        IndexAccess,

        #endregion

        #region LOGIC
        RgLogic = 500,

        Or,                     //Numeric OR
        And,                    //Numeric AND
        Not,

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

        Call,                   //Invoke function

        Exit,

        #endregion

        #region SYSTEM
        RgSystem,

        SysCall,                //Generate software interrupt

        StartFncCall,   //Mark the start of function call expressions
        EndFncCall,     //Mark the end of function call expressions; evaluate expressions and determine result registers
        MarkAsArg,      //Mark previous expression as argument; Delimits argument evaluation
        //IG: [START] [Var] [MARK] [EXPRESSION] [MARK] [VAR] [MARK] [END]

        #endregion

        //For temporary operations
        #region DevCodes

        Print,

        #endregion

        #region Primitives

        //Primitive Constructions
        Int,
        CString,

        #endregion

        #region Directives

        Lcn,                    //create an LC[n] directive

        #endregion
    }

    public enum OpModifier
    {
        None,
        Immediate,
        FromLastTemp,
        NullOrDefault,
        LTempRTemp,
        LRawRTemp,
        LTempRRaw,
        LRawRRaw
    }
}