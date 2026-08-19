using Microsoft.Extensions.Logging;
namespace Algorithms.Stack;

/// <summary>
///     It checks if an expression has matching and balanced parentheses.
/// @author Mohit Singh. <a href="https://github.com/mohit-gogitter">mohit-gogitter</a>
/// </summary>
public class BalancedParenthesesChecker
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private static readonly Dictionary<char, char> ParenthesesMap = new Dictionary<char, char>
        {
            { '(', ')' },
            { '{', '}' },
            { '[', ']' },
        };

    /// <summary>
    /// Determines if a given string expression containing brackets is balanced.
    /// A string is considered balanced if all opening brackets have corresponding closing brackets
    /// in the correct order. The supported brackets are '()', '{}', and '[]'.
    /// </summary>
    /// <param name="expression">
    /// The input string expression containing the brackets to check for balance.
    /// </param>
    /// <returns>
    /// <c>true</c> if the brackets in the expression are balanced; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the input expression contains invalid characters or is null/empty.
    /// Only '(', ')', '{', '}', '[', ']' characters are allowed.
    /// </exception>
    public bool IsBalanced(string expression)
    {
		_logger.LogDebug(">>> Entering IsBalanced(expression={Expression})", expression);
        if (string.IsNullOrEmpty(expression))
        {
			_logger.LogWarning("Throwing ArgumentException from IsBalanced(expression={Expression})", expression);
            throw new ArgumentException("The input expression cannot be null or empty.");
        }

        Stack<char> stack = new Stack<char>();
		_logger.LogDebug("IsBalanced(expression={Expression}): stack → {Stack}", expression, stack);
        foreach (char c in expression)
        {
            if (IsOpeningParenthesis(c))
            {
                stack.Push(c);
            }
            else if (IsClosingParenthesis(c))
            {
                if (!IsBalancedClosing(stack, c))
                {
                    return false;
                }
            }
            else
            {
                throw new ArgumentException($"Invalid character '{c}' found in the expression.");
            }
        }

		_logger.LogDebug("<<< Exiting IsBalanced(expression={Expression})", expression);
        return stack.Count == 0;
    }

    private static bool IsOpeningParenthesis(char c)
    {
		_logger.LogDebug(">>> Entering IsOpeningParenthesis(c={C})", c);
		_logger.LogDebug("<<< Exiting IsOpeningParenthesis(c={C})", c);
        return c == '(' || c == '{' || c == '[';
    }

    private static bool IsClosingParenthesis(char c)
    {
		_logger.LogDebug(">>> Entering IsClosingParenthesis(c={C})", c);
		_logger.LogDebug("IsClosingParenthesis(c={C}): c → {C1}", c, c);
		_logger.LogDebug("<<< Exiting IsClosingParenthesis(c={C})", c);
        return c == ')' || c == '}' || c == ']';
    }

    private static bool IsBalancedClosing(Stack<char> stack, char close)
    {
		_logger.LogDebug(">>> Entering IsBalancedClosing(stack={Stack},close={Close})", stack, close);
        if (stack.Count == 0)
        {
            return false;
        }

        char open = stack.Pop();
		_logger.LogDebug("IsBalancedClosing(stack={Stack},close={Close}): open → {Open}", stack, close, open);
		_logger.LogDebug("<<< Exiting IsBalancedClosing(stack={Stack},close={Close})", stack, close);
        return IsMatchingPair(open, close);
    }

    private static bool IsMatchingPair(char open, char close)
    {
		_logger.LogDebug(">>> Entering IsMatchingPair(open={Open},close={Close})", open, close);
		_logger.LogDebug("<<< Exiting IsMatchingPair(open={Open},close={Close})", open, close);
        return ParenthesesMap.ContainsKey(open) && ParenthesesMap[open] == close;
    }
}
