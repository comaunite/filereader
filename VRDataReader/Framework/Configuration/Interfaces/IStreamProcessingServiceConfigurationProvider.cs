namespace VRDataReader.Framework.Configuration.Interfaces
{
    public interface IStreamProcessingServiceConfigurationProvider
    {
        int BatchSize { get; }

        string BoxLineIdentifier { get; }

        string ItemLineIdentifier { get; }
    }
}
