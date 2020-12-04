using System.Collections.Generic;

/// <summary>
/// Extension Methods of States and Pieces
/// </summary>
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

    public static State MoveCertify(this State state, int i, int j, int ti, int tj)
    {
        switch (state[i, j])
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
        return State.Empty;
    }

    public static IEnumerable<State> Next(this State state, bool whitemove = true)
    {
        byte i, j;
        Piece p;
        State copy;
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
                            if (j == 1 && state[i, j + 1] == Piece.None)
                                yield return state.Move(i, j, i, j + 2, false, false, false, false, i);
                            if (state[i, j + 1] == Piece.None)
                                yield return state.Move(i, j, i, j + 1);
                            if (i < 7 && state[i + 1, j + 1].IsBlack())
                                yield return state.Move(i, j, i + 1, j + 1);
                            if (i > 0 && state[i - 1, j + 1].IsBlack())
                                yield return state.Move(i, j, i - 1, j + 1);
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
                            if (j == 6 && state[i, j - 1] == Piece.None)
                                yield return state.Move(i, j, i, j - 2, false, false, false, false, i);
                            if (state[i, j - 1] == Piece.None)
                                yield return state.Move(i, j, i, j - 1);
                            if (i < 7 && state[i + 1, j - 1].IsBlack())
                                yield return state.Move(i, j, i + 1, j - 1);
                            if (i > 0 && state[i - 1, j - 1].IsBlack())
                                yield return state.Move(i, j, i - 1, j - 1);
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