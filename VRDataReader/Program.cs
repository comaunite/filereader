// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using VRCore.Logger;
using VRCore.Logger.Interfaces;
using VRDatabase;
using VRDatabase.Interfaces;
using VRDatabase.Repositories;
using VRDatabase.Repositories.Interfaces;
using VRDataReader.Framework.Configuration;
using VRDataReader.Framework.Configuration.Interfaces;
using VRDataReader.Services;
using VRDataReader.Services.Interfaces;

Console.WriteLine("Starting the VR Data Reader...");
Console.WriteLine("=================================================================");

Console.WriteLine("Disclaimer: I decided to omit retry policy, as this wasn't really discussed in requirements and at files larger than memory size,");
Console.WriteLine("best bet is to probably delete all previous attempt results or prefetch all imported identifiers from DB, depending on the cost of keeping those in memory...");
Console.WriteLine("I also was a bit too lazy to implement UnitTests, but made structure reasonable enough to run them.");
Console.WriteLine("Generally speaking, I tried to optimize for memory footprint, so some 'nice' things like ORM had to go.");

Console.WriteLine("=================================================================");

ServiceCollection services = new();

services.AddScoped<IDbContext, DbContext>();
services.AddScoped<IBoxRepository, BoxRepository>();
services.AddScoped<IDataReaderServiceConfigurationProvider, DataReaderServiceConfigurationProvider>();
services.AddScoped<IStreamProcessingServiceConfigurationProvider, StreamProcessingServiceConfigurationProvider>();
services.AddScoped<IStreamProcessingService, StreamProcessingService>();
services.AddScoped<IDataReaderService, DataReaderService>();
services.AddScoped<ILogger, ConsoleLogger>();

await using (var provider = services.BuildServiceProvider())
{
    var readerService = provider.GetService<IDataReaderService>();

    await readerService?.ReadAsync()!;
}

Console.WriteLine("Finished");