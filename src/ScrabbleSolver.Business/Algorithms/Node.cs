using System.Collections.Generic;

namespace ScrabbleSolver.Business.Algorithms
{
    internal class ScrabbleNode
    {
        /// <summary>
        /// The value of the node (Letter)
        /// </summary>
        public char Value { get; set; }

        /// <summary>
        /// Flags the node as the end of the word.
        /// </summary>
        public bool IsEnd { get; set; }

        /// <summary>
        /// Children of this node.
        /// </summary>
        public List<ScrabbleNode> Children { get; set; }

        public ScrabbleNode( char value ) { Value = value; }
    }

}
