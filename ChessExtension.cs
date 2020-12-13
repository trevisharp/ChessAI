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
        State ns = State.Empty;
        int di = ti - i, dj = tj - j;
        int column;
        bool canmove = false;
        Piece target = state[ti, tj];

        //Play Certify
        switch (state[i, j])
        {
            case Piece.WhitePawn:
                #region WhitePawn
                if (di == 0)
                {
                    if (dj == 2 && j == 1 && target == Piece.None)
                        ns = state.Move(i, j, ti, tj, false, false, false, false, (byte)i);
                    else if (dj == 1 && target == Piece.None)
                        ns = state.Move(i, j, ti, tj);
                }
                else if ((di == 1 && dj == 1) || (di == -1 && dj == 1))
                {
                    if (target.IsBlack())
                        ns = state.Move(i, j, ti, tj);
                    else if (state.EnPassantInfo > 0) //EnPassant
                    {
                        column = state.EnPassantInfo - 1;
                        if (column == ti && j == 4)
                        {
                            ns = state.Move(i, j, ti, tj, false, false, false, false);
                            ns[ti, tj - 1] = Piece.None;
                        }
                    }
                }
                //Promotion
                if (dj == 1 && ns != State.Empty && tj == 7)
                {
                    ns = state.Move(i, j, ti, tj);
                    ns[ti, tj] = Piece.WhiteQueen;
                }
                #endregion
                break;
            case Piece.WhiteRook:
                #region WhiteRook
                canmove = true;
                if (dj == 0 && di > 0)
                {
                    for (int _i = i + 1; _i < ti; _i++)
                    {
                        if (state[_i, j] != Piece.None)
                        {
                            canmove = false;
                            break;
                        }
                    }
                }
                else if (dj == 0 && di < 0)
                {
                    for (int _i = i - 1; _i > ti; _i--)
                    {
                        if (state[_i, j] != Piece.None)
                        {
                            canmove = false;
                            break;
                        }
                    }
                }
                else if (dj > 0 && di == 0)
                {
                    for (int _j = j + 1; _j < tj; _j++)
                    {
                        if (state[i, _j] != Piece.None)
                        {
                            canmove = false;
                            break;
                        }
                    }
                }
                else if (dj < 0 && di == 0)
                {
                    for (int _j = j - 1; _j > tj; _j--)
                    {
                        if (state[i, _j] != Piece.None)
                        {
                            canmove = false;
                            break;
                        }
                    }
                }
                else canmove = false;

                if (state[ti, tj].IsWhite())
                    canmove = false;
                if (canmove)
                {
                    if (j == 0)
                    {
                        if (i == 0)
                            ns = state.Move(i, j, ti, tj, false, false, true, false);
                        else if (i == 7)
                            ns = state.Move(i, j, ti, tj, false, false, false, true);
                        else ns = state.Move(i, j, ti, tj);
                    }
                    else ns = state.Move(i, j, ti, tj);
                }
                #endregion
                break;
            case Piece.WhiteKnight:
                #region WhiteKnight
                if (!target.IsWhite() && (
                    (di == 1 && dj == 2) ||
                    (di == -1 && dj == 2) ||
                    (di == 1 && dj == -2) ||
                    (di == -1 && dj == -2) ||
                    (di == 2 && dj == 1) ||
                    (di == -2 && dj == 1) ||
                    (di == 2 && dj == -1) ||
                    (di == -2 && dj == -1)
                    )) ns = state.Move(i, j, ti, tj);
                #endregion
                break;
            case Piece.WhiteBishop:
                #region WhiteBishop
                #endregion
                break;
            case Piece.WhiteQueen:
                #region WhiteQueen
                #endregion
                break;
            case Piece.WhiteKing:
                #region WhiteKing
                #endregion
                break;

            case Piece.BlackPawn:
                #region BlackPawn
                if (di == 0)
                {
                    if (dj == -2 && j == 6 && target == Piece.None)
                        ns = state.Move(i, j, ti, tj, false, false, false, false, (byte)i);
                    else if (dj == -1 && target == Piece.None)
                        ns = state.Move(i, j, ti, tj);
                }
                else if ((di == 1 && dj == -1) || (di == -1 && dj == -1))
                {
                    if (target.IsWhite())
                        ns = state.Move(i, j, ti, tj);
                    else if (state.EnPassantInfo > 0) //EnPassant
                    {
                        column = state.EnPassantInfo - 1;
                        if (column == ti && j == 3)
                        {
                            ns = state.Move(i, j, ti, tj, false, false, false, false);
                            ns[ti, tj + 1] = Piece.None;
                        }
                    }
                }
                //Promotion
                if (dj == -1 && ns != State.Empty && tj == 0)
                {
                    ns = state.Move(i, j, ti, tj);
                    ns[ti, tj] = Piece.BlackQueen;
                }
                #endregion
                break;
            case Piece.BlackRook:
                #region BlackRook
                canmove = true;
                if (dj == 0 && di > 0)
                {
                    for (int _i = i + 1; _i < ti; _i++)
                    {
                        if (state[_i, j] != Piece.None)
                        {
                            canmove = false;
                            break;
                        }
                    }
                }
                else if (dj == 0 && di < 0)
                {
                    for (int _i = i - 1; _i > ti; _i--)
                    {
                        if (state[_i, j] != Piece.None)
                        {
                            canmove = false;
                            break;
                        }
                    }
                }
                else if (dj > 0 && di == 0)
                {
                    for (int _j = j + 1; _j < tj; _j++)
                    {
                        if (state[i, _j] != Piece.None)
                        {
                            canmove = false;
                            break;
                        }
                    }
                }
                else if (dj < 0 && di == 0)
                {
                    for (int _j = j - 1; _j > tj; _j--)
                    {
                        if (state[i, _j] != Piece.None)
                        {
                            canmove = false;
                            break;
                        }
                    }
                }
                else canmove = false;

                if (state[ti, tj].IsBlack())
                    canmove = false;
                if (canmove)
                {
                    if (j == 7)
                    {
                        if (i == 0)
                            ns = state.Move(i, j, ti, tj, true, false, false, false);
                        else if (i == 7)
                            ns = state.Move(i, j, ti, tj, false, true, false, false);
                        else ns = state.Move(i, j, ti, tj);
                    }
                    else ns = state.Move(i, j, ti, tj);
                }
                #endregion
                break;
            case Piece.BlackKnight:
                #region BlackKnight
                if (!target.IsBlack() && (
                    (di == 1 && dj == 2) ||
                    (di == -1 && dj == 2) ||
                    (di == 1 && dj == -2) ||
                    (di == -1 && dj == -2) ||
                    (di == 2 && dj == 1) ||
                    (di == -2 && dj == 1) ||
                    (di == 2 && dj == -1) ||
                    (di == -2 && dj == -1)
                    )) ns = state.Move(i, j, ti, tj);
                #endregion
                break;
            case Piece.BlackBishop:
                #region BlackBishop
                #endregion
                break;
            case Piece.BlackQueen:
                #region BlackQueen
                #endregion
                break;
            case Piece.BlackKing:
                #region BlackKing
                #endregion
                break;
        }

        //Check/Checkmate Certify
        if (ns != State.Empty)
        {

        }

        return ns;
    }

    public static IEnumerable<State> Next(this State state, bool whitemove = true)
    {
        byte i, j;
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