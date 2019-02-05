using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLangLib;

namespace Lexer
{
    public class Lexer
    {
        

        public TokenStream Tokenize()
        {
            TokenStream stream = null;


            return stream;
        }
    }

    internal class LexPage
    {
        private string _identifier;
        private List<Token> _outputStack;
        private List<Lexeme> _operatorStack;
        private int _enclosureLevel;

        public LexPage(string identifier)
        {
            _identifier = identifier;

            _outputStack = new List<Token>();
            _operatorStack = new List<Lexeme>();

            _enclosureLevel = 0;
        }

        public void Push(Token t)
        {
            _outputStack.Add(t);
        }

        public void Push(Lexeme lexeme, string word)
        {
            //collapse operator stack for enclosed area
            if (LexemeHelper.isCloseGroup(lexeme))
            {
                Lexeme compatGroup;
                switch (lexeme)
                {
                    case Lexeme.CloseBrace:
                        compatGroup = Lexeme.OpenBrace;
                        break;
                    case Lexeme.CloseBracket:
                        compatGroup = Lexeme.OpenBracket;
                        break;
                    case Lexeme.CloseParenth:
                        compatGroup = Lexeme.OpenParenth;
                        break;
                    default:
                        compatGroup = Lexeme.NA;
                        break;
                }

                var compatFound = false;
                for (var i = _operatorStack.Count - 1; i > 0; --i)
                {
                    var curr = _operatorStack[i];

                    if (curr == compatGroup)
                    {
                        compatFound = true;
                        _operatorStack.Remove(_operatorStack[i]);
                        break;
                    }

                    _outputStack.Add(new Token(_operatorStack[i], null, 0));
                    _operatorStack.Remove(_operatorStack[i]);
                }
            }
            //append and track open container
            else if (LexemeHelper.isOpenGroup(lexeme))
            {
                _operatorStack.Add(lexeme);
                ++_enclosureLevel;
            }
            else
            {

            }
        }
    }
}
