using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLangLib
{
    public enum ErrorCode
    {
        Warning,
        //File Warnings
        FileRedundant,
        BadFileExtension,

        //Macro Warnings
        MacroRedefine,

        //Dependency Warnings
        ImportRedefine,

        //AST Validation Warnings
        

        Error,
        //File Errors
        FileNotFound,

        //Macro Errors
        MacroUndefined,

        //AST Validation Errors
        AbsentAssemblyBlock,
        AbsertFunctionBody,
        UnexpectedToken,

        //Data tables
        SymbolRedefinition,
        SymbolUndefined,

        //Platform
        UnrecognizedInstructionFamily,
        UnrecognizedOS,
        UnsupportedOS
    }

    public class ErrorReporter
    {
        private static ErrorReporter _self;
        private readonly List<ErrorDetails> _errorDetails;
        private ErrorReporter()
        {
            _errorDetails = new List<ErrorDetails>();

            FatalError = false;
        }

        public bool FatalError { get; private set; }

        public static ErrorReporter GetInstance()
        {
            return _self ?? (_self = new ErrorReporter());
        }

        public void Add(string msg, ErrorCode errorCode)
        {
            var errorDetails = new ErrorDetails(msg, errorCode);

            if (errorCode > ErrorCode.Error)
            {
                FatalError = true;
            }

            _errorDetails.Add(errorDetails);
        }

        public ErrorDetails Pop(bool erase=true)
        {
            if (_errorDetails.Count == 0)
            {
                return null;
            }

            var errorDetails = _errorDetails[0];

            if (erase) _errorDetails.Remove(errorDetails);

            return errorDetails;
        }

        public void PrintNext()
        {
            if (_errorDetails.Count == 0)
            {
                return;
            }

            var errorDetails = Pop();
            var cc = errorDetails.ErrorCode < ErrorCode.Error ? ConsoleColor.DarkYellow : ConsoleColor.DarkRed;
            ColorIO.WriteLineClr(errorDetails.Message, cc);
        }

        public void PrintAll()
        {
            while (_errorDetails.Count > 0)
            {
                PrintNext();
            }
        }

        public bool ValidateAndFlush()
        {
            PrintAll();
            return FatalError;
        }
    }

    public class ErrorDetails
    {
        public string Message;
        public ErrorCode ErrorCode;

        public ErrorDetails(string msg, ErrorCode errCode)
        {
            Message = msg;
            ErrorCode = errCode;
        }
    }
}
