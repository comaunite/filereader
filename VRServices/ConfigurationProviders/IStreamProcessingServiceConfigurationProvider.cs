namespace VRServices.ConfigurationProviders
{
    public interface IStreamProcessingServiceConfigurationProvider
    {
        int BatchSize { get; }

        string BoxLineIdentifier { get; }

        string ItemLineIdentifier { get; }
    }
}
