using QuikGraph;

namespace ChromaticGraphTheory.Algorithms.Graphs
{
    public class GraphReader
    {
        public static BidirectionalGraph<int, IEdge<int>> GetFromFile(string path)
        {
            List<IEdge<int>> edges = new();
            using (StreamReader reader = File.OpenText(path))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Trim().Split();
                    if (values.Length == 0) continue;
                    if (values.Length != 2) throw new Exception("Every row needs to have two numbers");

                    bool parsed = int.TryParse(values[0], out int v);
                    parsed &= int.TryParse(values[1], out int u);

                    if (!parsed) throw new Exception("Row contains invalid number");
                    if (u < 1 || v < 1) throw new Exception("Number has to be positive integer");

                    edges.Add(new Edge<int>(u - 1, v - 1));
                }
            }

            var n = edges.Max(edge => Math.Max(edge.Source, edge.Target)) + 1;
            var graph = new BidirectionalGraph<int, IEdge<int>>(false, n);
            graph.AddVertexRange(Enumerable.Range(0, n));
            graph.AddEdgeRange(edges);

            return graph;
        }
    }
}
