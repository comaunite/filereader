using System.Text;
using Moq;
using VRCore.Entities;
using VRCore.Logger.Interfaces;
using VRDatabase.Repositories.Interfaces;
using VRDataReader.Framework.Configuration.Interfaces;
using VRDataReader.Services;

namespace VRDataReaderTests
{
    public class UnitTests
    {
        [Fact(Skip = "Fake test to analyze memory consumption during development")]
        public async Task StressTest()
        {
            var configurationProvider = new Mock<IStreamProcessingServiceConfigurationProvider>(MockBehavior.Strict);
            configurationProvider.Setup(x => x.BoxLineIdentifier).Returns("HDR");
            configurationProvider.Setup(x => x.ItemLineIdentifier).Returns("LINE");
            configurationProvider.Setup(x => x.BatchSize).Returns(3);

            var boxRepository = new Mock<IBoxRepository>(MockBehavior.Strict);
            boxRepository.Setup(x => x.BulkInsertAsync(It.IsAny<IList<Box>>())).Returns(Task.FromResult(false));

            var logger = new Mock<ILogger>(MockBehavior.Strict);
            logger.Setup(x => x.Log(It.IsAny<string>()));


            var service = new StreamProcessingService(configurationProvider.Object, boxRepository.Object, logger.Object);

            var builder = new StringBuilder();

            var randomizer = new Random();

            for (int i = 0; i < 1_000; i++)
            {
                var rnd1 = randomizer.Next(100_000, 999_999);
                var rnd2 = randomizer.Next(100_000, 999_999);

                builder.AppendLine($"HDR  T{rnd1}                                                                                     68{rnd2}                           ");
                builder.AppendLine();

                for (int j = 0; j < 10; j++)
                {
                    var rndL1 = randomizer.Next(100_000, 999_999);
                    var rndL2 = randomizer.Next(100_000, 999_999);
                    var rndL3 = randomizer.Next(1, 9);

                    builder.AppendLine($"LINE G000{rndL1}                           9781473{rndL2}                     {rndL3}      ");
                    builder.AppendLine();
                }
            }

            var fakeFileContents = builder.ToString();

            //File.WriteAllText(@"C:\Temp\data_long.txt", fakeFileContents);

            var fakeFileBytes = Encoding.UTF8.GetBytes(fakeFileContents);

            var fakeMemoryStream = new MemoryStream(fakeFileBytes);

            using (var streamReader = new StreamReader(fakeMemoryStream))
            {
                await service.ProcessAsync(streamReader);
            }

            Assert.NotNull("Crazy stuff!");
        }
    }
}