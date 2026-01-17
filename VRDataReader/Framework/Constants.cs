// ReSharper disable InconsistentNaming
namespace VRDataReader.Framework
{
    public static class Constants
    {
        public const string LISTENING_DIR_CONFIG_KEY = "listeningDir";
        public const string TEMP_DIR_CONFIG_KEY = "tempDir";
        public const string FAILED_DIR_CONFIG_KEY = "failedDir";
        public const string PROCESSED_DIR_CONFIG_KEY = "processedDir";

        public const string BATCH_SIZE_CONFIG_KEY = "batchSize";
        public const int DEFAULT_BATCH_SIZE = 3;

        public const string BOX_LINE_IDENTIFIER = "HDR";
        public const string ITEM_LINE_IDENTIFIER = "LINE";
    }
}
