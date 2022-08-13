using System;
using System.Collections.Generic;
using System.Text;

namespace compiler.Analyzer
{
    sealed class NumberExpressionSyntax: ExpressionSyntax
    {
        public SyntaxToken NumberToken { get;  }
        public NumberExpressionSyntax(SyntaxToken numberToken)
        {
            NumberToken = numberToken;
        }

        public override SyntaxKind Kind => SyntaxKind.NumberExpression;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return NumberToken;
        }
    }
}
