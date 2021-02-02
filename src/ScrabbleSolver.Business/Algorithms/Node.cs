using System;
using System.Collections.Generic;

namespace ScrabbleSolver.Business.Algorithms
{
    internal class ScrabbleNode
    {
        public char Value { get; set; }

        public bool IsEnd { get; set; }

        public List<ScrabbleNode> Children { get; set; }

        public ScrabbleNode( char value ) { Value = value; }
    }

}
