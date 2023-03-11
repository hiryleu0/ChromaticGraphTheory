using QuikGraph.Graphviz.Dot;

namespace ChromaticGraphTheory.Visualization.Visualizators
{
    public abstract class Visualizator
    {
        protected static readonly Dictionary<int, GraphvizColor> ColorsPalette = new()
        {
            {1, GraphvizColor.Magenta }, 
            {2, GraphvizColor.LightBlue }, 
            {3, GraphvizColor.LightGreen }, 
            {4, GraphvizColor.PaleVioletRed }, 
            {5, GraphvizColor.Orange }, 
            {6, GraphvizColor.Violet }, 
            {7, GraphvizColor.DarkGray }, 
            {8, GraphvizColor.Cyan }, 
            {9, GraphvizColor.DeepSkyBlue }, 
            {10, GraphvizColor.LightPink },
        };
    }
}
