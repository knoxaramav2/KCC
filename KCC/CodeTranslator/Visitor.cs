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

        public ScopeBlock MakeParent(string name)
        {
            var parent = new ScopeBlock(name);

            this._parent = parent;
            _parent._child = this;

            return parent;
        }

        public ScopeBlock MakeSibling(string name)
        {
            if (HasSameNameSibling(name))
            {
                return null;
            }

            var sibling = new ScopeBlock(name);

            this._next = sibling;
            sibling._previous = this;

            return sibling;
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

                    var args = instructions.Aggregate("",
                        delegate(string current, KCCParser.InstructionContext instruction)
                        {
                            return current + (string) VisitInstruction(instruction);
                        });

                    db.SaveFunction(
                        fncDcl.fnc_proto().restric_id().GetText(),
                        _currentScope.GetScopeString(),
                        fncDcl.fnc_proto().symbol_id().GetText(),
                        args);
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
            Debug.PrintDbg($"Instruction found : {context.var_decl().symbol_id()[0].GetText()} {context.var_decl().symbol_id()[1].GetText()}");
            var defaultValue = "";

            if (context.var_decl().assignment() != null)
            {
                defaultValue = context.var_decl().assignment().value_id().GetText();
            }

            var ids = context.var_decl().symbol_id();
            return $"{ids[0].GetText()} {ids[1].GetText()},";

            //return base.VisitInstruction(context);
        }

        public override object VisitVar_decl(KCCParser.Var_declContext context)
        {
            var args = new List<string>();



            return args.ToArray();
        }

        public override object VisitReturn(KCCParser.ReturnContext context)
        {
            return base.VisitReturn(context);
        }
    }
}
