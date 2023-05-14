using ChromaticGraphTheory.Algorithms.Graphs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChromaticGraphTheory.Algorithms.Test.Algorithms
{
    [TestClass]
    public class LargestFirstV2OrientedColoringTests
    {
        [TestMethod]
        public void FullGraphTest()
        {
            int size = 20;
            var graph = GraphFactory.GetFullGraph(size);
            
            var algorithm = new LargestFirstV2OrientedColoring(graph);

            var coloring = algorithm.Execute();
            coloring.Max(((int x, int, int) _) => _.x).Should().Be(size);
        }

        [TestMethod]
        public void CycleOfLengthDevidedBy3Test()
        {
            int size = 21;
            var graph = GraphFactory.GetCycle(size);

            var algorithm = new LargestFirstV2OrientedColoring(graph);

            var coloring = algorithm.Execute();
            coloring.Max(((int x, int, int) _) => _.x).Should().Be(5);
        }

        [TestMethod]
        public void CycleOfLength2Mod3Test()
        {
            int size = 20;
            var graph = GraphFactory.GetCycle(size);

            var algorithm = new LargestFirstV2OrientedColoring(graph);

            var coloring = algorithm.Execute();
            coloring.Max(((int x, int, int) _) => _.x).Should().Be(5);
        }

        [TestMethod]
        public void CycleOfLength1Mod3Test()
        {
            int size = 22;
            var graph = GraphFactory.GetCycle(size);

            var algorithm = new LargestFirstV2OrientedColoring(graph);

            var coloring = algorithm.Execute();
            coloring.Max(((int x, int, int) _) => _.x).Should().Be(4);
        }

        [TestMethod]
        public void StarGraphTest()
        {
            int size = 20;
            var graph = GraphFactory.GetStarGraph(size);

            var algorithm = new LargestFirstV2OrientedColoring(graph);

            var coloring = algorithm.Execute();
            coloring.Max(((int x, int, int) _) => _.x).Should().Be(2);
        }

        [TestMethod]
        public void GraphFromTextfile1Test()
        {
            var graph = GraphReader.GetFromFile("../../../Examples/TextGraph1.txt");

            var algorithm = new LargestFirstV2OrientedColoring(graph);

            var coloring = algorithm.Execute();
            coloring.Max(((int x, int, int) _) => _.x).Should().Be(3);
        }

        [TestMethod]
        public void GraphFromTextfile2Test()
        {
            var graph = GraphReader.GetFromFile("../../../Examples/TextGraph2.txt");

            var algorithm = new LargestFirstV2OrientedColoring(graph);

            var coloring = algorithm.Execute();
            coloring.Max(((int x, int, int) _) => _.x).Should().Be(5);
        }
    }
}