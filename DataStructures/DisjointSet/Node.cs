using Microsoft.Extensions.Logging;
namespace DataStructures.DisjointSet;

/// <summary>
/// node class to be used by disjoint set to represent nodes in Disjoint Set forest.
/// </summary>
/// <typeparam name="T">generic type for data to be stored.</typeparam>
public class Node<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    public int Rank { get; set; }

    public Node<T> Parent { get; set; }

    public T Data { get; set; }

    public Node(T data)
    {
		_logger.LogDebug(">>> Entering Node(data={Data})", data);
        Data = data;
		_logger.LogDebug("<<< Exiting Node(data={Data})", data);
        Parent = this;
		_logger.LogDebug("Node(data={Data}): Parent → {Parent}", data, Parent);
    }
}
