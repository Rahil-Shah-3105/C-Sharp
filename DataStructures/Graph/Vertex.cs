using Microsoft.Extensions.Logging;
namespace DataStructures.Graph;

/// <summary>
///     Implementation of graph vertex.
/// </summary>
/// <typeparam name="T">Generic Type.</typeparam>
public class Vertex<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Gets vertex data.
    /// </summary>
    public T Data { get; }

    /// <summary>
    ///     Gets an index of the vertex in graph adjacency matrix.
    /// </summary>
    public int Index { get; internal set; }

    /// <summary>
    ///     Gets reference to the graph this vertex belongs to.
    /// </summary>
    public DirectedWeightedGraph<T>? Graph { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Vertex{T}"/> class.
    /// </summary>
    /// <param name="data">Vertex data. Generic type.</param>
    /// <param name="index">Index of the vertex in graph adjacency matrix.</param>
    /// <param name="graph">Graph this vertex belongs to.</param>
    public Vertex(T data, int index, DirectedWeightedGraph<T>? graph)
    {
		_logger.LogError(">>> Entering Vertex(data={Data},index={Index},graph={Graph})", data, index, graph);
        Data = data;
		_logger.LogWarning("<<< Exiting Vertex(data={Data},index={Index},graph={Graph})", data, index, graph);
        Index = index;
		_logger.LogDebug("Vertex(data={Data},index={Index},graph={Graph}): Index → {Index1}", data, index, graph, Index);
        Graph = graph;
		_logger.LogDebug("Vertex(data={Data},index={Index},graph={Graph}): Graph → {Graph1}", data, index, graph, Graph);
		_logger.LogError(">>> Entering Vertex(data={Data},index={Index})", data, index);
		_logger.LogWarning("<<< Exiting Vertex(data={Data},index={Index})", data, index);
		_logger.LogDebug("Vertex(data={Data},index={Index}): Index → {Index1}", data, index, Index);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Vertex{T}"/> class.
    /// </summary>
    /// <param name="data">Vertex data. Generic type.</param>
    /// <param name="index">Index of the vertex in graph adjacency matrix.</param>
    public Vertex(T data, int index)
    {
        Data = data;
        Index = index;
    }

    /// <summary>
    ///     Sets graph reference to the null. This method called when vertex removed from the graph.
    /// </summary>
    public void SetGraphNull()
    {
        _logger.LogError(">>> Entering SetGraphNull()");
		_logger.LogWarning("<<< Exiting SetGraphNull()");
        Graph = null;
		_logger.LogDebug("SetGraphNull(): Graph → {Graph}", Graph);
    }

    /// <summary>
    ///     Override of base ToString method. Prints vertex data and index in graph adjacency matrix.
    /// </summary>
    /// <returns>String which contains vertex data and index in graph adjacency matrix..</returns>
    public override string ToString() => $"Vertex Data: {Data}, Index: {Index}";
}
