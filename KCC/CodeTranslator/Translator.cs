using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;

namespace CodeTranslator
{
    public class Translator
    {
        public Translator()
        {
            
        }

        public object Translate(string raw)
        {
            var inputStream = new AntlrInputStream(raw);
            var lexer = new KCCLexer(inputStream);
            var ctStream = new CommonTokenStream(lexer);
            var kccParser = new KCCParser(ctStream);
            var asmContext = kccParser.rules();

            //Create visitors
            //var visitor = new Visitor();
            var asmVisitor = new AsmVisitor();

            foreach (var a in asmContext.asm())
            {
                asmVisitor.VisitAsm(a);
            }

            var asms = asmVisitor.GetAssemblies();

            foreach (var a in asms)
            {
                Console.WriteLine(a.Id);
                foreach (var expr in a.Block.Expressions)
                {
                    Console.WriteLine(expr.ToString());
                }
                
                //read blocks

            }

            //var results = asmVisitor.Visit(kccParser.asm());

            return null;
        }
    }
}
