using algorithms.MapRepresentation;
using algorithms.PathFinding;
using algos.Image;
using System;
using System.Windows.Forms;

namespace algos
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            RenderAStarVisualization();
            RenderFloodFillVisualization();
        }

        private void RenderAStarVisualization()
        {
            var map = Board.FromString(new string[]
            {
                "    ##  ##   ##",
                "##           ##",
                "#### ##  ##   #",
                "  ## ######   #",
                "      ### ##   ",
                " ##   ### ###  ",
                " ###  ## ####  ",
                "#######  ####  ",
                "##   ####     #",
                "       ##   ###",
                "               ",
                "               ",
                "#####    ##   #",
                "#####  ####  ##",
                "       ##    ##"
            });

            var pf = new AStar(map);

            var path = pf.FindPath(new Location(0, 0), new Location(12, 14));

            var mv = new MapVisualizer(map);

            pictureBox1.Image = mv.Render(path);
        }

        private void RenderFloodFillVisualization()
        {
            var map = Board.FromString(new string[]
            {
                "    ##  ##   ##",
                "##           ##",
                "#### ##  ##   #",
                "  ## ######   #",
                "      ### ##   ",
                " ##   ### ###  ",
                " ###  ## ####  ",
                "#######  ####  ",
                "##   ####     #",
                "       ##   ###",
                "               ",
                "               ",
                "#####    ##   #",
                "#####  ####  ##",
                "       ##    ##"
            });

            var ff = new FloodFill(map);

            var overlay = ff.GetRegion(new Location(0, 0));

            var mv = new MapVisualizer(map);

            pictureBox2.Image = mv.Render(overlay);
        }
    }
}