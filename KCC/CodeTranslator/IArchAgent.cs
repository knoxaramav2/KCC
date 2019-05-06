namespace CodeTranslator
{
    /// <summary>
    /// Provides interface for architecture target
    /// </summary>
    public interface IArchAgent
    {
        void Init(InstDeclController graph);

        /// <summary>
        /// Generates assembly header
        /// </summary>
        /// <returns>Formatted header assembly</returns>
        string GetHeader();

        /// <summary>
        /// Generate global data section
        /// </summary>
        /// <returns>Return formatted data section</returns>
        string GetGlobals();

        /// <summary>
        /// Generate constants fields
        /// </summary>
        /// <returns>Return formatted constant data fields</returns>
        string GetConstData();

        /// <summary>
        /// Generate function declared within current assembly
        /// </summary>
        /// <returns>Returns formatted function fields</returns>
        string GetFunctionDefs();

        /// <summary>
        /// Convert an opcode and accompanying info into a line instruction
        /// </summary>
        /// <param name="opcode">KCC internal code for instruction</param>
        /// <param name="arg0">First argument</param>
        /// <param name="arg1">Second argument</param>
        /// <param name="spcl1">Special argument modifier 1</param>
        /// <param name="spcl2">Special argument modifier 2</param>
        /// <param name="mode">Instruction modifier</param>
        /// <returns>Formatted string representing target instruction translation</returns>
        string FormatInstruction(InstOp opcode, int arg0, int arg1, int spcl1, int spcl2, InstOp mode);
    }
}