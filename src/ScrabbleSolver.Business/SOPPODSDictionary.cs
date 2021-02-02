using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ScrabbleSolver.Business
{
    /// <summary>
    /// Stores the data for the Scrabble dictionary
    /// </summary>
    public class SOPPODSDictionary
    {
        #region Singleton

        private static Lazy<SOPPODSDictionary> _inst = new Lazy<SOPPODSDictionary>( () => new SOPPODSDictionary() );

        public static SOPPODSDictionary Inst
        {
            get => _inst.Value;
        }

        private SOPPODSDictionary()
        {
        }

        #endregion

        private bool _loaded = false;
        private string[ ] _dictionary;
        private Algorithms.ScrabbleTree _tree;

        /// <summary>
        /// Load the dictionary
        /// </summary>
        /// <returns></returns>
        public Task<string[]> Load()
        {
            return Task.Run( async () =>
            {
                if ( !_loaded )
                {

                    var client = new HttpClient();
                    // In reality this would not be hardcoded but read from config file or some sort of
                    // startup API call that would return this path.
                    var response = await client.GetAsync( "https://raw.githubusercontent.com/jherink/scrabble-solver/main/SOPPODSDictionary.txt" );

                    if ( response.IsSuccessStatusCode )
                    {
                        _dictionary = response.Content.ReadAsStringAsync().Result.Split( '\n' );

                        _tree = new Algorithms.ScrabbleTree();
#if DEBUG
                        // Track loading performance.
                        var stopwatch = new System.Diagnostics.Stopwatch();
                        stopwatch.Start();
#endif
                        foreach ( var entry in _dictionary )
                        {
                            _tree.Add( entry );
                        }

#if DEBUG
                        stopwatch.Stop();
                        System.Diagnostics.Debug.WriteLine( $"Scrabble tree loaded with {_dictionary.Length} entries in {stopwatch.ElapsedMilliseconds}ms" );
#endif
                    }
                    else
                    {
                        throw new Exception( "Error loading dictionary!" );
                    }

                    _loaded = true;
                }

                return _dictionary;
            } );
        }

        public IScrabbleWord Search( string word )
        {
#if DEBUG
            // Track loading performance.
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
#endif
            var sWord = _tree.Search( word.ToUpper() );
#if DEBUG
            stopwatch.Stop();
            System.Diagnostics.Debug.WriteLine( $"Scrabble search performed in {stopwatch.ElapsedMilliseconds}ms" );
#endif
            return sWord;
        }
    }
}
