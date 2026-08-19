using Algorithms.Crypto.Utils;
using Algorithms.Crypto.Exceptions;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Crypto.Utils
{
    [TestFixture]
    public class ValidationUtilsTests
    {
		private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        [Test]
        public void CheckDataLength_WithBufferOutOfBounds_ShouldThrowDataLengthException()
        {
			_logger.LogDebug(">>> Entering CheckDataLength_WithBufferOutOfBounds_ShouldThrowDataLengthException()");
            // Arrange
            var buffer = new byte[5];  // A byte array of length 5
            var offset = 3;               // Starting at index 3
            var length = 4;               // Expecting to read 4 bytes (which will exceed the buffer size)
            var errorMessage = "Buffer is too short";
			_logger.LogDebug("CheckDataLength_WithBufferOutOfBounds_ShouldThrowDataLengthException(): errorMessage → {ErrorMessage}", errorMessage);

            // Act
            var act = () => ValidationUtils.CheckDataLength(buffer, offset, length, errorMessage);
			_logger.LogDebug("CheckDataLength_WithBufferOutOfBounds_ShouldThrowDataLengthException(): act → {Act}", act);

            // Assert
            act.Should().Throw<DataLengthException>()
                .WithMessage(errorMessage);
				_logger.LogDebug("<<< Exiting CheckDataLength_WithBufferOutOfBounds_ShouldThrowDataLengthException()");
        }

        [Test]
        public void CheckOutputLength_WithCondition_ShouldThrowOutputLengthException()
        {
			_logger.LogDebug(">>> Entering CheckOutputLength_WithCondition_ShouldThrowOutputLengthException()");
            // Arrange
            var condition = true;
            var errorMessage = "Output length is invalid";
			_logger.LogDebug("CheckOutputLength_WithCondition_ShouldThrowOutputLengthException(): errorMessage → {ErrorMessage}", errorMessage);

            // Act
            var act = () => ValidationUtils.CheckOutputLength(condition, errorMessage);
			_logger.LogDebug("CheckOutputLength_WithCondition_ShouldThrowOutputLengthException(): act → {Act}", act);

            // Assert
            act.Should().Throw<OutputLengthException>()
               .WithMessage(errorMessage);
			_logger.LogDebug("<<< Exiting CheckOutputLength_WithCondition_ShouldThrowOutputLengthException()");
        }

        [Test]
        public void CheckOutputLength_WithCondition_ShouldNotThrowOutputLengthException()
        {
			_logger.LogDebug(">>> Entering CheckOutputLength_WithCondition_ShouldNotThrowOutputLengthException()");
            // Arrange
            var condition = false;
            var errorMessage = "Output length is invalid";
			_logger.LogDebug("CheckOutputLength_WithCondition_ShouldNotThrowOutputLengthException(): errorMessage → {ErrorMessage}", errorMessage);

            // Act
            var act = () => ValidationUtils.CheckOutputLength(condition, errorMessage);
			_logger.LogDebug("CheckOutputLength_WithCondition_ShouldNotThrowOutputLengthException(): act → {Act}", act);

            // Assert
            act.Should().NotThrow<OutputLengthException>();
			_logger.LogDebug("<<< Exiting CheckOutputLength_WithCondition_ShouldNotThrowOutputLengthException()");
        }

        [Test]
        public void CheckOutputLength_WithBufferOutOfBounds_ShouldThrowOutputLengthException()
        {
			_logger.LogDebug(">>> Entering CheckOutputLength_WithBufferOutOfBounds_ShouldThrowOutputLengthException()");
            // Arrange
            var buffer = new byte[5];
            var offset = 3;
            var length = 4;
            var errorMessage = "Output buffer is too short";
			_logger.LogDebug("CheckOutputLength_WithBufferOutOfBounds_ShouldThrowOutputLengthException(): errorMessage → {ErrorMessage}", errorMessage);

            // Act
            var act = () => ValidationUtils.CheckOutputLength(buffer, offset, length, errorMessage);
			_logger.LogDebug("CheckOutputLength_WithBufferOutOfBounds_ShouldThrowOutputLengthException(): act → {Act}", act);

            // Assert
            act.Should().Throw<OutputLengthException>()
               .WithMessage(errorMessage);
			_logger.LogDebug("<<< Exiting CheckOutputLength_WithBufferOutOfBounds_ShouldThrowOutputLengthException()");
        }

        [Test]
        public void CheckOutputLength_WithBProperBufferSize_ShouldThrowOutputLengthException()
        {
			_logger.LogDebug(">>> Entering CheckOutputLength_WithBProperBufferSize_ShouldThrowOutputLengthException()");
            // Arrange
            var buffer = new byte[5];
            var offset = 0;
            var length = 4;
            var errorMessage = "Output buffer is too short";
			_logger.LogDebug("CheckOutputLength_WithBProperBufferSize_ShouldThrowOutputLengthException(): errorMessage → {ErrorMessage}", errorMessage);

            // Act
            var act = () => ValidationUtils.CheckOutputLength(buffer, offset, length, errorMessage);
			_logger.LogDebug("CheckOutputLength_WithBProperBufferSize_ShouldThrowOutputLengthException(): act → {Act}", act);

            // Assert
            act.Should().NotThrow<OutputLengthException>();
			_logger.LogDebug("<<< Exiting CheckOutputLength_WithBProperBufferSize_ShouldThrowOutputLengthException()");
        }

        [Test]
        public void CheckOutputLength_SpanExceedsLimit_ShouldThrowOutputLengthException()
        {
			_logger.LogDebug(">>> Entering CheckOutputLength_SpanExceedsLimit_ShouldThrowOutputLengthException()");
            // Arrange
            Span<byte> output = new byte[10];
            var outputLength = output.Length;
            var maxLength = 5;
            var errorMessage = "Output exceeds maximum length";
			_logger.LogDebug("CheckOutputLength_SpanExceedsLimit_ShouldThrowOutputLengthException(): errorMessage → {ErrorMessage}", errorMessage);

            // Act
            var act = () => ValidationUtils.CheckOutputLength(outputLength > maxLength, errorMessage); // Capture the length
			_logger.LogDebug("CheckOutputLength_SpanExceedsLimit_ShouldThrowOutputLengthException(): act → {Act}", act);

            // Assert
            act.Should().Throw<OutputLengthException>()
                .WithMessage(errorMessage);
				_logger.LogDebug("<<< Exiting CheckOutputLength_SpanExceedsLimit_ShouldThrowOutputLengthException()");
        }

        [Test]
        public void CheckOutputLength_SpanDoesNotExceedLimit_ShouldThrowOutputLengthException()
        {
			_logger.LogDebug(">>> Entering CheckOutputLength_SpanDoesNotExceedLimit_ShouldThrowOutputLengthException()");
            // Arrange
            Span<byte> output = new byte[10];
            var outputLength = output.Length;
            var maxLength = 15;
            var errorMessage = "Output exceeds maximum length";
			_logger.LogDebug("CheckOutputLength_SpanDoesNotExceedLimit_ShouldThrowOutputLengthException(): errorMessage → {ErrorMessage}", errorMessage);

            // Act
            var act = () => ValidationUtils.CheckOutputLength(outputLength > maxLength, errorMessage); // Capture the length
			_logger.LogDebug("CheckOutputLength_SpanDoesNotExceedLimit_ShouldThrowOutputLengthException(): act → {Act}", act);

            // Assert
            act.Should().NotThrow<OutputLengthException>();
			_logger.LogDebug("<<< Exiting CheckOutputLength_SpanDoesNotExceedLimit_ShouldThrowOutputLengthException()");
        }
    }
}
