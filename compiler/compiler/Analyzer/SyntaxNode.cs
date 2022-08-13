using System;
using System.Collections.Generic;
using System.Text;

namespace compiler.Analyzer
{
    abstract class SyntaxNode
    {
        public abstract SyntaxKind Kind { get; }
        public abstract IEnumerable<SyntaxNode> GetChildren();
    }
}
