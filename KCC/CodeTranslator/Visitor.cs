using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using CommonLangLib;

namespace CodeTranslator
{
    internal class KccVisitor : KCCBaseVisitor<object>
    {
        private InstDeclController _controller;

        public KccVisitor()
        {
           _controller = InstDeclController.GetInstance();
        }

        //assembly
        public override object VisitAssembly_decl([NotNull] KCCParser.Assembly_declContext context)
        {
            var sym = context.symbol_id().GetText();
            _controller.CreateScope(sym, "asm", BodyType.Asm);

            VisitAsm_block(context.asm_block());

            return base.VisitAssembly_decl(context);
        }

        public override object VisitAsm_block([NotNull] KCCParser.Asm_blockContext context)
        {

            foreach(var class_def in context.class_decl())
            {
                VisitClass_decl(class_def);
            }

            foreach (var function_def in context.function_decl())
            {
                VisitFunction_decl(function_def);
            }

            return base.VisitAsm_block(context);
        }

        //classes
        public override object VisitClass_decl([NotNull] KCCParser.Class_declContext context)
        {
            return base.VisitClass_decl(context);
        }

        //functions
        public override object VisitFunction_decl([NotNull] KCCParser.Function_declContext context)
        {
            _controller.CreateScope(context.symbol_id()[1].GetText(), context.symbol_id()[0].GetText(), BodyType.Function);
            VisitFnc_header(context.fnc_header());
            VisitFunction_block(context.function_block());

            _controller.ExitScope();
            return null;
        }

        public override object VisitFnc_header([NotNull] KCCParser.Fnc_headerContext context)
        {
            foreach(var v in context.var_decl())
            {
                VisitVar_decl(v);
            }

            return null;
        }

        public override object VisitFunction_block([NotNull] KCCParser.Function_blockContext context)
        {
            var express = context.expression();

            foreach (var e in express)
            {
                //VisitExpression(e);
                Visit(e);
            }

            return null;
        }

        public override object VisitVar_decl([NotNull] KCCParser.Var_declContext context)
        {
            var type = context.symbol_id()[0].GetText();
            var id = context.symbol_id()[1].GetText();

            _controller.DeclareVariable(id, type);
            
            if (context.expression() != null)
            {
                VisitExpression(context.expression());
                _controller.AddInstruction(InstOp.Set, id, null, OpModifier.FromLastTemp);
            }

            return null;
        }

        public override object VisitExpression([NotNull] KCCParser.ExpressionContext context)
        {
            
            return null;
        }

        public override object VisitSet_alt([NotNull] KCCParser.Set_altContext context)
        {
            string declType = null;
            string symbolId = null;
            string op = context.op_sum.Text;
            string index = context.index_anyvalue()?.GetText();
            InstOp instOp = InstOp.NoOp;

            var dCtx = context.expression();
            int exprIdx = 0;

            if (dCtx.Length == 2)//Contains a type, so is definition
            {
                declType = (string) Visit(dCtx[0]);
                symbolId = context.symbol_id().GetText();
                exprIdx = 1;
            } else//Reference existing variable
            {
                symbolId = context.symbol_id().GetText();
            }

            _controller.DeclareVariable(symbolId, declType);

            if (context.op_sum != null)
            {
                switch (context.op_sum.Text)
                {
                    case "=":   instOp = InstOp.Set; break;
                    case "+=":  instOp = InstOp.SetAdd; break;
                    case "-=":  instOp = InstOp.SetSub; break;
                    case "*=":  instOp = InstOp.SetMult; break;
                    case "/=":  instOp = InstOp.SetDiv; break;
                    case "%=":  instOp = InstOp.SetModulo; break;
                    case "&=":  instOp = InstOp.SetAnd; break;
                    case "^=":  instOp = InstOp.SetOr; break;
                    case "|=":  instOp = InstOp.SetXor; break;
                }

                Visit(dCtx[exprIdx]);
                _controller.AddInstruction(instOp, symbolId, index, OpModifier.FromLastTemp);
            } else
            {
                _controller.AddInstruction(InstOp.Set, symbolId, index, OpModifier.NullOrDefault);
            }

            return null;
        }

        public override object VisitSimple_value([NotNull] KCCParser.Simple_valueContext context)
        {
            return context.value_id()?.symbol_id()?.GetText();
        }

        
    }
}
