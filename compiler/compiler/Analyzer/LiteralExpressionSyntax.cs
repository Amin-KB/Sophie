using System;
using System.Collections.Generic;
using System.Text;

namespace compiler.Analyzer
{
    public sealed class LiterlaExpressionSyntax: ExpressionSyntax
    {
        public SyntaxToken LiteralToken { get;  }
        public LiterlaExpressionSyntax(SyntaxToken literalToken)
        {
            LiteralToken = literalToken;
        }

        public override SyntaxKind Kind => SyntaxKind.NumberExpression;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return LiteralToken;
        }
    }
}
