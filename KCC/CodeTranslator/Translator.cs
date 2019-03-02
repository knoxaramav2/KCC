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

        public void Translate(string raw)
        {
            var inputStream = new AntlrInputStream(raw);
            var lexer = new KCCLexer(inputStream);
            var ctStream = new CommonTokenStream(lexer);
            var kccParser = new KCCParser(ctStream);

            var asmContext = kccParser.rules();
            var visitor = new Visitor();

            //Get all assembly definitions in file
            var fileAsms = (KCCParser.AsmContext[]) visitor.Visit(asmContext);

            foreach (var asm in fileAsms)
            {
                var asmModel = (Asm) visitor.VisitAsm(asm);
                Console.WriteLine("Assembly " + asmModel.Id + " visited");
            }

            return;
        }
    }
}
