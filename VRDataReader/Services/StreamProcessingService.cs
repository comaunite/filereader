using VRCore.Entities;
using VRCore.Interface;
using VRDatabase.Repositories.Interfaces;
using VRDataReader.Framework.Configuration.Interfaces;
using VRDataReader.Framework.Exceptions;
using VRDataReader.Services.Interfaces;

namespace VRDataReader.Services
{
    public class StreamProcessingService : IStreamProcessingService
    {
        private readonly IStreamProcessingServiceConfigurationProvider _configurationProvider;
        private readonly IBoxRepository _boxRepository;
        private readonly ILogger _logger;

        public StreamProcessingService(IStreamProcessingServiceConfigurationProvider configurationProvider, IBoxRepository boxRepository, ILogger logger)
        {
            _configurationProvider = configurationProvider;
            _boxRepository = boxRepository;
            _logger = logger;
        }

        public async Task ProcessAsync(StreamReader streamReader)
        {
            List<Box> boxBatch = [];

            var line = await streamReader.ReadLineAsync();

            while (!streamReader.EndOfStream)
            {
                if (line?.StartsWith(_configurationProvider.BoxLineIdentifier) ?? false)
                {
                    var box = InitBox(line);

                    if (!await _boxRepository.Exists(t =>
                        t.BoxIdentifier.Equals(box.BoxIdentifier)
                        && t.SupplierIdentifier.Equals(box.SupplierIdentifier)))
                    {
                        _logger.Log($"Found box {box.BoxIdentifier}... Reading contents...");

                        line = await streamReader.ReadLineAsync();

                        while (!streamReader.EndOfStream
                            && !(line?.StartsWith(_configurationProvider.BoxLineIdentifier) ?? false))
                        {
                            if (line?.StartsWith(_configurationProvider.ItemLineIdentifier) ?? false)
                            {
                                var item = InitItem(line);

                                _logger.Log($"Adding item {item.Isbn}...");

                                box.Items.Add(item);
                            }

                            line = await streamReader.ReadLineAsync();
                        }

                        boxBatch.Add(box);

                        if (boxBatch.Count >= _configurationProvider.BatchSize
                            || streamReader.EndOfStream)
                        {
                            _logger.Log($"Saving {boxBatch.Count} boxes into DB...");

                            await _boxRepository.BulkInsertAsync(boxBatch);

                            boxBatch = [];
                        }
                    }
                    else
                    {
                        _logger.Log($"WARN: Box {box.BoxIdentifier} already exists in DB, skipping...");
                    }
                }
            }
        }

        private static Box InitBox(string line)
        {
            var supplierIdentifier = line.Substring(5, 92).Trim();
            var boxIdentifier = line.Substring(96, 35).Trim();

            var box = new Box()
            {
                SupplierIdentifier = supplierIdentifier,
                BoxIdentifier = boxIdentifier
            };

            return box;
        }

        private static Item InitItem(string line)
        {
            var poNumber = line.Substring(5, 37).Trim();
            var isbn = line.Substring(42, 34).Trim();
            var quantity = line.Substring(76, 7).Trim();

            if (!int.TryParse(quantity, out int intQuantity))
            {
                throw new StreamReaderProcessingException($"Failed to parse item count for Line {isbn}");
            }

            var item = new Item()
            {
                PoNumber = poNumber,
                Isbn = isbn,
                Quantity = intQuantity
            };

            return item;
        }
    }
}
