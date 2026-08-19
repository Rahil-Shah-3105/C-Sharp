using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Graph;
using FluentAssertions;
using NUnit.Framework;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Graph;

public class ArticulationPointsTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void Find_SimpleChain_ReturnsMiddleVertex()
    {
		_logger.LogDebug(">>> Entering Find_SimpleChain_ReturnsMiddleVertex()");
        // Arrange: A - B - C (B is articulation point)
        var vertices = new[] { "A", "B", "C" };
		_logger.LogDebug("Find_SimpleChain_ReturnsMiddleVertex(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B" },
            "B" => new[] { "A", "C" },
            "C" => new[] { "B" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = ArticulationPoints.Find(vertices, GetNeighbors);
		_logger.LogDebug("Find_SimpleChain_ReturnsMiddleVertex(): result → {Result}", result);

        // Assert
        result.Should().ContainSingle();
        result.Should().Contain("B");
		_logger.LogDebug("<<< Exiting Find_SimpleChain_ReturnsMiddleVertex()");
    }

    [Test]
    public void Find_Triangle_ReturnsEmpty()
    {
		_logger.LogDebug(">>> Entering Find_Triangle_ReturnsEmpty()");
        // Arrange: A - B - C - A (no articulation points)
        var vertices = new[] { "A", "B", "C" };
		_logger.LogDebug("Find_Triangle_ReturnsEmpty(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B", "C" },
            "B" => new[] { "A", "C" },
            "C" => new[] { "A", "B" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = ArticulationPoints.Find(vertices, GetNeighbors);
		_logger.LogDebug("Find_Triangle_ReturnsEmpty(): result → {Result}", result);

        // Assert
        result.Should().BeEmpty();
		_logger.LogDebug("<<< Exiting Find_Triangle_ReturnsEmpty()");
    }

    [Test]
    public void Find_StarGraph_ReturnsCenterVertex()
    {
		_logger.LogDebug(">>> Entering Find_StarGraph_ReturnsCenterVertex()");
        // Arrange: Star with center A
        var vertices = new[] { "A", "B", "C", "D" };
		_logger.LogDebug("Find_StarGraph_ReturnsCenterVertex(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B", "C", "D" },
            "B" => new[] { "A" },
            "C" => new[] { "A" },
            "D" => new[] { "A" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = ArticulationPoints.Find(vertices, GetNeighbors);
		_logger.LogDebug("Find_StarGraph_ReturnsCenterVertex(): result → {Result}", result);

        // Assert
        result.Should().ContainSingle();
        result.Should().Contain("A");
		_logger.LogDebug("<<< Exiting Find_StarGraph_ReturnsCenterVertex()");
    }

    [Test]
    public void Find_BridgeGraph_ReturnsMultiplePoints()
    {
		_logger.LogDebug(">>> Entering Find_BridgeGraph_ReturnsMultiplePoints()");
        // Arrange: (A-B-C) - D - (E-F-G)
        var vertices = new[] { "A", "B", "C", "D", "E", "F", "G" };
		_logger.LogDebug("Find_BridgeGraph_ReturnsMultiplePoints(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B" },
            "B" => new[] { "A", "C", "D" },
            "C" => new[] { "B" },
            "D" => new[] { "B", "E" },
            "E" => new[] { "D", "F" },
            "F" => new[] { "E", "G" },
            "G" => new[] { "F" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = ArticulationPoints.Find(vertices, GetNeighbors);
		_logger.LogDebug("Find_BridgeGraph_ReturnsMultiplePoints(): result → {Result}", result);

        // Assert
        result.Should().HaveCount(4);
        result.Should().Contain(new[] { "B", "D", "E", "F" });
		_logger.LogDebug("<<< Exiting Find_BridgeGraph_ReturnsMultiplePoints()");
    }

    [Test]
    public void Find_DisconnectedGraph_FindsPointsInEachComponent()
    {
		_logger.LogDebug(">>> Entering Find_DisconnectedGraph_FindsPointsInEachComponent()");
        // Arrange: (A-B-C) and (D-E-F)
        var vertices = new[] { "A", "B", "C", "D", "E", "F" };
		_logger.LogDebug("Find_DisconnectedGraph_FindsPointsInEachComponent(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B" },
            "B" => new[] { "A", "C" },
            "C" => new[] { "B" },
            "D" => new[] { "E" },
            "E" => new[] { "D", "F" },
            "F" => new[] { "E" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = ArticulationPoints.Find(vertices, GetNeighbors);
		_logger.LogDebug("Find_DisconnectedGraph_FindsPointsInEachComponent(): result → {Result}", result);

        // Assert
        result.Should().HaveCount(2);
        result.Should().Contain(new[] { "B", "E" });
		_logger.LogDebug("<<< Exiting Find_DisconnectedGraph_FindsPointsInEachComponent()");
    }

    [Test]
    public void Find_SingleVertex_ReturnsEmpty()
    {
		_logger.LogDebug(">>> Entering Find_SingleVertex_ReturnsEmpty()");
        // Arrange
        var vertices = new[] { "A" };
		_logger.LogDebug("Find_SingleVertex_ReturnsEmpty(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => Array.Empty<string>();

        // Act
        var result = ArticulationPoints.Find(vertices, GetNeighbors);
		_logger.LogDebug("Find_SingleVertex_ReturnsEmpty(): result → {Result}", result);

        // Assert
        result.Should().BeEmpty();
		_logger.LogDebug("<<< Exiting Find_SingleVertex_ReturnsEmpty()");
    }

    [Test]
    public void Find_TwoVertices_ReturnsEmpty()
    {
		_logger.LogDebug(">>> Entering Find_TwoVertices_ReturnsEmpty()");
        // Arrange: A - B
        var vertices = new[] { "A", "B" };
		_logger.LogDebug("Find_TwoVertices_ReturnsEmpty(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B" },
            "B" => new[] { "A" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = ArticulationPoints.Find(vertices, GetNeighbors);
		_logger.LogDebug("Find_TwoVertices_ReturnsEmpty(): result → {Result}", result);

        // Assert
        result.Should().BeEmpty();
		_logger.LogDebug("<<< Exiting Find_TwoVertices_ReturnsEmpty()");
    }

    [Test]
    public void Find_ComplexGraph_ReturnsCorrectPoints()
    {
		_logger.LogDebug(">>> Entering Find_ComplexGraph_ReturnsCorrectPoints()");
        // Arrange: Complex graph with multiple articulation points
        var vertices = new[] { 1, 2, 3, 4, 5, 6, 7 };
        IEnumerable<int> GetNeighbors(int v) => v switch
        {
            1 => new[] { 2, 3 },
            2 => new[] { 1, 3 },
            3 => new[] { 1, 2, 4 },
            4 => new[] { 3, 5, 6 },
            5 => new[] { 4, 6 },
            6 => new[] { 4, 5, 7 },
            7 => new[] { 6 },
            _ => Array.Empty<int>(),
        };

        // Act
        var result = ArticulationPoints.Find(vertices, GetNeighbors);
		_logger.LogDebug("Find_ComplexGraph_ReturnsCorrectPoints(): result → {Result}", result);

        // Assert
        result.Should().Contain(new[] { 3, 4, 6 });
		_logger.LogDebug("<<< Exiting Find_ComplexGraph_ReturnsCorrectPoints()");
    }

    [Test]
    public void Find_EmptyGraph_ReturnsEmpty()
    {
		_logger.LogDebug(">>> Entering Find_EmptyGraph_ReturnsEmpty()");
        // Arrange
        var vertices = Array.Empty<string>();
		_logger.LogDebug("Find_EmptyGraph_ReturnsEmpty(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => Array.Empty<string>();

        // Act
        var result = ArticulationPoints.Find(vertices, GetNeighbors);
		_logger.LogDebug("Find_EmptyGraph_ReturnsEmpty(): result → {Result}", result);

        // Assert
        result.Should().BeEmpty();
		_logger.LogDebug("<<< Exiting Find_EmptyGraph_ReturnsEmpty()");
    }

    [Test]
    public void Find_NullVertices_ThrowsArgumentNullException()
    {
		_logger.LogDebug(">>> Entering Find_NullVertices_ThrowsArgumentNullException()");
        // Act
        Action act = () => ArticulationPoints.Find<string>(null!, v => Array.Empty<string>());
		_logger.LogDebug("Find_NullVertices_ThrowsArgumentNullException(): act → {Act}", act);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("vertices");
		_logger.LogDebug("<<< Exiting Find_NullVertices_ThrowsArgumentNullException()");
    }

    [Test]
    public void Find_NullGetNeighbors_ThrowsArgumentNullException()
    {
		_logger.LogDebug(">>> Entering Find_NullGetNeighbors_ThrowsArgumentNullException()");
        // Arrange
        var vertices = new[] { "A" };
		_logger.LogDebug("Find_NullGetNeighbors_ThrowsArgumentNullException(): vertices → {Vertices}", vertices);

        // Act
        Action act = () => ArticulationPoints.Find(vertices, null!);
		_logger.LogDebug("Find_NullGetNeighbors_ThrowsArgumentNullException(): act → {Act}", act);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("getNeighbors");
		_logger.LogDebug("<<< Exiting Find_NullGetNeighbors_ThrowsArgumentNullException()");
    }

    [Test]
    public void IsArticulationPoint_ValidPoint_ReturnsTrue()
    {
		_logger.LogDebug(">>> Entering IsArticulationPoint_ValidPoint_ReturnsTrue()");
        // Arrange: A - B - C
        var vertices = new[] { "A", "B", "C" };
		_logger.LogDebug("IsArticulationPoint_ValidPoint_ReturnsTrue(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B" },
            "B" => new[] { "A", "C" },
            "C" => new[] { "B" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = ArticulationPoints.IsArticulationPoint("B", vertices, GetNeighbors);
		_logger.LogDebug("IsArticulationPoint_ValidPoint_ReturnsTrue(): result → {Result}", result);

        // Assert
        result.Should().BeTrue();
		_logger.LogDebug("<<< Exiting IsArticulationPoint_ValidPoint_ReturnsTrue()");
    }

    [Test]
    public void IsArticulationPoint_NotArticulationPoint_ReturnsFalse()
    {
		_logger.LogDebug(">>> Entering IsArticulationPoint_NotArticulationPoint_ReturnsFalse()");
        // Arrange: A - B - C
        var vertices = new[] { "A", "B", "C" };
		_logger.LogDebug("IsArticulationPoint_NotArticulationPoint_ReturnsFalse(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B" },
            "B" => new[] { "A", "C" },
            "C" => new[] { "B" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = ArticulationPoints.IsArticulationPoint("A", vertices, GetNeighbors);
		_logger.LogDebug("IsArticulationPoint_NotArticulationPoint_ReturnsFalse(): result → {Result}", result);

        // Assert
        result.Should().BeFalse();
		_logger.LogDebug("<<< Exiting IsArticulationPoint_NotArticulationPoint_ReturnsFalse()");
    }

    [Test]
    public void Count_SimpleChain_ReturnsOne()
    {
		_logger.LogDebug(">>> Entering Count_SimpleChain_ReturnsOne()");
        // Arrange: A - B - C
        var vertices = new[] { "A", "B", "C" };
		_logger.LogDebug("Count_SimpleChain_ReturnsOne(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B" },
            "B" => new[] { "A", "C" },
            "C" => new[] { "B" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = ArticulationPoints.Count(vertices, GetNeighbors);
		_logger.LogDebug("Count_SimpleChain_ReturnsOne(): result → {Result}", result);

        // Assert
        result.Should().Be(1);
		_logger.LogDebug("<<< Exiting Count_SimpleChain_ReturnsOne()");
    }

    [Test]
    public void Count_Triangle_ReturnsZero()
    {
		_logger.LogDebug(">>> Entering Count_Triangle_ReturnsZero()");
        // Arrange: A - B - C - A
        var vertices = new[] { "A", "B", "C" };
		_logger.LogDebug("Count_Triangle_ReturnsZero(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B", "C" },
            "B" => new[] { "A", "C" },
            "C" => new[] { "A", "B" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = ArticulationPoints.Count(vertices, GetNeighbors);
		_logger.LogDebug("Count_Triangle_ReturnsZero(): result → {Result}", result);

        // Assert
        result.Should().Be(0);
		_logger.LogDebug("<<< Exiting Count_Triangle_ReturnsZero()");
    }

    [Test]
    public void Find_LargeGraph_FindsAllPoints()
    {
		_logger.LogDebug(">>> Entering Find_LargeGraph_FindsAllPoints()");
        // Arrange: Large chain
        var vertices = Enumerable.Range(1, 10).ToArray();
		_logger.LogDebug("Find_LargeGraph_FindsAllPoints(): vertices → {Vertices}", vertices);
        IEnumerable<int> GetNeighbors(int v)
        {
            var neighbors = new List<int>();
			_logger.LogDebug("Find_LargeGraph_FindsAllPoints(): neighbors → {Neighbors}", neighbors);
            if (v > 1)
            {
                neighbors.Add(v - 1);
            }

            if (v < 10)
            {
                neighbors.Add(v + 1);
            }

            return neighbors;
        }

        // Act
        var result = ArticulationPoints.Find(vertices, GetNeighbors);
		_logger.LogDebug("Find_LargeGraph_FindsAllPoints(): result → {Result}", result);

        // Assert
        result.Should().HaveCount(8); // All except endpoints
        result.Should().NotContain(new[] { 1, 10 });
		_logger.LogDebug("<<< Exiting Find_LargeGraph_FindsAllPoints()");
    }
}
