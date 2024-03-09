namespace VRServices.Services.Interfaces
{
    public interface IStreamProcessingService
    {
        Task ProcessAsync(StreamReader streamReader);
    }
}