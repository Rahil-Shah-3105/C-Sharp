using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace Algorithms.Stack
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    /// The Code focuses on Converting an Infix Expression to a Postfix Expression and Evaluates the value for the expression.
    /// @author Aalok Choudhari. <a href="https://github.com/kaloa2025">kaloa2025</a>
    /// </summary>
    public static class InfixToPostfix
    {
        /// <summary>
        /// <param name="initialInfixExpression"> Infix Expression String to Convert.</param>
        /// <returns>Postfix Expression.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the input expression contains invalid characters or is null/empty.
        /// Only following characters are allowed : Parentheses['(',')'], Operands[a-z,A-Z,0-9], Operators['+','-','*','/','^'].
        /// </exception>
        /// </summary>
        public static string InfixToPostfixConversion(string initialInfixExpression)
        {
			_logger.LogDebug(">>> Entering InfixToPostfixConversion(initialInfixExpression={InitialInfixExpression})", initialInfixExpression);
            ValidateInfix(initialInfixExpression);

            Stack<char> stack = new Stack<char>();
			_logger.LogDebug("InfixToPostfixConversion(initialInfixExpression={InitialInfixExpression}): stack → {Stack}", initialInfixExpression, stack);
            StringBuilder postfixExpression = new StringBuilder();
			_logger.LogDebug("InfixToPostfixConversion(initialInfixExpression={InitialInfixExpression}): postfixExpression → {PostfixExpression}", initialInfixExpression, postfixExpression);

            foreach (char c in initialInfixExpression)
            {
                if (char.IsWhiteSpace(c))
                {
                    continue;
                }

                if (!IsValidCharacter(c))
                {
					_logger.LogWarning("Throwing ArgumentException from InfixToPostfixConversion(initialInfixExpression={InitialInfixExpression})", initialInfixExpression);
                    throw new ArgumentException($"Invalid character {c}.");
                }

                ProcessInfixCharacter(c, stack, postfixExpression);
            }

            EmptyRemainingStack(stack, postfixExpression);
			_logger.LogDebug("<<< Exiting InfixToPostfixConversion(initialInfixExpression={InitialInfixExpression})", initialInfixExpression);
            return postfixExpression.ToString();
        }

        /// <summary>
        /// <param name="postfixExpression"> Postfix Expression String to Evaluate.</param>
        /// <returns>Postfix Expression's Calculated value.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the input expression contains invalid characters or is null/empty.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Validates expression to have sufficient operands for performing operation.
        /// </exception>
        /// </summary>
        public static int PostfixExpressionEvaluation(string postfixExpression)
        {
			_logger.LogDebug(">>> Entering PostfixExpressionEvaluation(postfixExpression={PostfixExpression})", postfixExpression);
            ValidatePostfix(postfixExpression);

            Stack<int> stack = new Stack<int>();
			_logger.LogDebug("PostfixExpressionEvaluation(postfixExpression={PostfixExpression}): stack → {Stack}", postfixExpression, stack);
            foreach (char ch in postfixExpression)
            {
                if(char.IsWhiteSpace(ch))
                {
                    continue;
                }

                if(char.IsDigit(ch))
                {
                    stack.Push(ch - '0');
                    continue;
                }

                if (IsOperator(ch))
                {
                    EvaluateOperator(stack, ch);
                    continue;
                }

				_logger.LogWarning("Throwing InvalidOperationException from PostfixExpressionEvaluation(postfixExpression={PostfixExpression})", postfixExpression);
                throw new InvalidOperationException($"Invalid character in expression: {ch}");
            }

            if (stack.Count != 1)
            {
			_logger.LogDebug("<<< Exiting PostfixExpressionEvaluation(postfixExpression={PostfixExpression})", postfixExpression);
                throw new InvalidOperationException("Invalid postfix expression: Leftover operands.");
            }

            return stack.Pop();
        }

        private static void ProcessInfixCharacter(char c, Stack<char> stack, StringBuilder postfixExpression)
        {
			_logger.LogDebug(">>> Entering ProcessInfixCharacter(c={C},stack={Stack},postfixExpression={PostfixExpression})", c, stack, postfixExpression);
            if (IsOperand(c))
            {
                postfixExpression.Append(c);
                return;
            }

            if (c == '(')
            {
                stack.Push(c);
                return;
            }

            if (c == ')')
            {
                ProcessClosingParenthesis(stack, postfixExpression);
                return;
            }

            ProcessOperator(c, stack, postfixExpression);
			_logger.LogDebug("<<< Exiting ProcessInfixCharacter(c={C},stack={Stack},postfixExpression={PostfixExpression})", c, stack, postfixExpression);
        }

        private static void ProcessClosingParenthesis(Stack<char> stack, StringBuilder postfixExpression)
        {
			_logger.LogDebug(">>> Entering ProcessClosingParenthesis(stack={Stack},postfixExpression={PostfixExpression})", stack, postfixExpression);
            while (stack.Count > 0 && stack.Peek() != '(')
            {
                postfixExpression.Append(stack.Pop());
            }

            if (stack.Count == 0)
            {
				_logger.LogWarning("Throwing InvalidOperationException from ProcessClosingParenthesis(stack={Stack},postfixExpression={PostfixExpression})", stack, postfixExpression);
                throw new InvalidOperationException("Mismatched parentheses in expression.");
            }

            stack.Pop();
			_logger.LogDebug("<<< Exiting ProcessClosingParenthesis(stack={Stack},postfixExpression={PostfixExpression})", stack, postfixExpression);
        }

        private static void ProcessOperator(char c, Stack<char> stack, StringBuilder postfixExpression)
        {
			_logger.LogDebug(">>> Entering ProcessOperator(c={C},stack={Stack},postfixExpression={PostfixExpression})", c, stack, postfixExpression);
            while (stack.Count > 0 && stack.Peek() != '(' && Precedence(stack.Peek()) >= Precedence(c))
            {
                postfixExpression.Append(stack.Pop());
				_logger.LogDebug(">>> Entering Precedence(operatorChar={OperatorChar})", operatorChar);
				_logger.LogDebug("<<< Exiting Precedence(operatorChar={OperatorChar})", operatorChar);
            }

            stack.Push(c);
			_logger.LogDebug("<<< Exiting ProcessOperator(c={C},stack={Stack},postfixExpression={PostfixExpression})", c, stack, postfixExpression);
        }

        private static void EmptyRemainingStack(Stack<char> stack, StringBuilder postfix)
        {
			_logger.LogDebug(">>> Entering EmptyRemainingStack(stack={Stack},postfix={Postfix})", stack, postfix);
            while (stack.Count > 0)
            {
                if (stack.Peek() is '(' or ')')
                {
					_logger.LogWarning("Throwing InvalidOperationException from EmptyRemainingStack(stack={Stack},postfix={Postfix})", stack, postfix);
                    throw new InvalidOperationException("Mismatched parentheses.");
                }

                postfix.Append(stack.Pop());
            }
			_logger.LogDebug("<<< Exiting EmptyRemainingStack(stack={Stack},postfix={Postfix})", stack, postfix);
        }

        private static void EvaluateOperator(Stack<int> stack, char op)
        {
			_logger.LogDebug(">>> Entering EvaluateOperator(stack={Stack},op={Op})", stack, op);
            if (stack.Count < 2)
            {
				_logger.LogWarning("Throwing InvalidOperationException from EvaluateOperator(stack={Stack},op={Op})", stack, op);
                throw new InvalidOperationException("Insufficient operands");
            }

            int b = stack.Pop();
			_logger.LogDebug("EvaluateOperator(stack={Stack},op={Op}): b → {B}", stack, op, b);
            int a = stack.Pop();
			_logger.LogDebug("EvaluateOperator(stack={Stack},op={Op}): a → {A}", stack, op, a);

            if (op == '/' && b == 0)
            {
				_logger.LogWarning("Throwing DivideByZeroException from EvaluateOperator(stack={Stack},op={Op})", stack, op);
                throw new DivideByZeroException("Cannot divide by zero");
            }

            int result = op switch
            {
                '+' => a + b,
                '-' => a - b,
                '*' => a * b,
                '/' => a / b,
                '^' => (int)Math.Pow(a, b),
                _ => throw new InvalidOperationException($"Unknown operator {op}"),
            };

            stack.Push(result);
			_logger.LogDebug("<<< Exiting EvaluateOperator(stack={Stack},op={Op})", stack, op);
        }

        private static void ValidateInfix(string expr)
        {
			_logger.LogDebug(">>> Entering ValidateInfix(expr={Expr})", expr);
            if (string.IsNullOrEmpty(expr) || string.IsNullOrWhiteSpace(expr))
            {
				_logger.LogWarning("Throwing ArgumentException from ValidateInfix(expr={Expr})", expr);
			_logger.LogDebug("<<< Exiting ValidateInfix(expr={Expr})", expr);
                throw new ArgumentException("Infix cannot be null or empty.");
            }
        }

        private static void ValidatePostfix(string expr)
        {
			_logger.LogDebug(">>> Entering ValidatePostfix(expr={Expr})", expr);
            if (string.IsNullOrEmpty(expr) || string.IsNullOrWhiteSpace(expr))
            {
				_logger.LogWarning("Throwing ArgumentException from ValidatePostfix(expr={Expr})", expr);
			_logger.LogDebug("<<< Exiting ValidatePostfix(expr={Expr})", expr);
                throw new ArgumentException("Postfix cannot be null or empty.");
            }
        }

        /// <summary>
        /// Decided Operator Precedence.
        /// <param name="operatorChar"> Operator character whose precedence is asked.</param>
        /// <returns>Precedence rank of parameter operator character.</returns>
        /// </summary>
        [ExcludeFromCodeCoverage]
        private static int Precedence(char operatorChar)
        {
			_logger.LogDebug(">>> Entering Precedence(operatorChar={OperatorChar})", operatorChar);
            if (operatorChar == '^')
            {
                return 3;
            }

            if (operatorChar == '*' || operatorChar == '/')
            {
                return 2;
            }

            if (operatorChar == '+' || operatorChar == '-')
            {
			_logger.LogDebug("<<< Exiting Precedence(operatorChar={OperatorChar})", operatorChar);
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// Checks for character if its an Operand.
        /// <param name="ch"> Character asked to verify whether its an operand.</param>
        /// <returns>True if its a digit or a Letter.</returns>
        /// </summary>
        private static bool IsOperand(char ch) => char.IsLetterOrDigit(ch);

        private static readonly HashSet<char> Operators = new() { '+', '-', '*', '/', '^' };

        /// <summary>
        /// Checks Operator.
        /// <param name="ch"> Character asked to verify whether its an operator.</param>
        /// <returns>True if its allowded operator character.</returns>
        /// </summary>
        private static bool IsOperator(char ch) => Operators.Contains(ch);

        /// <summary>
        /// Checks Valid Character.
        /// <param name="c"> Character asked to verify whether its an valid Character for expression.</param>
        /// <returns>True if its allowded character.</returns>
        /// </summary>
        private static bool IsValidCharacter(char c) => IsOperand(c) || IsOperator(c) || IsParenthesis(c);

        private static bool IsParenthesis(char c) => c == '(' || c == ')';
    }
}
