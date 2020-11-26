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

public sealed class State
{
    private byte[] board = new byte[64];

    public Piece this[int x, int y]
    {
        get => (Piece)board[8 * y + x];
        set => board[8 * y + x] = (byte)value;
    }

    private State() { }

    private State(State original)
    {
        Array.Copy(original.board, this.board, 64);

        this.CanWhiteLeftCastling = original.CanWhiteLeftCastling;
        this.CanWhiteRightCastling = original.CanWhiteRightCastling;
        this.CanBlackLeftCastling = original.CanBlackLeftCastling;
        this.CanBlackRightCastling = original.CanBlackRightCastling;
    }

    private State(State original, int sx, int sy, int ex, int ey) : this(original)
    {
        Piece p = this[sx, sy];
        this[ex, ey] = p;
        this[sx, sy] = Piece.None;
    }

    public bool CanWhiteLeftCastling { get; set; } = true;
    public bool CanWhiteRightCastling { get; set; } = true;
    public bool CanBlackLeftCastling { get; set; } = true;
    public bool CanBlackRightCastling { get; set; } = true;

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

    public State Copy()
        => new State(this);

    static State()
    {
        empty = new State();
        classicWhite = new State()
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
        classicBlack = new State()
        {
            [0, 0] = Piece.BlackRook,
            [1, 0] = Piece.BlackKnight,
            [2, 0] = Piece.BlackBishop,
            [3, 0] = Piece.BlackQueen,
            [4, 0] = Piece.BlackKing,
            [5, 0] = Piece.BlackBishop,
            [6, 0] = Piece.BlackKnight,
            [7, 0] = Piece.BlackRook,
            [0, 1] = Piece.BlackPawn,
            [1, 1] = Piece.BlackPawn,
            [2, 1] = Piece.BlackPawn,
            [3, 1] = Piece.BlackPawn,
            [4, 1] = Piece.BlackPawn,
            [5, 1] = Piece.BlackPawn,
            [6, 1] = Piece.BlackPawn,
            [7, 1] = Piece.BlackPawn,
            [0, 7] = Piece.WhiteRook,
            [1, 7] = Piece.WhiteKnight,
            [2, 7] = Piece.WhiteBishop,
            [3, 7] = Piece.WhiteQueen,
            [4, 7] = Piece.WhiteKing,
            [5, 7] = Piece.WhiteBishop,
            [6, 7] = Piece.WhiteKnight,
            [7, 7] = Piece.WhiteRook,
            [0, 6] = Piece.WhitePawn,
            [1, 6] = Piece.WhitePawn,
            [2, 6] = Piece.WhitePawn,
            [3, 6] = Piece.WhitePawn,
            [4, 6] = Piece.WhitePawn,
            [5, 6] = Piece.WhitePawn,
            [6, 6] = Piece.WhitePawn,
            [7, 6] = Piece.WhitePawn
        };
    }

    /// <summary>
    /// No Piece State
    /// </summary>
    private static readonly State empty;
    public static State Empty => new State(empty);

    /// <summary>
    /// Classic Game For White Player
    /// </summary>
    private static readonly State classicWhite;
    public static State ClassicWhite => new State(classicWhite);

    /// <summary>
    /// Classic Game For Black Player
    /// </summary>
    private static readonly State classicBlack;
    public static State ClassicBlack => new State(classicBlack);
}