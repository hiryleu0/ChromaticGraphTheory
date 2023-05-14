using QuickGraph;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms.VisualStyles;

namespace ChromaticGraphTheory.Algorithms.Graphs
{
    public class GraphReader
    {
        public static (List<(int x, int y)> vertices, List<((int x, int y) p1, (int x, int y) p2)> edges) GetFromFile(string path)
        {
            List<(int x, int y)> vertices = new List<(int x, int y)>();
            List<((int x, int y) p1, (int x, int y) p2)> edges = new List<((int x, int y) p1, (int x, int y) p2)>();

            using (StreamReader reader = File.OpenText(path))
            {
                string line;
                int n = int.Parse(reader.ReadLine());

                for(int i = 0; i < n; i++)
                {
                    var c = reader.ReadLine().Split('(', ')', ',', ' ').Where(_ => !string.IsNullOrEmpty(_)).ToList();
                    var x = int.Parse(c[0]);
                    var y = int.Parse(c[1]);

                    vertices.Add((x, y));
                }

                n = int.Parse(reader.ReadLine());
                for (int i = 0; i < n; i++)
                {
                    var c = reader.ReadLine().Split('(', ')', ',', ' ').Where(_ => !string.IsNullOrEmpty(_)).ToList();
                    var x = int.Parse(c[0]);
                    var y = int.Parse(c[1]);
                    var z = int.Parse(c[2]);
                    var t = int.Parse(c[3]);

                    edges.Add(((x, y), (z, t)));
                }
            }

            return (vertices, edges);
        }

        public static void SaveToFile(List<(int x, int y)> vertices, List<((int x, int y) p1, (int x, int y) p2)> edges, string fileName)
        {
            using (StreamWriter writer = File.CreateText(fileName))
            {
                writer.WriteLine(vertices.Count);
                foreach(var v in vertices)
                {
                    writer.WriteLine(v.ToString());
                }

                writer.WriteLine(edges.Count);
                foreach(var e in edges)
                {
                    writer.WriteLine(e.ToString());
                }
            }
        }
    }
}
