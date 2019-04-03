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

    internal class KccVisitor : KCCBaseVisitor<object>
    {
        public Db db { get; private set; }
        private ProgramGraph graph { get; }
        private SqlBuilder _sqlBuilder;

        private void levelUpScope(string scope)
        {

        }

        private void levelDownScope(string scope)
        {

        }

        public KccVisitor()
        {
            db = new Db();
            graph = db.Graph;
        }

        public override object VisitAssembly(KCCParser.AssemblyContext context)
        {
            if (context.block_struct() == null)
            {
                ErrorReporter.GetInstance().Add("Missing assembly block", ErrorCode.AbsentAssemblyBlock);
                return null;
            }

            db.SaveAssembly(context.symbol_id().GetText());

            VisitBlock_struct(context.block_struct());

            graph.LeaveScope();

            return null;
        }

        public override object VisitBlock_struct(KCCParser.Block_structContext context)
        {
            foreach (var inst in context.inst_body())
            {
                var varDecl = inst.var_decl();
                var fncProto = inst.fnc_proto();
                var fncDcl = inst.fnc_decl();
                var classDcl = inst.@class();

                if (varDecl != null)
                {

                } else if (fncDcl != null)
                {
                    var group = fncDcl.fnc_proto().@group();
                    var instructions = group.instruction();

                    //var args = instructions.Aggregate("",
                     //   (current, instruction) => current + (string[]) VisitInstruction(instruction));

                    string args="", defaults="";

                    foreach (var instruction in instructions)
                    {
                        var values = (string[]) VisitInstruction(instruction);
                        if (values == null) continue;
                        args += values[0] + ",";
                        defaults += values[1] + ",";
                    }

                    //clear ending commas
                    if (args.Length > 0)
                    {
                        args = args.Remove(args.Length - 1);
                        defaults = defaults.Remove(defaults.Length - 1);
                    }
                    
                    db.SaveFunction(
                        fncDcl.fnc_proto().restric_id().GetText(),
                        fncDcl.fnc_proto().symbol_id().GetText(),
                        args, defaults);

                    //Process and save executable instructions
                    VisitBlock_exec(inst.fnc_decl().block_exec());

                    graph.LeaveScope();

                } else if (fncProto != null)
                {

                } else if (classDcl != null)
                {

                } else
                {
                    Debug.PrintDbg("BlockStruct Unexepected : " + inst);
                }
            }

            return null;
        }

        public override object VisitInstruction(KCCParser.InstructionContext context)
        {
            var r1 = context.var_decl();

            if (r1 != null)
            {
                return VisitVar_decl(r1);
            }

            return null;
        }

        public override object VisitVar_decl(KCCParser.Var_declContext context)
        {
            var args = new[] {"", ""};
            var r1 = context;
            string defaultValue = null;

            if (r1.assignment() != null)
            {
                defaultValue = r1.assignment().value_id().GetText();
            }

            db.SaveVariable(
                r1.symbol_id()[1].GetText(),
                r1.symbol_id()[0].GetText(),
                defaultValue);

            args[0] = r1.symbol_id()[1].GetText();
            args[1] = defaultValue;

            return args;
        }

        public override object VisitBlock_exec(KCCParser.Block_execContext context)
        {
            var instructions = context.inst_exec();

            foreach (var instruction in instructions)
            {
                VisitInst_exec(instruction);
            }

            return null;
        }

        public override object VisitInst_exec(KCCParser.Inst_execContext context)
        {
            if (context.instruction() != null)
            {

            }
            else if (context.keywords() != null) //keywords
            {
                VisitKeywords(context.keywords());
            }
            else
            {
                Debug.PrintDbg("Instruction not implemented");
                return null;
            }

            return null;
        }

        public override object VisitKeywords(KCCParser.KeywordsContext context)
        {
            if (context.@return() != null)
            {
                VisitReturn(context.@return());
            }
            else
            {
                Debug.PrintDbg("Unrecognized keyword");
            }

            return null;
        }

        public override object VisitReturn(KCCParser.ReturnContext context)
        {
            string result = null;

            if (context.expression() != null)
            {
                result = (string) VisitExpression(context.expression());
            }
            else
            {
                result = "#TBUFF";
            }

            db.SaveInstruction("return", result);

            return null;
        }

        //Note: Return reference to final expression result for chaining
        //if result not saved to variable, return as #TBUFF (temporary result buffer)
        public override object VisitExpression(KCCParser.ExpressionContext context)
        {
            Debug.PrintDbg("Visiting Expression");


            if (context.symbol_id() != null)
            {
                return context.symbol_id().GetText();
            } else if (context.unary_expr() != null)
            {
                VisitUnary_expr(context.unary_expr());
            }
            else
            {
                Debug.PrintDbg("Unimplimented expression");
                return null;
            }

            return null;
        }

        public override object VisitUnary_expr(KCCParser.Unary_exprContext context)
        {
            Debug.PrintDbg("Visiting Unary");


            var retObj = context.symbol_id().GetText();
            var opFirst = true;

            var firstToken = context.start.Text;
            if (firstToken == retObj)
            {
                opFirst = false;
            }

            switch (context.unary_ops().GetText())
            {
                case "++":

                    break;
                case "--":
                    break;
                default:
                    Debug.PrintDbg("Unrecognized unary operator : " + context.unary_ops().GetText());
                    break;
            }

            return retObj;
        }
    }
}
