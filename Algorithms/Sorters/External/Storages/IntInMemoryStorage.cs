using Microsoft.Extensions.Logging;
namespace Algorithms.Sorters.External.Storages;

public class IntInMemoryStorage(int[] array) : ISequentialStorage<int>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly int[] storage = array;

    public int Length => storage.Length;

    public ISequentialStorageReader<int> GetReader() => new InMemoryReader(storage);

    public ISequentialStorageWriter<int> GetWriter() => new InMemoryWriter(storage);

    private class InMemoryReader(int[] storage) : ISequentialStorageReader<int>
    {
        private readonly int[] storage = storage;
        private int offset;

        public void Dispose()
        {
			_logger.LogDebug(">>> Entering Dispose()");
            // Nothing to dispose here
			_logger.LogDebug("<<< Exiting Dispose()");
        }

        public int Read() => storage[offset++];
    }

    private class InMemoryWriter(int[] storage) : ISequentialStorageWriter<int>
    {
        private readonly int[] storage = storage;
        private int offset;

        public void Write(int value) => storage[offset++] = value;

        public void Dispose()
        {
            // Nothing to dispose here
        }
    }
}
