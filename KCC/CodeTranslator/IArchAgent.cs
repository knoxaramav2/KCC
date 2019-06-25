namespace CodeTranslator
{
    /// <summary>
    /// Provides interface for architecture target
    /// </summary>
    public interface IArchAgent
    {
        void Init(InstDeclController graph);

        /// <summary>
        /// Returns the full assembly file
        /// </summary>
        /// <returns></returns>
        string GetAll();

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
        /// Returns a list of externally defined (or undefined) methods
        /// </summary>
        /// <returns></returns>
        string GetExternalFunctionDeclarations();

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
        string FormatInstruction(InstOp opcode, string arg0, string arg1, string spcl1, string spcl2, OpModifier mode);

        /// <summary>
        /// Invokes a system call to a local assembler to generate the executable
        /// </summary>
        /// <param name="asmPath">Path the source assembly</param>
        /// <returns>False if errors occured</returns>
        bool InvokeLocalAssembler(string asmPath);

        string GetEpilogue();
    }
}