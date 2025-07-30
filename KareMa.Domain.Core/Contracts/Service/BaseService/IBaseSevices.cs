namespace KareMa.Domain.Core.Contracts.Service.BaseService
{
    public interface IBaseSevices
    {
        DateTime PersianToGregorian(string persianDateString);
        Task<string> UploadImage(IFormFile image);
    }
}
