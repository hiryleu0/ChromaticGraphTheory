using QuikGraph;

namespace ChromaticGraphTheory.Algorithms.Graphs
{
    public static class GraphFactory
    {
        public static BidirectionalGraph<int, IEdge<int>> GetFullGraph(int size)
        {
            var graph = new BidirectionalGraph<int, IEdge<int>>(false, size);
            for (int i = 0; i < size; i++)
            {
                graph.AddVertex(i);
            }
            for (int i = 0; i < size; i++)
                for (int j = i + 1; j < size; j++)
                    graph.AddEdge(new Edge<int>(i, j));

            return graph;
        }

        public static BidirectionalGraph<int, IEdge<int>> GetCycle(int size)
        {
            var graph = new BidirectionalGraph<int, IEdge<int>>(false, size);
            for (int i = 0; i < size; i++)
                graph.AddVertex(i);
            for (int i = 0; i < size; i++)
                graph.AddEdge(new Edge<int>(i, (i+1)%size));

            return graph;
        }

        public static BidirectionalGraph<int, IEdge<int>> GetStarGraph(int size)
        {
            var graph = new BidirectionalGraph<int, IEdge<int>>(false, size);
            for (int i = 0; i < size; i++)
                graph.AddVertex(i);
            for (int i = 1; i < size; i++)
                graph.AddEdge(new Edge<int>(0, i));

            return graph;
        }
    }
}
