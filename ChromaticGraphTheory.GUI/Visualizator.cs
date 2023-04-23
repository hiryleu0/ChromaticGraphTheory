using ImageMagick;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

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

        public int[] Coloring { get; }
        public string FilePath { get; }
        public List<(int x, int y)> Vertices = new List<(int, int)>();
        public List<((int x, int y) p1, (int x, int y) p2)> Edges = new List<((int, int), (int, int))>();

        public Visualizator(List<(int, int)> vertices, List<((int, int), (int, int))> edges , int[] coloring, string filePath)
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

            for (int i = 0; i <= Vertices.Count; i++)
            {
                using (Bitmap bitmap = new Bitmap(1026, 927))
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.FillRectangle(Brushes.White, 0, 0, 1026, 927);
                    foreach (var (e1, e2) in Edges)
                    {
                        g.DrawLine(Pens.Black, e1.x, e1.y, e2.x, e2.y);
                    }

                    for (int j = 0; j < Vertices.Count; j++)
                    {
                        var (x, y) = Vertices[j];
                        if (j < i)
                        {
                            using (Brush b = new SolidBrush(ColorsPalette[Coloring[j]]))
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
