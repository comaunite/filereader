namespace VRDataReader.Services.Interfaces
{
    public interface IStreamProcessingService
    {
        Task ProcessAsync(StreamReader streamReader);
    }
}