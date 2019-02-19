using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLangLib;

namespace Lexer
{
    /*
     * Accepts a PageInfo object and tokenizes it
     */
    public class Lexer
    {
        private List<LexPage> _pages;

        //Might end up chunking lines.
        //The language is designed to be minimizeable, so alternative
        //approaches may be better. I dunno, computers have lots
        //of ram now so...
        //TODO Math out more efficient method.
        private int TokenizeLine(string [] words)
        {

            return 0;
        }

        private string[] SplitBySpecial(string line)
        {
            //All deliminating characters
            char[] delim =
            {
                ' ', '\t',
                '+', '-', '*', '/', '^', '=', '%',
                '!', '@', '#', '$',
                '&', '|', '^'
            };

            return line.Split(delim, StringSplitOptions.RemoveEmptyEntries);
        }

        public bool Tokenize(PageInfo pageInfo)
        {
            TokenStream stream = null;

            var lexPage = new LexPage(pageInfo.Uri);

            return true;
        }
    }

    internal class LexPage
    {
        private string _identifier;
        private List<Token> _tokenStream;

        public LexPage(string identifier)
        {
            _identifier = identifier;
            _tokenStream = new List<Token>();
        }
    }
}
