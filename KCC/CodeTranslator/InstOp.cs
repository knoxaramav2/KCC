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


        #endregion

        #region MEM_SWAP
        RgMemSwap = 300,


        #endregion

        #region LOGIC
        RgLogic = 500,

        Or,                     //Numeric OR
        And,                    //Numeric AND

        BOr,                    //Bitwise OR
        BAnd,                   //Bitwise AND

        #endregion

        #region CONTROL
        RgControl = 800,

        Beq,                    //Branch if registers equal


        #endregion

    }
}