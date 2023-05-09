using QuickGraph;
using System.Collections.Generic;
using System.Linq;

namespace ChromaticGraphTheory.GUI
{
    public class LargestFirstV2OrientedColoring
    {
        public IBidirectionalGraph<int, IEdge<int>> Graph { get; private set; }

        public LargestFirstV2OrientedColoring(IBidirectionalGraph<int, IEdge<int>> graph)
        {
            Graph = graph;
        }

        public (int, int, int)[] Execute()
        {
            (int color, int order, int)[] coloring = new (int, int, int)[Graph.VertexCount];
            bool[,] usedColors = new bool[Graph.VertexCount + 1, Graph.VertexCount + 1];

            var orderedVertices = Graph.Vertices.ToList();
            orderedVertices.Sort((v1, v2) => Graph.OutDegree(v2) - Graph.OutDegree(v1));
            var coloredVertices = new List<int>();

            var order = 0;
            foreach(var v in orderedVertices)
            {
                order++;
                var color = 1;
                while (!IsColorAvailable(v, color, coloring, usedColors, coloredVertices)) color++;
                coloring[v] = (color, order, v);
                
                foreach(var w in coloredVertices)
                {
                    if (Graph.ContainsEdge(v, w))
                        usedColors[coloring[v].color, coloring[w].color] = true;
                    else if (Graph.ContainsEdge(w, v))
                        usedColors[coloring[w].color, coloring[v].color] = true;
                }

                coloredVertices.Add(v);
            }

            return coloring;
        }

        private bool IsColorAvailable(int vertex, int color, (int color, int, int)[] coloring, bool[,] usedColor, List<int> usedVertices)
        {
            foreach(var i in usedVertices)
            {
                if (Graph.ContainsEdge(i, vertex) && (coloring[i].color == color || usedColor[color, coloring[i].color]))
                {
                    return false;
                }
                else if (Graph.ContainsEdge(vertex, i) && (coloring[i].color == color || usedColor[coloring[i].color, color]))
                { 
                    return false;
                }
                else
                    for (int k = 0; k < Graph.VertexCount; k++)
                        if (k != i && k != vertex)
                            if ((Graph.ContainsEdge(i, k) && Graph.ContainsEdge(k, vertex)) || (Graph.ContainsEdge(vertex, k) && Graph.ContainsEdge(k, i)))
                                if (coloring[i].color == color)
                                    return false;
            }             

            return true;
        }
    }
}
