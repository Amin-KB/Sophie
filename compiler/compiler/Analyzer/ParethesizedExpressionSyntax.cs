using System;
using System.Collections.Generic;
using System.Text;

namespace compiler.Analyzer
{
    sealed class ParethesizedExpressionSyntax : ExpressionSyntax
    {
        public SyntaxToken OpenParethesisToken { get;}
        public ExpressionSyntax Expression { get; }
        public SyntaxToken CloseParethesisToken { get;}
       
        public override SyntaxKind Kind => SyntaxKind.ParethesizedExpression;

        public ParethesizedExpressionSyntax(SyntaxToken openParethesisToken, ExpressionSyntax expression, SyntaxToken closeParethesisToken)
        {
            OpenParethesisToken = openParethesisToken;
            Expression = expression;
            CloseParethesisToken = closeParethesisToken;               
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return OpenParethesisToken;
            yield return Expression;
            yield return CloseParethesisToken;
            
        }
    }
}
