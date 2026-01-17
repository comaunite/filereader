using VRCore.Entities;
using VRCore.Logger.Interfaces;
using VRDatabase.Repositories.Interfaces;
using VRDataReader.Framework.Configuration.Interfaces;
using VRDataReader.Framework.Exceptions;
using VRDataReader.Services.Interfaces;

namespace VRDataReader.Services
{
    public class StreamProcessingService(IStreamProcessingServiceConfigurationProvider configurationProvider, IBoxRepository boxRepository, ILogger logger)
        : IStreamProcessingService
    {
        public async Task ProcessAsync(StreamReader streamReader)
        {
            var boxBatch = new List<Box>(configurationProvider.BatchSize);
            var totalBoxCounter = 0;

            var line = await streamReader.ReadLineAsync();

            while (line != null)
            {
                var lineSpan = line.AsSpan();

                if (lineSpan.StartsWith(configurationProvider.BoxLineIdentifier))
                {
                    var box = InitBox(lineSpan);

                    boxBatch.Add(box);

                    logger.Log($"Found box #{++totalBoxCounter} by Id {box.BoxIdentifier}... Reading contents...");


                    // Read box items
                    line = await streamReader.ReadLineAsync();

                    while (line != null && !line.StartsWith(configurationProvider.BoxLineIdentifier))
                    {
                        var itemSpan = line.AsSpan();

                        if (itemSpan.StartsWith(configurationProvider.ItemLineIdentifier))
                        {
                            var item = InitItem(itemSpan);

                            logger.Log($"Adding item {item.Isbn}...");

                            box.Items.Add(item);
                        }

                        line = await streamReader.ReadLineAsync();
                    }

                    if (boxBatch.Count >= configurationProvider.BatchSize || line == null)
                    {
                        await SaveChangesAsync(boxBatch);
                    }

                    continue;
                }

                // Not a box, keep reading
                line = await streamReader.ReadLineAsync();
            }
        }

        private async Task SaveChangesAsync(List<Box> boxBatch)
        {
            if (boxBatch.Count == 0)
                return;

            logger.Log($"Saving {boxBatch.Count} boxes into DB...");

            await boxRepository.BulkInsertAsync(boxBatch);

            boxBatch.Clear();
        }

        private static Box InitBox(ReadOnlySpan<char> line)
        {
            return new Box
            {
                SupplierIdentifier = line.Slice(5, 92).Trim().ToString(),
                BoxIdentifier = line.Slice(96, 35).Trim().ToString()
            };
        }

        private static Item InitItem(ReadOnlySpan<char> line)
        {
            var isbnSpan = line.Slice(42, 34).Trim();
            var quantitySpan = line.Slice(76, 7).Trim();

            if (!int.TryParse(quantitySpan, out var quantity))
            {
                throw new StreamReaderProcessingException($"Failed to parse item count for Line {isbnSpan}");
            }

            var item = new Item
            {
                PoNumber = line.Slice(5, 37).Trim().ToString(),
                Isbn = isbnSpan.ToString(),
                Quantity = quantity
            };

            return item;
        }
    }
}
