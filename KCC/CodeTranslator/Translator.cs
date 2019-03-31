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
            kccParser.BuildParseTree = true;
            var visitor = new KCCBaseVisitor<Db>();

            var result = visitor.Visit(kccParser.assembly());

            return null;
        }
    }
}
