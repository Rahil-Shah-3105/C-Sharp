using Algorithms.Crypto.Exceptions;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Crypto.Exceptions
{
    [TestFixture]
    public class OutputLengthExceptionTests
    {
		private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        [Test]
        public void OutputLengthException_ShouldBeCreatedWithoutMessageOrInnerException()
        {
			_logger.LogDebug(">>> Entering OutputLengthException_ShouldBeCreatedWithoutMessageOrInnerException()");
            // Act
            var exception = new OutputLengthException();
			_logger.LogDebug("OutputLengthException_ShouldBeCreatedWithoutMessageOrInnerException(): exception → {Exception}", exception);

            // Assert
            exception.Should().BeOfType<OutputLengthException>()
                .And.Subject.As<OutputLengthException>()
                .Message.Should().NotBeNullOrEmpty();
            exception.InnerException.Should().BeNull();
			_logger.LogDebug("<<< Exiting OutputLengthException_ShouldBeCreatedWithoutMessageOrInnerException()");
        }

        [Test]
        public void OutputLengthException_ShouldSetMessage()
        {
			_logger.LogDebug(">>> Entering OutputLengthException_ShouldSetMessage()");
            // Arrange
            var expectedMessage = "Output buffer is too short.";
			_logger.LogDebug("OutputLengthException_ShouldSetMessage(): expectedMessage → {ExpectedMessage}", expectedMessage);

            // Act
            var exception = new OutputLengthException(expectedMessage);
			_logger.LogDebug("OutputLengthException_ShouldSetMessage(): exception → {Exception}", exception);

            // Assert
            exception.Should().BeOfType<OutputLengthException>()
                .And.Subject.As<OutputLengthException>()
                .Message.Should().Be(expectedMessage);
            exception.InnerException.Should().BeNull();
			_logger.LogDebug("<<< Exiting OutputLengthException_ShouldSetMessage()");
        }

        [Test]
        public void OutputLengthException_ShouldSetMessageAndInnerException()
        {
			_logger.LogDebug(">>> Entering OutputLengthException_ShouldSetMessageAndInnerException()");
            // Arrange
            var expectedMessage = "Output length error.";
			_logger.LogDebug("OutputLengthException_ShouldSetMessageAndInnerException(): expectedMessage → {ExpectedMessage}", expectedMessage);
            var innerException = new ArgumentException("Invalid argument");
			_logger.LogDebug("OutputLengthException_ShouldSetMessageAndInnerException(): innerException → {InnerException}", innerException);

            // Act
            var exception = new OutputLengthException(expectedMessage, innerException);
			_logger.LogDebug("OutputLengthException_ShouldSetMessageAndInnerException(): exception → {Exception}", exception);

            // Assert
            exception.Should().BeOfType<OutputLengthException>()
                .And.Subject.As<OutputLengthException>()
                .Message.Should().Be(expectedMessage);
            exception.InnerException.Should().Be(innerException);
			_logger.LogDebug("<<< Exiting OutputLengthException_ShouldSetMessageAndInnerException()");
        }

        [Test]
        public void OutputLengthException_MessageShouldNotBeNullWhenUsingDefaultConstructor()
        {
			_logger.LogDebug(">>> Entering OutputLengthException_MessageShouldNotBeNullWhenUsingDefaultConstructor()");
            // Act
            var exception = new OutputLengthException();
			_logger.LogDebug("OutputLengthException_MessageShouldNotBeNullWhenUsingDefaultConstructor(): exception → {Exception}", exception);

            // Assert
            exception.Message.Should().NotBeNullOrEmpty(); // Even the default Exception message is not null or empty.
			_logger.LogDebug("<<< Exiting OutputLengthException_MessageShouldNotBeNullWhenUsingDefaultConstructor()");
        }
    }
}
