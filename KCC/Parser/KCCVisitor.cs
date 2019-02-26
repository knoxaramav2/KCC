using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    class KCCVisitor : KCCBaseVisitor<object>
    {
        public List<string> Lines = new List<string>();

        public override object VisitExpression(KCCParser.ExpressionContext context)
        {
            Lines.Add($"{context.left.GetText()} {context.op.GetText()} {context.right.GetText()}");
            return base.VisitExpression(context);
        }
    }
}
