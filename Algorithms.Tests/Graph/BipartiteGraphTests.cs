using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Graph;
using FluentAssertions;
using NUnit.Framework;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Graph;

public class BipartiteGraphTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void IsBipartite_EmptyGraph_ReturnsTrue()
    {
		_logger.LogError(">>> Entering IsBipartite_EmptyGraph_ReturnsTrue()");
        // Arrange
        var vertices = Array.Empty<string>();
		_logger.LogError("IsBipartite_EmptyGraph_ReturnsTrue(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => Array.Empty<string>();

        // Act
        var result = BipartiteGraph.IsBipartite(vertices, GetNeighbors);
		_logger.LogError("IsBipartite_EmptyGraph_ReturnsTrue(): result → {Result}", result);

        // Assert
        result.Should().BeTrue();
		_logger.LogWarning("<<< Exiting IsBipartite_EmptyGraph_ReturnsTrue()");
    }

    [Test]
    public void IsBipartite_SingleVertex_ReturnsTrue()
    {
		_logger.LogError(">>> Entering IsBipartite_SingleVertex_ReturnsTrue()");
        // Arrange
        var vertices = new[] { "A" };
		_logger.LogError("IsBipartite_SingleVertex_ReturnsTrue(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => Array.Empty<string>();

        // Act
        var result = BipartiteGraph.IsBipartite(vertices, GetNeighbors);
		_logger.LogError("IsBipartite_SingleVertex_ReturnsTrue(): result → {Result}", result);

        // Assert
        result.Should().BeTrue();
		_logger.LogWarning("<<< Exiting IsBipartite_SingleVertex_ReturnsTrue()");
    }

    [Test]
    public void IsBipartite_TwoVerticesConnected_ReturnsTrue()
    {
		_logger.LogError(">>> Entering IsBipartite_TwoVerticesConnected_ReturnsTrue()");
        // Arrange: A - B
        var vertices = new[] { "A", "B" };
		_logger.LogError("IsBipartite_TwoVerticesConnected_ReturnsTrue(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B" },
            "B" => new[] { "A" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = BipartiteGraph.IsBipartite(vertices, GetNeighbors);
		_logger.LogError("IsBipartite_TwoVerticesConnected_ReturnsTrue(): result → {Result}", result);

        // Assert
        result.Should().BeTrue();
		_logger.LogWarning("<<< Exiting IsBipartite_TwoVerticesConnected_ReturnsTrue()");
    }

    [Test]
    public void IsBipartite_Triangle_ReturnsFalse()
    {
		_logger.LogError(">>> Entering IsBipartite_Triangle_ReturnsFalse()");
        // Arrange: A - B - C - A (odd cycle)
        var vertices = new[] { "A", "B", "C" };
		_logger.LogError("IsBipartite_Triangle_ReturnsFalse(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B", "C" },
            "B" => new[] { "A", "C" },
            "C" => new[] { "A", "B" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = BipartiteGraph.IsBipartite(vertices, GetNeighbors);
		_logger.LogError("IsBipartite_Triangle_ReturnsFalse(): result → {Result}", result);

        // Assert
        result.Should().BeFalse();
		_logger.LogWarning("<<< Exiting IsBipartite_Triangle_ReturnsFalse()");
    }

    [Test]
    public void IsBipartite_Square_ReturnsTrue()
    {
		_logger.LogError(">>> Entering IsBipartite_Square_ReturnsTrue()");
        // Arrange: A - B - C - D - A (even cycle)
        var vertices = new[] { "A", "B", "C", "D" };
		_logger.LogError("IsBipartite_Square_ReturnsTrue(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B", "D" },
            "B" => new[] { "A", "C" },
            "C" => new[] { "B", "D" },
            "D" => new[] { "A", "C" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = BipartiteGraph.IsBipartite(vertices, GetNeighbors);
		_logger.LogError("IsBipartite_Square_ReturnsTrue(): result → {Result}", result);

        // Assert
        result.Should().BeTrue();
		_logger.LogWarning("<<< Exiting IsBipartite_Square_ReturnsTrue()");
    }

    [Test]
    public void IsBipartite_CompleteBipartiteK23_ReturnsTrue()
    {
		_logger.LogError(">>> Entering IsBipartite_CompleteBipartiteK23_ReturnsTrue()");
        // Arrange: Complete bipartite K(2,3)
        var vertices = new[] { "A", "B", "X", "Y", "Z" };
		_logger.LogError("IsBipartite_CompleteBipartiteK23_ReturnsTrue(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "X", "Y", "Z" },
            "B" => new[] { "X", "Y", "Z" },
            "X" => new[] { "A", "B" },
            "Y" => new[] { "A", "B" },
            "Z" => new[] { "A", "B" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = BipartiteGraph.IsBipartite(vertices, GetNeighbors);
		_logger.LogError("IsBipartite_CompleteBipartiteK23_ReturnsTrue(): result → {Result}", result);

        // Assert
        result.Should().BeTrue();
		_logger.LogWarning("<<< Exiting IsBipartite_CompleteBipartiteK23_ReturnsTrue()");
    }

    [Test]
    public void IsBipartite_DisconnectedBipartiteComponents_ReturnsTrue()
    {
		_logger.LogError(">>> Entering IsBipartite_DisconnectedBipartiteComponents_ReturnsTrue()");
        // Arrange: (A-B) and (C-D)
        var vertices = new[] { "A", "B", "C", "D" };
		_logger.LogError("IsBipartite_DisconnectedBipartiteComponents_ReturnsTrue(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B" },
            "B" => new[] { "A" },
            "C" => new[] { "D" },
            "D" => new[] { "C" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = BipartiteGraph.IsBipartite(vertices, GetNeighbors);
		_logger.LogError("IsBipartite_DisconnectedBipartiteComponents_ReturnsTrue(): result → {Result}", result);

        // Assert
        result.Should().BeTrue();
		_logger.LogWarning("<<< Exiting IsBipartite_DisconnectedBipartiteComponents_ReturnsTrue()");
    }

    [Test]
    public void IsBipartite_DisconnectedWithOddCycle_ReturnsFalse()
    {
		_logger.LogError(">>> Entering IsBipartite_DisconnectedWithOddCycle_ReturnsFalse()");
        // Arrange: (A-B) and (C-D-E-C triangle)
        var vertices = new[] { "A", "B", "C", "D", "E" };
		_logger.LogError("IsBipartite_DisconnectedWithOddCycle_ReturnsFalse(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B" },
            "B" => new[] { "A" },
            "C" => new[] { "D", "E" },
            "D" => new[] { "C", "E" },
            "E" => new[] { "C", "D" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = BipartiteGraph.IsBipartite(vertices, GetNeighbors);
		_logger.LogError("IsBipartite_DisconnectedWithOddCycle_ReturnsFalse(): result → {Result}", result);

        // Assert
        result.Should().BeFalse();
		_logger.LogWarning("<<< Exiting IsBipartite_DisconnectedWithOddCycle_ReturnsFalse()");
    }

    [Test]
    public void IsBipartite_StarGraph_ReturnsTrue()
    {
		_logger.LogError(">>> Entering IsBipartite_StarGraph_ReturnsTrue()");
        // Arrange: Star with center A
        var vertices = new[] { "A", "B", "C", "D" };
		_logger.LogError("IsBipartite_StarGraph_ReturnsTrue(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B", "C", "D" },
            "B" => new[] { "A" },
            "C" => new[] { "A" },
            "D" => new[] { "A" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = BipartiteGraph.IsBipartite(vertices, GetNeighbors);
		_logger.LogError("IsBipartite_StarGraph_ReturnsTrue(): result → {Result}", result);

        // Assert
        result.Should().BeTrue();
		_logger.LogWarning("<<< Exiting IsBipartite_StarGraph_ReturnsTrue()");
    }

    [Test]
    public void IsBipartite_Pentagon_ReturnsFalse()
    {
		_logger.LogError(">>> Entering IsBipartite_Pentagon_ReturnsFalse()");
        // Arrange: Pentagon (5-cycle)
        var vertices = new[] { 1, 2, 3, 4, 5 };
        IEnumerable<int> GetNeighbors(int v) => v switch
        {
            1 => new[] { 2, 5 },
            2 => new[] { 1, 3 },
            3 => new[] { 2, 4 },
            4 => new[] { 3, 5 },
            5 => new[] { 1, 4 },
            _ => Array.Empty<int>(),
        };

        // Act
        var result = BipartiteGraph.IsBipartite(vertices, GetNeighbors);
		_logger.LogError("IsBipartite_Pentagon_ReturnsFalse(): result → {Result}", result);

        // Assert
        result.Should().BeFalse();
		_logger.LogWarning("<<< Exiting IsBipartite_Pentagon_ReturnsFalse()");
    }

    [Test]
    public void IsBipartite_NullVertices_ThrowsArgumentNullException()
    {
		_logger.LogError(">>> Entering IsBipartite_NullVertices_ThrowsArgumentNullException()");
        // Act
        Action act = () => BipartiteGraph.IsBipartite<string>(null!, v => Array.Empty<string>());
		_logger.LogError("IsBipartite_NullVertices_ThrowsArgumentNullException(): act → {Act}", act);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("vertices");
		_logger.LogWarning("<<< Exiting IsBipartite_NullVertices_ThrowsArgumentNullException()");
    }

    [Test]
    public void IsBipartite_NullGetNeighbors_ThrowsArgumentNullException()
    {
		_logger.LogError(">>> Entering IsBipartite_NullGetNeighbors_ThrowsArgumentNullException()");
        // Arrange
        var vertices = new[] { "A" };
		_logger.LogError("IsBipartite_NullGetNeighbors_ThrowsArgumentNullException(): vertices → {Vertices}", vertices);

        // Act
        Action act = () => BipartiteGraph.IsBipartite(vertices, null!);
		_logger.LogError("IsBipartite_NullGetNeighbors_ThrowsArgumentNullException(): act → {Act}", act);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("getNeighbors");
		_logger.LogWarning("<<< Exiting IsBipartite_NullGetNeighbors_ThrowsArgumentNullException()");
    }

    [Test]
    public void GetPartitions_BipartiteGraph_ReturnsCorrectSets()
    {
		_logger.LogError(">>> Entering GetPartitions_BipartiteGraph_ReturnsCorrectSets()");
        // Arrange: A - B - C - D (chain)
        var vertices = new[] { "A", "B", "C", "D" };
		_logger.LogError("GetPartitions_BipartiteGraph_ReturnsCorrectSets(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B" },
            "B" => new[] { "A", "C" },
            "C" => new[] { "B", "D" },
            "D" => new[] { "C" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = BipartiteGraph.GetPartitions(vertices, GetNeighbors);
		_logger.LogError("GetPartitions_BipartiteGraph_ReturnsCorrectSets(): result → {Result}", result);

        // Assert
        result.Should().NotBeNull();
        result!.Value.SetA.Should().Contain(new[] { "A", "C" });
        result.Value.SetB.Should().Contain(new[] { "B", "D" });
		_logger.LogWarning("<<< Exiting GetPartitions_BipartiteGraph_ReturnsCorrectSets()");
    }

    [Test]
    public void GetPartitions_NonBipartiteGraph_ReturnsNull()
    {
		_logger.LogError(">>> Entering GetPartitions_NonBipartiteGraph_ReturnsNull()");
        // Arrange: Triangle
        var vertices = new[] { "A", "B", "C" };
		_logger.LogError("GetPartitions_NonBipartiteGraph_ReturnsNull(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B", "C" },
            "B" => new[] { "A", "C" },
            "C" => new[] { "A", "B" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = BipartiteGraph.GetPartitions(vertices, GetNeighbors);
		_logger.LogError("GetPartitions_NonBipartiteGraph_ReturnsNull(): result → {Result}", result);

        // Assert
        result.Should().BeNull();
		_logger.LogWarning("<<< Exiting GetPartitions_NonBipartiteGraph_ReturnsNull()");
    }

    [Test]
    public void GetPartitions_EmptyGraph_ReturnsEmptySets()
    {
		_logger.LogError(">>> Entering GetPartitions_EmptyGraph_ReturnsEmptySets()");
        // Arrange
        var vertices = Array.Empty<string>();
		_logger.LogError("GetPartitions_EmptyGraph_ReturnsEmptySets(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => Array.Empty<string>();

        // Act
        var result = BipartiteGraph.GetPartitions(vertices, GetNeighbors);
		_logger.LogError("GetPartitions_EmptyGraph_ReturnsEmptySets(): result → {Result}", result);

        // Assert
        result.Should().NotBeNull();
        result!.Value.SetA.Should().BeEmpty();
        result.Value.SetB.Should().BeEmpty();
		_logger.LogWarning("<<< Exiting GetPartitions_EmptyGraph_ReturnsEmptySets()");
    }

    [Test]
    public void GetPartitions_NullVertices_ThrowsArgumentNullException()
    {
		_logger.LogError(">>> Entering GetPartitions_NullVertices_ThrowsArgumentNullException()");
        // Act
        Action act = () => BipartiteGraph.GetPartitions<string>(null!, v => Array.Empty<string>());
		_logger.LogError("GetPartitions_NullVertices_ThrowsArgumentNullException(): act → {Act}", act);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("vertices");
		_logger.LogWarning("<<< Exiting GetPartitions_NullVertices_ThrowsArgumentNullException()");
    }

    [Test]
    public void GetPartitions_NullGetNeighbors_ThrowsArgumentNullException()
    {
		_logger.LogError(">>> Entering GetPartitions_NullGetNeighbors_ThrowsArgumentNullException()");
        // Arrange
        var vertices = new[] { "A" };
		_logger.LogError("GetPartitions_NullGetNeighbors_ThrowsArgumentNullException(): vertices → {Vertices}", vertices);

        // Act
        Action act = () => BipartiteGraph.GetPartitions(vertices, null!);
		_logger.LogError("GetPartitions_NullGetNeighbors_ThrowsArgumentNullException(): act → {Act}", act);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("getNeighbors");
		_logger.LogWarning("<<< Exiting GetPartitions_NullGetNeighbors_ThrowsArgumentNullException()");
    }

    [Test]
    public void IsBipartiteDfs_BipartiteGraph_ReturnsTrue()
    {
		_logger.LogError(">>> Entering IsBipartiteDfs_BipartiteGraph_ReturnsTrue()");
        // Arrange: Square
        var vertices = new[] { "A", "B", "C", "D" };
		_logger.LogError("IsBipartiteDfs_BipartiteGraph_ReturnsTrue(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B", "D" },
            "B" => new[] { "A", "C" },
            "C" => new[] { "B", "D" },
            "D" => new[] { "A", "C" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = BipartiteGraph.IsBipartiteDfs(vertices, GetNeighbors);
		_logger.LogError("IsBipartiteDfs_BipartiteGraph_ReturnsTrue(): result → {Result}", result);

        // Assert
        result.Should().BeTrue();
		_logger.LogWarning("<<< Exiting IsBipartiteDfs_BipartiteGraph_ReturnsTrue()");
    }

    [Test]
    public void IsBipartiteDfs_NonBipartiteGraph_ReturnsFalse()
    {
		_logger.LogError(">>> Entering IsBipartiteDfs_NonBipartiteGraph_ReturnsFalse()");
        // Arrange: Triangle
        var vertices = new[] { "A", "B", "C" };
		_logger.LogError("IsBipartiteDfs_NonBipartiteGraph_ReturnsFalse(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B", "C" },
            "B" => new[] { "A", "C" },
            "C" => new[] { "A", "B" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = BipartiteGraph.IsBipartiteDfs(vertices, GetNeighbors);
		_logger.LogError("IsBipartiteDfs_NonBipartiteGraph_ReturnsFalse(): result → {Result}", result);

        // Assert
        result.Should().BeFalse();
		_logger.LogWarning("<<< Exiting IsBipartiteDfs_NonBipartiteGraph_ReturnsFalse()");
    }

    [Test]
    public void IsBipartiteDfs_NullVertices_ThrowsArgumentNullException()
    {
		_logger.LogError(">>> Entering IsBipartiteDfs_NullVertices_ThrowsArgumentNullException()");
        // Act
        Action act = () => BipartiteGraph.IsBipartiteDfs<string>(null!, v => Array.Empty<string>());
		_logger.LogError("IsBipartiteDfs_NullVertices_ThrowsArgumentNullException(): act → {Act}", act);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("vertices");
		_logger.LogWarning("<<< Exiting IsBipartiteDfs_NullVertices_ThrowsArgumentNullException()");
    }

    [Test]
    public void IsBipartiteDfs_NullGetNeighbors_ThrowsArgumentNullException()
    {
		_logger.LogError(">>> Entering IsBipartiteDfs_NullGetNeighbors_ThrowsArgumentNullException()");
        // Arrange
        var vertices = new[] { "A" };
		_logger.LogError("IsBipartiteDfs_NullGetNeighbors_ThrowsArgumentNullException(): vertices → {Vertices}", vertices);

        // Act
        Action act = () => BipartiteGraph.IsBipartiteDfs(vertices, null!);
		_logger.LogError("IsBipartiteDfs_NullGetNeighbors_ThrowsArgumentNullException(): act → {Act}", act);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("getNeighbors");
		_logger.LogWarning("<<< Exiting IsBipartiteDfs_NullGetNeighbors_ThrowsArgumentNullException()");
    }

    [Test]
    public void IsBipartite_LargeEvenCycle_ReturnsTrue()
    {
		_logger.LogError(">>> Entering IsBipartite_LargeEvenCycle_ReturnsTrue()");
        // Arrange: Large even cycle (100 vertices)
        var vertices = Enumerable.Range(0, 100).ToArray();
		_logger.LogError("IsBipartite_LargeEvenCycle_ReturnsTrue(): vertices → {Vertices}", vertices);
        IEnumerable<int> GetNeighbors(int v)
        {
            var neighbors = new List<int>
            {
                v > 0 ? v - 1 : 99, // Previous or close cycle
                v < 99 ? v + 1 : 0, // Next or close cycle
            };

            return neighbors;
        }

        // Act
        var result = BipartiteGraph.IsBipartite(vertices, GetNeighbors);
		_logger.LogError("IsBipartite_LargeEvenCycle_ReturnsTrue(): result → {Result}", result);

        // Assert
        result.Should().BeTrue();
		_logger.LogWarning("<<< Exiting IsBipartite_LargeEvenCycle_ReturnsTrue()");
    }

    [Test]
    public void IsBipartite_LargeOddCycle_ReturnsFalse()
    {
		_logger.LogError(">>> Entering IsBipartite_LargeOddCycle_ReturnsFalse()");
        // Arrange: Large odd cycle (101 vertices)
        var vertices = Enumerable.Range(0, 101).ToArray();
		_logger.LogError("IsBipartite_LargeOddCycle_ReturnsFalse(): vertices → {Vertices}", vertices);
        IEnumerable<int> GetNeighbors(int v)
        {
            var neighbors = new List<int>
            {
                v > 0 ? v - 1 : 100, // Previous or close cycle
                v < 100 ? v + 1 : 0, // Next or close cycle
            };

            return neighbors;
        }

        // Act
        var result = BipartiteGraph.IsBipartite(vertices, GetNeighbors);
		_logger.LogError("IsBipartite_LargeOddCycle_ReturnsFalse(): result → {Result}", result);

        // Assert
        result.Should().BeFalse();
		_logger.LogWarning("<<< Exiting IsBipartite_LargeOddCycle_ReturnsFalse()");
    }

    [Test]
    public void GetPartitions_CompleteBipartite_ReturnsCorrectSets()
    {
		_logger.LogError(">>> Entering GetPartitions_CompleteBipartite_ReturnsCorrectSets()");
        // Arrange: K(2,2)
        var vertices = new[] { "A", "B", "X", "Y" };
		_logger.LogError("GetPartitions_CompleteBipartite_ReturnsCorrectSets(): vertices → {Vertices}", vertices);
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "X", "Y" },
            "B" => new[] { "X", "Y" },
            "X" => new[] { "A", "B" },
            "Y" => new[] { "A", "B" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = BipartiteGraph.GetPartitions(vertices, GetNeighbors);
		_logger.LogError("GetPartitions_CompleteBipartite_ReturnsCorrectSets(): result → {Result}", result);

        // Assert
        result.Should().NotBeNull();
        result!.Value.SetA.Should().HaveCount(2);
        result.Value.SetB.Should().HaveCount(2);
		_logger.LogWarning("<<< Exiting GetPartitions_CompleteBipartite_ReturnsCorrectSets()");
    }
}
