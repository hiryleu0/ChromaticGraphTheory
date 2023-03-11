using ChromaticGraphTheory.Visualization.DOT;
using QuikGraph;
using QuikGraph.Graphviz;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using ImageMagick;
using QuikGraph.Graphviz.Dot;

namespace ChromaticGraphTheory.Visualization.Visualizators
{
    public class ColorsVisualizator : Visualizator
    {
        public ColorsVisualizator(BidirectionalGraph<int, IEdge<int>> graph, int[] coloring, string filePath) : base(graph, coloring, filePath)
        {
        }

        protected override int DotFilesCount => Coloring.Max();

        protected override bool ShouldBeColored(int vertex, int dotFileNo)
        {
            return Coloring[vertex] <= dotFileNo;
        }
    }
}