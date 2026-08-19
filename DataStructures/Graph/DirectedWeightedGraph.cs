using Microsoft.Extensions.Logging;
namespace DataStructures.Graph;

/// <summary>
///     Implementation of the directed weighted graph via adjacency matrix.
/// </summary>
/// <typeparam name="T">Generic Type.</typeparam>
public class DirectedWeightedGraph<T> : IDirectedWeightedGraph<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Capacity of the graph, indicates the maximum amount of vertices.
    /// </summary>
    private readonly int capacity;

    /// <summary>
    ///     Adjacency matrix which reflects the edges between vertices and their weight.
    ///     Zero value indicates no edge between two vertices.
    /// </summary>
    private readonly double[,] adjacencyMatrix;

    /// <summary>
    ///     Initializes a new instance of the <see cref="DirectedWeightedGraph{T}"/> class.
    /// </summary>
    /// <param name="capacity">Capacity of the graph, indicates the maximum amount of vertices.</param>
    public DirectedWeightedGraph(int capacity)
    {
		_logger.LogDebug(">>> Entering DirectedWeightedGraph(capacity={Capacity})", capacity);
        ThrowIfNegativeCapacity(capacity);

        this.capacity = capacity;
		_logger.LogDebug("DirectedWeightedGraph(capacity={Capacity}): capacity → {Capacity1}", capacity, capacity);
		_logger.LogDebug("DirectedWeightedGraph(capacity={Capacity}): this.capacity → {Arg1}", capacity, this.capacity);
        Vertices = new Vertex<T>[capacity];
        adjacencyMatrix = new double[capacity, capacity];
		_logger.LogDebug("DirectedWeightedGraph(capacity={Capacity}): Vertices → {Vertices}", capacity, Vertices);
		_logger.LogDebug("DirectedWeightedGraph(capacity={Capacity}): adjacencyMatrix → {AdjacencyMatrix}", capacity, adjacencyMatrix);
        Count = 0;
		_logger.LogDebug("DirectedWeightedGraph(capacity={Capacity}): Count → {Count}", capacity, Count);
		_logger.LogDebug("<<< Exiting DirectedWeightedGraph(capacity={Capacity})", capacity);
    }

    /// <summary>
    ///     Gets list of vertices of the graph.
    /// </summary>
    public Vertex<T>?[] Vertices { get; private set; }

    /// <summary>
    ///     Gets current amount of vertices in the graph.
    /// </summary>
    public int Count { get; private set; }

    /// <summary>
    ///     Adds new vertex to the graph.
    /// </summary>
    /// <param name="data">Data of the vertex.</param>
    /// <returns>Reference to created vertex.</returns>
    public Vertex<T> AddVertex(T data)
    {
		_logger.LogDebug(">>> Entering AddVertex(data={Data})", data);
        ThrowIfOverflow();
        var vertex = new Vertex<T>(data, Count, this);
		_logger.LogDebug("AddVertex(data={Data}): vertex → {Vertex}", data, vertex);
        Vertices[Count] = vertex;
        Count++;
		_logger.LogDebug("<<< Exiting AddVertex(data={Data})", data);
        return vertex;
    }

    /// <summary>
    ///     Creates an edge between two vertices of the graph.
    /// </summary>
    /// <param name="startVertex">Vertex, edge starts at.</param>
    /// <param name="endVertex">Vertex, edge ends at.</param>
    /// <param name="weight">Double weight of an edge.</param>
    public void AddEdge(Vertex<T> startVertex, Vertex<T> endVertex, double weight)
    {
		_logger.LogDebug(">>> Entering AddEdge(startVertex={StartVertex},endVertex={EndVertex},weight={Weight})", startVertex, endVertex, weight);
        ThrowIfVertexNotInGraph(startVertex);
        ThrowIfVertexNotInGraph(endVertex);

        ThrowIfWeightZero(weight);

        var currentEdgeWeight = adjacencyMatrix[startVertex.Index, endVertex.Index];

        ThrowIfEdgeExists(currentEdgeWeight);

        adjacencyMatrix[startVertex.Index, endVertex.Index] = weight;
		_logger.LogDebug("<<< Exiting AddEdge(startVertex={StartVertex},endVertex={EndVertex},weight={Weight})", startVertex, endVertex, weight);
    }

    /// <summary>
    ///     Removes vertex from the graph.
    /// </summary>
    /// <param name="vertex">Vertex to be removed.</param>
    public void RemoveVertex(Vertex<T> vertex)
    {
		_logger.LogDebug(">>> Entering RemoveVertex(vertex={Vertex})", vertex);
        ThrowIfVertexNotInGraph(vertex);

        int indexToRemove = vertex.Index;
        vertex.Index = -1;
		_logger.LogDebug("RemoveVertex(vertex={Vertex}): vertex.Index → {Arg1}", vertex, vertex.Index);
        vertex.SetGraphNull();

        // Update the vertex array and the index of vertices.
        for (int i = indexToRemove; i < Count - 1; i++)
        {
            Vertices[i] = Vertices[i + 1];
            Vertices[i]!.Index = i;
        }

        Vertices[Count - 1] = null;

        // Update adjacency matrix to remove the row and column of the removed vertex.
        for (int i = 0; i < Count; i++)
        {
            for (int j = 0; j < Count; j++)
            {
                if (i < indexToRemove && j < indexToRemove)
                {
                    continue;
                }
                else if (i < indexToRemove && j >= indexToRemove && j < Count - 1)
                {
                    adjacencyMatrix[i, j] = adjacencyMatrix[i, j + 1];
                }
                else if (i >= indexToRemove && i < Count - 1 && j < indexToRemove)
                {
                    adjacencyMatrix[i, j] = adjacencyMatrix[i + 1, j];
                }
                else if (i >= indexToRemove && i < Count - 1 && j >= indexToRemove && j < Count - 1)
                {
                    adjacencyMatrix[i, j] = adjacencyMatrix[i + 1, j + 1];
                }
                else if (i == Count - 1 || j == Count - 1)
                {
                    adjacencyMatrix[i, j] = 0;
                }
                else
                {
					_logger.LogWarning("Throwing InvalidOperationException from RemoveVertex(vertex={Vertex})", vertex);
                    throw new InvalidOperationException();
                }
            }
        }

        Count--;
		_logger.LogDebug("<<< Exiting RemoveVertex(vertex={Vertex})", vertex);
    }

    /// <summary>
    ///     Removes edge between two vertices.
    /// </summary>
    /// <param name="startVertex">Vertex, edge starts at.</param>
    /// <param name="endVertex">Vertex, edge ends at.</param>
    public void RemoveEdge(Vertex<T> startVertex, Vertex<T> endVertex)
    {
		_logger.LogDebug(">>> Entering RemoveEdge(startVertex={StartVertex},endVertex={EndVertex})", startVertex, endVertex);
        ThrowIfVertexNotInGraph(startVertex);
        ThrowIfVertexNotInGraph(endVertex);
        adjacencyMatrix[startVertex.Index, endVertex.Index] = 0;
		_logger.LogDebug("<<< Exiting RemoveEdge(startVertex={StartVertex},endVertex={EndVertex})", startVertex, endVertex);
    }

    /// <summary>
    ///     Gets a neighbors of particular vertex.
    /// </summary>
    /// <param name="vertex">Vertex, method gets list of neighbors for.</param>
    /// <returns>Collection of the neighbors of particular vertex.</returns>
    public IEnumerable<Vertex<T>?> GetNeighbors(Vertex<T> vertex)
    {
		_logger.LogDebug(">>> Entering GetNeighbors(vertex={Vertex})", vertex);
        ThrowIfVertexNotInGraph(vertex);

        for (var i = 0; i < Count; i++)
        {
            if (adjacencyMatrix[vertex.Index, i] != 0)
            {
                yield return Vertices[i];
            }
        }
		_logger.LogDebug("<<< Exiting GetNeighbors(vertex={Vertex})", vertex);
    }

    /// <summary>
    ///     Returns true, if there is an edge between two vertices.
    /// </summary>
    /// <param name="startVertex">Vertex, edge starts at.</param>
    /// <param name="endVertex">Vertex, edge ends at.</param>
    /// <returns>True if edge exists, otherwise false.</returns>
    public bool AreAdjacent(Vertex<T> startVertex, Vertex<T> endVertex)
    {
		_logger.LogDebug(">>> Entering AreAdjacent(startVertex={StartVertex},endVertex={EndVertex})", startVertex, endVertex);
        ThrowIfVertexNotInGraph(startVertex);
        ThrowIfVertexNotInGraph(endVertex);

		_logger.LogDebug("<<< Exiting AreAdjacent(startVertex={StartVertex},endVertex={EndVertex})", startVertex, endVertex);
        return adjacencyMatrix[startVertex.Index, endVertex.Index] != 0;
    }

    /// <summary>
    /// Return the distance between two vertices in the graph.
    /// </summary>
    /// <param name="startVertex">first vertex in edge.</param>
    /// <param name="endVertex">secnod vertex in edge.</param>
    /// <returns>distance between the two.</returns>
    public double AdjacentDistance(Vertex<T> startVertex, Vertex<T> endVertex)
    {
		_logger.LogDebug(">>> Entering AdjacentDistance(startVertex={StartVertex},endVertex={EndVertex})", startVertex, endVertex);
        if (AreAdjacent(startVertex, endVertex))
        {
		_logger.LogDebug("<<< Exiting AdjacentDistance(startVertex={StartVertex},endVertex={EndVertex})", startVertex, endVertex);
            return adjacencyMatrix[startVertex.Index, endVertex.Index];
        }

        return 0;
    }

    private static void ThrowIfNegativeCapacity(int capacity)
    {
		_logger.LogDebug(">>> Entering ThrowIfNegativeCapacity(capacity={Capacity})", capacity);
        if (capacity < 0)
        {
			_logger.LogWarning("Throwing InvalidOperationException from ThrowIfNegativeCapacity(capacity={Capacity})", capacity);
		_logger.LogDebug("<<< Exiting ThrowIfNegativeCapacity(capacity={Capacity})", capacity);
            throw new InvalidOperationException("Graph capacity should always be a non-negative integer.");
        }
    }

    private static void ThrowIfWeightZero(double weight)
    {
		_logger.LogDebug(">>> Entering ThrowIfWeightZero(weight={Weight})", weight);
        if (weight.Equals(0.0d))
        {
			_logger.LogWarning("Throwing InvalidOperationException from ThrowIfWeightZero(weight={Weight})", weight);
		_logger.LogDebug("<<< Exiting ThrowIfWeightZero(weight={Weight})", weight);
            throw new InvalidOperationException("Edge weight cannot be zero.");
        }
    }

    private static void ThrowIfEdgeExists(double currentEdgeWeight)
    {
		_logger.LogDebug(">>> Entering ThrowIfEdgeExists(currentEdgeWeight={CurrentEdgeWeight})", currentEdgeWeight);
        if (!currentEdgeWeight.Equals(0.0d))
        {
			_logger.LogWarning("Throwing InvalidOperationException from ThrowIfEdgeExists(currentEdgeWeight={CurrentEdgeWeight})", currentEdgeWeight);
		_logger.LogDebug("<<< Exiting ThrowIfEdgeExists(currentEdgeWeight={CurrentEdgeWeight})", currentEdgeWeight);
            throw new InvalidOperationException($"Vertex already exists: {currentEdgeWeight}");
        }
    }

    private void ThrowIfOverflow()
    {
		_logger.LogDebug(">>> Entering ThrowIfOverflow()");
        if (Count == capacity)
        {
			_logger.LogWarning("Throwing InvalidOperationException from ThrowIfOverflow()");
		_logger.LogDebug("<<< Exiting ThrowIfOverflow()");
            throw new InvalidOperationException("Graph overflow.");
        }
    }

    private void ThrowIfVertexNotInGraph(Vertex<T> vertex)
    {
		_logger.LogDebug(">>> Entering ThrowIfVertexNotInGraph(vertex={Vertex})", vertex);
        if (vertex.Graph != this)
        {
			_logger.LogWarning("Throwing InvalidOperationException from ThrowIfVertexNotInGraph(vertex={Vertex})", vertex);
		_logger.LogDebug("<<< Exiting ThrowIfVertexNotInGraph(vertex={Vertex})", vertex);
            throw new InvalidOperationException($"Vertex does not belong to graph: {vertex}.");
        }
    }
}
