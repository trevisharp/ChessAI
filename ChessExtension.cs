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

    public static byte Value(this Piece piece)
    {
        switch (piece)
        {
            default:
            case Piece.None:
            case Piece.WhiteKing:
            case Piece.BlackKing:
                return 0;
            
            case Piece.WhiteBishop:
            case Piece.BlackBishop:
            case Piece.WhiteKnight:
            case Piece.BlackKnight:
                return 3;
            
            case Piece.BlackRook:
            case Piece.WhiteRook:
                return 5;
            
            case Piece.BlackQueen:
            case Piece.WhiteQueen:
                return 9;
        }
    }

    public static bool WhiteTouchIn(this State state, int i, int j)
    {
        return false;
    }

    public static bool BlackTouchIn(this State state, int i, int j)
    {
        return false;
    }

    public static bool IsWhiteCheck(this State state)
    {
        return false;
    }

    public static bool IsBlackCheck(this State state)
    {
        return false;
    }

    // TODO: Check Rook Condition
    // TODO: Check Condition
    public static State MoveCertify(this State state, int i, 
        int j, int ti, int tj, Piece piece = Piece.None)
    {
        State ns = State.Empty;
        int di = ti - i, dj = tj - j, ui, uj;
        int column;
        bool canmove = false;
        Piece target = state[ti, tj];

        if (i == ti && j == tj)
            return ns;

        if (piece == Piece.None)
            piece = state[i, j];

        //Play Certify
        switch (piece)
        {
            case Piece.WhitePawn:
                #region WhitePawn
                if (di == 0)
                {
                    if (dj == 2 && j == 1 && target == Piece.None)
                        ns = state.Move(i, j, ti, tj, false, false, false, false, (byte)i, 0, 0);
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
                            ns = state.Move(i, j, ti, tj, false, false, false, false, 0, -1);
                            ns[ti, tj - 1] = Piece.None;
                        }
                    }
                }
                //Promotion
                if (dj == 1 && ns != State.Empty && tj == 7)
                {
                    ns = state.Move(i, j, ti, tj, 8, 0);
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
                canmove = true;
                if (di != dj && di != -dj)
                    break;
                if (dj > 0 && di > 0)
                {
                    for (int d = 1; d < di; d++)
                    {
                        if (state[i + d, j + d] != Piece.None)
                        {
                            canmove = false;
                            break;
                        }
                    }
                }
                else if (dj > 0 && di < 0)
                {
                    for (int d = 1; d < di; d++)
                    {
                        if (state[i - d, j + d] != Piece.None)
                        {
                            canmove = false;
                            break;
                        }
                    }
                }
                else if (dj < 0 && di > 0)
                {
                    for (int d = 1; d < di; d++)
                    {
                        if (state[i + d, j - d] != Piece.None)
                        {
                            canmove = false;
                            break;
                        }
                    }
                }
                else if (dj < 0 && di < 0)
                {
                    for (int d = 1; d < di; d++)
                    {
                        if (state[i - d, j - d] != Piece.None)
                        {
                            canmove = false;
                            break;
                        }
                    }
                }
                else canmove = false;

                if (target.IsWhite())
                    canmove = false;
                if (canmove)
                    ns = state.Move(i, j, ti, tj);
                #endregion
                break;
            case Piece.WhiteQueen:
                #region WhiteQueen
                if (di != dj && di != -dj && di != 0 && dj != 0)
                    break;
                ui = di > 0 ? 1 : (di < 0 ? -1 : 0);
                uj = dj > 0 ? 1 : (dj < 0 ? -1 : 0);
                canmove = true;
                for (int _i = i + ui, _j = j + uj; _i < ti || _j < tj; _i += ui, _j += uj)
                {
                    if (state[_i, _j] != Piece.None)
                    {
                        canmove = false;
                        break;
                    }
                }
                if (target.IsWhite())
                    break;
                if (canmove)
                    ns = state.Move(i, j, ti, tj);
                #endregion
                break;
            case Piece.WhiteKing:
                #region WhiteKing
                if (target.IsWhite())
                    break;
                if (di > 1 || di < -1 || dj > 1 || dj < -1)
                {
                    if (state.CanWhiteLeftCastling && di == -2)
                    {
                        if (state[0, 0] == Piece.WhiteRook &&
                             state[1, 0] == Piece.None &&
                             state[2, 0] == Piece.None &&
                             state[3, 0] == Piece.None)
                        {
                            ns = state.Move(i, j, ti, tj, false, false, true, true, false, true);
                            ns[0, 0] = Piece.None;
                            ns[3, 0] = Piece.WhiteRook;
                        }
                        else break;
                    }
                    else if (state.CanWhiteRightCastling && di == 2)
                    {
                        if (state[7, 0] == Piece.WhiteRook &&
                             state[6, 0] == Piece.None &&
                             state[5, 0] == Piece.None)
                        {
                            ns = state.Move(i, j, ti, tj, false, false, true, true, false, true);
                            ns[7, 0] = Piece.None;
                            ns[5, 0] = Piece.WhiteRook;
                        }
                        else break;
                    }
                    else break;
                }
                else ns = state.Move(i, j, ti, tj, false, false, true, true, false, true);
                #endregion
                break;

            case Piece.BlackPawn:
                #region BlackPawn
                if (di == 0)
                {
                    if (dj == -2 && j == 6 && target == Piece.None)
                        ns = state.Move(i, j, ti, tj, false, false, false, false, (byte)i, 0, 0);
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
                            ns = state.Move(i, j, ti, tj, false, false, false, false, -1, 0);
                            ns[ti, tj + 1] = Piece.None;
                        }
                    }
                }
                //Promotion
                if (dj == -1 && ns != State.Empty && tj == 0)
                {
                    ns = state.Move(i, j, ti, tj, 0, 8);
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
                canmove = true;
                if (di != dj && di != -dj)
                    break;
                if (dj > 0 && di > 0)
                {
                    for (int d = 1; d < di; d++)
                    {
                        if (state[i + d, j + d] != Piece.None)
                        {
                            canmove = false;
                            break;
                        }
                    }
                }
                else if (dj > 0 && di < 0)
                {
                    for (int d = 1; d < di; d++)
                    {
                        if (state[i - d, j + d] != Piece.None)
                        {
                            canmove = false;
                            break;
                        }
                    }
                }
                else if (dj < 0 && di > 0)
                {
                    for (int d = 1; d < di; d++)
                    {
                        if (state[i + d, j - d] != Piece.None)
                        {
                            canmove = false;
                            break;
                        }
                    }
                }
                else if (dj < 0 && di < 0)
                {
                    for (int d = 1; d < di; d++)
                    {
                        if (state[i - d, j - d] != Piece.None)
                        {
                            canmove = false;
                            break;
                        }
                    }
                }
                else canmove = false;

                if (target.IsBlack())
                    canmove = false;
                if (canmove)
                    ns = state.Move(i, j, ti, tj);
                #endregion
                break;
            case Piece.BlackQueen:
                #region BlackQueen
                if (di != dj && di != -dj && di != 0 && dj != 0)
                    break;
                ui = di > 0 ? 1 : (di < 0 ? -1 : 0);
                uj = dj > 0 ? 1 : (dj < 0 ? -1 : 0);
                canmove = true;
                for (int _i = i + ui, _j = j + uj; _i < ti || _j < tj; _i += ui, _j += uj)
                {
                    if (state[_i, _j] != Piece.None)
                    {
                        canmove = false;
                        break;
                    }
                }
                if (target.IsBlack())
                    break;
                if (canmove)
                    ns = state.Move(i, j, ti, tj);
                #endregion
                break;
            case Piece.BlackKing:
                #region BlackKing
                if (target.IsBlack())
                    break;
                if (di > 1 || di < -1 || dj > 1 || dj < -1)
                {
                    if (state.CanBlackLeftCastling && di == -2)
                    {
                        if (state[0, 7] == Piece.BlackRook &&
                             state[1, 7] == Piece.None &&
                             state[2, 7] == Piece.None &&
                             state[3, 7] == Piece.None)
                        {
                            ns = state.Move(i, j, ti, tj, true, true, false, false, true, false);
                            ns[0, 7] = Piece.None;
                            ns[3, 7] = Piece.BlackRook;
                        }
                        else break;
                    }
                    else if (state.CanBlackRightCastling && di == 2)
                    {
                        if (state[7, 7] == Piece.BlackRook &&
                             state[6, 7] == Piece.None &&
                             state[5, 7] == Piece.None)
                        {
                            ns = state.Move(i, j, ti, tj, true, true, false, false, true, false);
                            ns[7, 7] = Piece.None;
                            ns[5, 7] = Piece.BlackRook;
                        }
                        else break;
                    }
                    else break;
                }
                else ns = state.Move(i, j, ti, tj, false, false, true, true, false, true);
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
                            if (state[i, j - 1] == Piece.None 
                                && j > 1 
                                && notCauseWhiteCheck(i, j - 1))
                                yield return state.Move(i, j, i, j - 1);
                            
                            if (state[i + 1, j - 1].IsWhite() 
                                && i < 7
                                && notCauseWhiteCheck(i + 1, j - 1))
                                yield return state.Move(i, j, i + 1, j - 1);
                            
                            if (state[i + 1, j - 1].IsWhite() 
                                && i > 0
                                && notCauseWhiteCheck(i - 1, j - 1))
                                yield return state.Move(i, j, i + 1, j - 1);
                            
                            if (state[i, j - 2] == Piece.None 
                                && j == 1 
                                && notCauseWhiteCheck(i, j - 2))
                                yield return state.Move(i, j, i + 1, j - 1, false, false, false, false, i);
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

        bool notCauseWhiteCheck(int ti, int tj)
        {
            int vi = state.WhiteKingXLocation - i, 
                vj = state.WhiteKingYLocation - j,
                di = ti - i,
                dj = tj - j;
            
            if (vi == 0 && dj != 0)
            {
                
            }
            else if (vj == 0)
            {

            }
            else if (vi == vj || vi == -vj)
            {

            }

            return true;
        }

        bool notCauseBlackCheck(int ti, int tj)
        {
            return false;
        }
    }
}