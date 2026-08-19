using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Graph;
using FluentAssertions;
using NUnit.Framework;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Graph;

public class BridgesTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void Find_SimpleChain_ReturnsAllEdges()
    {
		_logger.LogDebug(">>> Entering Find_SimpleChain_ReturnsAllEdges()");
        // Arrange: A - B - C (both edges are bridges)
        var vertices = new[] { "A", "B", "C" };
		_logger.LogDebug("Find_SimpleChain_ReturnsAllEdges(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B" },
            "B" => new[] { "A", "C" },
            "C" => new[] { "B" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = Bridges.Find(vertices, GetNeighbors);
		_logger.LogDebug("Find_SimpleChain_ReturnsAllEdges(): result → {Result}", result);

        // Assert
        result.Should().HaveCount(2);
        result.Should().Contain(new[] { ("A", "B"), ("B", "C") });
		_logger.LogDebug("<<< Exiting Find_SimpleChain_ReturnsAllEdges()");
    }

    [Test]
    public void Find_Triangle_ReturnsEmpty()
    {
		_logger.LogDebug(">>> Entering Find_Triangle_ReturnsEmpty()");
        // Arrange: A - B - C - A (no bridges in cycle)
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
        var result = Bridges.Find(vertices, GetNeighbors);
		_logger.LogDebug("Find_Triangle_ReturnsEmpty(): result → {Result}", result);

        // Assert
        result.Should().BeEmpty();
		_logger.LogDebug("<<< Exiting Find_Triangle_ReturnsEmpty()");
    }

    [Test]
    public void Find_TwoComponentsConnectedByBridge_ReturnsBridge()
    {
		_logger.LogDebug(">>> Entering Find_TwoComponentsConnectedByBridge_ReturnsBridge()");
        // Arrange: (A-B-C) - D - (E-F-G)
        var vertices = new[] { "A", "B", "C", "D", "E", "F", "G" };
		_logger.LogDebug("Find_TwoComponentsConnectedByBridge_ReturnsBridge(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B", "C" },
            "B" => new[] { "A", "C", "D" },
            "C" => new[] { "A", "B" },
            "D" => new[] { "B", "E" },
            "E" => new[] { "D", "F", "G" },
            "F" => new[] { "E", "G" },
            "G" => new[] { "E", "F" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = Bridges.Find(vertices, GetNeighbors);
		_logger.LogDebug("Find_TwoComponentsConnectedByBridge_ReturnsBridge(): result → {Result}", result);

        // Assert
        result.Should().HaveCount(2);
        result.Should().Contain(new[] { ("B", "D"), ("D", "E") });
		_logger.LogDebug("<<< Exiting Find_TwoComponentsConnectedByBridge_ReturnsBridge()");
    }

    [Test]
    public void Find_StarGraph_ReturnsAllEdges()
    {
		_logger.LogDebug(">>> Entering Find_StarGraph_ReturnsAllEdges()");
        // Arrange: Star with center A
        var vertices = new[] { "A", "B", "C", "D" };
		_logger.LogDebug("Find_StarGraph_ReturnsAllEdges(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B", "C", "D" },
            "B" => new[] { "A" },
            "C" => new[] { "A" },
            "D" => new[] { "A" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = Bridges.Find(vertices, GetNeighbors);
		_logger.LogDebug("Find_StarGraph_ReturnsAllEdges(): result → {Result}", result);

        // Assert
        result.Should().HaveCount(3);
        result.Should().Contain(new[] { ("A", "B"), ("A", "C"), ("A", "D") });
		_logger.LogDebug("<<< Exiting Find_StarGraph_ReturnsAllEdges()");
    }

    [Test]
    public void Find_DisconnectedGraph_FindsBridgesInEachComponent()
    {
		_logger.LogDebug(">>> Entering Find_DisconnectedGraph_FindsBridgesInEachComponent()");
        // Arrange: (A-B-C) and (D-E-F)
        var vertices = new[] { "A", "B", "C", "D", "E", "F" };
		_logger.LogDebug("Find_DisconnectedGraph_FindsBridgesInEachComponent(): vertices → {Vertices}", vertices);
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
        var result = Bridges.Find(vertices, GetNeighbors);
		_logger.LogDebug("Find_DisconnectedGraph_FindsBridgesInEachComponent(): result → {Result}", result);

        // Assert
        result.Should().HaveCount(4);
        result.Should().Contain(new[] { ("A", "B"), ("B", "C"), ("D", "E"), ("E", "F") });
		_logger.LogDebug("<<< Exiting Find_DisconnectedGraph_FindsBridgesInEachComponent()");
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
        var result = Bridges.Find(vertices, GetNeighbors);
		_logger.LogDebug("Find_SingleVertex_ReturnsEmpty(): result → {Result}", result);

        // Assert
        result.Should().BeEmpty();
		_logger.LogDebug("<<< Exiting Find_SingleVertex_ReturnsEmpty()");
    }

    [Test]
    public void Find_TwoVertices_ReturnsBridge()
    {
		_logger.LogDebug(">>> Entering Find_TwoVertices_ReturnsBridge()");
        // Arrange: A - B
        var vertices = new[] { "A", "B" };
		_logger.LogDebug("Find_TwoVertices_ReturnsBridge(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B" },
            "B" => new[] { "A" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = Bridges.Find(vertices, GetNeighbors);
		_logger.LogDebug("Find_TwoVertices_ReturnsBridge(): result → {Result}", result);

        // Assert
        result.Should().ContainSingle();
        result.Should().Contain(("A", "B"));
		_logger.LogDebug("<<< Exiting Find_TwoVertices_ReturnsBridge()");
    }

    [Test]
    public void Find_ComplexGraph_ReturnsCorrectBridges()
    {
		_logger.LogDebug(">>> Entering Find_ComplexGraph_ReturnsCorrectBridges()");
        // Arrange: Complex graph with cycles and bridges
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
        var result = Bridges.Find(vertices, GetNeighbors);
		_logger.LogDebug("Find_ComplexGraph_ReturnsCorrectBridges(): result → {Result}", result);

        // Assert
        result.Should().Contain(new[] { (3, 4), (6, 7) });
		_logger.LogDebug("<<< Exiting Find_ComplexGraph_ReturnsCorrectBridges()");
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
        var result = Bridges.Find(vertices, GetNeighbors);
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
        Action act = () => Bridges.Find<string>(null!, v => Array.Empty<string>());
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
        Action act = () => Bridges.Find(vertices, null!);
		_logger.LogDebug("Find_NullGetNeighbors_ThrowsArgumentNullException(): act → {Act}", act);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("getNeighbors");
		_logger.LogDebug("<<< Exiting Find_NullGetNeighbors_ThrowsArgumentNullException()");
    }

    [Test]
    public void IsBridge_ValidBridge_ReturnsTrue()
    {
		_logger.LogDebug(">>> Entering IsBridge_ValidBridge_ReturnsTrue()");
        // Arrange: A - B - C
        var vertices = new[] { "A", "B", "C" };
		_logger.LogDebug("IsBridge_ValidBridge_ReturnsTrue(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B" },
            "B" => new[] { "A", "C" },
            "C" => new[] { "B" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = Bridges.IsBridge("A", "B", vertices, GetNeighbors);
		_logger.LogDebug("IsBridge_ValidBridge_ReturnsTrue(): result → {Result}", result);

        // Assert
        result.Should().BeTrue();
		_logger.LogDebug("<<< Exiting IsBridge_ValidBridge_ReturnsTrue()");
    }

    [Test]
    public void IsBridge_ReverseEdge_ReturnsTrue()
    {
		_logger.LogDebug(">>> Entering IsBridge_ReverseEdge_ReturnsTrue()");
        // Arrange: A - B - C
        var vertices = new[] { "A", "B", "C" };
		_logger.LogDebug("IsBridge_ReverseEdge_ReturnsTrue(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B" },
            "B" => new[] { "A", "C" },
            "C" => new[] { "B" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = Bridges.IsBridge("B", "A", vertices, GetNeighbors);
		_logger.LogDebug("IsBridge_ReverseEdge_ReturnsTrue(): result → {Result}", result);

        // Assert
        result.Should().BeTrue();
		_logger.LogDebug("<<< Exiting IsBridge_ReverseEdge_ReturnsTrue()");
    }

    [Test]
    public void IsBridge_NotBridge_ReturnsFalse()
    {
		_logger.LogDebug(">>> Entering IsBridge_NotBridge_ReturnsFalse()");
        // Arrange: A - B - C - A (triangle)
        var vertices = new[] { "A", "B", "C" };
		_logger.LogDebug("IsBridge_NotBridge_ReturnsFalse(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B", "C" },
            "B" => new[] { "A", "C" },
            "C" => new[] { "A", "B" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = Bridges.IsBridge("A", "B", vertices, GetNeighbors);
		_logger.LogDebug("IsBridge_NotBridge_ReturnsFalse(): result → {Result}", result);

        // Assert
        result.Should().BeFalse();
		_logger.LogDebug("<<< Exiting IsBridge_NotBridge_ReturnsFalse()");
    }

    [Test]
    public void Count_SimpleChain_ReturnsTwo()
    {
		_logger.LogDebug(">>> Entering Count_SimpleChain_ReturnsTwo()");
        // Arrange: A - B - C
        var vertices = new[] { "A", "B", "C" };
		_logger.LogDebug("Count_SimpleChain_ReturnsTwo(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B" },
            "B" => new[] { "A", "C" },
            "C" => new[] { "B" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = Bridges.Count(vertices, GetNeighbors);
		_logger.LogDebug("Count_SimpleChain_ReturnsTwo(): result → {Result}", result);

        // Assert
        result.Should().Be(2);
		_logger.LogDebug("<<< Exiting Count_SimpleChain_ReturnsTwo()");
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
        var result = Bridges.Count(vertices, GetNeighbors);
		_logger.LogDebug("Count_Triangle_ReturnsZero(): result → {Result}", result);

        // Assert
        result.Should().Be(0);
		_logger.LogDebug("<<< Exiting Count_Triangle_ReturnsZero()");
    }

    [Test]
    public void Find_LargeChain_FindsAllBridges()
    {
		_logger.LogDebug(">>> Entering Find_LargeChain_FindsAllBridges()");
        // Arrange: Large chain
        var vertices = Enumerable.Range(1, 10).ToArray();
		_logger.LogDebug("Find_LargeChain_FindsAllBridges(): vertices → {Vertices}", vertices);
        IEnumerable<int> GetNeighbors(int v)
        {
            var neighbors = new List<int>();
			_logger.LogDebug("Find_LargeChain_FindsAllBridges(): neighbors → {Neighbors}", neighbors);
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
        var result = Bridges.Find(vertices, GetNeighbors);
		_logger.LogDebug("Find_LargeChain_FindsAllBridges(): result → {Result}", result);

        // Assert
        result.Should().HaveCount(9); // All edges in chain are bridges
		_logger.LogDebug("<<< Exiting Find_LargeChain_FindsAllBridges()");
    }
}
