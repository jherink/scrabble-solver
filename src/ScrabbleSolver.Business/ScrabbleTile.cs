using System;

namespace ScrabbleSolver.Business
{
    public interface IScrabbleTileConst
    {
        string Letter { get; }

        int Points { get; }
    }

    /// <summary>
    /// Represents a Scrabble tile
    /// </summary>
    public class ScrabbleTile : IScrabbleTileConst
    {
        /// <summary>
        /// The tile's letter
        /// </summary>
        public string Letter { get; set; }

        /// <summary>
        /// The number of points the letter is worth.
        /// </summary>
        public int Points { get; set; }
    }
}
