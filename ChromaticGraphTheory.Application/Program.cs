using ChromaticGraphTheory.Algorithms.Algorithms;
using ChromaticGraphTheory.Algorithms.Graphs;
using ChromaticGraphTheory.Visualization.Visualizators;

var graph = GraphFactory.GetStarGraph(12);
var coloring = new MaxIndependentOrientedColoring(graph).Execute();

new ColorsVisualizator(graph, coloring, "../../../results/graph21.colors.gif").VisualizeColoring();
new VerticesVisualizator(graph, coloring, "../../../results/graph21.vertices.gif").VisualizeColoring();
