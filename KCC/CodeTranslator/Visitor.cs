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

            _controller.ExitScope();

            return null;
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

            return null;
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
                _controller.AddInstruction(InstOp.Set, id, null, null, OpModifier.FromLastTemp);
            }

            return null;
        }

        //Expressions***********************************

        public override object VisitSet_alt([NotNull] KCCParser.Set_altContext context)
        {
            string declType;
            string symbolId;
            string op = context.op_sum.Text;
            string index = context.index_anyvalue()?.GetText();
            InstOp instOp = InstOp.NoOp;

            var dCtx = context.expression();
            int exprIdx = 0;

            symbolId = context.symbol_id().GetText();

            if (dCtx.Length == 2)//Contains a type, so is definition
            {
                declType = (string) Visit(dCtx[0]);
                _controller.DeclareVariable(symbolId, declType);
                exprIdx = 1;
            }

            if (op != null)
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

                var value = (string) Visit(dCtx[exprIdx]);
                OpModifier valueType = value == null ? OpModifier.FromLastTemp : OpModifier.None;
                _controller.AddInstruction(instOp, symbolId, value, index, valueType);
            } else
            {
                _controller.AddInstruction(InstOp.Set, symbolId, null, index, OpModifier.NullOrDefault);
            }

            return null;
        }

        public override object VisitFnc_call([NotNull] KCCParser.Fnc_callContext context)
        {
            var fnc = (string)VisitAccessor(context.accessor());

            Visit(context.call_group());

            _controller.AddInstruction(InstOp.Call, fnc, null);

            return null;
        }

        public override object VisitCall_group([NotNull] KCCParser.Call_groupContext context)
        {
            _controller.AddInstruction(InstOp.StartFncCall, null, null);

            foreach (var e in context.expression())
            {
                var res = (string) Visit(e);
                _controller.AddInstruction(InstOp.MarkAsArg, res, null);
            }

            _controller.AddInstruction(InstOp.EndFncCall, null, null);

            return null;
        }

        public override object VisitPost_inc([NotNull] KCCParser.Post_incContext context)
        {
            _controller.AddInstruction(InstOp.PostIncrement, (string) Visit(context.accessor()), null);
            return null;
        }

        public override object VisitPost_dec([NotNull] KCCParser.Post_decContext context)
        {
            _controller.AddInstruction(InstOp.PostDecrement, (string)Visit(context.accessor()), null);
            return null;
        }

        public override object VisitPre_inc([NotNull] KCCParser.Pre_incContext context)
        {
            _controller.AddInstruction(InstOp.PreIncrement, (string)Visit(context.accessor()), null);
            return null;
        }

        public override object VisitPre_dec([NotNull] KCCParser.Pre_decContext context)
        {
            _controller.AddInstruction(InstOp.PreDecrement, (string)Visit(context.accessor()), null);
            return null;
        }

        public override object VisitNot([NotNull] KCCParser.NotContext context)
        {
            _controller.AddInstruction(InstOp.Not, null, null, null, OpModifier.FromLastTemp);
            return null;
        }

        public override object VisitInvert([NotNull] KCCParser.InvertContext context)
        {
            _controller.AddInstruction(InstOp.BInv, null, null, null, OpModifier.FromLastTemp);
            return null;
        }

        //Math*************************
        public override object VisitMath1([NotNull] KCCParser.Math1Context context)
        {
            var lval = (string) Visit(context.expression()[0]);
            var rval = (string)Visit(context.expression()[1]);

            OpModifier mod = OpModifier.None;
            InstOp op = InstOp.NoOp;

            if (lval == null && rval == null) mod = OpModifier.LTempRTemp;
            else if (lval != null && rval == null) mod = OpModifier.LRawRTemp;
            else if (lval == null && rval != null) mod = OpModifier.LTempRRaw;
            else mod = OpModifier.LRawRRaw;

            switch (context.prod_op.Text)
            {
                case "*":
                    op = InstOp.Mult;
                    break;
                case "/":
                    op = InstOp.Div;
                    break;
                case "%":
                    op = InstOp.Modulo;
                    break;
                case "**":
                    op = InstOp.Power;
                    break;
            }

            _controller.AddInstruction(op, lval, rval, null, mod);

            return null;
        }

        public override object VisitMath2([NotNull] KCCParser.Math2Context context)
        {
            var lval = (string)Visit(context.expression()[0]);
            var rval = (string)Visit(context.expression()[1]);

            OpModifier mod = OpModifier.None;
            InstOp op = InstOp.NoOp;

            if (lval == null && rval == null) mod = OpModifier.LTempRTemp;
            else if (lval != null && rval == null) mod = OpModifier.LRawRTemp;
            else if (lval == null && rval != null) mod = OpModifier.LTempRRaw;
            else mod = OpModifier.LRawRRaw;

            switch (context.sum_op.Text)
            {
                case "+":
                    op = InstOp.Add;
                    break;
                case "-":
                    op = InstOp.Sub;
                    break;
            }

            _controller.AddInstruction(op, lval, rval, null, mod);

            return null;
        }

        //Comparators**********************
        public override object VisitShift([NotNull] KCCParser.ShiftContext context)
        {
            var lval = (string)Visit(context.expression()[0]);
            var rval = (string)Visit(context.expression()[1]);

            OpModifier mod = OpModifier.None;
            InstOp op = InstOp.NoOp;

            if (lval == null && rval == null) mod = OpModifier.LTempRTemp;
            else if (lval != null && rval == null) mod = OpModifier.LRawRTemp;
            else if (lval == null && rval != null) mod = OpModifier.LTempRRaw;
            else mod = OpModifier.LRawRRaw;

            switch (context.shift_op.Text)
            {
                case "<<":
                    op = InstOp.ShL;
                    break;
                case ">>":
                    op = InstOp.ShR;
                    break;
            }

            _controller.AddInstruction(op, lval, rval, null, mod);

            return null;
        }

        public override object VisitCompare1([NotNull] KCCParser.Compare1Context context)
        {
            var lval = (string)Visit(context.expression()[0]);
            var rval = (string)Visit(context.expression()[1]);

            OpModifier mod = OpModifier.None;
            InstOp op = InstOp.NoOp;

            if (lval == null && rval == null) mod = OpModifier.LTempRTemp;
            else if (lval != null && rval == null) mod = OpModifier.LRawRTemp;
            else if (lval == null && rval != null) mod = OpModifier.LTempRRaw;
            else mod = OpModifier.LRawRRaw;

            switch (context.gl_compare.Text)
            {
                case "<":
                    op = InstOp.Lss;
                    break;
                case "<=":
                    op = InstOp.LssEqu;
                    break;
                case ">":
                    op = InstOp.Gtr;
                    break;
                case ">=":
                    op = InstOp.GtrEqu;
                    break;
            }

            _controller.AddInstruction(op, lval, rval, null, mod);

            return null;
        }

        public override object VisitCompare2([NotNull] KCCParser.Compare2Context context)
        {
            var lval = (string)Visit(context.expression()[0]);
            var rval = (string)Visit(context.expression()[1]);

            OpModifier mod = OpModifier.None;
            InstOp op = InstOp.NoOp;

            if (lval == null && rval == null) mod = OpModifier.LTempRTemp;
            else if (lval != null && rval == null) mod = OpModifier.LRawRTemp;
            else if (lval == null && rval != null) mod = OpModifier.LTempRRaw;
            else mod = OpModifier.LRawRRaw;

            switch (context.eq_compare.Text)
            {
                case "==":
                    op = InstOp.Equ;
                    break;
                case "!+":
                    op = InstOp.NEqu;
                    break;
            }

            _controller.AddInstruction(op, lval, rval, null, mod);

            return null;
        }

        public override object VisitB_and([NotNull] KCCParser.B_andContext context)
        {
            var lval = (string)Visit(context.expression()[0]);
            var rval = (string)Visit(context.expression()[1]);

            OpModifier mod = OpModifier.None;

            if (lval == null && rval == null) mod = OpModifier.LTempRTemp;
            else if (lval != null && rval == null) mod = OpModifier.LRawRTemp;
            else if (lval == null && rval != null) mod = OpModifier.LTempRRaw;
            else mod = OpModifier.LRawRRaw;

            _controller.AddInstruction(InstOp.BAnd, lval, rval, null, mod);

            return null;
        }

        public override object VisitB_or([NotNull] KCCParser.B_orContext context)
        {
            var lval = (string)Visit(context.expression()[0]);
            var rval = (string)Visit(context.expression()[1]);

            OpModifier mod = OpModifier.None;

            if (lval == null && rval == null) mod = OpModifier.LTempRTemp;
            else if (lval != null && rval == null) mod = OpModifier.LRawRTemp;
            else if (lval == null && rval != null) mod = OpModifier.LTempRRaw;
            else mod = OpModifier.LRawRRaw;

            _controller.AddInstruction(InstOp.BOr, lval, rval, null, mod);

            return null;
        }

        public override object VisitB_xor([NotNull] KCCParser.B_xorContext context)
        {
            var lval = (string)Visit(context.expression()[0]);
            var rval = (string)Visit(context.expression()[1]);

            OpModifier mod = OpModifier.None;

            if (lval == null && rval == null) mod = OpModifier.LTempRTemp;
            else if (lval != null && rval == null) mod = OpModifier.LRawRTemp;
            else if (lval == null && rval != null) mod = OpModifier.LTempRRaw;
            else mod = OpModifier.LRawRRaw;

            _controller.AddInstruction(InstOp.BXor, lval, rval, null, mod);

            return null;
        }

        public override object VisitL_and([NotNull] KCCParser.L_andContext context)
        {
            var lval = (string)Visit(context.expression()[0]);
            var rval = (string)Visit(context.expression()[1]);

            OpModifier mod = OpModifier.None;
            InstOp op = InstOp.NoOp;

            if (lval == null && rval == null) mod = OpModifier.LTempRTemp;
            else if (lval != null && rval == null) mod = OpModifier.LRawRTemp;
            else if (lval == null && rval != null) mod = OpModifier.LTempRRaw;
            else mod = OpModifier.LRawRRaw;

            switch (context.lg_and.Text)
            {
                case "&&":
                    op = InstOp.And;
                    break;
                case "!&":
                    op = InstOp.Nand;
                    break;
            }

            _controller.AddInstruction(op, lval, rval, null, mod);

            return null;
        }

        public override object VisitL_or([NotNull] KCCParser.L_orContext context)
        {
            var lval = (string)Visit(context.expression()[0]);
            var rval = (string)Visit(context.expression()[1]);

            OpModifier mod = OpModifier.None;
            InstOp op = InstOp.NoOp;

            if (lval == null && rval == null) mod = OpModifier.LTempRTemp;
            else if (lval != null && rval == null) mod = OpModifier.LRawRTemp;
            else if (lval == null && rval != null) mod = OpModifier.LTempRRaw;
            else mod = OpModifier.LRawRRaw;

            switch (context.lg_or.Text)
            {
                case "||":
                    op = InstOp.Or;
                    break;
                case "!|":
                    op = InstOp.Nor;
                    break;
                case "|||":
                    op = InstOp.Xor;
                    break;
                case "!||":
                    op = InstOp.XNor;
                    break;
            }

            _controller.AddInstruction(op, lval, rval, null, mod);

            return null;
        }

        //Note: Null values indicate expression results, not specific values/symbols
        public override object VisitList([NotNull] KCCParser.ListContext context)
        {
            var ret = new List<string>();

            foreach(var e in context.expression())
            {
                ret.Add((string) Visit(e));
            }

            return base.VisitList(context);
        }

        public override object VisitParan([NotNull] KCCParser.ParanContext context)
        {
            if (context.expression() != null)
            {
                Visit(context.expression());
            }
            
            return null;
        }

        public override object VisitSimple_value([NotNull] KCCParser.Simple_valueContext context)
        {
            return (string)VisitValue_id(context.value_id());
        }

        public override object VisitAccessor([NotNull] KCCParser.AccessorContext context)
        {
            var ret = "";
            var len = context.symbol_id().Length;

            for (var i=0; i<len; ++i)
            {
                ret += context.symbol_id()[i].GetText();
                if (i < len - 2)
                {
                    ret += '.';
                }
            }

            return ret;
        }

        public override object VisitValue_id([NotNull] KCCParser.Value_idContext context)
        {
            if (context.@bool() != null)
            {
                return context.@bool().GetText();
            } else if (context.@string() != null)
            {
                _controller.AddDirective(Directives.Lc, context.@string().GetText());
                _controller.AddDirective(Directives.Ascii, context.@string().GetText(), true);
                return context.@string().GetText();
            } else if (context.@char() != null)
            {
                return context.@char().GetText();
            } else if (context.integer() != null)
            {
                return context.integer().GetText();
            } else if (context.@decimal() != null)
            {
                return context.@decimal().GetText();
            } else if (context.symbol_id() != null)
            {
                return context.symbol_id().GetText();
            }

            return null;
        }
    }
}
