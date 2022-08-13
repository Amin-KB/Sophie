using System;
using System.Collections.Generic;
using System.Text;

namespace compiler.Analyzer
{
    public sealed class Evaluator
    {
        private readonly ExpressionSyntax _root;
        public Evaluator(ExpressionSyntax root)
        {
            this._root = root;
        }
        public int evaluate()
        {
            return EvaluateExpression(_root);   
        }
        private int EvaluateExpression(ExpressionSyntax node)
        {
            if (node is LiterlaExpressionSyntax number)
            {
                return (int)number.LiteralToken.Value;
            }
            if (node is BinaryExpressionSyntax binary)
            {
                var left = EvaluateExpression(binary.Left);
                var right = EvaluateExpression(binary.Right);

                if (binary.OperatorToken.Kind == SyntaxKind.PlusToken)
                    return left + right;
                else if (binary.OperatorToken.Kind == SyntaxKind.MinusToken)
                    return left - right;
                else if (binary.OperatorToken.Kind == SyntaxKind.StarToken)
                    return left * right;
                else if (binary.OperatorToken.Kind == SyntaxKind.SlashToken)
                    return left / right;
                else
                    throw new Exception($"Unexpected binary operator {binary.OperatorToken.Kind}");
            }
            if (node is ParethesizedExpressionSyntax p)
                return EvaluateExpression(p.Expression);
                throw new Exception($"Unexpected node {node.Kind}");
        }
    }
}
