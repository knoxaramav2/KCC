using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using CommonLangLib;

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
            var kccParser = new KCCParser(ctStream) {BuildParseTree = true};
            var visitor = new KccVisitor();

            var result = visitor.Visit(kccParser.assembly());
            var db = visitor.db;

            var table = db.Query("select * from functions");
            //table.Load();
            foreach (var row in table.Rows)
            {
                //Debug.PrintDbg(row[""]);
            }

            return null;
        }
    }
}
