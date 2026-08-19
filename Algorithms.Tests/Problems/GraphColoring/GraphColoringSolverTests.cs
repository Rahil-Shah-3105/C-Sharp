using Algorithms.Problems.GraphColoring;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Problems.GraphColoring;

[TestFixture]
public sealed class GraphColoringSolverTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    /// Helper method to validate that a coloring is valid for a given graph.
    /// </summary>
    /// <param name="adjacencyMatrix">The graph adjacency matrix.</param>
    /// <param name="colors">The color assignment to validate.</param>
    /// <remarks>
    /// A valid coloring must satisfy:
    /// 1. All vertices are colored (no -1 values).
    /// 2. No two adjacent vertices have the same color.
    /// </remarks>
    private static void AssertValidColoring(bool[,] adjacencyMatrix, int[] colors)
    {
		_logger.LogDebug(">>> Entering AssertValidColoring(adjacencyMatrix={AdjacencyMatrix},colors={Colors})", adjacencyMatrix, colors);
        var numVertices = adjacencyMatrix.GetLength(0);

		_logger.LogDebug("AssertValidColoring(adjacencyMatrix={AdjacencyMatrix},colors={Colors}): numVertices → {NumVertices}", adjacencyMatrix, colors, numVertices);
        Assert.That(colors, Has.Length.EqualTo(numVertices), 
            "Color array length must match number of vertices.");

        // Check all vertices are colored
        for (var i = 0; i < numVertices; i++)
        {
            Assert.That(colors[i], Is.GreaterThanOrEqualTo(0),
                $"Vertex {i} is not colored (has value -1).");
        }

        // Check no adjacent vertices have the same color
        for (var i = 0; i < numVertices; i++)
        {
            for (var j = i + 1; j < numVertices; j++)
            {
                if (adjacencyMatrix[i, j])
                {
                    Assert.That(colors[i], Is.Not.EqualTo(colors[j]),
                        $"Adjacent vertices {i} and {j} have the same color {colors[i]}.");
                }
            }
        }
		_logger.LogDebug("<<< Exiting AssertValidColoring(adjacencyMatrix={AdjacencyMatrix},colors={Colors})", adjacencyMatrix, colors);
    }

    /// <summary>
    /// Helper method to create an empty graph (no edges).
    /// </summary>
    private static bool[,] CreateEmptyGraph(int vertices)
    {
		_logger.LogDebug(">>> Entering CreateEmptyGraph(vertices={Vertices})", vertices);
		_logger.LogDebug("<<< Exiting CreateEmptyGraph(vertices={Vertices})", vertices);
        return new bool[vertices, vertices];
    }

    /// <summary>
    /// Helper method to create a complete graph where all vertices are connected.
    /// </summary>
    private static bool[,] CreateCompleteGraph(int vertices)
    {
		_logger.LogDebug(">>> Entering CreateCompleteGraph(vertices={Vertices})", vertices);
        var graph = new bool[vertices, vertices];
        for (var i = 0; i < vertices; i++)
        {
            for (var j = 0; j < vertices; j++)
            {
                if (i != j)
                {
                    graph[i, j] = true;
                }
            }
        }
		_logger.LogDebug("<<< Exiting CreateCompleteGraph(vertices={Vertices})", vertices);
        return graph;
    }

    /// <summary>
    /// Helper method to create a bipartite graph (two sets with edges only between sets).
    /// </summary>
    private static bool[,] CreateBipartiteGraph(int setASize, int setBSize)
    {
		_logger.LogDebug(">>> Entering CreateBipartiteGraph(setASize={SetASize},setBSize={SetBSize})", setASize, setBSize);
        var total = setASize + setBSize;
        var graph = new bool[total, total];
		_logger.LogDebug("CreateBipartiteGraph(setASize={SetASize},setBSize={SetBSize}): total → {Total}", setASize, setBSize, total);
        
        // Connect all vertices in set A to all vertices in set B
        for (var i = 0; i < setASize; i++)
        {
            for (var j = setASize; j < total; j++)
            {
                graph[i, j] = true;
                graph[j, i] = true;
            }
        }
		_logger.LogDebug("<<< Exiting CreateBipartiteGraph(setASize={SetASize},setBSize={SetBSize})", setASize, setBSize);
        return graph;
    }

    /// <summary>
    /// Helper method to create a cycle graph.
    /// </summary>
    private static bool[,] CreateCycleGraph(int vertices)
    {
		_logger.LogDebug(">>> Entering CreateCycleGraph(vertices={Vertices})", vertices);
        var graph = new bool[vertices, vertices];
        for (var i = 0; i < vertices; i++)
        {
            var next = (i + 1) % vertices;
			_logger.LogDebug("CreateCycleGraph(vertices={Vertices}): next → {Next}", vertices, next);
            graph[i, next] = true;
            graph[next, i] = true;
        }
		_logger.LogDebug("<<< Exiting CreateCycleGraph(vertices={Vertices})", vertices);
        return graph;
    }

    /// <summary>
    /// Helper method to create a path graph (linear chain).
    /// </summary>
    private static bool[,] CreatePathGraph(int vertices)
    {
		_logger.LogDebug(">>> Entering CreatePathGraph(vertices={Vertices})", vertices);
        var graph = new bool[vertices, vertices];
        for (var i = 0; i < vertices - 1; i++)
        {
            graph[i, i + 1] = true;
            graph[i + 1, i] = true;
        }
		_logger.LogDebug("<<< Exiting CreatePathGraph(vertices={Vertices})", vertices);
        return graph;
    }

    [Test]
    public void ColorGraph_ThrowsArgumentNullException_WhenAdjacencyMatrixIsNull()
    {
		_logger.LogDebug(">>> Entering ColorGraph_ThrowsArgumentNullException_WhenAdjacencyMatrixIsNull()");
        var solver = new GraphColoringSolver();

		_logger.LogDebug("ColorGraph_ThrowsArgumentNullException_WhenAdjacencyMatrixIsNull(): solver → {Solver}", solver);
        Assert.Throws<ArgumentNullException>(() => solver.ColorGraph(null!, 3));
		_logger.LogDebug("<<< Exiting ColorGraph_ThrowsArgumentNullException_WhenAdjacencyMatrixIsNull()");
    }

    [Test]
    public void ColorGraph_ThrowsArgumentException_WhenAdjacencyMatrixIsNotSquare()
    {
		_logger.LogDebug(">>> Entering ColorGraph_ThrowsArgumentException_WhenAdjacencyMatrixIsNotSquare()");
        var solver = new GraphColoringSolver();
        var nonSquareMatrix = new bool[3, 4];
		_logger.LogDebug("ColorGraph_ThrowsArgumentException_WhenAdjacencyMatrixIsNotSquare(): solver → {Solver}", solver);

        Assert.Throws<ArgumentException>(() => solver.ColorGraph(nonSquareMatrix, 3));
		_logger.LogDebug("<<< Exiting ColorGraph_ThrowsArgumentException_WhenAdjacencyMatrixIsNotSquare()");
    }

    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(-5)]
    public void ColorGraph_ThrowsArgumentException_WhenNumColorsIsNonPositive(int numColors)
    {
		_logger.LogDebug(">>> Entering ColorGraph_ThrowsArgumentException_WhenNumColorsIsNonPositive(numColors={NumColors})", numColors);
        var solver = new GraphColoringSolver();
        var graph = CreateEmptyGraph(3);
		_logger.LogDebug("ColorGraph_ThrowsArgumentException_WhenNumColorsIsNonPositive(numColors={NumColors}): graph → {Graph}", numColors, graph);
		_logger.LogDebug("ColorGraph_ThrowsArgumentException_WhenNumColorsIsNonPositive(numColors={NumColors}): solver → {Solver}", numColors, solver);

        Assert.Throws<ArgumentException>(() => solver.ColorGraph(graph, numColors));
		_logger.LogDebug("<<< Exiting ColorGraph_ThrowsArgumentException_WhenNumColorsIsNonPositive(numColors={NumColors})", numColors);
    }

    [Test]
    public void ColorGraph_ReturnsEmptyArray_ForEmptyGraph()
    {
		_logger.LogDebug(">>> Entering ColorGraph_ReturnsEmptyArray_ForEmptyGraph()");
        var solver = new GraphColoringSolver();
        var graph = new bool[0, 0];
		_logger.LogDebug("ColorGraph_ReturnsEmptyArray_ForEmptyGraph(): solver → {Solver}", solver);

        var result = solver.ColorGraph(graph, 1);
		_logger.LogDebug("ColorGraph_ReturnsEmptyArray_ForEmptyGraph(): result → {Result}", result);

        Assert.That(result, Is.Empty);
		_logger.LogDebug("<<< Exiting ColorGraph_ReturnsEmptyArray_ForEmptyGraph()");
    }

    [Test]
    public void ColorGraph_ColorsSingleVertex_WithOneColor()
    {
		_logger.LogDebug(">>> Entering ColorGraph_ColorsSingleVertex_WithOneColor()");
        var solver = new GraphColoringSolver();
        var graph = CreateEmptyGraph(1);
		_logger.LogDebug("ColorGraph_ColorsSingleVertex_WithOneColor(): graph → {Graph}", graph);
		_logger.LogDebug("ColorGraph_ColorsSingleVertex_WithOneColor(): solver → {Solver}", solver);

        var result = solver.ColorGraph(graph, 1);
		_logger.LogDebug("ColorGraph_ColorsSingleVertex_WithOneColor(): result → {Result}", result);

        Assert.That(result, Has.Length.EqualTo(1));
        Assert.That(result[0], Is.EqualTo(0));
        AssertValidColoring(graph, result);
		_logger.LogDebug("<<< Exiting ColorGraph_ColorsSingleVertex_WithOneColor()");
    }

    [Test]
    public void ColorGraph_ColorsDisconnectedVertices_WithOneColor()
    {
		_logger.LogDebug(">>> Entering ColorGraph_ColorsDisconnectedVertices_WithOneColor()");
        var solver = new GraphColoringSolver();
        var graph = CreateEmptyGraph(5); // No edges
		_logger.LogDebug("ColorGraph_ColorsDisconnectedVertices_WithOneColor(): graph → {Graph}", graph);
		_logger.LogDebug("ColorGraph_ColorsDisconnectedVertices_WithOneColor(): solver → {Solver}", solver);

        var result = solver.ColorGraph(graph, 1);
		_logger.LogDebug("ColorGraph_ColorsDisconnectedVertices_WithOneColor(): result → {Result}", result);

        Assert.That(result, Has.Length.EqualTo(5));
        AssertValidColoring(graph, result);
        
        // All vertices should have the same color since there are no edges
        Assert.That(result.Distinct().Count(), Is.EqualTo(1));
		_logger.LogDebug("<<< Exiting ColorGraph_ColorsDisconnectedVertices_WithOneColor()");
    }

    [Test]
    public void ColorGraph_ColorsBipartiteGraph_WithTwoColors()
    {
		_logger.LogDebug(">>> Entering ColorGraph_ColorsBipartiteGraph_WithTwoColors()");
        var solver = new GraphColoringSolver();
        var graph = CreateBipartiteGraph(3, 3);
		_logger.LogDebug("ColorGraph_ColorsBipartiteGraph_WithTwoColors(): graph → {Graph}", graph);
		_logger.LogDebug("ColorGraph_ColorsBipartiteGraph_WithTwoColors(): solver → {Solver}", solver);

        var result = solver.ColorGraph(graph, 2);
		_logger.LogDebug("ColorGraph_ColorsBipartiteGraph_WithTwoColors(): result → {Result}", result);

        Assert.That(result, Has.Length.EqualTo(6));
        AssertValidColoring(graph, result);
        
        // Bipartite graph should use exactly 2 colors
        Assert.That(result.Distinct().Count(), Is.LessThanOrEqualTo(2));
		_logger.LogDebug("<<< Exiting ColorGraph_ColorsBipartiteGraph_WithTwoColors()");
    }

    [Test]
    public void ColorGraph_ThrowsArgumentException_ForBipartiteGraphWithOneColor()
    {
		_logger.LogDebug(">>> Entering ColorGraph_ThrowsArgumentException_ForBipartiteGraphWithOneColor()");
        var solver = new GraphColoringSolver();
        var graph = CreateBipartiteGraph(2, 2);
		_logger.LogDebug("ColorGraph_ThrowsArgumentException_ForBipartiteGraphWithOneColor(): graph → {Graph}", graph);
		_logger.LogDebug("ColorGraph_ThrowsArgumentException_ForBipartiteGraphWithOneColor(): solver → {Solver}", solver);

        // Bipartite graph with edges requires at least 2 colors
        Assert.Throws<ArgumentException>(() => solver.ColorGraph(graph, 1));
		_logger.LogDebug("<<< Exiting ColorGraph_ThrowsArgumentException_ForBipartiteGraphWithOneColor()");
    }

    [Test]
    public void ColorGraph_ColorsPathGraph_WithTwoColors()
    {
		_logger.LogDebug(">>> Entering ColorGraph_ColorsPathGraph_WithTwoColors()");
        var solver = new GraphColoringSolver();
        var graph = CreatePathGraph(5);
		_logger.LogDebug("ColorGraph_ColorsPathGraph_WithTwoColors(): graph → {Graph}", graph);
		_logger.LogDebug("ColorGraph_ColorsPathGraph_WithTwoColors(): solver → {Solver}", solver);

        var result = solver.ColorGraph(graph, 2);
		_logger.LogDebug("ColorGraph_ColorsPathGraph_WithTwoColors(): result → {Result}", result);

        Assert.That(result, Has.Length.EqualTo(5));
        AssertValidColoring(graph, result);
        
        // Path graph can be colored with 2 colors
        Assert.That(result.Distinct().Count(), Is.LessThanOrEqualTo(2));
		_logger.LogDebug("<<< Exiting ColorGraph_ColorsPathGraph_WithTwoColors()");
    }

    [Test]
    public void ColorGraph_ColorsEvenCycle_WithTwoColors()
    {
		_logger.LogDebug(">>> Entering ColorGraph_ColorsEvenCycle_WithTwoColors()");
        var solver = new GraphColoringSolver();
        var graph = CreateCycleGraph(6); // Even cycle
		_logger.LogDebug("ColorGraph_ColorsEvenCycle_WithTwoColors(): graph → {Graph}", graph);
		_logger.LogDebug("ColorGraph_ColorsEvenCycle_WithTwoColors(): solver → {Solver}", solver);

        var result = solver.ColorGraph(graph, 2);
		_logger.LogDebug("ColorGraph_ColorsEvenCycle_WithTwoColors(): result → {Result}", result);

        Assert.That(result, Has.Length.EqualTo(6));
        AssertValidColoring(graph, result);
        
        // Even cycle can be colored with 2 colors
        Assert.That(result.Distinct().Count(), Is.LessThanOrEqualTo(2));
		_logger.LogDebug("<<< Exiting ColorGraph_ColorsEvenCycle_WithTwoColors()");
    }

    [Test]
    public void ColorGraph_ThrowsArgumentException_ForOddCycleWithTwoColors()
    {
		_logger.LogDebug(">>> Entering ColorGraph_ThrowsArgumentException_ForOddCycleWithTwoColors()");
        var solver = new GraphColoringSolver();
        var graph = CreateCycleGraph(5); // Odd cycle
		_logger.LogDebug("ColorGraph_ThrowsArgumentException_ForOddCycleWithTwoColors(): graph → {Graph}", graph);
		_logger.LogDebug("ColorGraph_ThrowsArgumentException_ForOddCycleWithTwoColors(): solver → {Solver}", solver);

        // Odd cycle requires at least 3 colors
        Assert.Throws<ArgumentException>(() => solver.ColorGraph(graph, 2));
		_logger.LogDebug("<<< Exiting ColorGraph_ThrowsArgumentException_ForOddCycleWithTwoColors()");
    }

    [Test]
    public void ColorGraph_ColorsOddCycle_WithThreeColors()
    {
		_logger.LogDebug(">>> Entering ColorGraph_ColorsOddCycle_WithThreeColors()");
        var solver = new GraphColoringSolver();
        var graph = CreateCycleGraph(5); // Odd cycle
		_logger.LogDebug("ColorGraph_ColorsOddCycle_WithThreeColors(): graph → {Graph}", graph);
		_logger.LogDebug("ColorGraph_ColorsOddCycle_WithThreeColors(): solver → {Solver}", solver);

        var result = solver.ColorGraph(graph, 3);
		_logger.LogDebug("ColorGraph_ColorsOddCycle_WithThreeColors(): result → {Result}", result);

        Assert.That(result, Has.Length.EqualTo(5));
        AssertValidColoring(graph, result);
		_logger.LogDebug("<<< Exiting ColorGraph_ColorsOddCycle_WithThreeColors()");
    }

    [Test]
    public void ColorGraph_ColorsTriangle_WithThreeColors()
    {
		_logger.LogDebug(">>> Entering ColorGraph_ColorsTriangle_WithThreeColors()");
        var solver = new GraphColoringSolver();
        var graph = CreateCompleteGraph(3); // Triangle (K3)

		_logger.LogDebug("ColorGraph_ColorsTriangle_WithThreeColors(): graph → {Graph}", graph);
		_logger.LogDebug("ColorGraph_ColorsTriangle_WithThreeColors(): solver → {Solver}", solver);
        var result = solver.ColorGraph(graph, 3);
		_logger.LogDebug("ColorGraph_ColorsTriangle_WithThreeColors(): result → {Result}", result);

        Assert.That(result, Has.Length.EqualTo(3));
        AssertValidColoring(graph, result);
        
        // Complete graph K3 requires exactly 3 colors
        Assert.That(result.Distinct().Count(), Is.EqualTo(3));
		_logger.LogDebug("<<< Exiting ColorGraph_ColorsTriangle_WithThreeColors()");
    }

    [Test]
    public void ColorGraph_ThrowsArgumentException_ForTriangleWithTwoColors()
    {
		_logger.LogDebug(">>> Entering ColorGraph_ThrowsArgumentException_ForTriangleWithTwoColors()");
        var solver = new GraphColoringSolver();
        var graph = CreateCompleteGraph(3); // Triangle (K3)

		_logger.LogDebug("ColorGraph_ThrowsArgumentException_ForTriangleWithTwoColors(): graph → {Graph}", graph);
		_logger.LogDebug("ColorGraph_ThrowsArgumentException_ForTriangleWithTwoColors(): solver → {Solver}", solver);
        // Triangle requires 3 colors
        Assert.Throws<ArgumentException>(() => solver.ColorGraph(graph, 2));
		_logger.LogDebug("<<< Exiting ColorGraph_ThrowsArgumentException_ForTriangleWithTwoColors()");
    }

    [Test]
    public void ColorGraph_ColorsCompleteGraphK4_WithFourColors()
    {
		_logger.LogDebug(">>> Entering ColorGraph_ColorsCompleteGraphK4_WithFourColors()");
        var solver = new GraphColoringSolver();
        var graph = CreateCompleteGraph(4); // K4
		_logger.LogDebug("ColorGraph_ColorsCompleteGraphK4_WithFourColors(): graph → {Graph}", graph);
		_logger.LogDebug("ColorGraph_ColorsCompleteGraphK4_WithFourColors(): solver → {Solver}", solver);

        var result = solver.ColorGraph(graph, 4);
		_logger.LogDebug("ColorGraph_ColorsCompleteGraphK4_WithFourColors(): result → {Result}", result);

        Assert.That(result, Has.Length.EqualTo(4));
        AssertValidColoring(graph, result);
        
        // Complete graph K4 requires exactly 4 colors
        Assert.That(result.Distinct().Count(), Is.EqualTo(4));
		_logger.LogDebug("<<< Exiting ColorGraph_ColorsCompleteGraphK4_WithFourColors()");
    }

    [Test]
    public void ColorGraph_ThrowsArgumentException_ForCompleteGraphK4WithThreeColors()
    {
		_logger.LogDebug(">>> Entering ColorGraph_ThrowsArgumentException_ForCompleteGraphK4WithThreeColors()");
        var solver = new GraphColoringSolver();
        var graph = CreateCompleteGraph(4); // K4
		_logger.LogDebug("ColorGraph_ThrowsArgumentException_ForCompleteGraphK4WithThreeColors(): graph → {Graph}", graph);
		_logger.LogDebug("ColorGraph_ThrowsArgumentException_ForCompleteGraphK4WithThreeColors(): solver → {Solver}", solver);

        // K4 requires 4 colors
        Assert.Throws<ArgumentException>(() => solver.ColorGraph(graph, 3));
		_logger.LogDebug("<<< Exiting ColorGraph_ThrowsArgumentException_ForCompleteGraphK4WithThreeColors()");
    }

    [Test]
    public void ColorGraph_ColorsStarGraph_WithTwoColors()
    {
		_logger.LogDebug(">>> Entering ColorGraph_ColorsStarGraph_WithTwoColors()");
        var solver = new GraphColoringSolver();
        
		_logger.LogDebug("ColorGraph_ColorsStarGraph_WithTwoColors(): solver → {Solver}", solver);
        // Star graph: one central vertex connected to all others
        var graph = new bool[5, 5];
        for (var i = 1; i < 5; i++)
        {
            graph[0, i] = true;
            graph[i, 0] = true;
        }

        var result = solver.ColorGraph(graph, 2);
		_logger.LogDebug("ColorGraph_ColorsStarGraph_WithTwoColors(): result → {Result}", result);

        Assert.That(result, Has.Length.EqualTo(5));
        AssertValidColoring(graph, result);
        
        // Star graph requires only 2 colors
        Assert.That(result.Distinct().Count(), Is.LessThanOrEqualTo(2));
		_logger.LogDebug("<<< Exiting ColorGraph_ColorsStarGraph_WithTwoColors()");
    }

    [Test]
    public void ColorGraph_HandlesPetersenGraph_WithThreeColors()
    {
		_logger.LogDebug(">>> Entering ColorGraph_HandlesPetersenGraph_WithThreeColors()");
        var solver = new GraphColoringSolver();
        
		_logger.LogDebug("ColorGraph_HandlesPetersenGraph_WithThreeColors(): solver → {Solver}", solver);
        // Simplified version: a graph that requires 3 colors
        // Creating a graph with a triangle and additional connections
        var graph = new bool[5, 5];
        
        // Triangle: vertices 0, 1, 2
        graph[0, 1] = graph[1, 0] = true;
        graph[1, 2] = graph[2, 1] = true;
        graph[2, 0] = graph[0, 2] = true;
        
        // Additional edges to vertex 3
        graph[0, 3] = graph[3, 0] = true;
        graph[1, 3] = graph[3, 1] = true;
        
        // Additional edges to vertex 4
        graph[2, 4] = graph[4, 2] = true;
        graph[3, 4] = graph[4, 3] = true;

        var result = solver.ColorGraph(graph, 3);
		_logger.LogDebug("ColorGraph_HandlesPetersenGraph_WithThreeColors(): result → {Result}", result);

        Assert.That(result, Has.Length.EqualTo(5));
        AssertValidColoring(graph, result);
		_logger.LogDebug("<<< Exiting ColorGraph_HandlesPetersenGraph_WithThreeColors()");
    }

    [Test]
    public void ColorGraph_AllColorsWithinRange()
    {
		_logger.LogDebug(">>> Entering ColorGraph_AllColorsWithinRange()");
        var solver = new GraphColoringSolver();
        var graph = CreateCompleteGraph(4);
		_logger.LogDebug("ColorGraph_AllColorsWithinRange(): graph → {Graph}", graph);
		_logger.LogDebug("ColorGraph_AllColorsWithinRange(): solver → {Solver}", solver);
        var numColors = 4;

        var result = solver.ColorGraph(graph, numColors);
		_logger.LogDebug("ColorGraph_AllColorsWithinRange(): result → {Result}", result);

        // Verify all colors are in range [0, numColors)
        foreach (var color in result)
        {
            Assert.That(color, Is.InRange(0, numColors - 1));
        }
		_logger.LogDebug("<<< Exiting ColorGraph_AllColorsWithinRange()");
    }

    [Test]
    public void ColorGraph_SymmetricMatrix_ProducesSameResult()
    {
		_logger.LogDebug(">>> Entering ColorGraph_SymmetricMatrix_ProducesSameResult()");
        var solver = new GraphColoringSolver();
        
		_logger.LogDebug("ColorGraph_SymmetricMatrix_ProducesSameResult(): solver → {Solver}", solver);
        // Create a symmetric graph
        var graph = new bool[4, 4];
        graph[0, 1] = graph[1, 0] = true;
        graph[1, 2] = graph[2, 1] = true;
        graph[2, 3] = graph[3, 2] = true;

        var result = solver.ColorGraph(graph, 3);
		_logger.LogDebug("ColorGraph_SymmetricMatrix_ProducesSameResult(): result → {Result}", result);

        Assert.That(result, Has.Length.EqualTo(4));
        AssertValidColoring(graph, result);
		_logger.LogDebug("<<< Exiting ColorGraph_SymmetricMatrix_ProducesSameResult()");
    }

    [Test]
    public void ColorGraph_LargerGraph_ProducesValidColoring()
    {
		_logger.LogDebug(">>> Entering ColorGraph_LargerGraph_ProducesValidColoring()");
        var solver = new GraphColoringSolver();
        
		_logger.LogDebug("ColorGraph_LargerGraph_ProducesValidColoring(): solver → {Solver}", solver);
        // Create a larger graph (10 vertices, random edges)
        var graph = new bool[10, 10];
        
        // Create edges forming a more complex structure
        for (var i = 0; i < 9; i++)
        {
            graph[i, i + 1] = graph[i + 1, i] = true;
        }
        
        // Add some cross edges
        graph[0, 5] = graph[5, 0] = true;
        graph[2, 7] = graph[7, 2] = true;
        graph[3, 8] = graph[8, 3] = true;

        var result = solver.ColorGraph(graph, 3);
		_logger.LogDebug("ColorGraph_LargerGraph_ProducesValidColoring(): result → {Result}", result);

        Assert.That(result, Has.Length.EqualTo(10));
        AssertValidColoring(graph, result);
		_logger.LogDebug("<<< Exiting ColorGraph_LargerGraph_ProducesValidColoring()");
    }
}
