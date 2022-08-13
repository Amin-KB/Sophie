﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace compiler.Analyzer
{
    sealed class SyntaxTree
    {
        public ExpressionSyntax Root { get;}
        public SyntaxToken EndOfFileToken { get;}
        public IReadOnlyList<string>Diagnostics { get;}
        public SyntaxTree(IEnumerable<string> diagnostics,ExpressionSyntax root, SyntaxToken endOfFileToken)
        {
            Diagnostics = diagnostics.ToArray();
            Root = root;
            EndOfFileToken = endOfFileToken;    
        }
        public static SyntaxTree Parse(string text)
        {
            var parser= new Parser(text);
            return parser.Parse();  
        }
    }
}
