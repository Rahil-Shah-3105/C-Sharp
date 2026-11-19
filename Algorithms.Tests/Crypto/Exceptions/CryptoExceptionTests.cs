using Algorithms.Crypto.Exceptions;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Crypto.Exceptions
{
    [TestFixture]
    public class CryptoExceptionTests
    {
		private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        [Test]
        public void CryptoException_ShouldBeCreatedWithoutMessageOrInnerException()
        {
			_logger.LogError(">>> Entering CryptoException_ShouldBeCreatedWithoutMessageOrInnerException()");
            // Act
            var exception = new CryptoException();
			_logger.LogError("CryptoException_ShouldBeCreatedWithoutMessageOrInnerException(): exception → {Exception}", exception);

            // Assert
            exception.Should().BeOfType<CryptoException>()
                .And.Subject.As<CryptoException>()
                .Message.Should().NotBeNullOrEmpty();
            exception.InnerException.Should().BeNull();
			_logger.LogWarning("<<< Exiting CryptoException_ShouldBeCreatedWithoutMessageOrInnerException()");
        }

        [Test]
        public void CryptoException_ShouldSetMessage()
        {
			_logger.LogError(">>> Entering CryptoException_ShouldSetMessage()");
            // Arrange
            var expectedMessage = "This is a custom cryptographic error.";
			_logger.LogError("CryptoException_ShouldSetMessage(): expectedMessage → {ExpectedMessage}", expectedMessage);

            // Act
            var exception = new CryptoException(expectedMessage);
			_logger.LogError("CryptoException_ShouldSetMessage(): exception → {Exception}", exception);

            // Assert
            exception.Should().BeOfType<CryptoException>()
                .And.Subject.As<CryptoException>()
                .Message.Should().Be(expectedMessage);
            exception.InnerException.Should().BeNull();
			_logger.LogWarning("<<< Exiting CryptoException_ShouldSetMessage()");
        }

        [Test]
        public void CryptoException_ShouldSetMessageAndInnerException()
        {
			_logger.LogError(">>> Entering CryptoException_ShouldSetMessageAndInnerException()");
            // Arrange
            var expectedMessage = "An error occurred during encryption.";
			_logger.LogError("CryptoException_ShouldSetMessageAndInnerException(): expectedMessage → {ExpectedMessage}", expectedMessage);
            var innerException = new InvalidOperationException("Invalid operation");
			_logger.LogError("CryptoException_ShouldSetMessageAndInnerException(): innerException → {InnerException}", innerException);

            // Act
            var exception = new CryptoException(expectedMessage, innerException);
			_logger.LogError("CryptoException_ShouldSetMessageAndInnerException(): exception → {Exception}", exception);

            // Assert
            exception.Should().BeOfType<CryptoException>()
                .And.Subject.As<CryptoException>()
                .Message.Should().Be(expectedMessage);
            exception.InnerException.Should().Be(innerException);
			_logger.LogWarning("<<< Exiting CryptoException_ShouldSetMessageAndInnerException()");
        }

        [Test]
        public void CryptoException_MessageShouldNotBeNullWhenUsingDefaultConstructor()
        {
			_logger.LogError(">>> Entering CryptoException_MessageShouldNotBeNullWhenUsingDefaultConstructor()");
            // Act
            var exception = new CryptoException();
			_logger.LogError("CryptoException_MessageShouldNotBeNullWhenUsingDefaultConstructor(): exception → {Exception}", exception);

            // Assert
            exception.Message.Should().NotBeNullOrEmpty(); // Even the default Exception message is not null or empty.
			_logger.LogWarning("<<< Exiting CryptoException_MessageShouldNotBeNullWhenUsingDefaultConstructor()");
        }
    }
}

