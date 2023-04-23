using QuickGraph;

namespace ChromaticGraphTheory.GUI
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
            bool[,] usedColors = new bool[Graph.VertexCount, Graph.VertexCount]; ;

            for(int i=0; i<Graph.VertexCount; i++)
            {
                var color = 1;
                while (!IsColorAvailable(i, color, coloring, usedColors)) color++;
                coloring[i] = color;
                
                for(int j=0; j<i; j++)
                {
                    if (Graph.ContainsEdge(i, j))
                        usedColors[coloring[i], coloring[j]] = true;
                    else if (Graph.ContainsEdge(j, i))
                        usedColors[coloring[j], coloring[i]] = true;
                }
            }

            return coloring;
        }

        private bool IsColorAvailable(int vertex, int color, int[] coloring, bool[,] usedColor)
        {
            for(int i=0; i<vertex; i++)
            {
                if (Graph.ContainsEdge(i, vertex) && (coloring[i] == color || usedColor[color, coloring[i]]))
                {
                    return false;
                }
                else if (Graph.ContainsEdge(vertex, i) && (coloring[i] == color || usedColor[coloring[i], color]))
                { 
                    return false;
                }
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
