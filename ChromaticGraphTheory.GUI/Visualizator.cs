using ImageMagick;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms.VisualStyles;

namespace ChromaticGraphTheory.GUI
{
    public class Visualizator
    {
        protected static readonly Dictionary<int, Color> ColorsPalette = new Dictionary<int, Color>()
        {
            {1, Color.LightBlue },
            {2, Color.IndianRed },
            {3, Color.LightYellow },
            {4, Color.LightPink },
            {5, Color.Cyan },
            {6, Color.LightGreen },
            {7, Color.DarkGray },
            {8, Color.Orange },
            {9, Color.DarkOliveGreen },
            {10, Color.DarkBlue },
            {11, Color.Magenta },
            {12, Color.LightGray },
        };

        public (int color, int order, int vertex)[] Coloring { get; }
        public string FilePath { get; }
        public List<(int x, int y)> Vertices = new List<(int, int)>();
        public List<((int x, int y) p1, (int x, int y) p2)> Edges = new List<((int, int), (int, int))>();

        public Visualizator(List<(int, int)> vertices, List<((int, int), (int, int))> edges , (int color, int order, int vertex)[] coloring, string filePath)
        {
            Coloring = coloring;
            FilePath = filePath;
            Vertices = vertices;
            Edges = edges;
        }
        public void VisualizeColoring()
        {
            // Generating dot files
            var bmpFilePaths = GenerateBmpFiles();

            //Generating gif file and removing svg files
            using (MagickImageCollection collection = new MagickImageCollection())
            {
                int i = 0;
                foreach (var bmpFilePath in bmpFilePaths)
                {
                    collection.Add(bmpFilePath);
                    collection[i++].AnimationDelay = 100;

                }
                collection.Optimize();
                collection.Write(FilePath, MagickFormat.Gif);
            }
            
            foreach (var bmpFilePath in bmpFilePaths)
            {
                File.Delete(bmpFilePath);
            }
        }
        
        int radius = 16;

        private List<string> GenerateBmpFiles()
        {
            List<string> bmpFilePaths = new List<string>();

            for (int i = 1; i <= Vertices.Count + 1; i++)
            {
                using (Bitmap bitmap = new Bitmap(1026, 927))
                using (Graphics g = Graphics.FromImage(bitmap))
                using (Pen p = new Pen(Color.Black))
                {
                    g.FillRectangle(Brushes.White, 0, 0, 1026, 927);
                    p.CustomEndCap = new AdjustableArrowCap(10, 10);

                    foreach (var (e1, e2) in Edges)
                    {
                        int x1 = e1.x, x2 = e2.x, y1 = e1.y, y2 = e2.y;

                        double sqrt = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
                        int x3 = (int)(x2 - (x2 - x1) * radius / sqrt);
                        int y3 = (int)(y2 - (y2 - y1) * radius / sqrt);
                        g.DrawLine(p, e1.x, e1.y, x3, y3);
                    }

                    for (int j = 1; j <= Vertices.Count; j++)
                    {
                        var coloringElemnt = Coloring.Single(c => c.order == j);
                        var (x, y) = Vertices[coloringElemnt.vertex];
                        if (j < i)
                        {
                            using (Brush b = new SolidBrush(ColorsPalette[coloringElemnt.color]))
                            {
                                g.FillEllipse(b, x - radius, y - radius, 2 * radius, 2 * radius);
                            }
                        }
                        else
                        {
                            g.FillEllipse(Brushes.Black, x - radius, y - radius, 2 * radius, 2 * radius);
                        }
                    }

                    var path = "___tmp_file" + i.ToString() + ".bmp";
                    bitmap.Save(path);
                    bmpFilePaths.Add(path);
                }
            }

            return bmpFilePaths;
        }
    }
}
