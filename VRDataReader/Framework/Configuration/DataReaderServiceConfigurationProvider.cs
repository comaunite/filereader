using System.Configuration;
using VRDataReader.Framework.Configuration.Interfaces;
using VRDataReader.Framework.Exceptions;

namespace VRDataReader.Framework.Configuration
{
    public class DataReaderServiceConfigurationProvider : IDataReaderServiceConfigurationProvider
    {
        private readonly string? _listeningPath = ConfigurationManager.AppSettings[Constants.LISTENING_DIR_CONFIG_KEY];
        private readonly string? _tempPath = ConfigurationManager.AppSettings[Constants.TEMP_DIR_CONFIG_KEY];
        private readonly string? _failedPath = ConfigurationManager.AppSettings[Constants.FAILED_DIR_CONFIG_KEY];
        private readonly string? _processedPath = ConfigurationManager.AppSettings[Constants.PROCESSED_DIR_CONFIG_KEY];

        public string ListeningPath
        {
            get
            {
                if (string.IsNullOrEmpty(_listeningPath))
                {
                    throw new DataReaderConfigurationException($"Missing configuration key for {Constants.LISTENING_DIR_CONFIG_KEY}");
                }

                return _listeningPath;
            }
        }

        public string TempPath
        {
            get
            {
                if (string.IsNullOrEmpty(_tempPath))
                {
                    throw new DataReaderConfigurationException($"Missing configuration key for {Constants.TEMP_DIR_CONFIG_KEY}");
                }

                return _tempPath;
            }
        }

        public string FailedPath
        {
            get
            {
                if (string.IsNullOrEmpty(_failedPath))
                {
                    throw new DataReaderConfigurationException($"Missing configuration key for {Constants.FAILED_DIR_CONFIG_KEY}");
                }

                return _failedPath;
            }
        }

        public string ProcessedPath
        {
            get
            {
                if (string.IsNullOrEmpty(_processedPath))
                {
                    throw new DataReaderConfigurationException($"Missing configuration key for {Constants.PROCESSED_DIR_CONFIG_KEY}");
                }

                return _processedPath;
            }
        }
    }
}
