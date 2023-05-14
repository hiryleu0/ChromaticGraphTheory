using QuikGraph;
using System.Runtime.ExceptionServices;

namespace ChromaticGraphTheory.Algorithms.Graphs
{
    static class MyExtensions
    {
        private static Random rng = new Random();
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }

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

        public static BidirectionalGraph<int, IEdge<int>> GetRandomGraph(int size, int edgesCount)
        {
            var rnd = new Random((int)DateTimeOffset.UtcNow.UtcTicks);
            var graph = new BidirectionalGraph<int, IEdge<int>>(false, size);
            
            for (int i = 0; i < size; i++)
                graph.AddVertex(i);

            List<IEdge<int>> edges = new();

            for (int i = 0; i < size; i++)
            {
                for(int j=0; j < size; j++)
                {
                    if(i != j)
                    {
                        var edge = rnd.Next(0, 2) == 0 ? new Edge<int>(i, j) : new Edge<int>(j, i);
                        edges.Add(edge);
                    }
                }
            }

            edges.Shuffle();

            for(int i=0; i < edgesCount; i++)
            {
                if (i >= edges.Count) break;
                graph.AddEdge(edges[i]);
            }

            return graph;
        }
    }
}
