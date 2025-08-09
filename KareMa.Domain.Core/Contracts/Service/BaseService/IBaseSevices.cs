namespace KareMa.Domain.Core.Contracts.Service.BaseService
{
    public interface IBaseSevices
    {
        DateTime PersianToGregorianAsync(string persianDateString);
        Task<string> UploadImage(IFormFile image);
    }
}
