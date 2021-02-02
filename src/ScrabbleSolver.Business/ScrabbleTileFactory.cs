using System;

namespace ScrabbleSolver.Business
{
    /// <summary>
    /// In reality, these would be in a DB table so they could be updated if needed.
    /// </summary>
    public static class ScrabbleTileFactory
    {
        public static ScrabbleTile A => new ScrabbleTile { Letter = "A", Points = 1 };
        public static ScrabbleTile B => new ScrabbleTile { Letter = "B", Points = 3 };
        public static ScrabbleTile C => new ScrabbleTile { Letter = "C", Points = 3 };
        public static ScrabbleTile D => new ScrabbleTile { Letter = "D", Points = 2 };
        public static ScrabbleTile E => new ScrabbleTile { Letter = "E", Points = 1 };
        public static ScrabbleTile F => new ScrabbleTile { Letter = "F", Points = 4 };
        public static ScrabbleTile G => new ScrabbleTile { Letter = "G", Points = 2 };
        public static ScrabbleTile H => new ScrabbleTile { Letter = "H", Points = 4 };
        public static ScrabbleTile I => new ScrabbleTile { Letter = "I", Points = 1 };
        public static ScrabbleTile J => new ScrabbleTile { Letter = "J", Points = 8 };
        public static ScrabbleTile K => new ScrabbleTile { Letter = "K", Points = 5 };
        public static ScrabbleTile L => new ScrabbleTile { Letter = "L", Points = 1 };
        public static ScrabbleTile M => new ScrabbleTile { Letter = "M", Points = 3 };
        public static ScrabbleTile N => new ScrabbleTile { Letter = "N", Points = 1 };
        public static ScrabbleTile O => new ScrabbleTile { Letter = "O", Points = 1 };
        public static ScrabbleTile P => new ScrabbleTile { Letter = "P", Points = 3 };
        public static ScrabbleTile Q => new ScrabbleTile { Letter = "Q", Points = 10 };
        public static ScrabbleTile R => new ScrabbleTile { Letter = "R", Points = 1 };
        public static ScrabbleTile S => new ScrabbleTile { Letter = "S", Points = 1 };
        public static ScrabbleTile T => new ScrabbleTile { Letter = "T", Points = 1 };
        public static ScrabbleTile U => new ScrabbleTile { Letter = "U", Points = 1 };
        public static ScrabbleTile V => new ScrabbleTile { Letter = "V", Points = 4 };
        public static ScrabbleTile W => new ScrabbleTile { Letter = "W", Points = 4 };
        public static ScrabbleTile X => new ScrabbleTile { Letter = "X", Points = 8 };
        public static ScrabbleTile Y => new ScrabbleTile { Letter = "Y", Points = 4 };
        public static ScrabbleTile Z => new ScrabbleTile { Letter = "Z", Points = 10 };

        internal static IScrabbleTileConst Get( char value )
        {
            switch (value)
            {
                case 'A':
                    return A;
                case 'B':
                    return B;
                case 'C':
                    return C;
                case 'D':
                    return D;
                case 'E':
                    return E;
                case 'F':
                    return F;
                case 'G':
                    return G;
                case 'H':
                    return H;
                case 'I':
                    return I;
                case 'J':
                    return J;
                case 'K':
                    return K;
                case 'L':
                    return L;
                case 'M':
                    return M;
                case 'N':
                    return N;
                case 'O':
                    return O;
                case 'P':
                    return P;
                case 'Q':
                    return Q;
                case 'R':
                    return R;
                case 'S':
                    return S;
                case 'T':
                    return T;
                case 'U':
                    return U;
                case 'V':
                    return V;
                case 'W':
                    return W;
                case 'X':
                    return X;
                case 'Y':
                    return Y;
                case 'Z':
                    return Z;
                default:
                    throw new ArgumentException( $"Unknown character {value}" );
            }
        }
    }
}
