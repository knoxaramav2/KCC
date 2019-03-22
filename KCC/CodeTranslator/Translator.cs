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
            AssemblyRegistry.Init();

            var inputStream = new AntlrInputStream(raw);
            var lexer = new KCCLexer(inputStream);
            var ctStream = new CommonTokenStream(lexer);
            var kccParser = new KCCParser(ctStream);
            kccParser.BuildParseTree = true;
            

            var result = new AssemblyVisitor().Visit(kccParser.assembly());

            return null;
            /*
            var asmContext = kccParser.rules();

            //Create visitors
            var asmVisitor = new AssemblyVisitor();
            

            foreach (var a in asmContext.assembly())
            {
                asmVisitor.VisitAssembly(a);
            }

            var asms = asmVisitor.GetAssemblies();

            foreach (var a in asms)
            {
                Console.WriteLine(a.Id);
            }

            //var results = asmVisitor.Visit(kccParser.asm());

            return null;*/
        }
    }
}
