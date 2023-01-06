using algorithms.MapRepresentation;
using algorithms.PathFinding;
using algorithms.TreeSearching;
using algos.Image;
using games.TicTacToe;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace algos
{
    public partial class Main : Form
    {
        private TicTacToe ttt;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            RenderAStarVisualization();
            RenderVoronoiVisualization();
            RenderFloodFillVisualization();
            RenderMonteCarloVisualization();
        }

        private void RenderAStarVisualization()
        {
            var map = OceanNavigator.FromString(new string[]
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

            var mv = new OceanNavigatorVisualizer(map);

            pictureBox1.Image = mv.Render(path);
        }

        private void RenderVoronoiVisualization()
        {
            var map = OceanNavigator.FromString(new string[]
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

            var voronoi = new Voronoi(map);

            voronoi.Add(Player.One, 3, 1);
            voronoi.Add(Player.One, 2, 14);

            voronoi.Add(Player.Two, 6, 10);
            voronoi.Add(Player.Two, 12, 11);

            var grid = voronoi.Fill();

            var mv = new OceanNavigatorVisualizer(map);

            pictureBox4.Image = mv.Render(grid);
        }

        private void RenderFloodFillVisualization()
        {
            var map = OceanNavigator.FromString(new string[]
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

            var mv = new OceanNavigatorVisualizer(map);

            pictureBox2.Image = mv.Render(overlay);
        }

        private void RenderMonteCarloVisualization()
        {
            ttt = new TicTacToe();

            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var timer = Stopwatch.StartNew();

            var mcts = new MonteCarloTreeSearch(true);

            var best = mcts.GetBestMove(ttt, new SearchCancellationToken(() => timer.ElapsedMilliseconds > 1000));

            ttt.MakeMove(best.BestMove);

            e.Result = best;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            var mv = new TicTacToeVisualizer(ttt);

            pictureBox3.Image = mv.Render(e.Result as SearchResult);

            if (ttt.GetAllMoves().Count == 0)
            {
                ttt = new TicTacToe();
            }

            backgroundWorker1.RunWorkerAsync();
        }
    }
}