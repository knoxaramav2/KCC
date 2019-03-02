using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTranslator
{
    class Expression
    {

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

    class Block
    {

    }

    class Asm
    {
        public string Id;
        public KCCParser.BlockContext BlockContext;
    }

    internal class Visitor : KCCBaseVisitor<object>
    {
        public List<Asm> Assemblies = new List<Asm>();
        public List<Block> Blocks = new List<Block>();

        public override object VisitRules(KCCParser.RulesContext context)
        {
            return null;
        }

        public override object VisitAsm(KCCParser.AsmContext context)
        {
            

            var asm = new Asm
            {
                Id = context.symbol_id().IDENTIFIER().GetValue(0).ToString(),
                BlockContext = context.block()
        };

            Assemblies.Add(asm);

            return asm;
        }

        public override object VisitAssembly(KCCParser.AssemblyContext context)
        {
            var idx = context.RuleIndex;
            return null;
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
}
