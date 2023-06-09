﻿using QuickGraph;

namespace ChromaticGraphTheory.GUI
{
    public class GreedyOrientedColoring
    {
        public IBidirectionalGraph<int, IEdge<int>> Graph { get; private set; }

        public GreedyOrientedColoring(IBidirectionalGraph<int, IEdge<int>> graph)
        {
            Graph = graph;
        }

        public (int, int, int)[] Execute()
        {
            (int color, int order, int)[] coloring = new (int, int, int)[Graph.VertexCount];
            bool[,] usedColors = new bool[Graph.VertexCount+1, Graph.VertexCount+1];

            for(int i=0; i<Graph.VertexCount; i++)
            {
                var color = 1;
                while (!IsColorAvailable(i, color, coloring, usedColors)) color++;
                coloring[i] = (color, i+1, i);
                
                for(int j=0; j<i; j++)
                {
                    if (Graph.ContainsEdge(i, j))
                        usedColors[coloring[i].color, coloring[j].color] = true;
                    else if (Graph.ContainsEdge(j, i))
                        usedColors[coloring[j].color, coloring[i].color] = true;
                }
            }

            return coloring;
        }

        private bool IsColorAvailable(int vertex, int color, (int color, int, int)[] coloring, bool[,] usedColor)
        {
            for(int i=0; i<vertex; i++)
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
