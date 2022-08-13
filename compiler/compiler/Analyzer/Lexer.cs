using System;
using System.Collections.Generic;
using System.Text;

namespace compiler.Analyzer
{
     class Lexer
    {
        private readonly string _text;
        private int _position;
        private List<string> _diagnostics=new List<string>();
        public char Current 
        { 
            get
            {
                if (_position >= _text.Length)
                {
                    return '\0';
                }
                return _text[_position];
            }
        }
        public Lexer(string text)
        {
            _text = text;
        }
        public IEnumerable<string> Dianostics => _diagnostics;
        private void Next()
        {
            _position++;
        }
         public SyntaxToken NextToken()
        {
            // numbers
            // + - * /
            // White Spaces
            if(_position >= _text.Length)
                return new SyntaxToken(SyntaxKind.EndOfFileToken, _position, "\0", null);
            if (char.IsDigit(Current))
            {
                var start = _position;
                while (char.IsDigit(Current))
                    Next();
                var length = _position - start;
                var text= _text.Substring(start, length);
               
                if (!int.TryParse(text, out var value))
                {
                 _diagnostics.Add($"the number {_text} cannot be represented by an Int32");
                }
                return new SyntaxToken(SyntaxKind.NumberToken, start, text,value);
            }

            if (char.IsWhiteSpace(Current))
            {
                var start = _position;
                while (char.IsWhiteSpace(Current))
                    Next();
                var length = _position - start;
                var text = _text.Substring(start, length);
                return new SyntaxToken(SyntaxKind.WhiteSpaceToken, start, text, null);
            }
            if (Current == '+')        
                return new SyntaxToken(SyntaxKind.PlusToken, _position++, "+", null);        
            if (Current == '-')           
                return new SyntaxToken(SyntaxKind.MinusToken, _position++, "-", null);
            if (Current == '*')
                return new SyntaxToken(SyntaxKind.StarToken, _position++, "*", null);
            if (Current == '/')
                return new SyntaxToken(SyntaxKind.SlashToken, _position++, "/", null);
            if (Current == '(')
                return new SyntaxToken(SyntaxKind.OpenParenthesisToken, _position++, "(", null);
            if (Current == ')')
                return new SyntaxToken(SyntaxKind.CloseParenthesisToken, _position++, ")", null);
            _diagnostics.Add($"ERROR: bad character input: '{Current}'");
            return new SyntaxToken(SyntaxKind.UnKnownToken, _position++, _text.Substring(_position - 1,1), null);
        }
    }
}
