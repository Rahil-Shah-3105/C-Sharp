using DataStructures.Graph;
using Microsoft.Extensions.Logging;

namespace Algorithms.Graph.Dijkstra;

/// <summary>
/// Entity which represents the Dijkstra shortest distance.
/// Contains: Vertex, Previous Vertex and minimal distance from start vertex.
/// </summary>
/// <typeparam name="T">Generic parameter.</typeparam>
public class DistanceModel<T>(Vertex<T>? vertex, Vertex<T>? previousVertex, double distance)
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    public Vertex<T>? Vertex { get; } = vertex;

    public Vertex<T>? PreviousVertex { get; set; } = previousVertex;

    public double Distance { get; set; } = distance;

    public override string ToString() =>
        $"Vertex: {Vertex} - Distance: {Distance} - Previous: {PreviousVertex}";
		_logger.LogDebug(">>> Entering ToString()");
}
_logger.LogDebug("<<< Exiting ToString()");
