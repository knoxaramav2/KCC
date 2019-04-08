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
                    VisitVar_decl(varDecl);
                } else if (fncDcl != null)
                {
                    //saved first to fix argument declaration scope
                    VisitFnc_decl(fncDcl);
                } else if (fncProto != null)
                {
                    ErrorReporter.GetInstance().Add("Prototype without block not supported", ErrorCode.AbsertFunctionBody);
                } else if (classDcl != null)
                {
                    VisitClass(classDcl);
                } else
                {
                    Debug.PrintDbg("BlockStruct Unexepected : " + inst);
                }
            }

            return null;
        }

        public override object VisitFnc_decl(KCCParser.Fnc_declContext context)
        {
            VisitFnc_proto(context.fnc_proto());
            VisitBlock_exec(context.block_exec());
            graph.LeaveScope();
            return null;
        }

        public override object VisitFnc_proto(KCCParser.Fnc_protoContext context)
        {
            var inst = (string[][]) VisitDecl_group(context.decl_group());

            var args = "";
            var defs = "";

            foreach (var res in inst)
            {
                args += res[0] + ",";
                defs += res[1] + ",";
            }

            args = args.Trim(',');
            defs = defs.Trim(',');

            db.SaveFunction(context.restric_id().GetText(), context.symbol_id().GetText(), args, defs);
            return null;
        }

        /// <summary>
        /// Visits parameter header for function and returns parameter declarations and default values
        /// </summary>
        /// <param name="context"></param>
        /// <returns>
        /// [0] "type id, type id,..."
        /// [1] "val1, val2, ..."
        /// </returns>
        public override object VisitDecl_group(KCCParser.Decl_groupContext context)
        {
            return context.var_proto_decl().Select(pdecl => (string[]) VisitVar_proto_decl(pdecl)).ToArray();
        }

        public override object VisitVar_proto_decl(KCCParser.Var_proto_declContext context)
        {
            var decl = "";
            var def = "";
            var ret = new string[2];

            decl = $"{context.symbol_id()[0].GetText()} {context.symbol_id()[1].GetText()}";
            def = null;

            if (context.value_id() != null)
            {
                def = context.value_id().GetText();
            } else if (context.symbol_id().Length == 3)
            {
                def = context.symbol_id()[2].GetText();
            }

            ret[0] = decl;
            ret[1] = def;

            return ret;
        }

        /// <summary>
        /// Visits executable sections and accepts return values in the form
        /// of [op] [arg0] [arg1]
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitInstruction(KCCParser.InstructionContext context)
        {
            var instructions = new List<string[]>();

            if (context.var_decl() != null)
            {
                instructions = (List<string[]>) VisitVar_decl(context.var_decl());
            } else if (context.assignment() != null)
            {
                instructions = (List<string[]>) VisitAssignment(context.assignment());
            } else if (context.keywords() != null)
            {
                VisitKeywords(context.keywords());
            }
            else
            {
                Debug.PrintDbg("Instruction not defined");
                return null;
            }

            foreach (var i in instructions)
            {
                db.SaveInstruction(i[0], i[1], i[2]);
            }

            return null;
        }

        public override object VisitVar_decl(KCCParser.Var_declContext context)
        {
            var type = context.symbol_id()[0].GetText();
            var id = context.symbol_id()[1].GetText();
            string defaultValue = null;
            var instructions = new List<string[]>();

            //initial declaration
            instructions.Add(new []{ReservedKw.Declare, type, id});
            db.SaveVariable(id, type);

            if (context.assignment() != null)
            {
                if (context.assignment().assign_ops().GetText() != "=")
                {
                    ErrorReporter.GetInstance().Add($"Unexpected token {context.assignment().assign_ops().GetText()}", ErrorCode.UnexpectedToken);
                    return null;
                }

                var result = (List<string[]>) VisitAssignment(context.assignment());
                foreach (var r in result)
                {
                    instructions.Add(r);
                }
            }

            return instructions;
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
            var instructions = new List<string[]>();
            string op = null;
            var raw_op = context.assign_ops().GetText();
            string recipient = null;
            var value = "";

            
            if (context.symbol_id() != null)
            {
                //assign to pre-declared object
                recipient = context.symbol_id().GetText();
            }
            else
            {
                //assign as initializer
                recipient = db.Graph.GetLastDeclared();
            }

            if (recipient == null)
            {
                //TODO Check for correctness
                ErrorReporter.GetInstance().Add("Unable to assign object", ErrorCode.Error);
            }

            value = (string) VisitExpression(context.expression());

            switch (raw_op)
            {
                case "=":
                    op = ReservedKw.Set;
                    break;
                case "+=":
                    op = ReservedKw.SetSum;
                    break;
                case "-=":
                    op = ReservedKw.SetDiff;
                    break;
                case "*=":
                    op = ReservedKw.SetProduct;
                    break;
                case "/=":
                    op = ReservedKw.SetQuotient;
                    break;
            }

            instructions.Add(new []{op, recipient, value});

            return instructions;
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
