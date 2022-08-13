using System;
using System.Collections.Generic;
using System.Text;

namespace compiler
{
    public enum SyntaxKind
    {
        //Tokens
        UnKnownToken,
        EndOfFileToken,
        WhiteSpaceToken,
        NumberToken,        
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,
        OpenParenthesisToken,
        CloseParenthesisToken,
        
       //Expressions
        NumberExpression,
        BinaryExpression,
        ParethesizedExpression
    }
}
