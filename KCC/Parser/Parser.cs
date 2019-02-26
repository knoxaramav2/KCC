using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;

namespace Parser
{
    public class Parser
    {
        public static void ParseStream(string code)
        {
            var inputStream = new AntlrInputStream(code);
            var lexer = new KCCLexer(inputStream);
            var ctStream = new CommonTokenStream(lexer);
            var kccParser = new KCCParser(ctStream);

            var context = kccParser.rules();
            var visitor = new KCCVisitor();
            visitor.Visit(context);

            foreach (var line in visitor.Lines)
            {
                Console.WriteLine("{0}", line);
            }
        }
    }
}
