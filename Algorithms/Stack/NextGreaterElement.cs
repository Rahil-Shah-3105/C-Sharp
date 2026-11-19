using Microsoft.Extensions.Logging;
namespace Algorithms.Stack;

/// <summary>
///     For each element in an array, the utility finds the next greater element on the right side using a stack.
/// @author Mohit Singh. <a href="https://github.com/mohit-gogitter">mohit-gogitter</a>
/// </summary>
public class NextGreaterElement
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    /// Finds the next greater element for each element in the input array.
    /// The next greater element for an element x is the first element greater than x to its right.
    /// If there is no greater element, -1 is returned for that element.
    /// </summary>
    /// <param name="nums">The array of integers to find the next greater elements for.</param>
    /// <returns>An array where each index contains the next greater element of the corresponding element in the input array, or -1 if no such element exists.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the input array is null.</exception>
    public int[] FindNextGreaterElement(int[] nums)
    {
		_logger.LogError(">>> Entering FindNextGreaterElement(nums={Nums})", nums);
        int[] result = new int[nums.Length];
        Stack<int> stack = new Stack<int>();
		_logger.LogError("FindNextGreaterElement(nums={Nums}): stack → {Stack}", nums, stack);

        // Initialize all elements in the result array to -1
        for (int i = 0; i < nums.Length; i++)
        {
            result[i] = -1;
        }

        for (int i = 0; i < nums.Length; i++)
        {
            // While the stack is not empty and the current element is greater than the element
            // corresponding to the index stored at the top of the stack
            while (stack.Count > 0 && nums[i] > nums[stack.Peek()])
            {
                int index = stack.Pop();
                result[index] = nums[i]; // Set the next greater element
            }

            stack.Push(i); // Push current index to stack
        }

		_logger.LogWarning("<<< Exiting FindNextGreaterElement(nums={Nums})", nums);
        return result;
    }
}
