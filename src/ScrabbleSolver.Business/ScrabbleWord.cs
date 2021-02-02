using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ScrabbleSolver.Business
{
    /// <summary>
    /// Interface for Scrabble words
    /// </summary>
    public interface IScrabbleWord
    {
        string Text { get; }

        int Points { get; }
    }

    public class ScrabbleWord : ICollection<IScrabbleTileConst>, IScrabbleWord
    {
        private readonly List<IScrabbleTileConst> _tiles = new List<IScrabbleTileConst>();

        public int Count => _tiles.Count;

        public bool IsReadOnly => false;

        public string Text => string.Join( "", _tiles.Select( t => t.Letter ) );

        public int Points => _tiles.Sum( t => t.Points );

        public void Add( IScrabbleTileConst item )
        {
            _tiles.Add( item );
        }

        public void Clear()
        {
            _tiles.Clear();
        }

        public bool Contains( IScrabbleTileConst item )
        {
            return _tiles.Contains( item ); // NOTE: this checks contents via pointer reference.
        }

        public void CopyTo( IScrabbleTileConst[ ] array, int arrayIndex )
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator<IScrabbleTileConst> GetEnumerator()
        {
            return _tiles.GetEnumerator();
        }

        public bool Remove( IScrabbleTileConst item )
        {
            return _tiles.Remove( item ); // NOTE: This removes via pointer reference.
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

    }
}
