using System;

public enum Piece : byte
{
    None = 0,
    WhitePawn = 1,
    WhiteRook = 2,
    WhiteKnight = 3,
    WhiteBishop = 4,
    WhiteQueen = 5,
    WhiteKing = 6,
    BlackPawn = 7,
    BlackRook = 8,
    BlackKnight = 9,
    BlackBishop = 10,
    BlackQueen = 11,
    BlackKing = 12
}

public readonly struct State
{
    private readonly byte[] board;
    private readonly byte aditionalinfo;

    public Piece this[int x, int y]
    {
        get => (Piece)board[8 * y + x];
        set => board[8 * y + x] = (byte)value;
    }

    public bool CanWhiteLeftCastling => this.aditionalinfo % 2 == 0;
    public bool CanWhiteRightCastling => (this.aditionalinfo >> 1) % 2 == 0;
    public bool CanBlackLeftCastling => (this.aditionalinfo >> 2) % 2 == 0;
    public bool CanBlackRightCastling => (this.aditionalinfo >> 3) % 2 == 0;
    public byte EnPassantInfo => (byte)(this.aditionalinfo >> 4);

    private State(bool empty)
    {
        board = new byte[64];
        aditionalinfo = empty ? 255 : 0;
    }

    private State(State original)
    {
        this.board = new byte[64];
        Array.Copy(original.board, this.board, 64);
        this.aditionalinfo = original.aditionalinfo;
    }

    private State(State original, int sx, int sy, int ex, int ey)
    {
        this.board = new byte[64];
        Array.Copy(original.board, this.board, 64);
        this.aditionalinfo = (byte)(original.aditionalinfo % 16);
        Piece p = this[sx, sy];
        this[ex, ey] = p;
        this[sx, sy] = Piece.None;
    }

    private State(State original, int sx, int sy, int ex, int ey,
        byte castlinginfo)
    {
        this.board = new byte[64];
        Array.Copy(original.board, this.board, 64);
        this.aditionalinfo = (byte)(castlinginfo % 16);
        Piece p = this[sx, sy];
        this[ex, ey] = p;
        this[sx, sy] = Piece.None;
    }

    private State(State original, int sx, int sy, int ex, int ey,
        byte castlinginfo, byte enpassantinfo)
    {
        this.board = new byte[64];
        Array.Copy(original.board, this.board, 64);
        this.aditionalinfo = (byte)(enpassantinfo << 4 + castlinginfo);
        Piece p = this[sx, sy];
        this[ex, ey] = p;
        this[sx, sy] = Piece.None;
    }

    /// <summary>
    /// Generate a new state com base in a moviment.
    /// </summary>
    /// <param name="sx">x start location of moviment.</param>
    /// <param name="sy">y start location of moviment.</param>
    /// <param name="ex">x end location of moviment.</param>
    /// <param name="ey">y end location of moviment.</param>
    /// <returns>A new state</returns>
    public State Move(int sx, int sy, int ex, int ey)
    {
        if (sx < 0 || sx > 7 || sy < 0 || sy > 7 ||
            ex < 0 || ex > 7 || ey < 0 || ey > 7)
            throw new InvalidOperationException("Um movimento ocorre fora do tabuleiro");
        return new State(this, sx, sy, ex, ey);
    }

    /// <summary>
    /// Generate a new state com base in a moviment.
    /// </summary>
    /// <param name="sx">x start location of moviment.</param>
    /// <param name="sy">y start location of moviment.</param>
    /// <param name="ex">x end location of moviment.</param>
    /// <param name="ey">y end location of moviment.</param>
    /// <param name="lefBlkLoseCastling">True if Black lose yours Castling left move.</param>
    /// <param name="rigBlkLoseCastling">True if Black lose yours Castling right move.</param>
    /// <param name="lefWhtLoseCastling">True if White lose yours Castling left move.</param>
    /// <param name="rigWhtLoseCastling">True if White lose yours Castling right move.</param>
    /// <returns>A new state</returns>
    public State Move(int sx, int sy, int ex, int ey,
        bool lefBlkLoseCastling, bool rigBlkLoseCastling,
        bool lefWhtLoseCastling, bool rigWhtLoseCastling)
    {
        if (sx < 0 || sx > 7 || sy < 0 || sy > 7 ||
            ex < 0 || ex > 7 || ey < 0 || ey > 7)
            throw new InvalidOperationException("Um movimento ocorre fora do tabuleiro");
        byte castlinginfo = (byte)(this.aditionalinfo % 16);
        if (lefWhtLoseCastling)
            castlinginfo += 1;
        if (rigWhtLoseCastling)
            castlinginfo += 2;
        if (lefBlkLoseCastling)
            castlinginfo += 4;
        if (rigBlkLoseCastling)
            castlinginfo += 8;
        return new State(this, sx, sy, ex, ey, castlinginfo);
    }

    /// <summary>
    /// Generate a new state com base in a moviment.
    /// </summary>
    /// <param name="sx">x start location of moviment.</param>
    /// <param name="sy">y start location of moviment.</param>
    /// <param name="ex">x end location of moviment.</param>
    /// <param name="ey">y end location of moviment.</param>
    /// <param name="lefBlkLoseCastling">True if Black lose yours Castling left move.</param>
    /// <param name="rigBlkLoseCastling">True if Black lose yours Castling right move.</param>
    /// <param name="lefWhtLoseCastling">True if White lose yours Castling left move.</param>
    /// <param name="rigWhtLoseCastling">True if White lose yours Castling right move.</param>
    /// <param name="column">En Passant Pawn Column.</param>
    /// <returns>A new state</returns>
    public State Move(int sx, int sy, int ex, int ey,
        bool lefBlkLoseCastling, bool rigBlkLoseCastling,
        bool lefWhtLoseCastling, bool rigWhtLoseCastling,
        byte column)
    {
        if (sx < 0 || sx > 7 || sy < 0 || sy > 7 ||
            ex < 0 || ex > 7 || ey < 0 || ey > 7)
            throw new InvalidOperationException("Um movimento ocorre fora do tabuleiro");
        byte castlinginfo = (byte)(this.aditionalinfo % 16);
        byte enpassantinfo = 0;
        if (lefWhtLoseCastling)
            castlinginfo += 1;
        if (rigWhtLoseCastling)
            castlinginfo += 2;
        if (lefBlkLoseCastling)
            castlinginfo += 4;
        if (rigBlkLoseCastling)
            castlinginfo += 8;
        castlinginfo += (byte)(column + 1);
        return new State(this, sx, sy, ex, ey, castlinginfo, enpassantinfo);
    }

    public State Copy()
        => new State(this);

    public static bool operator ==(State a, State b)
    {
        for (int i = 0; i < 64; i++)
            if (a.board[i] != b.board[i])
                return false;
        return a.aditionalinfo == b.aditionalinfo;
    }

    public static bool operator !=(State a, State b)
        => !(a == b);

    static State()
    {
        empty = new State(true);
        classic = new State(false)
        {
            [0, 0] = Piece.WhiteRook,
            [1, 0] = Piece.WhiteKnight,
            [2, 0] = Piece.WhiteBishop,
            [3, 0] = Piece.WhiteQueen,
            [4, 0] = Piece.WhiteKing,
            [5, 0] = Piece.WhiteBishop,
            [6, 0] = Piece.WhiteKnight,
            [7, 0] = Piece.WhiteRook,
            [0, 1] = Piece.WhitePawn,
            [1, 1] = Piece.WhitePawn,
            [2, 1] = Piece.WhitePawn,
            [3, 1] = Piece.WhitePawn,
            [4, 1] = Piece.WhitePawn,
            [5, 1] = Piece.WhitePawn,
            [6, 1] = Piece.WhitePawn,
            [7, 1] = Piece.WhitePawn,
            [0, 7] = Piece.BlackRook,
            [1, 7] = Piece.BlackKnight,
            [2, 7] = Piece.BlackBishop,
            [3, 7] = Piece.BlackQueen,
            [4, 7] = Piece.BlackKing,
            [5, 7] = Piece.BlackBishop,
            [6, 7] = Piece.BlackKnight,
            [7, 7] = Piece.BlackRook,
            [0, 6] = Piece.BlackPawn,
            [1, 6] = Piece.BlackPawn,
            [2, 6] = Piece.BlackPawn,
            [3, 6] = Piece.BlackPawn,
            [4, 6] = Piece.BlackPawn,
            [5, 6] = Piece.BlackPawn,
            [6, 6] = Piece.BlackPawn,
            [7, 6] = Piece.BlackPawn
        };
    }

    /// <summary>
    /// No Piece State
    /// </summary>
    private static readonly State empty;
    public static State Empty => empty;

    /// <summary>
    /// Classic Game For White Player
    /// </summary>
    private static readonly State classic;
    public static State Classic => classic;

    public override bool Equals(object obj)
        => obj is State s && s == this;

    public override int GetHashCode()
        => board.GetHashCode();
}