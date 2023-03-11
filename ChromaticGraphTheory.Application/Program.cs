using ChromaticGraphTheory.Algorithms.Algorithms;
using ChromaticGraphTheory.Algorithms.Graphs;
using ChromaticGraphTheory.Visualization.Visualizators;
using QuikGraph;

var graph = new BidirectionalGraph<int, IEdge<int>>();
graph.AddVerticesAndEdgeRange(new IEdge<int>[]
{
    new Edge<int>(0, 1),
    new Edge<int>(3, 2),
});
var coloring = new MaxIndependentOrientedColoring(graph).Execute();

new ColorsVisualizator(graph, coloring, "../../../results/graph22.colors.gif").VisualizeColoring();
new VerticesVisualizator(graph, coloring, "../../../results/graph22.vertices.gif").VisualizeColoring();
