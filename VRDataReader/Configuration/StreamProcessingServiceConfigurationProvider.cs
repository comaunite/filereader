using System.Configuration;
using VRServices.ConfigurationProviders;

namespace VRDataReader.Configuration
{
    public class StreamProcessingServiceConfigurationProvider : IStreamProcessingServiceConfigurationProvider
    {
        public int BatchSize
        {
            get
            {
                if (int.TryParse(ConfigurationManager.AppSettings[Constants.BATCH_SIZE_CONFIG_KEY], out int result))
                {
                    return result;
                }

                return Constants.DEFAULT_BATCH_SIZE;
            }
        }

        public string BoxLineIdentifier => Constants.BOX_LINE_IDENTIFIER;

        public string ItemLineIdentifier => Constants.ITEM_LINE_IDENTIFIER;
    }
}
