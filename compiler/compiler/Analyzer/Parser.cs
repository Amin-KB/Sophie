using System;
using System.Collections.Generic;
using System.Text;

namespace compiler.Analyzer
{
    internal sealed class Parser
    {
        private readonly SyntaxToken[] _tokens;
        private int _position;
        private List<string> _diagnostics=new List<string>();   
        public Parser(string text)
        {
            var tokens = new List<SyntaxToken>();
            var lexer = new Lexer(text);
            SyntaxToken token;
            do
            {
                token = lexer.NextToken();
                if (token.Kind != SyntaxKind.WhiteSpaceToken &&
                    token.Kind != SyntaxKind.UnKnownToken)
                {
                    tokens.Add(token);
                }
            } while (token.Kind != SyntaxKind.EndOfFileToken);
            _tokens= tokens.ToArray();
            _diagnostics.AddRange(lexer.Dianostics);
        }
        /// <summary>
        /// We can see look ahead
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        private SyntaxToken Peek(int offset)
        {
            var index = _position + offset; 
            if (index >= _tokens.Length)
                return _tokens[_tokens.Length - 1];
            return _tokens[index];  
        }

        public IEnumerable<string> Diagnostics => _diagnostics;
        /// <summary>
        /// it gives us the current Token
        /// </summary>
        private SyntaxToken Current => Peek(0);

        private SyntaxToken MatchToken(SyntaxKind kind)
        {
            if (Current.Kind == kind)
                return NextToken();
            _diagnostics.Add($"ERROR: unexpected token <{Current.Kind}>, expected <{kind}>");
            return new SyntaxToken(kind, Current.Position, null, null);
        }
        private SyntaxToken NextToken()
        {
            var current = Current;
            _position++;
            return current; 
        }
        /// <summary>
        /// Parsing tree
        /// </summary>
        /// <returns></returns>
        public SyntaxTree Parse()
        {
            var expression = ParseExpression();
            var endOfFileToken = MatchToken(SyntaxKind.EndOfFileToken);
            return new SyntaxTree(_diagnostics, expression, endOfFileToken);
        }
        private ExpressionSyntax ParseExpression()
        {
            return ParseTerm();
        }
       
        public ExpressionSyntax ParseTerm()
        {
            //we start the Parsing with left
            var left = ParseFactor();
            while (Current.Kind == SyntaxKind.PlusToken ||
             Current.Kind == SyntaxKind.MinusToken)
            {
                var operatorToken = NextToken();
                var right = ParseFactor();
                left = new BinaryExpressionSyntax(left, operatorToken, right);
            }
            return left;
        }

        public ExpressionSyntax ParseFactor()
        {
            //we start the Parsing with left
            var left = ParsePrimaryExpression();
            while (Current.Kind == SyntaxKind.StarToken ||  Current.Kind == SyntaxKind.SlashToken)
            {
                var operatorToken = NextToken();
                var right = ParsePrimaryExpression();
                left = new BinaryExpressionSyntax(left, operatorToken, right);
            }
            return left;
        }

        private ExpressionSyntax ParsePrimaryExpression()
        {
            if (Current.Kind ==SyntaxKind.OpenParenthesisToken)
            {
                var left = NextToken();
                var expression = ParseExpression();
                var right = MatchToken(SyntaxKind.CloseParenthesisToken);
                return new ParethesizedExpressionSyntax(left, expression, right);
            }
            var numberToken= MatchToken(SyntaxKind.NumberToken);
            return new LiterlaExpressionSyntax(numberToken); 
        }
    }
}
