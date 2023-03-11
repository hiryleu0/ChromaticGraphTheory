using ChromaticGraphTheory.Visualization.DOT;
using QuikGraph;
using QuikGraph.Graphviz;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using ImageMagick;

namespace ChromaticGraphTheory.Visualization.Visualizators
{
    public class ColoringVisualizator : Visualizator
    {
        public static void VisualizeColoring(BidirectionalGraph<int, IEdge<int>> graph, int[] coloring, string filePath)
        {
            // Generating dot files
            int n = coloring.Max();
            List<(string, string)> tmpFilePaths = new();

            GraphvizAlgorithm<int, IEdge<int>> graphviz = new GraphvizAlgorithm<int, IEdge<int>>(graph);
            for(int i = 0; i <= n; i++)
            {
                FormatVertexEventHandler<int> handler = (sender, args) => {
                    if (coloring[args.Vertex] <= i)
                    {
                        args.VertexFormat.Style = QuikGraph.Graphviz.Dot.GraphvizVertexStyle.Filled;
                        args.VertexFormat.FillColor = ColorsPalette[coloring[args.Vertex]];
                    }
                    args.VertexFormat.Shape = QuikGraph.Graphviz.Dot.GraphvizVertexShape.Circle;
                };

                string dotFilePath = filePath + i + "___tmp";
                string svgFilePath = filePath + i + "___tmp.svg";
                graphviz.FormatVertex += handler;
                
                graphviz.Generate(new FileDotEngine(), dotFilePath);
                tmpFilePaths.Add((dotFilePath, svgFilePath));
                graphviz.FormatVertex -= handler;
            }

            //Generating svg files and removing dot files
            foreach(var (dotFilePath, svgFilePath) in tmpFilePaths)
            {
                var fullDotFilePath = dotFilePath + ".dot";
                DOTHandler.ConvertDotToSvg(fullDotFilePath, svgFilePath);
                File.Delete(fullDotFilePath);
            }

            //Generating gif file and removing svg files
            using (MagickImageCollection collection = new MagickImageCollection())
            {
                int i = 0;
                foreach (var (_, svgFilePath) in tmpFilePaths)
                {
                    collection.Add(svgFilePath);
                    collection[i++].AnimationDelay = 100;

                } 
                collection.Optimize();
                collection.Write(filePath + ".tiff", MagickFormat.Tiff);

                foreach (var (_, svgFilePath) in tmpFilePaths)
                {
                    File.Delete(svgFilePath);
                }
            }
        }
    }
}