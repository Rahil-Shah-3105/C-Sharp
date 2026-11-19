using Algorithms.Graph.MinimumSpanningTree;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Graph.MinimumSpanningTree;

internal class KruskalTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void ValidateGraph_adjWrongSize_ThrowsException()
    {
		_logger.LogError(">>> Entering ValidateGraph_adjWrongSize_ThrowsException()");
        // Wrong number of columns
        var adj = new[,]
        {
            { 0, 3, 4, float.PositiveInfinity },
            { 3, 0, 5, 6 },
            { 4, 5, 0, float.PositiveInfinity },
            { float.PositiveInfinity, 6, float.PositiveInfinity, 0 },
            { float.PositiveInfinity, 2, float.PositiveInfinity, float.PositiveInfinity },
        };
        Assert.Throws<ArgumentException>(() => Kruskal.Solve(adj), "adj must be square!");

        // Wrong number of rows
        adj = new[,]
        {
            { 0, 3, 4, float.PositiveInfinity, float.PositiveInfinity },
            { 3, 0, 5, 6, 2 },
            { 4, 5, 0, float.PositiveInfinity, float.PositiveInfinity },
            { float.PositiveInfinity, 6, float.PositiveInfinity, 0, float.PositiveInfinity },
        };
        Assert.Throws<ArgumentException>(() => Kruskal.Solve(adj), "adj must be square!");
		_logger.LogWarning("<<< Exiting ValidateGraph_adjWrongSize_ThrowsException()");
    }

    [Test]
    public void ValidateGraph_adjDirectedGraph_ThrowsException()
    {
		_logger.LogError(">>> Entering ValidateGraph_adjDirectedGraph_ThrowsException()");
        // Nodes 1 and 2 have a directed edge
        var adj = new[,]
        {
            { 0, float.PositiveInfinity, 4, float.PositiveInfinity, float.PositiveInfinity },
            { 3, 0, 5, 6, 2 },
            { 4, 5, 0, float.PositiveInfinity, float.PositiveInfinity },
            { float.PositiveInfinity, 6, float.PositiveInfinity, 0, float.PositiveInfinity },
            { float.PositiveInfinity, 2, float.PositiveInfinity, float.PositiveInfinity, 0 },
        };
        Assert.Throws<ArgumentException>(() => Kruskal.Solve(adj), "adj must be symmetric!");
		_logger.LogWarning("<<< Exiting ValidateGraph_adjDirectedGraph_ThrowsException()");
    }

    [Test]
    public void Solve_adjGraph1_CorrectAnswer()
    {
		_logger.LogError(">>> Entering Solve_adjGraph1_CorrectAnswer()");
        /* Graph
         *       (1)
         *       / \
         *      3   2
         *     /     \
         *   (0)--2--(2)
         */
        var adj = new float[,]
        {
            { 0, 3, 2 },
            { 3, 0, 2 },
            { 2, 2, 0 },
        };

        /* Expected MST
         *      (1)
         *        \ 
         *         2
         *          \
         *  (0)--2--(2)
         */
        var expected = new[,]
        {
            { float.PositiveInfinity, float.PositiveInfinity, 2 },
            { float.PositiveInfinity, float.PositiveInfinity, 2 },
            { 2, 2, float.PositiveInfinity },
        };

        Kruskal.Solve(adj).Cast<float>().SequenceEqual(expected.Cast<float>()).Should().BeTrue();
		_logger.LogWarning("<<< Exiting Solve_adjGraph1_CorrectAnswer()");
    }

    [Test]
    public void Solve_adjGraph2_CorrectAnswer()
    {
		_logger.LogError(">>> Entering Solve_adjGraph2_CorrectAnswer()");
        /* Graph
         *  (0)     (4)
         *   |\     /
         *   | 3   2
         *   |  \ /
         *   4  (1)
         *   |  / \
         *   | 5   6
         *   |/     \
         *  (2)     (3)
         */
        var adj = new[,]
        {
            { 0, 3, 4, float.PositiveInfinity, float.PositiveInfinity },
            { 3, 0, 5, 6, 2 },
            { 4, 5, 0, float.PositiveInfinity, float.PositiveInfinity },
            { float.PositiveInfinity, 6, float.PositiveInfinity, 0, float.PositiveInfinity },
            { float.PositiveInfinity, 2, float.PositiveInfinity, float.PositiveInfinity, 0 },
        };

        /* Expected MST
         *  (0)     (4)
         *   |\     /
         *   | 3   2
         *   |  \ /
         *   4  (1)
         *   |    \
         *   |     6
         *   |      \
         *  (2)     (3)
         */
        var expected = new[,]
        {
            { float.PositiveInfinity, 3, 4, float.PositiveInfinity, float.PositiveInfinity },
            { 3, float.PositiveInfinity, float.PositiveInfinity, 6, 2 },
            { 4, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity },
            { float.PositiveInfinity, 6, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity },
            { float.PositiveInfinity, 2, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity },
        };

        Kruskal.Solve(adj).Cast<float>().SequenceEqual(expected.Cast<float>()).Should().BeTrue();
		_logger.LogWarning("<<< Exiting Solve_adjGraph2_CorrectAnswer()");
    }

    [Test]
    public void Solve_adjGraph3_CorrectAnswer()
    {
		_logger.LogError(">>> Entering Solve_adjGraph3_CorrectAnswer()");
        /* Graph
         *  (0)--3--(2)     (4)--2--(5)
         *    \     / \     /
         *     4   1   4   6
         *      \ /     \ /
         *      (1)--2--(3)
         */
        var adj = new[,]
        {
            { 0, 4, 3, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity },
            { 4, 0, 1, 2, float.PositiveInfinity, float.PositiveInfinity },
            { 3, 1, 0, 4, float.PositiveInfinity, float.PositiveInfinity },
            { float.PositiveInfinity, 2, 4, 0, 6, float.PositiveInfinity },
            { float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, 6, 0, 2 },
            { float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, 2, 0 },
        };

        /* Graph
         *  (0)--3--(2)     (4)--2--(5)
         *          /       /
         *         1       6
         *        /       /
         *      (1)--2--(3)
         */
        var expected = new[,]
        {
            { float.PositiveInfinity, float.PositiveInfinity, 3, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity },
            { float.PositiveInfinity, float.PositiveInfinity, 1, 2, float.PositiveInfinity, float.PositiveInfinity },
            { 3, 1, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity },
            { float.PositiveInfinity, 2, float.PositiveInfinity, float.PositiveInfinity, 6, float.PositiveInfinity },
            { float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, 6, float.PositiveInfinity, 2 },
            { float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, 2, float.PositiveInfinity },
        };

        Kruskal.Solve(adj).Cast<float>().SequenceEqual(expected.Cast<float>()).Should().BeTrue();
		_logger.LogWarning("<<< Exiting Solve_adjGraph3_CorrectAnswer()");
    }

    [Test]
    public void Solve_adjGraph4_CorrectAnswer()
    {
		_logger.LogError(">>> Entering Solve_adjGraph4_CorrectAnswer()");
        /* Graph
         *  (0)--7--(1)--8--(2)
         *    \     / \     /
         *     5   9   7   5
         *      \ /     \ /
         *      (3)--15-(4)
         *        \     / \
         *         6   8   9
         *          \ /     \
         *          (5)--11-(6)
         */
        var adj = new[,]
        {
            { 0, 7, float.PositiveInfinity, 5, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity },
            { 7, 0, 8, 9, 7, float.PositiveInfinity, float.PositiveInfinity },
            { float.PositiveInfinity, 8, 0, float.PositiveInfinity, 5, float.PositiveInfinity, float.PositiveInfinity },
            { 5, 9, float.PositiveInfinity, 0, 15, 6, float.PositiveInfinity },
            { float.PositiveInfinity, 7, 5, 15, 0, 8, 9 },
            { float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, 6, 8, 0, 11 },
            { float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, 9, 11, 0 },
        };

        /* Expected MST
         *  (0)--7--(1)     (2)
         *    \       \     /
         *     5       7   5
         *      \       \ /
         *      (3)     (4)
         *        \       \
         *         6       9
         *          \       \
         *          (5)     (6)
         */
        var expected = new[,]
        {
            { float.PositiveInfinity, 7, float.PositiveInfinity, 5, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity },
            { 7, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, 7, float.PositiveInfinity, float.PositiveInfinity },
            { float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, 5, float.PositiveInfinity, float.PositiveInfinity },
            { 5, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, 6, float.PositiveInfinity },
            { float.PositiveInfinity, 7, 5, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, 9 },
            { float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, 6, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity },
            { float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, 9, float.PositiveInfinity, float.PositiveInfinity },
        };

        Kruskal.Solve(adj).Cast<float>().SequenceEqual(expected.Cast<float>()).Should().BeTrue();
		_logger.LogWarning("<<< Exiting Solve_adjGraph4_CorrectAnswer()");
    }

    [Test]
    public void Solve_adjGraph5_CorrectAnswer()
    {
		_logger.LogError(">>> Entering Solve_adjGraph5_CorrectAnswer()");
        /* Graph
         *  (0)--8--(1)--15-(2)
         *   |\     /     __/|\
         *   | 4   5  __25  13 12 
         *   |  \ /__/       |   \
         *  10  (3)----14---(4)  (5)
         *   |  / \        _/|   /
         *   | 9   6   __16  18 30
         *   |/     \ /      |/
         *  (6)--18-(7)--20-(8)
         */
        var adj = new[,]
        {
            { 0, 8, float.PositiveInfinity, 4, float.PositiveInfinity, float.PositiveInfinity, 10, float.PositiveInfinity, float.PositiveInfinity },
            { 8, 0, 15, 5, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity },
            { float.PositiveInfinity, 15, 0, 25, 13, 12, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity },
            { 4, 5, 25, 0, 14, float.PositiveInfinity, 9, 6, float.PositiveInfinity },
            { float.PositiveInfinity, float.PositiveInfinity, 13, 14, 0, float.PositiveInfinity, float.PositiveInfinity, 16, 18 },
            { float.PositiveInfinity, float.PositiveInfinity, 12, float.PositiveInfinity, float.PositiveInfinity, 0, float.PositiveInfinity, float.PositiveInfinity, 30 },
            { 10, float.PositiveInfinity, float.PositiveInfinity, 9, float.PositiveInfinity, float.PositiveInfinity, 0, 18, float.PositiveInfinity },
            { float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, 6, 16, float.PositiveInfinity, 18, 0, 20 },
            { float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, 18, 30, float.PositiveInfinity, 20, 0 },
        };

        /* Expected MST
         *  (0)     (1)     (2)
         *    \     /        |\
         *     4   5        13 12 
         *      \ /          |   \
         *      (3)----14---(4)  (5)
         *      / \          |
         *     9   6         18
         *    /     \        |
         *  (6)     (7)     (8)
         */
        var expected = new[,]
        {
            {
                float.PositiveInfinity,
                float.PositiveInfinity,
                float.PositiveInfinity,
                4,
                float.PositiveInfinity,
                float.PositiveInfinity,
                float.PositiveInfinity,
                float.PositiveInfinity,
                float.PositiveInfinity,
            },
            {
                float.PositiveInfinity,
                float.PositiveInfinity,
                float.PositiveInfinity,
                5,
                float.PositiveInfinity,
                float.PositiveInfinity,
                float.PositiveInfinity,
                float.PositiveInfinity,
                float.PositiveInfinity,
            },
            {
                float.PositiveInfinity,
                float.PositiveInfinity,
                float.PositiveInfinity,
                float.PositiveInfinity,
                13,
                12,
                float.PositiveInfinity,
                float.PositiveInfinity,
                float.PositiveInfinity,
            },
            {
                4,
                5,
                float.PositiveInfinity,
                float.PositiveInfinity,
                14,
                float.PositiveInfinity,
                9,
                6,
                float.PositiveInfinity,
            },
            {
                float.PositiveInfinity,
                float.PositiveInfinity,
                13,
                14,
                float.PositiveInfinity,
                float.PositiveInfinity,
                float.PositiveInfinity,
                float.PositiveInfinity,
                18,
            },
            {
                float.PositiveInfinity,
                float.PositiveInfinity,
                12,
                float.PositiveInfinity,
                float.PositiveInfinity,
                float.PositiveInfinity,
                float.PositiveInfinity,
                float.PositiveInfinity,
                float.PositiveInfinity,
            },
            {
                float.PositiveInfinity,
                float.PositiveInfinity,
                float.PositiveInfinity,
                9,
                float.PositiveInfinity,
                float.PositiveInfinity,
                float.PositiveInfinity,
                float.PositiveInfinity,
                float.PositiveInfinity,
            },
            {
                float.PositiveInfinity,
                float.PositiveInfinity,
                float.PositiveInfinity,
                6,
                float.PositiveInfinity,
                float.PositiveInfinity,
                float.PositiveInfinity,
                float.PositiveInfinity,
                float.PositiveInfinity,
            },
            {
                float.PositiveInfinity,
                float.PositiveInfinity,
                float.PositiveInfinity,
                float.PositiveInfinity,
                18,
                float.PositiveInfinity,
                float.PositiveInfinity,
                float.PositiveInfinity,
                float.PositiveInfinity,
            },
        };

        Kruskal.Solve(adj).Cast<float>().SequenceEqual(expected.Cast<float>()).Should().BeTrue();
		_logger.LogWarning("<<< Exiting Solve_adjGraph5_CorrectAnswer()");
    }

    [Test]
    public void Solve_adjGraph6_CorrectAnswer()
    {
		_logger.LogError(">>> Entering Solve_adjGraph6_CorrectAnswer()");
        /* Graph
         *  (0)--7--(1)     (2)
         *    \     /       /|
         *     5   9       5 |
         *      \ /       /  |
         *      (3)     (4)  2
         *              / \  |
         *             8   9 |
         *            /     \|
         *          (5)--11-(6)
         */
        var adj = new[,]
        {
            { 0, 7, float.PositiveInfinity, 5, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity },
            { 7, 0, float.PositiveInfinity, 9, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity },
            { float.PositiveInfinity, float.PositiveInfinity, 0, float.PositiveInfinity, 5, float.PositiveInfinity, 2 },
            { 5, 9, float.PositiveInfinity, 0, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity },
            { float.PositiveInfinity, float.PositiveInfinity, 5, float.PositiveInfinity, 0, 8, 9 },
            { float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, 8, 0, 11 },
            { float.PositiveInfinity, float.PositiveInfinity, 2, float.PositiveInfinity, 9, 11, 0 },
        };

        /* Expected MST
         *  (0)--7--(1)     (2)
         *    \             /|
         *     5           5 |
         *      \         /  |
         *      (3)     (4)  2
         *              /    |
         *             8     |
         *            /      |
         *          (5)     (6)
         */
        var expected = new[,]
        {
            { float.PositiveInfinity, 7, float.PositiveInfinity, 5, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity },
            { 7, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity },
            { float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, 5, float.PositiveInfinity, 2 },
            { 5, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity },
            { float.PositiveInfinity, float.PositiveInfinity, 5, float.PositiveInfinity, float.PositiveInfinity, 8, float.PositiveInfinity },
            { float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, 8, float.PositiveInfinity, float.PositiveInfinity },
            { float.PositiveInfinity, float.PositiveInfinity, 2, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity },
        };

        Kruskal.Solve(adj).Cast<float>().SequenceEqual(expected.Cast<float>()).Should().BeTrue();
		_logger.LogWarning("<<< Exiting Solve_adjGraph6_CorrectAnswer()");
    }

    [Test]
    public void ValidateGraph_ListDirectedGraph_ThrowsException()
    {
		_logger.LogError(">>> Entering ValidateGraph_ListDirectedGraph_ThrowsException()");
        var adj = new[]
        {
            new Dictionary<int, float>{ { 2, 4 } },
            new Dictionary<int, float>{ { 0, 3 }, { 2, 5 }, { 3, 6 }, { 4, 2 } },
            new Dictionary<int, float>{ { 0, 4 }, { 1, 5 } },
            new Dictionary<int, float>{ { 1, 6 } },
            new Dictionary<int, float>{ { 1, 2 } },
        };
        Assert.Throws<ArgumentException>(() => Kruskal.Solve(adj), "Graph must be undirected!");
		_logger.LogWarning("<<< Exiting ValidateGraph_ListDirectedGraph_ThrowsException()");
    }

    [Test]
    public void Solve_ListGraph1_CorrectAnswer()
    {
		_logger.LogError(">>> Entering Solve_ListGraph1_CorrectAnswer()");
        /* Graph
         *       (1)
         *       / \
         *      3   2
         *     /     \
         *   (0)--2--(2)
         */
        var adj = new[]
        {
            new Dictionary<int, float>{ { 1, 3 }, { 2, 2 } },
            new Dictionary<int, float>{ { 0, 3 }, { 2, 2 } },
            new Dictionary<int, float>{ { 0, 2 }, { 1, 2 } },
        };

        /* Expected MST
         *      (1)
         *        \ 
         *         2
         *          \
         *  (0)--2--(2)
         */
        var expected = new[]
        {
            new Dictionary<int, float>{ { 2, 2 } },
            new Dictionary<int, float>{ { 2, 2 } },
            new Dictionary<int, float>{ { 0, 2 }, { 1, 2 } },
        };

        var res = Kruskal.Solve(adj);
		_logger.LogError("Solve_ListGraph1_CorrectAnswer(): res → {Res}", res);
        for (var i = 0; i < adj.Length; i++)
        {
            res[i].OrderBy(edge => edge.Key).SequenceEqual(expected[i]).Should().BeTrue();
        }
		_logger.LogWarning("<<< Exiting Solve_ListGraph1_CorrectAnswer()");
    }

    [Test]
    public void Solve_ListGraph2_CorrectAnswer()
    {
		_logger.LogError(">>> Entering Solve_ListGraph2_CorrectAnswer()");
        /* Graph
         *  (0)     (4)
         *   |\     /
         *   | 3   2
         *   |  \ /
         *   4  (1)
         *   |  / \
         *   | 5   6
         *   |/     \
         *  (2)     (3)
         */
        var adj = new[]
        {
            new Dictionary<int, float>{ { 1, 3 }, { 2, 4 } },
            new Dictionary<int, float>{ { 0, 3 }, { 2, 5 }, { 3, 6 }, { 4, 2 } },
            new Dictionary<int, float>{ { 0, 4 }, { 1, 5 } },
            new Dictionary<int, float>{ { 1, 6 } },
            new Dictionary<int, float>{ { 1, 2 } },
        };

        /* Expected MST
         *  (0)     (4)
         *   |\     /
         *   | 3   2
         *   |  \ /
         *   4  (1)
         *   |    \
         *   |     6
         *   |      \
         *  (2)     (3)
         */
        var expected = new[]
        {
            new Dictionary<int, float>{ { 1, 3 }, { 2, 4 } },
            new Dictionary<int, float>{ { 0, 3 }, { 3, 6 }, { 4, 2 } },
            new Dictionary<int, float>{ { 0, 4 } },
            new Dictionary<int, float>{ { 1, 6 } },
            new Dictionary<int, float>{ { 1, 2 } },
        };

        var res = Kruskal.Solve(adj);
		_logger.LogError("Solve_ListGraph2_CorrectAnswer(): res → {Res}", res);
        for (var i = 0; i < adj.Length; i++)
        {
            res[i].OrderBy(edge => edge.Key).SequenceEqual(expected[i]).Should().BeTrue();
        }
		_logger.LogWarning("<<< Exiting Solve_ListGraph2_CorrectAnswer()");
    }

    [Test]
    public void Solve_ListGraph3_CorrectAnswer()
    {
		_logger.LogError(">>> Entering Solve_ListGraph3_CorrectAnswer()");
        /* Graph
         *  (0)--3--(2)     (4)--2--(5)
         *    \     / \     /
         *     4   1   4   6
         *      \ /     \ /
         *      (1)--2--(3)
         */
        var adj = new[]
        {
            new Dictionary<int, float>{ { 1, 4 }, { 2, 3 } },
            new Dictionary<int, float>{ { 0, 4 }, { 2, 1 }, { 3, 2 } },
            new Dictionary<int, float>{ { 0, 3 }, { 1, 1 }, { 3, 4 } },
            new Dictionary<int, float>{ { 1, 2 }, { 2, 4 }, { 4, 6 } },
            new Dictionary<int, float>{ { 3, 6 }, { 5, 2 } },
            new Dictionary<int, float>{ { 4, 2 } },
        };

        /* Graph
         *  (0)--3--(2)     (4)--2--(5)
         *          /       /
         *         1       6
         *        /       /
         *      (1)--2--(3)
         */
        var expected = new[]
        {
            new Dictionary<int, float>{ { 2, 3 } },
            new Dictionary<int, float>{ { 2, 1 }, { 3, 2 } },
            new Dictionary<int, float>{ { 0, 3 }, { 1, 1 } },
            new Dictionary<int, float>{ { 1, 2 }, { 4, 6 } },
            new Dictionary<int, float>{ { 3, 6 }, { 5, 2 } },
            new Dictionary<int, float>{ { 4, 2 } },
        };

        var res = Kruskal.Solve(adj);
		_logger.LogError("Solve_ListGraph3_CorrectAnswer(): res → {Res}", res);
        for (var i = 0; i < adj.Length; i++)
        {
            res[i].OrderBy(edge => edge.Key).SequenceEqual(expected[i]).Should().BeTrue();
        }
		_logger.LogWarning("<<< Exiting Solve_ListGraph3_CorrectAnswer()");
    }

    [Test]
    public void Solve_ListGraph4_CorrectAnswer()
    {
		_logger.LogError(">>> Entering Solve_ListGraph4_CorrectAnswer()");
        /* Graph
         *  (0)--7--(1)--8--(2)
         *    \     / \     /
         *     5   9   7   5
         *      \ /     \ /
         *      (3)--15-(4)
         *        \     / \
         *         6   8   9
         *          \ /     \
         *          (5)--11-(6)
         */
        var adj = new[]
        {
            new Dictionary<int, float>{ { 1, 7 }, { 3, 5 } },
            new Dictionary<int, float>{ { 0, 7 }, { 2, 8 }, { 3, 9 }, { 4, 7 } },
            new Dictionary<int, float>{ { 1, 8 }, { 4, 5 } },
            new Dictionary<int, float>{ { 0, 5 }, { 1, 9 }, { 4, 15 }, { 5, 6 } },
            new Dictionary<int, float>{ { 1, 7 }, { 2, 5 }, { 3, 15 }, { 5, 8 }, { 6, 9 } },
            new Dictionary<int, float>{ { 3, 6 }, { 4, 8 }, { 6, 11 } },
            new Dictionary<int, float>{ { 4, 9 }, { 5, 11 } },
        };

        /* Expected MST
         *  (0)--7--(1)     (2)
         *    \       \     /
         *     5       7   5
         *      \       \ /
         *      (3)     (4)
         *        \       \
         *         6       9
         *          \       \
         *          (5)     (6)
         */
        var expected = new[]
        {
            new Dictionary<int, float>{ { 1, 7 }, { 3, 5 } },
            new Dictionary<int, float>{ { 0, 7 }, { 4, 7 } },
            new Dictionary<int, float>{ { 4, 5 } },
            new Dictionary<int, float>{ { 0, 5 }, { 5, 6 } },
            new Dictionary<int, float>{ { 1, 7 }, { 2, 5 }, { 6, 9 } },
            new Dictionary<int, float>{ { 3, 6 } },
            new Dictionary<int, float>{ { 4, 9 } },
        };

        var res = Kruskal.Solve(adj);
		_logger.LogError("Solve_ListGraph4_CorrectAnswer(): res → {Res}", res);
        for (var i = 0; i < adj.Length; i++)
        {
            res[i].OrderBy(edge => edge.Key).SequenceEqual(expected[i]).Should().BeTrue();
        }
		_logger.LogWarning("<<< Exiting Solve_ListGraph4_CorrectAnswer()");
    }

    [Test]
    public void Solve_ListGraph5_CorrectAnswer()
    {
		_logger.LogError(">>> Entering Solve_ListGraph5_CorrectAnswer()");
        /* Graph
         *  (0)--8--(1)--15-(2)
         *   |\     /     __/|\
         *   | 4   5  __25  13 12 
         *   |  \ /__/       |   \
         *  10  (3)----14---(4)  (5)
         *   |  / \        _/|   /
         *   | 9   6   __16  18 30
         *   |/     \ /      |/
         *  (6)--18-(7)--20-(8)
         */
        var adj = new[]
        {
            new Dictionary<int, float>{ { 1, 8 }, { 3, 4 }, { 6, 10 } },
            new Dictionary<int, float>{ { 0, 8 }, { 2, 15 }, { 3, 5 } },
            new Dictionary<int, float>{ { 1, 15 }, { 3, 25 }, { 4, 13 }, { 5, 12 } },
            new Dictionary<int, float>{ { 0, 4 }, { 1, 5 }, { 2, 25 }, { 4, 14 }, { 6, 9 }, { 7, 6 } },
            new Dictionary<int, float>{ { 2, 13 }, { 3, 14 }, { 7, 16 }, { 8, 18 } },
            new Dictionary<int, float>{ { 2, 12 }, { 8, 30 } },
            new Dictionary<int, float>{ { 0, 10 }, { 3, 9 }, { 7, 18 } },
            new Dictionary<int, float>{ { 3, 6 }, { 4, 16 }, { 6, 18 }, { 8, 20 } },
            new Dictionary<int, float>{ { 4, 18 }, { 5, 30 }, { 7, 20 } },
        };

        /* Expected MST
         *  (0)     (1)     (2)
         *    \     /        |\
         *     4   5        13 12 
         *      \ /          |   \
         *      (3)----14---(4)  (5)
         *      / \          |
         *     9   6         18
         *    /     \        |
         *  (6)     (7)     (8)
         */
        var expected = new[]
        {
            new Dictionary<int, float>{ { 3, 4 } },
            new Dictionary<int, float>{ { 3, 5 } },
            new Dictionary<int, float>{ { 4, 13 }, { 5, 12 } },
            new Dictionary<int, float>{ { 0, 4 }, { 1, 5 }, { 4, 14 }, { 6, 9 }, { 7, 6 } },
            new Dictionary<int, float>{ { 2, 13 }, { 3, 14 }, { 8, 18 } },
            new Dictionary<int, float>{ { 2, 12 } },
            new Dictionary<int, float>{ { 3, 9 } },
            new Dictionary<int, float>{ { 3, 6 } },
            new Dictionary<int, float>{ { 4, 18 } },
        };

        var res = Kruskal.Solve(adj);
		_logger.LogError("Solve_ListGraph5_CorrectAnswer(): res → {Res}", res);
        for (var i = 0; i < adj.Length; i++)
        {
            res[i].OrderBy(edge => edge.Key).SequenceEqual(expected[i]).Should().BeTrue();
        }
		_logger.LogWarning("<<< Exiting Solve_ListGraph5_CorrectAnswer()");
    }

    [Test]
    public void Solve_ListGraph6_CorrectAnswer()
    {
		_logger.LogError(">>> Entering Solve_ListGraph6_CorrectAnswer()");
        /* Graph
         *  (0)--7--(1)     (2)
         *    \     /       /|
         *     5   9       5 |
         *      \ /       /  |
         *      (3)     (4)  2
         *              / \  |
         *             8   9 |
         *            /     \|
         *          (5)--11-(6)
         */
        var adj = new[]
        {
            new Dictionary<int, float>{ { 1, 7 }, { 3, 5 } },
            new Dictionary<int, float>{ { 0, 7 }, { 3, 9 } },
            new Dictionary<int, float>{ { 4, 5 }, { 6, 2 } },
            new Dictionary<int, float>{ { 0, 5 }, { 1, 9 } },
            new Dictionary<int, float>{ { 2, 5 }, { 5, 8 }, { 6, 9 } },
            new Dictionary<int, float>{ { 4, 8 }, { 6, 11 } },
            new Dictionary<int, float>{ { 2, 2 }, { 4, 9 }, { 5, 11 } },
        };

        /* Expected MST
         *  (0)--7--(1)     (2)
         *    \             /|
         *     5           5 |
         *      \         /  |
         *      (3)     (4)  2
         *              /    |
         *             8     |
         *            /      |
         *          (5)     (6)
         */
        var expected = new[]
        {
            new Dictionary<int, float>{ { 1, 7 }, { 3, 5 } },
            new Dictionary<int, float>{ { 0, 7 } },
            new Dictionary<int, float>{ { 4, 5 }, { 6, 2 } },
            new Dictionary<int, float>{ { 0, 5 } },
            new Dictionary<int, float>{ { 2, 5 }, { 5, 8 } },
            new Dictionary<int, float>{ { 4, 8 } },
            new Dictionary<int, float>{ { 2, 2 } },
        };

        var res = Kruskal.Solve(adj);
		_logger.LogError("Solve_ListGraph6_CorrectAnswer(): res → {Res}", res);
        for (var i = 0; i < adj.Length; i++)
        {
            res[i].OrderBy(edge => edge.Key).SequenceEqual(expected[i]).Should().BeTrue();
        }
		_logger.LogWarning("<<< Exiting Solve_ListGraph6_CorrectAnswer()");
    }
}
