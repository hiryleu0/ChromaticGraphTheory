using QuikGraph;

namespace ChromaticGraphTheory.Visualization.Visualizators
{
    public class VerticesVisualizator : Visualizator
    {
        public VerticesVisualizator(BidirectionalGraph<int, IEdge<int>> graph, int[] coloring, string filePath) : base(graph, coloring, filePath)
        {
        }

        protected override int DotFilesCount => Graph.VertexCount;

        protected override bool ShouldBeColored(int vertex, int dotFileNo)
        {
            return vertex <= dotFileNo - 1;
        }
    }
}