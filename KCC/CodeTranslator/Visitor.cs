﻿using System;
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
    
    internal class AssemblyVisitor : KCCBaseVisitor<object>
    {
        private List<object> _assemblies = new List<object>();

        public override object VisitAssembly(KCCParser.AssemblyContext context)
        {
            if (context.block_struct() == null)
            {
                ErrorReporter.GetInstance().Add("Missing assembly block", ErrorCode.AbsentAssemblyBlock);
                return null;
            }

            new BlockStructVisitor().VisitBlock_struct(context.block_struct());

            return base.VisitAssembly(context);
        }

        public override object VisitReturn(KCCParser.ReturnContext context)
        {
            return base.VisitReturn(context);
        }
    }

    internal class BlockStructVisitor : KCCBaseVisitor<object>
    {

        public override object VisitBlock_struct(KCCParser.Block_structContext context)
        {
            var body = context.inst_body();
            foreach (var b in body)
            {
                if (b.var_decl() != null)
                {
                    Console.WriteLine("Var decl found");
                } else if (b.fnc_proto() != null)
                {
                    Console.WriteLine("FncProto found");

                } else if (b.fnc_decl() != null)
                {
                    Console.WriteLine("FncDcl found");

                } else if (b.@class() != null)
                {
                    Console.WriteLine("Class found");
                } else
                {
                    //EMPTY
                }
            }

            return base.VisitBlock_struct(context);
        }

        
    }

    internal class ExecBlockVisitor : KCCBaseVisitor<object>
    {
        public override object VisitBlock_exec(KCCParser.Block_execContext context)
        {
            return base.VisitBlock_exec(context);
        }
    }

    internal class FunctionPrototypeVisitor : KCCBaseVisitor<KCCParser.Fnc_protoContext>
    {
        private readonly List<object> _prototypes = new List<object>();

        public override KCCParser.Fnc_protoContext VisitFnc_proto(KCCParser.Fnc_protoContext context)
        {


            return base.VisitFnc_proto(context);
        }
    }

    internal class FunctionDeclareVisitor : KCCBaseVisitor<KCCParser.Fnc_declContext>
    {
        public override KCCParser.Fnc_declContext VisitFnc_decl(KCCParser.Fnc_declContext context)
        {
            Console.WriteLine("Found Fnc Proto");

            return base.VisitFnc_decl(context);
        }
    }
}
