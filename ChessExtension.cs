using System.Collections.Generic;

public static class ChessExtension
{
    public static bool IsWhite(this Piece piece)
        => (byte)piece > 0 && (byte)piece < 7;

    public static bool IsBlack(this Piece piece)
        => (byte)piece > 6 && (byte)piece < 13;

    public static bool IsWhiteCheck(this State state)
    {
        return false;
    }
    
    public static bool IsBlackCheck(this State state)
    {
        return false;
    }

    public static IEnumerable<State> Next(this State state, bool whitemove = true)
    {
        int i, j;
        Piece p;
        if (whitemove)
        {
            for (j = 0; j < 8; j++)
            {
                for (i = 0; i < 8; i++)
                {
                    p = state[i, j];
                    if (!p.IsWhite())
                        continue;
                    switch (p)
                    {
                        case Piece.WhitePawn:
                            break;
                        case Piece.WhiteRook:
                            break;
                        case Piece.WhiteKnight:
                            break;
                        case Piece.WhiteBishop:
                            break;
                        case Piece.WhiteQueen:
                            break;
                        case Piece.WhiteKing:
                            break;
                    }
                }
            }
        }
        else
        {
            for (j = 0; j < 8; j++)
            {
                for (i = 0; i < 8; i++)
                {
                    p = state[i, j];
                    if (!p.IsBlack())
                        continue;
                    switch (p)
                    {
                        case Piece.BlackPawn:
                            break;
                        case Piece.BlackRook:
                            break;
                        case Piece.BlackKnight:
                            break;
                        case Piece.BlackBishop:
                            break;
                        case Piece.BlackQueen:
                            break;
                        case Piece.BlackKing:
                            break;
                    }
                }
            }
        }
        yield break;
    }
}