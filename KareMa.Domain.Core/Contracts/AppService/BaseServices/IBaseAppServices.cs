namespace KareMa.Domain.Core.Contracts.AppService.BaseServices
{
    public interface IBaseAppServices
    {
        Task<string> UploadImageAsync(IFormFile image);
    }

}
