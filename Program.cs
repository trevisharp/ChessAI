using System;
using System.Drawing;
using System.Windows.Forms;

Application.SetHighDpiMode(HighDpiMode.SystemAware);
Application.EnableVisualStyles();
Application.SetCompatibleTextRenderingDefault(false);
Application.Run(new FrmMain());

public class FrmMain : Form
{
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
        Action<int, int, int, int> drawpiece = null;

        Bitmap pieces = Image.FromFile("pieces.png") as Bitmap;
        int size = 0, padding = 0;
        Brush b1 = new SolidBrush(Color.FromArgb(235, 236, 208)),
              b2 = new SolidBrush(Color.FromArgb(119, 149, 86));

        State state = State.Classic;

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
            drawpiece = (i, j, x, y) =>
            {
                g.DrawImage(pieces, new Rectangle(padding + 20 + x * size, 20 + y * size, size, size),
                    new Rectangle(i * 200, j * 200, 200, 200), GraphicsUnit.Pixel);
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
                        g.FillRectangle(b1, padding + 20 + x * size, 20 + y * size, size, size);
                    else g.FillRectangle(b2, padding + 20 + x * size, 20 + y * size, size, size);

                    switch (state[x, y])
                    {
                        case Piece.None:
                            break;
                        case Piece.WhitePawn:
                            drawpiece(5, 0, x, y);
                            break;
                        case Piece.WhiteRook:
                            drawpiece(4, 0, x, y);
                            break;
                        case Piece.WhiteKnight:
                            drawpiece(3, 0, x, y);
                            break;
                        case Piece.WhiteBishop:
                            drawpiece(2, 0, x, y);
                            break;
                        case Piece.WhiteQueen:
                            drawpiece(1, 0, x, y);
                            break;
                        case Piece.WhiteKing:
                            drawpiece(0, 0, x, y);
                            break;
                        case Piece.BlackPawn:
                            drawpiece(5, 1, x, y);
                            break;
                        case Piece.BlackRook:
                            drawpiece(4, 1, x, y);
                            break;
                        case Piece.BlackKnight:
                            drawpiece(3, 1, x, y);
                            break;
                        case Piece.BlackBishop:
                            drawpiece(2, 1, x, y);
                            break;
                        case Piece.BlackQueen:
                            drawpiece(1, 1, x, y);
                            break;
                        case Piece.BlackKing:
                            drawpiece(0, 1, x, y);
                            break;
                    }
                }
            }
            draw();
        };
    }
}