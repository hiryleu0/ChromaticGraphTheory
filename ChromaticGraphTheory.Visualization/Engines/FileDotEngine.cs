using QuikGraph.Graphviz;
using QuikGraph.Graphviz.Dot;

namespace ChromaticGraphTheory.Visualization.Engines
{
    internal class FileDotEngine : IDotEngine
    {
        public string Run(GraphvizImageType imageType, string dot, string outputFilePath)
        {
            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                writer.Write(dot);
            }

            return System.IO.Path.GetFileName(outputFilePath);
        }
    }
}
