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
        public static Db db { get; private set; }
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
            if (db == null)
            {
                db = new Db();
                graph = db.Graph;
            }
        }

        public Db GetDb()
        {
            return db;
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
                    var group = fncDcl.fnc_proto().decl_group();
                    var param = group.var_decl();

                    //saved first to fix argument declaration scope
                    db.SaveFunction(
                        fncDcl.fnc_proto().restric_id().GetText(),
                        fncDcl.fnc_proto().symbol_id().GetText());

                    string args="", defaults="";

                    foreach (var p in param)
                    {
                        var values = (string[]) VisitVar_decl(p);
                        if (values == null)
                        {
                            args += ",";
                            defaults += ",";
                        }
                        else
                        {
                            args += $"{values[0]} {values[1]},";// values[0] + ",";
                            defaults += values[2] + ",";
                        }
                        
                    }

                    //clear ending commas
                    if (args.Length > 0)
                    {
                        args = args.Trim(',');
                        defaults = defaults.Trim(',');
                        db.UpdateFunctionParams(args, defaults);
                    }

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
            if (context.var_decl() != null)
            {
                VisitVar_decl(context.var_decl());
            } else if (context.assignment() != null)
            {
                VisitAssignment(context.assignment());
            } else if (context.keywords() != null)
            {
                VisitKeywords(context.keywords());
            }
            else
            {
                Debug.PrintDbg("Instruction not defined");
                return null;
            }

            return null;
        }

        public override object VisitVar_decl(KCCParser.Var_declContext context)
        {

            var type = context.symbol_id()[0].GetText();
            var id = context.symbol_id()[1].GetText();
            string defaultValue = null;

            if (context.assignment() != null)
            {
                if (context.assignment().assign_ops().GetText() != "=")
                {
                    ErrorReporter.GetInstance().Add($"Unexpected {context.assignment().assign_ops().GetText()}"
                                                    , ErrorCode.UnexpectedToken);
                    return null;
                }

                //Should be #TBUFF if not immediate value 
                defaultValue = (string) VisitAssignment(context.assignment());
            }

            //If value is not immediate, declare variable after expression is determined
            if (defaultValue == ReservedMeta.ResultBuffer)
            {
                db.SaveInstruction(ReservedKw.Declare, id, defaultValue);
                db.SaveVariable(id, type, null);
            }
            //If value is immediate, set as default value
            else
            {
                db.SaveInstruction(ReservedKw.Declare, id);
                //db.SaveInstruction(ReservedKw.Set, id, defaultValue);
                db.SaveVariable(id, type, defaultValue);
            }

            return new [] {type, id, defaultValue};
        }

        public override object VisitBlock_exec(KCCParser.Block_execContext context)
        {
            var instructions = context.instruction();

            foreach (var instruction in instructions)
            {
                VisitInstruction(instruction);
            }

            return null;
        }

        /// <summary>
        /// Processes expression steps in an assignment
        /// </summary>
        /// <param name="context"></param>
        /// <returns>
        /// Returns either an immediate value, or saves a set of instructions
        /// to be stored in #TBUFF
        /// </returns>
        public override object VisitAssignment(KCCParser.AssignmentContext context)
        {
            string value = null;
            var expression = context.expression();
            //check for immediate value
            if (expression.value_id() != null)
            {
                return expression.value_id().GetText();
            }

            if (expression.unary_expr() != null)
            {
                
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
            string result;

            if (context.expression() != null)
            {
                result = (string) VisitExpression(context.expression());
            }
            else
            {
                result = ReservedMeta.ResultBuffer;
            }

            db.SaveInstruction(ReservedKw.Return, result);

            return null;
        }

        //Note: Return reference to final expression result for chaining
        //if result not saved to variable, return as #TBUFF (temporary result buffer)
        public override object VisitExpression(KCCParser.ExpressionContext context)
        {

            string res = null;

            if (context.value_id() != null)
            {
                res = context.value_id().GetText();
            }
            else if (context.unary_expr() != null)
            {
                VisitUnary_expr(context.unary_expr());
                res = ReservedMeta.ResultBuffer;

            } else if (context.fnc_call() != null)
            {
                VisitFnc_call(context.fnc_call());
                res = ReservedMeta.ResultBuffer;
            }
            else
            {
                Debug.PrintDbg("Unimplimented expression");
            }

            return res;
        }

        public override object VisitFnc_call(KCCParser.Fnc_callContext context)
        {
            string args = null;
            var parameters = context.call_group().value_id();

            if (parameters != null)
            {
                args = parameters.Aggregate("", (current, p) => current + (p.GetText() + ","));
                args = args.TrimEnd(',');
            }

            db.SaveInstruction(ReservedKw.Invoke, context.symbol_id().GetText(), args);

            return null;
        }

        public override object VisitUnary_expr(KCCParser.Unary_exprContext context)
        {
            Debug.PrintDbg("Visiting Unary");

            if (context.lv_unary_ops() != null)
            {
                VisitLv_unary_ops(context.lv_unary_ops());
                return null;
            }

            if (context.rv_unary_ops() != null)
            {
                VisitRv_unary_ops(context.rv_unary_ops());
                return null;
            }

            var op = "";
            var operand = context.symbol_id().GetText();

            switch (context.unary_symb().GetText())
            {
                case "!":
                    op = ReservedKw.Not;
                    break;
                default:
                    Debug.PrintDbg($"Undefined unary expression {context.unary_symb().GetText()}");
                    return null;
            }

            db.SaveInstruction(op, operand);

            return null;
        }

        public override object VisitLv_unary_ops(KCCParser.Lv_unary_opsContext context)
        {
            db.SaveInstruction(context.increment() != null ? ReservedKw.Pre_Inc : ReservedKw.Pre_Dec,
                context.symbol_id().GetText());
            return null;
        }

        public override object VisitRv_unary_ops(KCCParser.Rv_unary_opsContext context)
        {
            db.SaveInstruction(context.increment() != null ? ReservedKw.Post_Inc : ReservedKw.Post_Dec,
                context.symbol_id().GetText());
            return null;
        }
    }
}
