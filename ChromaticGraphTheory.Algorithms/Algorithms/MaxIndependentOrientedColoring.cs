using QuikGraph;

namespace ChromaticGraphTheory.Algorithms.Algorithms
{
    public class MaxIndependentOrientedColoring
    {
        public IBidirectionalGraph<int, IEdge<int>> Graph { get; private set; }
        public MaxIndependentOrientedColoring(IBidirectionalGraph<int, IEdge<int>> graph)
        {
            Graph = graph;
        }

        public int[] Execute()
        {
            int[] coloring = new int[Graph.VertexCount];

            bool[] U = new bool[Graph.VertexCount];
            for (int _ = 0; _ < U.Length; _++) U[_] = true;

            int n = Graph.VertexCount;
            int i = 1;
            while(n > 0)
            {
                var GMC = GreedMonochromatic(U);
                for(int _ = 0; _ < U.Length; _++)
                    if (GMC[_])
                    {
                        U[_] = false;
                        coloring[_] = i;
                        n--;
                    }
                i++;
            }

            return coloring;
        }

        private bool[] GreedMonochromatic(bool[] V)
        {
            bool[] U = (bool[])V.Clone();
            int n = U.Count(c => c);
            bool[] S = new bool[Graph.VertexCount];

            while(n > 0)
            {
                int v = 0;
                List<int> minBv2 = null;

                for(int i=0; i < Graph.VertexCount; i++)
                {
                    if (U[i])
                    {
                        var Bv2 = new List<int>();
                        for (int j = 0; j < Graph.VertexCount; j++)
                            if (U[j] && i != j)
                            {
                                if (Graph.ContainsEdge(i, j) || Graph.ContainsEdge(j, i))
                                    Bv2.Add(j);
                                else
                                    for (int k = 0; k < Graph.VertexCount; k++)
                                        if (k != i && k != j)
                                            if ((Graph.ContainsEdge(i, k) && Graph.ContainsEdge(k, j)) || (Graph.ContainsEdge(j, k) && Graph.ContainsEdge(k, i)))
                                            {
                                                Bv2.Add(j);
                                                break;
                                            }
                            }

                        if (minBv2 == null || minBv2.Count > Bv2.Count)
                        {
                            v = i;
                            minBv2 = Bv2;
                        }
                    }
                }

                S[v] = true;
                U[v] = false;
                n--;
                foreach(var u in minBv2)
                {
                    U[u] = false;
                    n--;
                }    
            }

            return S;
        }
    }
}
