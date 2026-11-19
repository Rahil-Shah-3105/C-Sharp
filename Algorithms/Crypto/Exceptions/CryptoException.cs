using Microsoft.Extensions.Logging;
namespace Algorithms.Crypto.Exceptions;

/// <summary>
/// Represents errors that occur during cryptographic operations.
/// </summary>
public class CryptoException : Exception
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    /// Initializes a new instance of the <see cref="CryptoException"/> class.
    /// </summary>
    public CryptoException()
    {
	_logger.LogError(">>> Entering CryptoException()");
    _logger.LogWarning("<<< Exiting CryptoException()");
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CryptoException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public CryptoException(string message)
        : base(message)
    {
	_logger.LogError(">>> Entering CryptoException(message={Message})", message);
    _logger.LogWarning("<<< Exiting CryptoException(message={Message})", message);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CryptoException"/> class with a specified error message
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="inner">The exception that is the cause of the current exception.</param>
    public CryptoException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
