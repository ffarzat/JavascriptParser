using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr.Runtime.Tree;

namespace ConsoleApplication1
{
    /// <summary>
    /// Represents a code Generator (from AST) for Javascript
    /// </summary>
    public class JavascriptAstCodeGenerator
    {
        /// <summary>
        /// Keeps the original tree
        /// </summary>
        private ITree _tree;

        /// <summary>
        /// Return the base tree of code transformation
        /// </summary>
        public ITree Tree {
            get { return _tree; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public JavascriptAstCodeGenerator(ITree tree)
        {
            _tree = tree;
        }

        /// <summary>
        /// Do the magic. Visits the AST nodes and translation back to code
        /// </summary>
        /// <returns></returns>
        public string DoCodeTransformation()
        {
            return _tree.ToStringTree();
        }

    }
}
