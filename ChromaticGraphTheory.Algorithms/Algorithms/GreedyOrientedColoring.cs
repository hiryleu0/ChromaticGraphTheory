using QuikGraph;

namespace ChromaticGraphTheory.Algorithms.Algorithms
{
    public class GreedyOrientedColoring
    {
        public IBidirectionalGraph<int, IEdge<int>> Graph { get; private set; }
        public GreedyOrientedColoring(IBidirectionalGraph<int, IEdge<int>> graph)
        {
            Graph = graph;
        }

        public int[] Execute()
        {
            int[] coloring = new int[Graph.VertexCount];
            for(int i=0; i<Graph.VertexCount; i++)
            {
                var color = 1;
                while (!IsColorAvailable(i, color, coloring)) color++;
                coloring[i] = color;
            }

            return coloring;
        }

        private bool IsColorAvailable(int vertex, int color, int[] coloring)
        {
            for(int i=0; i<vertex; i++)
            {
                if ((Graph.ContainsEdge(i, vertex) || Graph.ContainsEdge(vertex, i)) && coloring[i] == color)
                    return false;
                else
                    for (int k = 0; k < Graph.VertexCount; k++)
                        if (k != i && k != vertex)
                            if ((Graph.ContainsEdge(i, k) && Graph.ContainsEdge(k, vertex)) || (Graph.ContainsEdge(vertex, k) && Graph.ContainsEdge(k, i)))
                                if (coloring[i] == color)
                                    return false;
            }             

            return true;
        }
    }
}
