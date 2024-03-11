using VRCore.Logger.Interfaces;
using VRDataReader.Framework.Configuration.Interfaces;
using VRDataReader.Services.Interfaces;

namespace VRDataReader.Services
{
    public class DataReaderService : IDataReaderService
    {
        private readonly IDataReaderServiceConfigurationProvider _configurationProvider;
        private readonly IStreamProcessingService _streamProcessingService;
        private readonly ILogger _logger;

        public DataReaderService(IDataReaderServiceConfigurationProvider configurationProvider, IStreamProcessingService streamProcessingService, ILogger logger)
        {
            _configurationProvider = configurationProvider;
            _streamProcessingService = streamProcessingService;
            _logger = logger;
        }

        public async Task ReadAsync()
        {
            Init();

            var files = new DirectoryInfo(_configurationProvider.ListeningPath).GetFiles("*");

            if (files == null || files.Length == 0)
            {
                _logger.Log($"No files found in {_configurationProvider.ListeningPath}");

                return;
            }

            _logger.Log($"Found {files.Length} files in {_configurationProvider.ListeningPath}");

            foreach (var file in files)
            {
                try
                {
                    _logger.Log($"Processing file {file.Name}");

                    MoveFile(file, _configurationProvider.TempPath, $"{file.Name}_{DateTime.Now:ddMMyyyyHHmmss}");

                    using (var streamReader = new StreamReader(file.FullName))
                    {
                        await _streamProcessingService.ProcessAsync(streamReader);
                    }

                    _logger.Log($"Finished processing file {file.Name}");

                    MoveFile(file, _configurationProvider.ProcessedPath, file.Name);
                }
                catch (Exception ex)
                {
                    _logger.Log($"ERROR: {ex.Message}");

                    MoveFile(file, _configurationProvider.FailedPath, file.Name);
                }
            }
        }

        private void Init()
        {
            Directory.CreateDirectory(_configurationProvider.ListeningPath);
            Directory.CreateDirectory(_configurationProvider.TempPath);
            Directory.CreateDirectory(_configurationProvider.FailedPath);
            Directory.CreateDirectory(_configurationProvider.ProcessedPath);
        }

        private void MoveFile(FileInfo file, string path, string newName)
        {
            file.MoveTo(Path.Combine(path, newName));

            _logger.Log($"File {file.Name} moved into {path}");
        }
    }
}
