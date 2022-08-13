using System;
using System.Collections.Generic;
using System.Text;

namespace compiler.Analyzer
{
    public sealed class BinaryExpressionSyntax: ExpressionSyntax
    {
        public ExpressionSyntax Left { get;}
        public SyntaxToken OperatorToken { get;}
        public ExpressionSyntax Right { get;  }

        public override SyntaxKind Kind => SyntaxKind.BinaryExpression;

        public BinaryExpressionSyntax(ExpressionSyntax left, SyntaxToken operatorSyntax, ExpressionSyntax right)
        {
            Left = left;
            OperatorToken = operatorSyntax;
            Right = right;
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
           yield return Left;
            yield return OperatorToken;
            yield return Right;
        }
    }
}
