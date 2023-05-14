using ChromaticGraphTheory.Algorithms;
using ChromaticGraphTheory.Algorithms.Graphs;
using System.Text;

List<(int, int, int, int, int)> data = new();

for(int i = 20; i<= 135; i++)
{
    var graph = GraphFactory.GetRandomGraph(i, (int)(2 * i * Math.Log(i)));    

    int dsatur = new DSaturOrientedColoring(graph).Execute().Max(((int color, int, int) _) => _.color);
    int greedy = new GreedyOrientedColoring(graph).Execute().Max(((int color, int, int) _) => _.color);
    int lfirs1 = new LargestFirstV1OrientedColoring(graph).Execute().Max(((int color, int, int) _) => _.color);
    int lfirs2 = new LargestFirstV2OrientedColoring(graph).Execute().Max(((int color, int, int) _) => _.color);

    data.Add((i, dsatur, greedy, lfirs1, lfirs2));
}


var csv = new StringBuilder();
csv.AppendLine("Size;DSatur;Greedy;LargestFirst V1;LargestFirst V2");

foreach(var (a, b, c, d, e) in data)
{
    csv.AppendLine($"{a};{b};{c};{d};{e}");
}

File.WriteAllText("../../../data/medium_size_medium_density.csv", csv.ToString());