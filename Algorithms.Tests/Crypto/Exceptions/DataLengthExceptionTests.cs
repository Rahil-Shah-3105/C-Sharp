using Algorithms.Crypto.Exceptions;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Crypto.Exceptions
{
    [TestFixture]
    public class DataLengthExceptionTests
    {
		private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        [Test]
        public void DataLengthException_ShouldBeCreatedWithoutMessageOrInnerException()
        {
			_logger.LogDebug(">>> Entering DataLengthException_ShouldBeCreatedWithoutMessageOrInnerException()");
            // Act
            var exception = new DataLengthException();
			_logger.LogDebug("DataLengthException_ShouldBeCreatedWithoutMessageOrInnerException(): exception → {Exception}", exception);

            // Assert
            exception.Should().BeOfType<DataLengthException>()
                .And.Subject.As<DataLengthException>()
                .Message.Should().NotBeNullOrEmpty();
            exception.InnerException.Should().BeNull();
			_logger.LogDebug("<<< Exiting DataLengthException_ShouldBeCreatedWithoutMessageOrInnerException()");
        }

        [Test]
        public void DataLengthException_ShouldSetMessage()
        {
			_logger.LogDebug(">>> Entering DataLengthException_ShouldSetMessage()");
            // Arrange
            var expectedMessage = "Data length is invalid.";
			_logger.LogDebug("DataLengthException_ShouldSetMessage(): expectedMessage → {ExpectedMessage}", expectedMessage);

            // Act
            var exception = new DataLengthException(expectedMessage);
			_logger.LogDebug("DataLengthException_ShouldSetMessage(): exception → {Exception}", exception);

            // Assert
            exception.Should().BeOfType<DataLengthException>()
                .And.Subject.As<DataLengthException>()
                .Message.Should().Be(expectedMessage);
            exception.InnerException.Should().BeNull();
			_logger.LogDebug("<<< Exiting DataLengthException_ShouldSetMessage()");
        }

        [Test]
        public void DataLengthException_ShouldSetMessageAndInnerException()
        {
			_logger.LogDebug(">>> Entering DataLengthException_ShouldSetMessageAndInnerException()");
            // Arrange
            var expectedMessage = "An error occurred due to incorrect data length.";
			_logger.LogDebug("DataLengthException_ShouldSetMessageAndInnerException(): expectedMessage → {ExpectedMessage}", expectedMessage);
            var innerException = new ArgumentException("Invalid argument");
			_logger.LogDebug("DataLengthException_ShouldSetMessageAndInnerException(): innerException → {InnerException}", innerException);

            // Act
            var exception = new DataLengthException(expectedMessage, innerException);
			_logger.LogDebug("DataLengthException_ShouldSetMessageAndInnerException(): exception → {Exception}", exception);

            // Assert
            exception.Should().BeOfType<DataLengthException>()
                .And.Subject.As<DataLengthException>()
                .Message.Should().Be(expectedMessage);
            exception.InnerException.Should().Be(innerException);
			_logger.LogDebug("<<< Exiting DataLengthException_ShouldSetMessageAndInnerException()");
        }

        [Test]
        public void DataLengthException_MessageShouldNotBeNullWhenUsingDefaultConstructor()
        {
			_logger.LogDebug(">>> Entering DataLengthException_MessageShouldNotBeNullWhenUsingDefaultConstructor()");
            // Act
            var exception = new DataLengthException();
			_logger.LogDebug("DataLengthException_MessageShouldNotBeNullWhenUsingDefaultConstructor(): exception → {Exception}", exception);

            // Assert
            exception.Message.Should().NotBeNullOrEmpty(); // Even the default Exception message is not null or empty.
			_logger.LogDebug("<<< Exiting DataLengthException_MessageShouldNotBeNullWhenUsingDefaultConstructor()");
        }
    }
}
