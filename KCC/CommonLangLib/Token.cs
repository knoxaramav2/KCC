using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CommonLangLib
{
    public class Token
    {
        private Lexeme lexeme;
        private string word;
        private ushort priority;

        public Token(Lexeme lexeme, string word, ushort priority)
        {
            this.lexeme = lexeme;
            this.word = word;
            this.priority = priority;
        }
    }

    public class TokenStream
    {
        private List<Token> _stream;

        public TokenStream(string identifier)
        {
            _stream = new List<Token>();
        }

        
    }
}
