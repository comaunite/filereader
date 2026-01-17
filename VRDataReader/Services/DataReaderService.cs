using VRCore.Logger.Interfaces;
using VRDataReader.Framework.Configuration.Interfaces;
using VRDataReader.Services.Interfaces;

namespace VRDataReader.Services
{
    public class DataReaderService(IDataReaderServiceConfigurationProvider configurationProvider, IStreamProcessingService streamProcessingService,
        ILogger logger) : IDataReaderService
    {
        public async Task ReadAsync()
        {
            Init();

            var files = new DirectoryInfo(configurationProvider.ListeningPath).GetFiles("*");

            if (files.Length == 0)
            {
                logger.Log($"No files found in {configurationProvider.ListeningPath}");

                return;
            }

            logger.Log($"Found {files.Length} files in {configurationProvider.ListeningPath}");

            foreach (var file in files)
            {
                try
                {
                    logger.Log($"Processing file {file.Name}");

                    MoveFile(file, configurationProvider.TempPath, $"{file.Name}_{DateTime.Now:ddMMyyyyHHmmss}");

                    using (var streamReader = new StreamReader(file.FullName))
                    {
                        await streamProcessingService.ProcessAsync(streamReader);
                    }

                    logger.Log($"Finished processing file {file.Name}");

                    MoveFile(file, configurationProvider.ProcessedPath, file.Name);
                }
                catch (Exception ex)
                {
                    logger.Log($"ERROR: {ex.Message}");

                    MoveFile(file, configurationProvider.FailedPath, file.Name);
                }
            }
        }

        private void Init()
        {
            Directory.CreateDirectory(configurationProvider.ListeningPath);
            Directory.CreateDirectory(configurationProvider.TempPath);
            Directory.CreateDirectory(configurationProvider.FailedPath);
            Directory.CreateDirectory(configurationProvider.ProcessedPath);
        }

        private void MoveFile(FileInfo file, string path, string newName)
        {
            file.MoveTo(Path.Combine(path, newName));

            logger.Log($"File {file.Name} moved into {path}");
        }
    }
}
