using ChromaticGraphTheory.Visualization.DOT;
using ImageMagick;
using QuikGraph.Graphviz;
using QuikGraph;
using QuikGraph.Graphviz.Dot;

namespace ChromaticGraphTheory.Visualization.Visualizators
{
    public abstract class Visualizator
    {
        protected static readonly Dictionary<int, GraphvizColor> ColorsPalette = new()
        {
            {1, GraphvizColor.LightBlue },
            {2, GraphvizColor.IndianRed },
            {3, GraphvizColor.LightYellow },
            {4, GraphvizColor.LightPink },
            {5, GraphvizColor.Cyan },
            {6, GraphvizColor.LightGreen },
            {7, GraphvizColor.DarkGray },
            {8, GraphvizColor.Orange },
            {9, GraphvizColor.DarkOliveGreen },
            {10, GraphvizColor.DarkBlue },
            {11, GraphvizColor.Magenta },
            {12, GraphvizColor.LightGray },
        };

        public BidirectionalGraph<int, IEdge<int>> Graph { get; }
        public int[] Coloring { get; }
        public string FilePath { get; }

        public Visualizator(BidirectionalGraph<int, IEdge<int>> graph, int[] coloring, string filePath)
        {
            Graph = graph;
            Coloring = coloring;
            FilePath = filePath;
        }
        public void VisualizeColoring()
        {
            // Generating dot files
            var dotFilePaths = GenerateDotFiles();

            //Generating svg files and removing dot files
            foreach (var dotFilePath in dotFilePaths)
            {
                DOTHandler.ConvertDotToSvg(dotFilePath);
                File.Delete(dotFilePath);
            }

            //Generating gif file and removing svg files
            using MagickImageCollection collection = new();
            int i = 0;
            foreach (var dotFilePath in dotFilePaths)
            {
                collection.Add(dotFilePath + ".svg");
                collection[i++].AnimationDelay = 100;

            }
            collection.Optimize();
            collection.Write(FilePath, MagickFormat.Gif);

            foreach (var dotFilePath in dotFilePaths)
            {
                File.Delete(dotFilePath + ".svg");
            }
        }

        private FormatVertexEventHandler<int> GenerateFormatVertexEventHandler(int i)
        {
            return (sender, args) => {
                if (ShouldBeColored(args.Vertex, i))
                {
                    args.VertexFormat.Style = GraphvizVertexStyle.Filled;
                    args.VertexFormat.FillColor = ColorsPalette[Coloring[args.Vertex]];
                }
                args.VertexFormat.Shape = GraphvizVertexShape.Circle;
            };
        }
        private List<string> GenerateDotFiles()
        {
            List<string> dotFilePaths = new();

            GraphvizAlgorithm<int, IEdge<int>> graphviz = new(Graph);
            for (int i = 0; i <= DotFilesCount; i++)
            {
                FormatVertexEventHandler<int> handler = GenerateFormatVertexEventHandler(i);

                string dotFilePath = FilePath + i + "___tmp.dot";
                graphviz.FormatVertex += handler;

                graphviz.Generate(new FileDotEngine(), dotFilePath);
                dotFilePaths.Add(dotFilePath);
                graphviz.FormatVertex -= handler;
            }

            return dotFilePaths;
        }

        protected abstract int DotFilesCount { get; }
        protected abstract bool ShouldBeColored(int vertex, int dotFileNo);
    }
}
