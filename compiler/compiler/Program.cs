using compiler.Analyzer;
using System;
using System.Linq;

namespace compiler
{
    internal static class Program
    {
        private static void Main()
        {
            while (true)
            {
                var showTree = false;
                Console.Write("> ");
                var line= Console.ReadLine();
                if(string.IsNullOrWhiteSpace(line))
                    return;

                if (line =="#showTree")
                {
                    showTree = !showTree;
                    Console.WriteLine(showTree ? "showing parse tree" : "Not showing parse tree");
                    continue;
                }
                else if (line == "#cls")
                {
                    Console.Clear();
                    continue;
                }
                var syntaxTree = SyntaxTree.Parse(line);

                if (showTree)
                {
                    
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    PrettyPrint(syntaxTree.Root);
                    Console.ResetColor();
                }
                
                if (!syntaxTree.Diagnostics.Any())
                {
                    var evaluator = new Evaluator(syntaxTree.Root);
                    var result = evaluator.evaluate();
                    Console.WriteLine(result);  
                }
                else
                {
                   
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    foreach (var diagnostic in syntaxTree.Diagnostics)
                    {
                        Console.WriteLine(diagnostic);
                    }
                    Console.ResetColor();
                }
            }
        }
        static void PrettyPrint(SyntaxNode node, string indent = "", bool isLast=true)
        {
            //├──
            //└──
            //│
            var marker= isLast ? "└──" : "├──";
            Console.Write(indent);
            Console.Write(marker);
            Console.Write(node.Kind);
            if (node is SyntaxToken t && t.Value!= null)
            {
                Console.Write(" ");
                Console.Write(t.Value);
            }
            Console.WriteLine();
            indent +=isLast ? "   ": "│  ";
            var last = node.GetChildren().LastOrDefault();

            foreach (var child in node.GetChildren())
                PrettyPrint(child, indent,child ==last);
        }
    }
}
