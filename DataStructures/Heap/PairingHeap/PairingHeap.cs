using System.Collections;
using Microsoft.Extensions.Logging;

namespace DataStructures.Heap.PairingHeap;

/// <summary>
/// A pairing minMax heap implementation.
/// </summary>
/// <typeparam name="T">Base type.</typeparam>
public class PairingHeap<T>(Sorting sortDirection = Sorting.Ascending) : IEnumerable<T> where T : IComparable
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly Sorting sorting = sortDirection;
    private readonly IComparer<T> comparer = new PairingNodeComparer<T>(sortDirection, Comparer<T>.Default);
    private readonly Dictionary<T, List<PairingHeapNode<T>>> mapping = [];

    private PairingHeapNode<T> root = null!;

    public int Count { get; private set; }

    /// <summary>
    /// Insert a new Node [O(1)].
    /// </summary>
    public void Insert(T newItem)
    {
		_logger.LogDebug(">>> Entering Insert(newItem={NewItem})", newItem);
        var newNode = new PairingHeapNode<T>(newItem);

		_logger.LogDebug("Insert(newItem={NewItem}): newNode → {NewNode}", newItem, newNode);
        root = RebuildHeap(root, newNode);
        Map(newItem, newNode);
		_logger.LogWarning("Insert(newItem={NewItem}): root → {Root}", newItem, root);

        Count++;
		_logger.LogDebug("<<< Exiting Insert(newItem={NewItem})", newItem);
    }

    /// <summary>
    /// Get the element from heap [O(log(n))].
    /// </summary>
    public T Extract()
    {
		_logger.LogDebug(">>> Entering Extract()");
        var minMax = root;

        RemoveMapping(minMax.Value, minMax);
        RebuildHeap(root.ChildrenHead);

        Count--;
		_logger.LogDebug("<<< Exiting Extract()");
        return minMax.Value;
    }

    /// <summary>
    /// Update heap key [O(log(n))].
    /// </summary>
    public void UpdateKey(T currentValue, T newValue)
    {
		_logger.LogDebug(">>> Entering UpdateKey(currentValue={CurrentValue},newValue={NewValue})", currentValue, newValue);
        if (!mapping.ContainsKey(currentValue))
        {
			_logger.LogWarning("Throwing ArgumentException from UpdateKey(currentValue={CurrentValue},newValue={NewValue})", currentValue, newValue);
            throw new ArgumentException("Current value is not present in this heap.");
        }

        var node = mapping[currentValue]?.Where(x => x.Value.Equals(currentValue)).FirstOrDefault();
		_logger.LogDebug("UpdateKey(currentValue={CurrentValue},newValue={NewValue}): node → {Node}", currentValue, newValue, node);

        if (comparer.Compare(newValue, node!.Value) > 0)
        {
            throw new ArgumentException($"New value is not {(sorting != Sorting.Descending ? "less" : "greater")} than old value.");
        }

        UpdateNodeValue(currentValue, newValue, node);

        if (node == root)
        {
            return;
        }

        DeleteChild(node);

        root = RebuildHeap(root, node);
		_logger.LogWarning("UpdateKey(currentValue={CurrentValue},newValue={NewValue}): root → {Root}", currentValue, newValue, root);
		_logger.LogDebug("<<< Exiting UpdateKey(currentValue={CurrentValue},newValue={NewValue})", currentValue, newValue);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
		_logger.LogDebug(">>> Entering GetEnumerator()");
		_logger.LogDebug("<<< Exiting GetEnumerator()");
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return mapping.SelectMany(x => x.Value).Select(x => x.Value).GetEnumerator();
    }

    /// <summary>
    /// Rebuild heap on action [O(log(n))].
    /// </summary>
    private void RebuildHeap(PairingHeapNode<T> headNode)
    {
		_logger.LogDebug(">>> Entering RebuildHeap(headNode={HeadNode})", headNode);
        if (headNode == null)
        {
            return;
        }

        var passOneResult = new List<PairingHeapNode<T>>();
		_logger.LogDebug("RebuildHeap(headNode={HeadNode}): passOneResult → {PassOneResult}", headNode, passOneResult);
        var current = headNode;
		_logger.LogDebug("RebuildHeap(headNode={HeadNode}): current → {Current}", headNode, current);

        if (current.Next == null)
        {
            headNode.Next = null!;
			_logger.LogDebug("RebuildHeap(headNode={HeadNode}): headNode.Next → {Arg1}", headNode, headNode.Next);
			_logger.LogDebug("RebuildHeap(headNode={HeadNode}): headNode.Previous → {Arg1}", headNode, headNode.Previous);
            headNode.Previous = null!;
            passOneResult.Add(headNode);
        }
        else
        {
            while (true)
            {
                if (current == null)
                {
                    break;
                }

                if (current.Next != null)
                {
                    var next = current.Next;
                    var nextNext = next.Next;
                    passOneResult.Add(RebuildHeap(current, next));
                    current = nextNext;
                }
                else
                {
                    var lastInserted = passOneResult[^1];
                    passOneResult[^1] = RebuildHeap(lastInserted, current);
                    break;
                }
            }
        }

        var passTwoResult = passOneResult[^1];
		_logger.LogDebug("RebuildHeap(headNode={HeadNode}): passTwoResult → {PassTwoResult}", headNode, passTwoResult);

        if (passOneResult.Count == 1)
        {
            root = passTwoResult;
			_logger.LogWarning("RebuildHeap(headNode={HeadNode}): root → {Root}", headNode, root);
            return;
        }

        for (var i = passOneResult.Count - 2; i >= 0; i--)
        {
            current = passOneResult[i];
            passTwoResult = RebuildHeap(passTwoResult, current);
        }

        root = passTwoResult;
		_logger.LogDebug("<<< Exiting RebuildHeap(headNode={HeadNode})", headNode);
    }

    private PairingHeapNode<T> RebuildHeap(PairingHeapNode<T> node1, PairingHeapNode<T> node2)
    {
		_logger.LogDebug(">>> Entering RebuildHeap(node1={Node1},node2={Node2})", node1, node2);
        if (node2 != null)
        {
            node2.Previous = null!;
			_logger.LogDebug("RebuildHeap(node1={Node1},node2={Node2}): node2.Previous → {Arg2}", node1, node2, node2.Previous);
			_logger.LogDebug("RebuildHeap(node1={Node1},node2={Node2}): node2.Next → {Arg2}", node1, node2, node2.Next);
            node2.Next = null!;
        }

        if (node1 == null)
        {
            return node2!;
        }

        node1.Previous = null!;
		_logger.LogDebug("RebuildHeap(node1={Node1},node2={Node2}): node1.Previous → {Arg2}", node1, node2, node1.Previous);
		_logger.LogDebug("RebuildHeap(node1={Node1},node2={Node2}): node1.Next → {Arg2}", node1, node2, node1.Next);
        node1.Next = null!;

        if (node2 != null && comparer.Compare(node1.Value, node2.Value) <= 0)
        {
            AddChild(ref node1, node2);
            return node1;
        }

        AddChild(ref node2!, node1);
        return node2;
    }

    private void AddChild(ref PairingHeapNode<T> parent, PairingHeapNode<T> child)
    {
		_logger.LogDebug(">>> Entering AddChild(parent={Parent},child={Child})", parent, child);
        if (parent.ChildrenHead == null)
        {
            parent.ChildrenHead = child;
			_logger.LogDebug("AddChild(parent={Parent},child={Child}): parent.ChildrenHead → {Arg2}", parent, child, parent.ChildrenHead);
			_logger.LogDebug("AddChild(parent={Parent},child={Child}): child.Previous → {Arg2}", parent, child, child.Previous);
            child.Previous = parent;
            return;
        }

        var head = parent.ChildrenHead;

        child.Previous = head;
        child.Next = head.Next;
		_logger.LogDebug("AddChild(parent={Parent},child={Child}): child.Next → {Arg2}", parent, child, child.Next);

        if (head.Next != null)
        {
            head.Next.Previous = child;
        }

		_logger.LogDebug("AddChild(parent={Parent},child={Child}): head.Next → {Arg2}", parent, child, head.Next);
        head.Next = child;
		_logger.LogDebug("<<< Exiting AddChild(parent={Parent},child={Child})", parent, child);
    }

    private void DeleteChild(PairingHeapNode<T> node)
    {
		_logger.LogDebug(">>> Entering DeleteChild(node={Node})", node);
        if (node.IsHeadChild)
        {
            var parent = node.Previous;

            if (node.Next != null)
            {
                node.Next.Previous = parent;
            }

            parent.ChildrenHead = node.Next!;
			_logger.LogDebug("DeleteChild(node={Node}): parent.ChildrenHead → {Arg1}", node, parent.ChildrenHead);
        }
        else
        {
            node.Previous.Next = node.Next;

            if (node.Next != null)
            {
                node.Next.Previous = node.Previous;
            }
        }
		_logger.LogDebug("<<< Exiting DeleteChild(node={Node})", node);
    }

    private void Map(T newItem, PairingHeapNode<T> newNode)
    {
		_logger.LogDebug(">>> Entering Map(newItem={NewItem},newNode={NewNode})", newItem, newNode);
        if (mapping.ContainsKey(newItem))
        {
            mapping[newItem].Add(newNode);
        }
        else
        {
            mapping[newItem] = [newNode];
        }
		_logger.LogDebug("<<< Exiting Map(newItem={NewItem},newNode={NewNode})", newItem, newNode);
    }

    private void UpdateNodeValue(T currentValue, T newValue, PairingHeapNode<T> node)
    {
		_logger.LogDebug(">>> Entering UpdateNodeValue(currentValue={CurrentValue},newValue={NewValue},node={Node})", currentValue, newValue, node);
        RemoveMapping(currentValue, node);
        node.Value = newValue;
		_logger.LogDebug("UpdateNodeValue(currentValue={CurrentValue},newValue={NewValue},node={Node}): node.Value → {Arg3}", currentValue, newValue, node, node.Value);
        Map(newValue, node);
		_logger.LogDebug("<<< Exiting UpdateNodeValue(currentValue={CurrentValue},newValue={NewValue},node={Node})", currentValue, newValue, node);
    }

    private void RemoveMapping(T currentValue, PairingHeapNode<T> node)
    {
		_logger.LogDebug(">>> Entering RemoveMapping(currentValue={CurrentValue},node={Node})", currentValue, node);
        mapping[currentValue].Remove(node);
        if (mapping[currentValue].Count == 0)
        {
            mapping.Remove(currentValue);
        }
		_logger.LogDebug("<<< Exiting RemoveMapping(currentValue={CurrentValue},node={Node})", currentValue, node);
    }
}
