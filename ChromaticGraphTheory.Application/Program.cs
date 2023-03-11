using ChromaticGraphTheory.Algorithms.Algorithms;
using ChromaticGraphTheory.Algorithms.Graphs;
using ChromaticGraphTheory.Visualization.Visualizators;
using QuikGraph;

var graph = GraphFactory.GetFullGraph(3);
var coloring = new MaxIndependentOrientedColoring(graph).Execute();

ColoringVisualizator.VisualizeColoring(graph, coloring, "../../../results/graph16");
