using QuickGraph;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace ChromaticGraphTheory.GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.MinimumSize = new Size(this.Width, this.Height);

            var bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using(Graphics g = Graphics.FromImage(bitmap))
            {
                g.FillRectangle(Brushes.White, 0, 0, this.Width - 200, this.Height);
            }
            pictureBox1.Image = bitmap;
            pictureBox1.Refresh();
        }

        List<(int x, int y)> vertices = new List<(int, int)>();
        List<((int x, int y) p1, (int x, int y) p2)> edges = new List<((int, int), (int, int))>();
        
        int radius = 16;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            using(Pen p = new Pen(Color.Black))
            {
                e.Graphics.FillRectangle(Brushes.White, 0, 0, this.Width - 200, this.Height);
                p.CustomEndCap = new AdjustableArrowCap(10, 10);
                
                foreach (var (e1, e2) in edges)
                {
                    int x1 = e1.x, x2 = e2.x, y1 = e1.y, y2 = e2.y;

                    double sqrt = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
                    int x3 = (int)(x2 - (x2 - x1) * radius / sqrt);
                    int y3 = (int)(y2 - (y2 - y1) * radius / sqrt);
                    e.Graphics.DrawLine(p, e1.x, e1.y, x3, y3);
                }

                foreach (var (x, y) in vertices)
                {
                    e.Graphics.FillEllipse((x,y) == selected ? Brushes.Gray : Brushes.Black, x - radius, y - radius, 2 * radius, 2 * radius);
                }
            }

            pictureBox1.Invalidate();
        }

        (int x, int y) selected = (-1, -1);

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var u = e as MouseEventArgs;
            if(u.Button == MouseButtons.Left)
            {
                if (vertices.Any(v => (v.x - u.X) * (v.x - u.X) + (v.y - u.Y) * (v.y - u.Y) <= radius * radius))
                {
                    var newSelected = vertices.Find(v => (v.x - u.X) * (v.x - u.X) + (v.y - u.Y) * (v.y - u.Y) <= radius * radius);
                    if (selected == (-1, -1))
                    {
                        selected = newSelected;
                    }
                    else if(selected == newSelected)
                    {
                        selected = (-1, -1);
                    }
                    else
                    {
                        if (edges.All(edge => edge != (selected, newSelected) && edge != (newSelected, selected)))
                        {
                            edges.Add((selected, newSelected));
                            selected = (-1, -1);
                        } else
                        {
                            selected = newSelected;
                        }
                    }
                }
                else if (vertices.Any(v => (v.x - u.X) * (v.x - u.X) + (v.y - u.Y) * (v.y - u.Y) <= 4 * radius * radius))
                {
                    if ((selected.x - u.X) * (selected.x - u.X) + (selected.y - u.Y) * (selected.y - u.Y) <= radius * radius)
                    {
                        selected = (-1, -1);
                    }
                        
                    return;
                }
                else
                {
                    vertices.Add((u.X, u.Y));
                    if(selected != (-1, -1))
                    {
                        edges.Add((selected, (u.X, u.Y)));
                        selected = (-1, -1);
                    }
                }
            }
            else if(u.Button == MouseButtons.Right)
            {
                try
                {
                    var vtx = vertices.First(v => (v.x - u.X) * (v.x - u.X) + (v.y - u.Y) * (v.y - u.Y) <= radius * radius);
                    vertices.Remove(vtx);
                    edges = edges.Where(edge => edge.p1 != vtx && edge.p2 != vtx).ToList();
                    selected = (-1, -1);
                }
                catch
                {

                }
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                BidirectionalGraph<int, IEdge<int>> graph = new BidirectionalGraph<int, IEdge<int>>();
                for(int i =0; i<vertices.Count; i++)
                {
                    graph.AddVertex(i);
                }

                foreach(var edge in edges)
                {
                    graph.AddEdge(new Edge<int>(
                        vertices.IndexOf(edge.p1),
                        vertices.IndexOf(edge.p2)
                        ));
                }

                var coloring = new LargestFirstV1OrientedColoring(graph).Execute();

                new Visualizator(vertices, edges, coloring, dialog.FileName).VisualizeColoring();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            edges.Clear();
            vertices.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                BidirectionalGraph<int, IEdge<int>> graph = new BidirectionalGraph<int, IEdge<int>>();
                for (int i = 0; i < vertices.Count; i++)
                {
                    graph.AddVertex(i);
                }

                foreach (var edge in edges)
                {
                    graph.AddEdge(new Edge<int>(
                        vertices.IndexOf(edge.p1),
                        vertices.IndexOf(edge.p2)
                        ));
                }

                var coloring = new GreedyOrientedColoring(graph).Execute();

                new Visualizator(vertices, edges, coloring, dialog.FileName).VisualizeColoring();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                BidirectionalGraph<int, IEdge<int>> graph = new BidirectionalGraph<int, IEdge<int>>();
                for (int i = 0; i < vertices.Count; i++)
                {
                    graph.AddVertex(i);
                }

                foreach (var edge in edges)
                {
                    graph.AddEdge(new Edge<int>(
                        vertices.IndexOf(edge.p1),
                        vertices.IndexOf(edge.p2)
                        ));
                }

                var coloring = new LargestFirstV2OrientedColoring(graph).Execute();

                new Visualizator(vertices, edges, coloring, dialog.FileName).VisualizeColoring();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                BidirectionalGraph<int, IEdge<int>> graph = new BidirectionalGraph<int, IEdge<int>>();
                for (int i = 0; i < vertices.Count; i++)
                {
                    graph.AddVertex(i);
                }

                foreach (var edge in edges)
                {
                    graph.AddEdge(new Edge<int>(
                        vertices.IndexOf(edge.p1),
                        vertices.IndexOf(edge.p2)
                        ));
                }

                var coloring = new DSaturOrientedColoring(graph).Execute();

                new Visualizator(vertices, edges, coloring, dialog.FileName).VisualizeColoring();
            }
        }
    }
}
