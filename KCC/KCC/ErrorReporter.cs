using System;
using System.Collections.Generic;
using System.Text;

namespace KCC
{
    enum ErrorCode
    {
        WARNING,

        ERROR
    }

    class ErrorReporter
    {
        private readonly List<ErrorDetails> _errorDetails;

        public ErrorReporter()
        {
            _errorDetails = new List<ErrorDetails>();
        }

        public void Add(string msg, ErrorCode errorCode)
        {
            var errorDetails = new ErrorDetails(msg, errorCode);

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
            var cc = errorDetails.ErrorCode < ErrorCode.ERROR ? ConsoleColor.DarkYellow : ConsoleColor.DarkRed;
            ColorIO.WriteLineClr(errorDetails.Message, cc);
        }
    }

    internal class ErrorDetails
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
