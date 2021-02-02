using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScrabbleSolver.Business;

namespace ScrabbleSolver.Console
{
    class Program
    {
        static void Main( string[ ] args )
        {
            System.Console.WriteLine();
            System.Console.WriteLine( "-------------------------------------------" );
            System.Console.WriteLine( "------  Welcome to Scrabble Solver!  ------" );
            System.Console.WriteLine( "-------------------------------------------" );

            System.Console.WriteLine( "Loading Dictionary..." );
            var t = SOPPODSDictionary.Inst.Load().ContinueWith( _ => StartGame() );

            Task.WaitAll( new Task[ ] { t } );

            System.Console.ReadLine();
        }

        static void StartGame()
        {
            System.Console.WriteLine( "Dictionary Loaded!" );
            var playing = true;
            while ( playing )
            {
                PrintMenu();

                var input = System.Console.ReadKey().KeyChar;
                System.Console.WriteLine();
                switch (char.ToUpperInvariant(input))
                {
                    case 'D':
                        DictionaryCommand();
                        break;
                    case 'S': // For testing, do not show to user in options.
                        SolutionCommand();
                        break;
                    case 'Q':
                        playing = false;
                        break;
                    default:
                        System.Console.WriteLine( "Unrecognized input!" );
                        break;
                }
            }
        }

        static void PrintMenu()
        {
            System.Console.WriteLine();
            System.Console.WriteLine( "MENU" );
            System.Console.WriteLine( "---------------------" );
            System.Console.WriteLine( "Dictionary Lookup        : D" );
            System.Console.WriteLine( "Solution (Top 10 results): S" );
            System.Console.WriteLine( "Quit                     : Q" );
            System.Console.WriteLine();
            System.Console.Write( "Please select option: " );
        }

        static void DictionaryCommand()
        {
            System.Console.Write( "Enter word to lookup: " );
            var input = System.Console.ReadLine();
            var word = SOPPODSDictionary.Inst.Search( input );
            if ( word == null )
            {
                System.Console.WriteLine( $"Sorry \"{input}\" is not a valid Scrabble word :(" );
            }
            else
            {
                System.Console.WriteLine( $"Woo Hoo! \"{input}\" is a valid Scrabble word and is worth {word.Points} points!" );
            }
        }

        static void SolutionCommand()
        {
            System.Console.Write( "Enter tiles to solution: " );
            var input = System.Console.ReadLine();

            if ( input.Length > 7 )
            {
                System.Console.WriteLine( "Only 7 tiles may be used at a time in determining a solution" );
            }
            else
            {
                var permeator = new Business.Algorithms.ScrabbleLetterPermeator();
                var results = new List<IScrabbleWord>();

                // NOTE: String permutation with substrings is very expensive O(n*n!).
                // In a real implementation you could decern which permutation lengths would be valid
                // and significatly trim the list down rather than just brute forcing it.  However,
                // capping tiles to 7 I think serves the purpose for the demonstration.
                foreach ( var p in permeator.Permiate( input, true ) )
                {
                    var word = SOPPODSDictionary.Inst.Search( p );
                    if ( word != null )
                    {
                        results.Add( word );
                    }
                }

                System.Console.WriteLine( $"{results.Count} possible words found! Top results: " );
                foreach ( var word in results.OrderByDescending( w => w.Points ).Take( 10 ) )
                {
                    System.Console.WriteLine( $"Word: \"{word.Text}\", Points: {word.Points}" );
                }
            }
        }
    }
}
