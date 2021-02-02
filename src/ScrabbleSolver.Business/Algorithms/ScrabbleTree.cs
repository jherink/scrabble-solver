using System;
using System.Collections.Generic;

namespace ScrabbleSolver.Business.Algorithms
{
    /// <summary>
    /// A tree to lookup and store scrabble word based on the Aho-Corasick string search algorithm.
    /// https://en.wikipedia.org/wiki/Aho%E2%80%93Corasick_algorithm
    /// </summary>
    public class ScrabbleTree
    {
        private readonly Dictionary<char, ScrabbleNode> _roots = new Dictionary<char, ScrabbleNode>();

        /// <summary>
        /// Adds a word to the Scrabble path.
        /// </summary>
        /// <param name="item"></param>
        public void Add( string item )
        {
            if ( !string.IsNullOrWhiteSpace( item ) )
            {
                if ( !_roots.ContainsKey( item[ 0 ] ) )
                {
                    _roots.Add( item[ 0 ], new ScrabbleNode( item[ 0 ] ) );
                }

                AddImpl( _roots[ item[ 0 ] ], Chop( item ) );
            }
        }

        private void AddImpl( ScrabbleNode head, string path )
        {
            if ( !string.IsNullOrWhiteSpace( path ) )
            {
                ScrabbleNode pathNode = null;
                if ( head.Children == null )
                { // first child
                    pathNode = new ScrabbleNode( path[ 0 ] );
                    head.Children = new List<ScrabbleNode>();
                    head.Children.Add( pathNode );
                }
                else
                {
                    // Look for existing child path.
                    foreach ( var child in head.Children )
                    {
                        if ( child.Value == path[ 0 ] )
                        {
                            pathNode = child;
                            break; // stop searching.
                        }
                    }

                    if ( pathNode == null )
                    { // new child path.
                        pathNode = new ScrabbleNode( path[ 0 ] );
                        head.Children.Add( pathNode );
                    }
                }

                AddImpl( pathNode, Chop( path ) );
            }
            else
            {
                head.IsEnd = true; // mark this node as the end of a word.
            }
        }

        /// <summary>
        /// Searches for a valid scrabble word.
        /// </summary>
        /// <param name="word">The word</param>
        /// <returns>The scrabble word if valid, null if no match found.</returns>
        public IScrabbleWord Search( string word )
        {
            var sWord = default( IScrabbleWord );
            if ( !string.IsNullOrWhiteSpace( word ) )
            {
                if ( _roots.ContainsKey( word[ 0 ] ) )
                {
                    var path = SearchForScrabblePath( _roots[ word[ 0 ] ], word );
                    if ( path.Count == word.Length && path[ path.Count - 1 ].IsEnd )
                    { // check we matched the whole provided word AND that we completed a word.
                        var scrabbleWord = new ScrabbleWord();
                        foreach ( var node in path )
                        {
                            scrabbleWord.Add( ScrabbleTileFactory.Get( node.Value ) );
                        }
                        sWord = scrabbleWord;
                    }
                }
            }

            return sWord;
        }

        private List<ScrabbleNode> SearchForScrabblePath( ScrabbleNode node, string word )
        {
            var path = new List<ScrabbleNode>();

            if ( !string.IsNullOrWhiteSpace( word ) )
            {
                if ( node.Value == word[ 0 ] )
                {
                    path.Add( node );
                    if ( node.Children != null && word.Length > 1 )
                    {  // keep going and search children for the remainder of the word.
                        foreach ( var child in node.Children )
                        {
                            if ( child.Value == word[ 1 ] )
                            {
                                path.AddRange( SearchForScrabblePath( child, Chop( word ) ) );
                                break; // stop processing this level.
                            }
                        }
                    }
                }
            }

            return path;
        }

        private string Chop( string input )
        {
            return string.IsNullOrWhiteSpace( input ) || input.Length <= 1 ? null : input.Substring( 1 );
        }
    }

}
