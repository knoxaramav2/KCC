using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLangLib
{
    public interface IFileProcessor<in TI, out TO>
    {
        TO Execute(TI input);
    }

    public class FileProcessException : Exception
    {
        public FileProcessError ErrorCode;

        public FileProcessException(string msg, FileProcessError error) : base(msg)
        {
            ErrorCode = error;
        }
    }

    public enum FileProcessError
    {

    }
}
