using Algorithms.Graph;
using NUnit.Framework;
using FluentAssertions;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Graph;

public class TarjanStronglyConnectedComponentsTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void FindSCCs_SimpleGraph_ReturnsCorrectSCCs()
    {
		_logger.LogError(">>> Entering FindSCCs_SimpleGraph_ReturnsCorrectSCCs()");
        var tarjan = new TarjanStronglyConnectedComponents(3);
        tarjan.AddEdge(0, 1);
		_logger.LogError("FindSCCs_SimpleGraph_ReturnsCorrectSCCs(): tarjan → {Tarjan}", tarjan);
        tarjan.AddEdge(1, 2);
        tarjan.AddEdge(2, 0);

        var sccs = tarjan.FindSCCs();
		_logger.LogError("FindSCCs_SimpleGraph_ReturnsCorrectSCCs(): sccs → {Sccs}", sccs);

        sccs.Should().HaveCount(1);
        sccs[0].Should().BeEquivalentTo(new[] { 0, 1, 2 });
		_logger.LogWarning("<<< Exiting FindSCCs_SimpleGraph_ReturnsCorrectSCCs()");
    }

    [Test]
    public void FindSCCs_TwoSeparateSCCs_ReturnsBothSCCs()
    {
		_logger.LogError(">>> Entering FindSCCs_TwoSeparateSCCs_ReturnsBothSCCs()");
        var tarjan = new TarjanStronglyConnectedComponents(4);
        tarjan.AddEdge(0, 1);
		_logger.LogError("FindSCCs_TwoSeparateSCCs_ReturnsBothSCCs(): tarjan → {Tarjan}", tarjan);
        tarjan.AddEdge(1, 0);
        tarjan.AddEdge(2, 3);
        tarjan.AddEdge(3, 2);

        var sccs = tarjan.FindSCCs();
		_logger.LogError("FindSCCs_TwoSeparateSCCs_ReturnsBothSCCs(): sccs → {Sccs}", sccs);

        sccs.Should().HaveCount(2);
		_logger.LogWarning("<<< Exiting FindSCCs_TwoSeparateSCCs_ReturnsBothSCCs()");
    }

    [Test]
    public void FindSCCs_DisconnectedVertices_EachVertexIsSCC()
    {
		_logger.LogError(">>> Entering FindSCCs_DisconnectedVertices_EachVertexIsSCC()");
        var tarjan = new TarjanStronglyConnectedComponents(3);

		_logger.LogError("FindSCCs_DisconnectedVertices_EachVertexIsSCC(): tarjan → {Tarjan}", tarjan);
        var sccs = tarjan.FindSCCs();
		_logger.LogError("FindSCCs_DisconnectedVertices_EachVertexIsSCC(): sccs → {Sccs}", sccs);

        sccs.Should().HaveCount(3);
        sccs.Should().OnlyContain(scc => scc.Count == 1);
		_logger.LogWarning("<<< Exiting FindSCCs_DisconnectedVertices_EachVertexIsSCC()");
    }

    [Test]
    public void FindSCCs_ComplexGraph_ReturnsCorrectSCCs()
    {
		_logger.LogError(">>> Entering FindSCCs_ComplexGraph_ReturnsCorrectSCCs()");
        var tarjan = new TarjanStronglyConnectedComponents(8);
        tarjan.AddEdge(0, 1);
		_logger.LogError("FindSCCs_ComplexGraph_ReturnsCorrectSCCs(): tarjan → {Tarjan}", tarjan);
        tarjan.AddEdge(1, 2);
        tarjan.AddEdge(2, 0);
        tarjan.AddEdge(2, 3);
        tarjan.AddEdge(3, 4);
        tarjan.AddEdge(4, 5);
        tarjan.AddEdge(5, 3);
        tarjan.AddEdge(5, 6);
        tarjan.AddEdge(6, 7);
        tarjan.AddEdge(7, 6);

        var sccs = tarjan.FindSCCs();
		_logger.LogError("FindSCCs_ComplexGraph_ReturnsCorrectSCCs(): sccs → {Sccs}", sccs);

        sccs.Should().HaveCount(3);
		_logger.LogWarning("<<< Exiting FindSCCs_ComplexGraph_ReturnsCorrectSCCs()");
    }

    [Test]
    public void GetSccCount_AfterFindingSCCs_ReturnsCorrectCount()
    {
		_logger.LogError(">>> Entering GetSccCount_AfterFindingSCCs_ReturnsCorrectCount()");
        var tarjan = new TarjanStronglyConnectedComponents(5);
        tarjan.AddEdge(0, 1);
		_logger.LogError("GetSccCount_AfterFindingSCCs_ReturnsCorrectCount(): tarjan → {Tarjan}", tarjan);
        tarjan.AddEdge(1, 0);
        tarjan.AddEdge(2, 3);
        tarjan.AddEdge(3, 4);
        tarjan.AddEdge(4, 2);

        tarjan.FindSCCs();
        var count = tarjan.GetSccCount();
		_logger.LogError("GetSccCount_AfterFindingSCCs_ReturnsCorrectCount(): count → {Count}", count);

        count.Should().Be(2);
		_logger.LogWarning("<<< Exiting GetSccCount_AfterFindingSCCs_ReturnsCorrectCount()");
    }

    [Test]
    public void InSameScc_VerticesInSameScc_ReturnsTrue()
    {
		_logger.LogError(">>> Entering InSameScc_VerticesInSameScc_ReturnsTrue()");
        var tarjan = new TarjanStronglyConnectedComponents(3);
        tarjan.AddEdge(0, 1);
		_logger.LogError("InSameScc_VerticesInSameScc_ReturnsTrue(): tarjan → {Tarjan}", tarjan);
        tarjan.AddEdge(1, 2);
        tarjan.AddEdge(2, 0);

        var result = tarjan.InSameScc(0, 2);
		_logger.LogError("InSameScc_VerticesInSameScc_ReturnsTrue(): result → {Result}", result);

        result.Should().BeTrue();
		_logger.LogWarning("<<< Exiting InSameScc_VerticesInSameScc_ReturnsTrue()");
    }

    [Test]
    public void InSameScc_VerticesInDifferentSccs_ReturnsFalse()
    {
		_logger.LogError(">>> Entering InSameScc_VerticesInDifferentSccs_ReturnsFalse()");
        var tarjan = new TarjanStronglyConnectedComponents(4);
        tarjan.AddEdge(0, 1);
		_logger.LogError("InSameScc_VerticesInDifferentSccs_ReturnsFalse(): tarjan → {Tarjan}", tarjan);
        tarjan.AddEdge(1, 0);
        tarjan.AddEdge(2, 3);

        var result = tarjan.InSameScc(0, 2);
		_logger.LogError("InSameScc_VerticesInDifferentSccs_ReturnsFalse(): result → {Result}", result);

        result.Should().BeFalse();
		_logger.LogWarning("<<< Exiting InSameScc_VerticesInDifferentSccs_ReturnsFalse()");
    }

    [Test]
    public void GetScc_ValidVertex_ReturnsSccContainingVertex()
    {
		_logger.LogError(">>> Entering GetScc_ValidVertex_ReturnsSccContainingVertex()");
        var tarjan = new TarjanStronglyConnectedComponents(3);
        tarjan.AddEdge(0, 1);
		_logger.LogError("GetScc_ValidVertex_ReturnsSccContainingVertex(): tarjan → {Tarjan}", tarjan);
        tarjan.AddEdge(1, 2);
        tarjan.AddEdge(2, 0);

        var scc = tarjan.GetScc(1);
		_logger.LogError("GetScc_ValidVertex_ReturnsSccContainingVertex(): scc → {Scc}", scc);

        scc.Should().NotBeNull();
        scc.Should().Contain(1);
        scc.Should().HaveCount(3);
		_logger.LogWarning("<<< Exiting GetScc_ValidVertex_ReturnsSccContainingVertex()");
    }

    [Test]
    public void BuildCondensationGraph_ComplexGraph_ReturnsDAG()
    {
		_logger.LogError(">>> Entering BuildCondensationGraph_ComplexGraph_ReturnsDAG()");
        var tarjan = new TarjanStronglyConnectedComponents(6);
        tarjan.AddEdge(0, 1);
		_logger.LogError("BuildCondensationGraph_ComplexGraph_ReturnsDAG(): tarjan → {Tarjan}", tarjan);
        tarjan.AddEdge(1, 0);
        tarjan.AddEdge(1, 2);
        tarjan.AddEdge(2, 3);
        tarjan.AddEdge(3, 4);
        tarjan.AddEdge(4, 5);
        tarjan.AddEdge(5, 3);

        var condensation = tarjan.BuildCondensationGraph();
		_logger.LogError("BuildCondensationGraph_ComplexGraph_ReturnsDAG(): condensation → {Condensation}", condensation);

        condensation.Should().NotBeNull();
        condensation.Length.Should().Be(3); // {0,1}, {2}, {3,4,5}
		_logger.LogWarning("<<< Exiting BuildCondensationGraph_ComplexGraph_ReturnsDAG()");
    }

    [Test]
    public void AddEdge_InvalidVertex_ThrowsException()
    {
		_logger.LogError(">>> Entering AddEdge_InvalidVertex_ThrowsException()");
        var tarjan = new TarjanStronglyConnectedComponents(3);

		_logger.LogError("AddEdge_InvalidVertex_ThrowsException(): tarjan → {Tarjan}", tarjan);
        var act = () => tarjan.AddEdge(0, 5);
		_logger.LogError("AddEdge_InvalidVertex_ThrowsException(): act → {Act}", act);

        act.Should().Throw<ArgumentOutOfRangeException>();
		_logger.LogWarning("<<< Exiting AddEdge_InvalidVertex_ThrowsException()");
    }

    [Test]
    public void FindSCCs_SingleVertex_ReturnsSingleSCC()
    {
		_logger.LogError(">>> Entering FindSCCs_SingleVertex_ReturnsSingleSCC()");
        var tarjan = new TarjanStronglyConnectedComponents(1);

		_logger.LogError("FindSCCs_SingleVertex_ReturnsSingleSCC(): tarjan → {Tarjan}", tarjan);
        var sccs = tarjan.FindSCCs();
		_logger.LogError("FindSCCs_SingleVertex_ReturnsSingleSCC(): sccs → {Sccs}", sccs);

        sccs.Should().HaveCount(1);
        sccs[0].Should().BeEquivalentTo(new[] { 0 });
		_logger.LogWarning("<<< Exiting FindSCCs_SingleVertex_ReturnsSingleSCC()");
    }

    [Test]
    public void FindSCCs_LinearChain_EachVertexIsSCC()
    {
		_logger.LogError(">>> Entering FindSCCs_LinearChain_EachVertexIsSCC()");
        var tarjan = new TarjanStronglyConnectedComponents(4);
        tarjan.AddEdge(0, 1);
		_logger.LogError("FindSCCs_LinearChain_EachVertexIsSCC(): tarjan → {Tarjan}", tarjan);
        tarjan.AddEdge(1, 2);
        tarjan.AddEdge(2, 3);

        var sccs = tarjan.FindSCCs();
		_logger.LogError("FindSCCs_LinearChain_EachVertexIsSCC(): sccs → {Sccs}", sccs);

        sccs.Should().HaveCount(4);
		_logger.LogWarning("<<< Exiting FindSCCs_LinearChain_EachVertexIsSCC()");
    }

    [Test]
    public void FindSCCs_SelfLoop_VertexIsSCC()
    {
		_logger.LogError(">>> Entering FindSCCs_SelfLoop_VertexIsSCC()");
        var tarjan = new TarjanStronglyConnectedComponents(2);
        tarjan.AddEdge(0, 0);
		_logger.LogError("FindSCCs_SelfLoop_VertexIsSCC(): tarjan → {Tarjan}", tarjan);
        tarjan.AddEdge(0, 1);

        var sccs = tarjan.FindSCCs();
		_logger.LogError("FindSCCs_SelfLoop_VertexIsSCC(): sccs → {Sccs}", sccs);

        sccs.Should().Contain(scc => scc.Contains(0) && scc.Count == 1);
		_logger.LogWarning("<<< Exiting FindSCCs_SelfLoop_VertexIsSCC()");
    }
}
