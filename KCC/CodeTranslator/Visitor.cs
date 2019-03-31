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

    internal class ScopeBlock
    {
        public string Name { get; private set; }
        private ScopeBlock _parent, _child, _previous, _next;

        private bool HasSameNameSibling(string name)
        {
            ScopeBlock left, right;
            left = this._previous;
            right = this._next;

            while (left != null && right != null)
            {
                if (left != null)
                {
                    left = left._previous;
                }

                if (right != null)
                {
                    right = right._next;
                }

                if ((left.Name == name) || (right.Name == name))
                {
                    return true;
                }
            }

            return false;
        }

        public ScopeBlock(string name)
        {
            _parent = _child = _previous = _next = null;
            this.Name = name;
        }

        public string GetScopeString()
        {
            var current = this;
            var ret = "";

            while (current != null)
            {
                ret += current.Name + ret;

                if (current._child != null)
                {
                    ret += "." + ret;
                }

                current = current._child;
            }

            return ret;
        }

        public ScopeBlock MakeChild(string name)
        {
            var child = new ScopeBlock(name);

            this._child = child;
            _child._parent = this;

            return _child;
        }

        public ScopeBlock MakeSibling(string name)
        {
            if (HasSameNameSibling(name))
            {
                return null;
            }

            var sibling = new ScopeBlock(name);

            //All siblings share a parent
            sibling._parent = _parent;

            this._next = sibling;
            sibling._previous = this;

            return sibling;
        }

        public ScopeBlock GetParent()
        {
            return _parent;
        }

        public ScopeBlock GetChild()
        {
            return _parent;
        }
    }

    internal class KccVisitor : KCCBaseVisitor<object>
    {
        public Db db { get; private set; }
        private SqlBuilder _sqlBuilder;

        private ScopeBlock _currentScope;

        private void levelUpScope(string scope)
        {

        }

        private void levelDownScope(string scope)
        {

        }

        public KccVisitor()
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

            //first scope should always be an assembly
            if (_currentScope == null)
            {
                _currentScope = new ScopeBlock(context.symbol_id().GetText());
            }

            db.SaveAssembly(context.symbol_id().GetText());

            VisitBlock_struct(context.block_struct());

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
                    Debug.PrintDbg("Found " + _currentScope.GetScopeString()+"."+fncDcl.fnc_proto().restric_id().GetText());

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
                        _currentScope.GetScopeString(),
                        fncDcl.fnc_proto().symbol_id().GetText(),
                        args, defaults);

                    //update scope to function body
                    _currentScope = _currentScope.MakeChild(fncDcl.fnc_proto().restric_id().GetText());

                    //Process and save executable instructions
                    VisitBlock_exec(inst.fnc_decl().block_exec());

                    //restore scope
                    _currentScope = _currentScope.GetChild();

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

            Debug.PrintDbg($"Declaration found : {r1.symbol_id()[0].GetText()} {r1.symbol_id()[1].GetText()}");

            string defaultValue = null;

            if (r1.assignment() != null)
            {
                defaultValue = r1.assignment().value_id().GetText();
            }

            db.SaveVariable(
                r1.symbol_id()[1].GetText(),
                _currentScope.GetScopeString(),
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
            //COMMAND, LVAL (ARG0), RVAL (ARG1)
            var instruction = new [] {"","",""};

            var r1 = context.instruction();
            var r2 = context.keywords();

            if (r1 != null)
            {
                VisitInstruction(r1);
            } else if (r2 != null)
            {
                var result = (string[]) VisitKeywords(r2);
                instruction[0] = result[0];
                instruction[1] = result[1];
            }

            if (instruction[0] != "")
            {
                db.SaveInstruction(_currentScope.GetScopeString(), instruction[0], instruction[1], instruction[2]);
            }

            return instruction;
        }

        public override object VisitKeywords(KCCParser.KeywordsContext context)
        {

            if (context.@return() != null)
            {
                return new [] { "return", (string) VisitReturn(context.@return()) };
            }
            else
            {
                Debug.PrintDbg("Unrecognized keyword");
            }

            return new[] {"", ""};
        }

        public override object VisitReturn(KCCParser.ReturnContext context)
        {
            var result = "";

            if (context.expression() != null)
            {
                result = (string) VisitExpression(context.expression()) ?? "#TBUFF";
            }

            db.SaveInstruction(_currentScope.GetScopeString(), "return", result);

            return result;
        }

        //Note: Return reference to final expression result for chaining
        //if result not saved to variable, return as #TBUFF (temporary result buffer)
        public override object VisitExpression(KCCParser.ExpressionContext context)
        {

            if (context.symbol_id() != null)
            {
                return context.symbol_id().GetText();
            } else if (context.unary_expr() != null)
            {
                VisitUnary_expr(context.unary_expr());
            }

            return null;
        }

        public override object VisitUnary_expr(KCCParser.Unary_exprContext context)
        {
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
