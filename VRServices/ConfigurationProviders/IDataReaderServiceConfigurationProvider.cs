namespace VRServices.ConfigurationProviders
{
    public interface IDataReaderServiceConfigurationProvider
    {
        string ListeningPath { get; }
        string TempPath { get; }
        string FailedPath { get; }
        string ProcessedPath { get; }
    }
}
