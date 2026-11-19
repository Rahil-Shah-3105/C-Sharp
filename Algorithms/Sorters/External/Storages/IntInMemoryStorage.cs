using Microsoft.Extensions.Logging;
namespace Algorithms.Sorters.External.Storages;

public class IntInMemoryStorage(int[] array) : ISequentialStorage<int>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly int[] storage = array;

    public int Length => storage.Length;

    public ISequentialStorageReader<int> GetReader()
    {
        _logger.LogError(">>> Entering GetReader()");
        _logger.LogWarning("<<< Exiting GetReader()");
        return new InMemoryReader(storage);
    }

    public ISequentialStorageWriter<int> GetWriter()
    {
        _logger.LogError(">>> Entering GetWriter()");
        _logger.LogWarning("<<< Exiting GetWriter()");
        return new InMemoryWriter(storage);
    }

    private class InMemoryReader(int[] storage) : ISequentialStorageReader<int>
    {
    	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        private readonly int[] storage = storage;
        private int offset;

        public void Dispose()
        {
			_logger.LogError(">>> Entering Dispose()");
            // Nothing to dispose here
			_logger.LogWarning("<<< Exiting Dispose()");
        }

        public int Read()
        {
            _logger.LogError(">>> Entering Read()");
            _logger.LogWarning("<<< Exiting Read()");
            return storage[offset++];
        }
    }

    private class InMemoryWriter(int[] storage) : ISequentialStorageWriter<int>
    {
    	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        private readonly int[] storage = storage;
        private int offset;

        public void Write(int value)
        {
            _logger.LogError(">>> Entering Write(value={Value})", value);
            storage[offset++] = value;
        }

        public void Dispose()
        {
            // Nothing to dispose here
        }
    }
}
