using System;
using System.Drawing;
using System.Windows.Forms;

Application.SetHighDpiMode(HighDpiMode.SystemAware);
Application.EnableVisualStyles();
Application.SetCompatibleTextRenderingDefault(false);
Application.Run(new FrmMain());

/// <summary>
/// Simple Interface
/// </summary>
public class FrmMain : Form
{
    public Game Game { get; set; }

    /// <summary>
    /// Build Interface interact with Game
    /// </summary>
    public FrmMain()
    {
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.Text = "Chess AI";
        this.WindowState = FormWindowState.Maximized;

        PictureBox pb = new PictureBox();
        pb.Dock = DockStyle.Fill;
        this.Controls.Add(pb);

        Timer t = new Timer();
        t.Interval = 40;

        Graphics g = null;
        Action draw = null;

        Action<int, int, int, int> drawijpiece = null;
        Action<Piece, int, int> drawpiece = null;

        Bitmap pieces = Image.FromFile("pieces.png") as Bitmap;
        int size = 0, padding = 0;
        Brush b1 = new SolidBrush(Color.FromArgb(235, 236, 208)),
              b2 = new SolidBrush(Color.FromArgb(119, 149, 86));
        int _x = -1, _y = -1;
        Point? p = null;
        Piece? sp = null;
        this.Game = new Game();

        this.Load += delegate
        {
            Bitmap bmp = new Bitmap(pb.Width, pb.Height);
            g = Graphics.FromImage(bmp);
            draw = delegate
            {
                pb.Image = bmp;
            };
            size = (pb.Height - 20) / 8;
            padding = (pb.Width - 20 - 8 * size) / 2;
            drawijpiece = (i, j, x, y) =>
            {
                g.DrawImage(pieces, new Rectangle(padding + 20 + x * size, pb.Height - 20 - (y + 1) * size, size, size),
                    new Rectangle(i * 200, j * 200, 200, 200), GraphicsUnit.Pixel);
            };
            drawpiece = (piece, x, y) =>
            {
                switch (piece)
                {
                    case Piece.None:
                        break;
                    case Piece.WhitePawn:
                        drawijpiece(5, 0, x, y);
                        break;
                    case Piece.WhiteRook:
                        drawijpiece(4, 0, x, y);
                        break;
                    case Piece.WhiteKnight:
                        drawijpiece(3, 0, x, y);
                        break;
                    case Piece.WhiteBishop:
                        drawijpiece(2, 0, x, y);
                        break;
                    case Piece.WhiteQueen:
                        drawijpiece(1, 0, x, y);
                        break;
                    case Piece.WhiteKing:
                        drawijpiece(0, 0, x, y);
                        break;
                    case Piece.BlackPawn:
                        drawijpiece(5, 1, x, y);
                        break;
                    case Piece.BlackRook:
                        drawijpiece(4, 1, x, y);
                        break;
                    case Piece.BlackKnight:
                        drawijpiece(3, 1, x, y);
                        break;
                    case Piece.BlackBishop:
                        drawijpiece(2, 1, x, y);
                        break;
                    case Piece.BlackQueen:
                        drawijpiece(1, 1, x, y);
                        break;
                    case Piece.BlackKing:
                        drawijpiece(0, 1, x, y);
                        break;
                }
            };
            t.Start();
        };

        t.Tick += delegate
        {
            g.Clear(Color.White);

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if ((x + y) % 2 == 0)
                        g.FillRectangle(b1, padding + 20 + x * size, pb.Height - 20 - (y + 1) * size, size, size);
                    else g.FillRectangle(b2, padding + 20 + x * size, pb.Height - 20 - (y + 1) * size, size, size);
                    //Don't draw the selected piece
                    if (_x == x && _y == y)
                        continue;
                    drawpiece(Game.Current[x, y], x, y);
                }
            }
            if (sp.HasValue)
            {
                drawpiece(sp.Value, (p.Value.X - 20 - padding) / size, (pb.Height - p.Value.Y - 20) / size);
            }
            draw();
        };

        pb.MouseDown += (s, e) =>
        {
            p = e.Location;
            _x = (p.Value.X - 20 - padding) / size;
            _y = (pb.Height - p.Value.Y - 20) / size;
            if (_x >= 0 && _x < 8 && _y >= 0 && _y < 8)
                sp = Game.Current[_x, _y];
        };
        pb.MouseUp += (s, e) =>
        {
            int x = (p.Value.X - 20 - padding) / size,
                y = (pb.Height - p.Value.Y - 20) / size;
            if (sp.HasValue && _x != x && _y != y)
            {
                OnMove(_x, _y, x, y);
            }
            p = null;
            _x = _y = -1;
            sp = null;
        };
        pb.MouseMove += (s, e) =>
        {
            if (p.HasValue)
            {
                p = e.Location;
            }
        };
    }

    public void OnMove(int sx, int sy, int ex, int ey)
    {
        
    }
}