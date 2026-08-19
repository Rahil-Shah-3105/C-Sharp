using Algorithms.Problems.KnightTour;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Problems.KnightTour
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestFixture]
    public sealed class OpenKnightTourTests
    {
        private static bool IsKnightMove((int r, int c) a, (int r, int c) b)
        {
			_logger.LogDebug(">>> Entering IsKnightMove(a={A},b={B})", a, b);
            var dr = Math.Abs(a.r - b.r);
            var dc = Math.Abs(a.c - b.c);
			_logger.LogDebug("IsKnightMove(a={A},b={B}): dc → {Dc}", a, b, dc);
			_logger.LogDebug("IsKnightMove(a={A},b={B}): dr → {Dr}", a, b, dr);
			_logger.LogDebug("<<< Exiting IsKnightMove(a={A},b={B})", a, b);
            return (dr == 1 && dc == 2) || (dr == 2 && dc == 1);
        }

        private static Dictionary<int, (int r, int c)> MapVisitOrder(int[,] board)
        {
            var n = board.GetLength(0);
            var map = new Dictionary<int, (int r, int c)>(n * n);
            for (var r = 0; r < n; r++)
            {
                for (var c = 0; c < n; c++)
                {
                    var v = board[r, c];
                    if (v <= 0)
                    {
                        continue;
                    }
                    // ignore zeros in partial/invalid boards
                    if (!map.TryAdd(v, (r, c)))
                    {
                        throw new AssertionException($"Duplicate visit number detected: {v}.");
                    }
                }
            }
            return map;
        }

        private static void AssertIsValidTour(int[,] board)
        {
			_logger.LogDebug(">>> Entering AssertIsValidTour(board={Board})", board);
            var n = board.GetLength(0);
            Assert.That(board.GetLength(1), Is.EqualTo(n), "Board must be square.");
			_logger.LogDebug("AssertIsValidTour(board={Board}): n → {N}", board, n);

            // 1) All cells visited and within [1..n*n]
            int min = int.MaxValue;
			_logger.LogDebug("AssertIsValidTour(board={Board}): min → {Min}", board, min);
            int max = int.MinValue;
			_logger.LogDebug("AssertIsValidTour(board={Board}): max → {Max}", board, max);
            
            var seen = new bool[n * n + 1]; // 1..n*n
            for (var r = 0; r < n; r++)
            {
                for (var c = 0; c < n; c++)
                {
                    var v = board[r, c];
                    Assert.That(v, Is.InRange(1, n * n),
                        $"Cell [{r},{c}] has out-of-range value {v}.");
                    Assert.That(seen[v], Is.False, $"Duplicate value {v} found.");
                    seen[v] = true;
                    if (v < min)
                    {
                        min = v;
                    }

                    if (v > max)
                    {
                        max = v;
                    }
                }
            }
            Assert.That(min, Is.EqualTo(1), "Tour must start at 1.");
            Assert.That(max, Is.EqualTo(n * n), "Tour must end at n*n.");

            // 2) Each successive step is a legal knight move
            var pos = MapVisitOrder(board); // throws if duplicates
			_logger.LogDebug("AssertIsValidTour(board={Board}): pos → {Pos}", board, pos);
            for (var step = 1; step < n * n; step++)
            {
                var a = pos[step];
                var b = pos[step + 1];
                Assert.That(IsKnightMove(a, b),
                    $"Step {step}->{step + 1} is not a legal knight move: {a} -> {b}.");
            }
			_logger.LogDebug("<<< Exiting AssertIsValidTour(board={Board})", board);
        }

        [Test]
        public void Tour_Throws_On_NonPositiveN()
        {
			_logger.LogDebug(">>> Entering Tour_Throws_On_NonPositiveN()");
            var solver = new OpenKnightTour();

			_logger.LogDebug("Tour_Throws_On_NonPositiveN(): solver → {Solver}", solver);
            Assert.Throws<ArgumentException>(() => solver.Tour(0));
            Assert.Throws<ArgumentException>(() => solver.Tour(-1));
            Assert.Throws<ArgumentException>(() => solver.Tour(-5));
			_logger.LogDebug("<<< Exiting Tour_Throws_On_NonPositiveN()");
        }

        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void Tour_Throws_On_Unsolvable_N_2_3_4(int n)
        {
			_logger.LogDebug(">>> Entering Tour_Throws_On_Unsolvable_N_2_3_4(n={N})", n);
            var solver = new OpenKnightTour();
            Assert.Throws<ArgumentException>(() => solver.Tour(n));
			_logger.LogDebug("Tour_Throws_On_Unsolvable_N_2_3_4(n={N}): solver → {Solver}", n, solver);
			_logger.LogDebug("<<< Exiting Tour_Throws_On_Unsolvable_N_2_3_4(n={N})", n);
        }

        [Test]
        public void Tour_Returns_Valid_1x1()
        {
			_logger.LogDebug(">>> Entering Tour_Returns_Valid_1x1()");
            var solver = new OpenKnightTour();
            var board = solver.Tour(1);
			_logger.LogDebug("Tour_Returns_Valid_1x1(): board → {Board}", board);
			_logger.LogDebug("Tour_Returns_Valid_1x1(): solver → {Solver}", solver);

            Assert.That(board.GetLength(0), Is.EqualTo(1));
            Assert.That(board.GetLength(1), Is.EqualTo(1));
            Assert.That(board[0, 0], Is.EqualTo(1));
            AssertIsValidTour(board);
			_logger.LogDebug("<<< Exiting Tour_Returns_Valid_1x1()");
        }

        /// <summary>
        /// The plain backtracking search can take some time on 5x5 depending on move ordering,
        /// but should still be manageable. We mark it as "Slow" and add a generous timeout.
        /// </summary>
        [Test, Category("Slow"), CancelAfterAttribute(30000)]
        public void Tour_Returns_Valid_5x5()
        {
			_logger.LogDebug(">>> Entering Tour_Returns_Valid_5x5()");
            var solver = new OpenKnightTour();
            var board = solver.Tour(5);
			_logger.LogDebug("Tour_Returns_Valid_5x5(): board → {Board}", board);
			_logger.LogDebug("Tour_Returns_Valid_5x5(): solver → {Solver}", solver);

            // Shape checks
            Assert.That(board.GetLength(0), Is.EqualTo(5));
            Assert.That(board.GetLength(1), Is.EqualTo(5));

            // Structural validity checks
            AssertIsValidTour(board);
			_logger.LogDebug("<<< Exiting Tour_Returns_Valid_5x5()");
        }

        [Test]
        public void Tour_Fills_All_Cells_No_Zeros_On_Successful_Boards()
        {
			_logger.LogDebug(">>> Entering Tour_Fills_All_Cells_No_Zeros_On_Successful_Boards()");
            var solver = new OpenKnightTour();
            var board = solver.Tour(5);
			_logger.LogDebug("Tour_Fills_All_Cells_No_Zeros_On_Successful_Boards(): board → {Board}", board);
			_logger.LogDebug("Tour_Fills_All_Cells_No_Zeros_On_Successful_Boards(): solver → {Solver}", solver);
			_logger.LogDebug("Tour_Fills_All_Cells_No_Zeros_On_Successful_Boards(): r → {R}", r);

            for (var r = 0; r < board.GetLength(0); r++)
            {
                for (var c = 0; c < board.GetLength(1); c++)
				_logger.LogDebug("Tour_Fills_All_Cells_No_Zeros_On_Successful_Boards(): c → {C}", c);
                {
                    Assert.That(board[r, c], Is.Not.EqualTo(0),
                        $"Found unvisited cell at [{r},{c}].");
                }
            }
			_logger.LogDebug("<<< Exiting Tour_Fills_All_Cells_No_Zeros_On_Successful_Boards()");
        }

        [Test]
        public void Tour_Produces_Values_In_Valid_Range_And_Unique()
        {
			_logger.LogDebug(">>> Entering Tour_Produces_Values_In_Valid_Range_And_Unique()");
            var solver = new OpenKnightTour();
            var n = 5;
			_logger.LogDebug("Tour_Produces_Values_In_Valid_Range_And_Unique(): solver → {Solver}", solver);
            var board = solver.Tour(n);
			_logger.LogDebug("Tour_Produces_Values_In_Valid_Range_And_Unique(): board → {Board}", board);

            var values = new List<int>(n * n);
			_logger.LogDebug("Tour_Produces_Values_In_Valid_Range_And_Unique(): values → {Values}", values);
            for (var r = 0; r < n; r++)
            {
                for (var c = 0; c < n; c++)
                {
                    values.Add(board[r, c]);
                }
            }

            values.Sort();
            // Expect [1..n*n]
            var expected = Enumerable.Range(1, n * n).ToArray();
			_logger.LogDebug("Tour_Produces_Values_In_Valid_Range_And_Unique(): expected → {Expected}", expected);
            Assert.That(values, Is.EqualTo(expected),
                "Board must contain each number exactly once from 1 to n*n.");
				_logger.LogDebug("<<< Exiting Tour_Produces_Values_In_Valid_Range_And_Unique()");
        }

        [Test]
        public void Tour_Returns_Square_Array()
        {
			_logger.LogDebug(">>> Entering Tour_Returns_Square_Array()");
            var solver = new OpenKnightTour();
            var board = solver.Tour(5);
			_logger.LogDebug("Tour_Returns_Square_Array(): board → {Board}", board);
			_logger.LogDebug("Tour_Returns_Square_Array(): solver → {Solver}", solver);

            Assert.That(board.GetLength(0), Is.EqualTo(board.GetLength(1)));
			_logger.LogDebug("<<< Exiting Tour_Returns_Square_Array()");
        }
    }
}
