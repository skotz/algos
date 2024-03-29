﻿using algorithms.MapRepresentation;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace algos.Image
{
    internal class OceanNavigatorVisualizer
    {
        private const int tileSize = 32;

        private const int borderSize = 4;

        private const int lineSize = 8;

        private static SolidBrush green = new SolidBrush(Color.FromArgb(42, 174, 35));

        private static SolidBrush blue = new SolidBrush(Color.FromArgb(26, 131, 127));

        private static SolidBrush red = new SolidBrush(Color.FromArgb(213, 42, 47));

        private static SolidBrush orange = new SolidBrush(Color.FromArgb(215, 119, 43));

        private static SolidBrush black = new SolidBrush(Color.FromArgb(0, 0, 0));

        private OceanNavigator _map;

        public OceanNavigatorVisualizer(OceanNavigator map)
        {
            _map = map;
        }

        public Bitmap Render()
        {
            return Render(new List<Location>());
        }

        public Bitmap Render(List<Location> path)
        {
            var b = new Bitmap(_map.Width * tileSize, _map.Height * tileSize);

            using (var g = Graphics.FromImage(b))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                for (int y = 0; y < _map.Height; y++)
                {
                    for (int x = 0; x < _map.Width; x++)
                    {
                        if (!_map[x, y].Traversable)
                        {
                            g.FillRectangle(green, x * tileSize, y * tileSize, tileSize, tileSize);
                        }
                        else
                        {
                            g.FillRectangle(blue, x * tileSize, y * tileSize, tileSize, tileSize);
                        }
                    }
                }

                if (path.Count > 1)
                {
                    var points = path.Select(p => new Point(p.X * tileSize + tileSize / 2, p.Y * tileSize + tileSize / 2)).ToArray();
                    var pen = new Pen(red, lineSize);
                    pen.StartCap = LineCap.Square;
                    pen.EndCap = LineCap.Square;
                    g.DrawLines(pen, points);
                }
            }

            return b;
        }

        public Bitmap Render(bool[,] overlay)
        {
            var b = new Bitmap(_map.Width * tileSize, _map.Height * tileSize);

            using (var g = Graphics.FromImage(b))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                for (int y = 0; y < _map.Height; y++)
                {
                    for (int x = 0; x < _map.Width; x++)
                    {
                        if (!_map[x, y].Traversable)
                        {
                            g.FillRectangle(green, x * tileSize, y * tileSize, tileSize, tileSize);
                        }
                        else
                        {
                            g.FillRectangle(blue, x * tileSize, y * tileSize, tileSize, tileSize);
                        }

                        if (overlay[x, y])
                        {
                            g.FillRectangle(red, x * tileSize + borderSize, y * tileSize + borderSize, tileSize - borderSize * 2, tileSize - borderSize * 2);
                        }
                    }
                }
            }

            return b;
        }

        public Bitmap Render(Player[,] overlay)
        {
            var b = new Bitmap(_map.Width * tileSize, _map.Height * tileSize);

            using (var g = Graphics.FromImage(b))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                for (int y = 0; y < _map.Height; y++)
                {
                    for (int x = 0; x < _map.Width; x++)
                    {
                        if (!_map[x, y].Traversable)
                        {
                            g.FillRectangle(green, x * tileSize, y * tileSize, tileSize, tileSize);
                        }
                        else
                        {
                            g.FillRectangle(blue, x * tileSize, y * tileSize, tileSize, tileSize);
                        }

                        if (overlay[x, y] == Player.One)
                        {
                            g.FillRectangle(red, x * tileSize + borderSize, y * tileSize + borderSize, tileSize - borderSize * 2, tileSize - borderSize * 2);
                        }
                        else if (overlay[x, y] == Player.Two)
                        {
                            g.FillRectangle(orange, x * tileSize + borderSize, y * tileSize + borderSize, tileSize - borderSize * 2, tileSize - borderSize * 2);
                        }
                        else if (overlay[x, y] == Player.Both)
                        {
                            g.FillRectangle(black, x * tileSize + borderSize, y * tileSize + borderSize, tileSize - borderSize * 2, tileSize - borderSize * 2);
                        }
                    }
                }
            }

            return b;
        }
    }
}