using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ScrabbleSolver.Business
{
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

        public Task<string[]> Load()
        {
            return Task.Run( async () =>
            {
                if ( !_loaded )
                {

                    var client = new HttpClient();
                    // In reality this would not be hardcoded but read from config file or some sort of
                    // startup API call that would return this path.
                    var response = await client.GetAsync( "https://s3.amazonaws.com/codechef_shared/download/NOV15/SCRABBLE.txt" );

                    if ( response.IsSuccessStatusCode )
                    {
                        _dictionary = response.Content.ReadAsStringAsync().Result.Split( '\n' );

                        /*_dictionary = new string[ ]
                        {
"AA",
"AAH",
"AAHED",
"AAHING",
"AAHS",
"AAL",
"AALII",
"AALIIS",
"AALS",
"AARDVARK",
"AARDVARKS",
"AARDWOLF",
"AARDWOLVES",
"AARGH",
"AARRGH",
"AARRGHH",
"AARTI",
"AARTIS",
"AAS",
"AASVOGEL",
"AASVOGELS",
"AB",
"ABA",
"ABAC",
"ABACA",
"ABACAS",
"ABACI",
"ABACK",
"ABACS"
                        };*/

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
