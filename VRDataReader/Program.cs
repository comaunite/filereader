// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using VRCore.Loggers;
using VRCore.Loggers.Interface;
using VRDatabase;
using VRDatabase.Interfaces;
using VRDatabase.Repositories;
using VRDatabase.Repositories.Interfaces;
using VRDataReader.Configuration;
using VRServices.ConfigurationProviders;
using VRServices.Services;
using VRServices.Services.Interfaces;

Console.WriteLine("Starting the VR Data Reader...");
Console.WriteLine("=================================================================");

ServiceCollection services = new();

services.AddScoped<IDbContext, DbContext>();
services.AddScoped<IBoxRepository, BoxRepository>();
services.AddScoped<IDataReaderServiceConfigurationProvider, DataReaderServiceConfigurationProvider>();
services.AddScoped<IStreamProcessingServiceConfigurationProvider, StreamProcessingServiceConfigurationProvider>();
services.AddScoped<IStreamProcessingService, StreamProcessingService>();
services.AddScoped<IDataReaderService, DataReaderService>();
services.AddScoped<ILogger, ConsoleLogger>();

using (var provider = services.BuildServiceProvider())
{
    var readerService = provider.GetService<IDataReaderService>();

    await readerService.ReadAsync();
}

Console.WriteLine("Finished");