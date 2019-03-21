using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTranslator
{
    class Expression
    {
        private string type;

    }

    class Class
    {
        public string Id;
        public Block Block;
        public List<Group> ArgList;

        public Class()
        {
            Id = "";
            Block = new Block();
            ArgList = new List<Group>();
        }
    }

    class Group
    {
        
    }

    //for classes and assemblies
    class BlockContainer : Block
    {
        //List<>

        public BlockContainer()
        {
            Type = BlockBodyType.Container;
        }
    }

    //for functions
    class BlockScope : Block
    {
        public BlockScope()
        {
            Type = BlockBodyType.Scope;
        }
    }

    class Block
    {
        public List<Expression> Expressions;
        public BlockBodyType Type;

        public enum BlockBodyType
        {
            Basic,
            Scope,
            Container
        }

        public Block()
        {
            Type = BlockBodyType.Basic;
            Expressions = new List<Expression>();
        }
    }

    class Asm
    {
        public string Id;
        public BlockContainer Block;
    }

    internal class Visitor : KCCBaseVisitor<object>
    {
        public List<Asm> Assemblies = new List<Asm>();
        public List<Block> Blocks = new List<Block>();

        public override object VisitRules(KCCParser.RulesContext context)
        {
            return context.asm();
        }


        public override object VisitBlock(KCCParser.BlockContext context)
        {
            
            var block = new Block();
            

            return null;
        }

        public override object VisitClass(KCCParser.ClassContext classContext)
        {

            return null;
        }
    }

    internal class AsmVisitor : KCCBaseVisitor<object>
    {
        private readonly List<Asm> _assemblies = new List<Asm>();

        public override object VisitAsm(KCCParser.AsmContext context)
        {
            var asm = new Asm
            {
                Id = context.symbol_id().GetText(),
                Block =(BlockContainer) (new BlockVisitor().VisitBlock(context.block()))
            };

            _assemblies.Add(asm);

            return asm;
        }

        public List<Asm> GetAssemblies()
        {
            return _assemblies;
        }
    }

    internal class ClassVisitor : KCCBaseVisitor<object>
    {
        public override object VisitClass(KCCParser.ClassContext context)
        {

            var blockContext = context.block();

            var blockVisitor = new BlockVisitor();
            var exprVisitor = new ExpressionVisitor();

            

            var kClass = new Class
            {
                Id = context.symbol_id().GetText()
            };

            return kClass;
        }
    }

    internal class BlockVisitor : KCCBaseVisitor<Block>
    {
        private readonly List<Block> _blocks = new List<Block>();

        public BlockScope VisitBlockScope(KCCParser.BlockContext context)
        {
            var blockScope = new BlockScope();

            var exprVisitor = new ExpressionVisitor();
            var expressions = context.expression();

            //visitor expressions
            foreach (var expr in expressions)
            {
                var expression = (Expression)exprVisitor.VisitExpression(expr);
                blockScope.Expressions.Add(expression);
            }

            return blockScope;
        }

        public Block VisitBlockContainer(KCCParser.BlockContext context)
        {
            var blockContainer = new BlockContainer();

            var exprVisitor = new ExpressionVisitor();
            var expressions = context.expression();

            //visitor expressions
            foreach (var expr in expressions)
            {
                var expression = (Expression)exprVisitor.VisitExpression(expr);
                blockContainer.Expressions.Add(expression);
            }

            return blockContainer;
        }

        public override Block VisitBlock(KCCParser.BlockContext context)
        {
            switch (context.RuleIndex)
            {
                case 1: return VisitBlockScope(context);
                case 2: return VisitBlockContainer(context);
                default:
                    return null;
            }
        }

        public List<Block> GetBlocks()
        {
            return _blocks;
        }
    }

    internal class ExpressionVisitor : KCCBaseVisitor<object>
    {
        private readonly List<Expression> _expressions = new List<Expression>();

        public override object VisitExpression(KCCParser.ExpressionContext context)
        {
            var expression = new Expression();

            var a = context.assign_expr();
            var b = context.var_decl();

            _expressions.Add(expression);
            return expression;
        }

        public List<Expression> GetExpressions()
        {
            return _expressions;
        }
    }

}
