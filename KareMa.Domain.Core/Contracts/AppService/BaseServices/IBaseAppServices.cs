using Microsoft.AspNetCore.Http;
namespace KareMa.Domain.Core.Contracts.AppService.BaseServices
{
    public interface IBaseAppServices
    {
        Task<string> UploadImage(IFormFile image);
    }

}
