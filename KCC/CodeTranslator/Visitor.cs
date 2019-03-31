using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLangLib;

namespace CodeTranslator
{
    class SyntaxErrors
    {
        private static SyntaxErrors _this;

        public static SyntaxErrors GetInstance()
        {
            return _this ?? (_this = new SyntaxErrors());
        }

        private readonly Hashtable _errors = new Hashtable();
        private int _errs = 0;

        public void Add(Error err)
        {
            _errors.Add(_errs++, err.ToString());
        }

        public int Size()
        {
            return _errs;
        }

        public string GetErrors(int index)
        {
            if (index < 0 || index >= _errs) return "";
            return (string) _errors[index];
        }
    }
    
    class Error
    {
        private readonly String _msg;
        private readonly int _line;

        public Error(String msg, int lineNumber)
        {
            this._msg = msg;
            this._line = lineNumber;
        }

        public override String ToString()
        {
            return _line + " : " + _msg;
        } 
    }
    
    internal class KCC : KCCBaseVisitor<object>
    {
        public Db db { get; private set; }

        public KCC()
        {
            db = new Db();
        }

        public override object VisitAssembly(KCCParser.AssemblyContext context)
        {
            if (context.block_struct() == null)
            {
                ErrorReporter.GetInstance().Add("Missing assembly block", ErrorCode.AbsentAssemblyBlock);
                return null;
            }

            return base.VisitAssembly(context);
        }

        public override object VisitReturn(KCCParser.ReturnContext context)
        {
            return base.VisitReturn(context);
        }
    }
}
