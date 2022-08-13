using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace compiler.Analyzer
{
    enum SyntaxKind
    {
        NumberToken,
        WhiteSpaceToken,
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,
        OpenParenthesisToken,
        CloseParenthesisToken,
        UnKnownToken,
        EndOfFileToken,
        NumberExpression,
        BinaryExpression,
        ParethesizedExpression
    }
    class SyntaxToken: SyntaxNode
    {
        public override SyntaxKind Kind { get;  }
        public int Position { get; }
        public string Text { get; }
        public object Value { get; }
        public SyntaxToken(SyntaxKind syntaxtkind, int position, string text,object value)
        {
            Kind=syntaxtkind;
            Position=position;
            Text=text;
            Value=value;
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            return Enumerable.Empty<SyntaxNode>();
        }
    }
}
