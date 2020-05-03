using algorithms.MapRepresentation;
using algorithms.TreeSearching;
using games.TicTacToe;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace algos.Image
{
    internal class TicTacToeVisualizer
    {
        private const int tileSize = 160;

        private const int borderSize = 16;

        private const int lineSize = 8;

        private static SolidBrush green = new SolidBrush(Color.FromArgb(42, 174, 35));

        private static SolidBrush blue = new SolidBrush(Color.FromArgb(26, 131, 127));

        private static SolidBrush red = new SolidBrush(Color.FromArgb(213, 42, 47));

        private static SolidBrush orange = new SolidBrush(Color.FromArgb(215, 119, 43));

        private TicTacToe _game;

        public TicTacToeVisualizer(TicTacToe game)
        {
            _game = game;
        }

        public Bitmap Render(SearchResult result)
        {
            var b = new Bitmap(3 * tileSize, 3 * tileSize);
            var font = new Font("Consolas", tileSize * 0.75f);
            var line = new Pen(blue, lineSize);

            using (var g = Graphics.FromImage(b))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawLine(line, 0, tileSize, tileSize * 3, tileSize);
                g.DrawLine(line, 0, tileSize * 2, tileSize * 3, tileSize * 2);
                g.DrawLine(line, tileSize, 0, tileSize, tileSize * 3);
                g.DrawLine(line, tileSize * 2, 0, tileSize * 2, tileSize * 3);

                //g.DrawString(result.DebugStatus, new Font("Consolas", 8f), Brushes.Black, 0, 0);

                for (int y = 0; y < 3; y++)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        if (_game.Board[x, y] == Player.One)
                        {
                            var size = g.MeasureString("x", font);
                            g.DrawString("x", font, red, x * tileSize + tileSize / 2 - size.Width / 2, y * tileSize + tileSize / 2 - size.Height / 2);
                        }
                        else if (_game.Board[x, y] == Player.Two)
                        {
                            var size = g.MeasureString("o", font);
                            g.DrawString("o", font, orange, x * tileSize + tileSize / 2 - size.Width / 2, y * tileSize + tileSize / 2 - size.Height / 2);
                        }
                    }
                }
            }

            return b;
        }
    }
}