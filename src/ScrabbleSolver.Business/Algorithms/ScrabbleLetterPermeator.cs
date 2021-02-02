using System;
using System.Collections.Generic;

namespace ScrabbleSolver.Business.Algorithms
{
    /// <summary>
    /// Tool to permiate a string
    /// </summary>
    public class ScrabbleLetterPermeator
    {
        /// <summary>
        /// Return a list of string permiation for the given string (and possibly substrings)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="includeSubstrings"></param>
        /// <returns></returns>
        public IEnumerable<string> Permiate( string str, bool includeSubstrings = false )
        {
            if ( includeSubstrings )
            {
                for ( var i = str.Length; i > 0; i-- )
                {
                    foreach ( var p in PermiateImpl( str.Substring(0, i), 0, i ) )
                    {
                        yield return p;
                    }
                }
            }
            else
            {
                foreach ( var p in PermiateImpl( str, 0, str.Length ) )
                {
                    yield return p;
                }
            }

        }

        private IEnumerable<string> PermiateImpl(string str, int a, int b )
        {
            if ( a >= b )
            {
                yield return str;
            }
            else
            {
                for ( int i = a; i < b; i++ )
                {
                    if ( ShouldSwap( str, a, i ) )
                    {
                        str = Swap( str, a, i );
                        foreach ( var p in PermiateImpl( str, a + 1, b ) )
                        {
                            yield return p;
                        }
                        str = Swap( str, a, i );
                    }
                }
            }
        }

        private bool ShouldSwap( string str, int a, int b )
        {
            for ( int i = a; i < b; i++ )
            {
                if ( str[ i ] == str[ b ] )
                {
                    return false;
                }
            }
            return true;
        }

        private string Swap(string str, int i, int j )
        {
            var buffer = str.ToCharArray();
            var temp = buffer[ i ];
            buffer[ i ] = buffer[ j ];
            buffer[ j ] = temp;
            return new string( buffer );
        }
    }
}
