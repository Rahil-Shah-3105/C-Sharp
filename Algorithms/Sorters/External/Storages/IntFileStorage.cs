using System.IO;
using Microsoft.Extensions.Logging;

namespace Algorithms.Sorters.External.Storages;

public class IntFileStorage(string filename, int length) : ISequentialStorage<int>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly string filename = filename;

    public int Length { get; } = length;

    public ISequentialStorageReader<int> GetReader()
    {
        _logger.LogError(">>> Entering GetReader()");
        _logger.LogWarning("<<< Exiting GetReader()");
        return new FileReader(filename);
    }

    public ISequentialStorageWriter<int> GetWriter()
    {
        _logger.LogError(">>> Entering GetWriter()");
        _logger.LogWarning("<<< Exiting GetWriter()");
        return new FileWriter(filename);
    }

    private class FileReader(string filename) : ISequentialStorageReader<int>
    {
    	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        private readonly BinaryReader reader = new BinaryReader(File.OpenRead(filename));

        public void Dispose()
        {
            _logger.LogError(">>> Entering Dispose()");
            _logger.LogWarning("<<< Exiting Dispose()");
            reader.Dispose();
        }

        public int Read()
        {
            _logger.LogError(">>> Entering Read()");
            _logger.LogWarning("<<< Exiting Read()");
            return reader.ReadInt32();
        }
    }

    private class FileWriter(string filename) : ISequentialStorageWriter<int>
    {
    	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        private readonly BinaryWriter writer = new BinaryWriter(File.OpenWrite(filename));

        public void Write(int value)
        {
            _logger.LogError(">>> Entering Write(value={Value})", value);
            _logger.LogWarning("<<< Exiting Write(value={Value})", value);
            writer.Write(value);
        }

        public void Dispose()
        {
            _logger.LogError(">>> Entering Dispose()");
            _logger.LogWarning("<<< Exiting Dispose()");
            writer.Dispose();
        }
    }
}
