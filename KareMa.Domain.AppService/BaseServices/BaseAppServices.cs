namespace KareMa.Domain.AppService.BaseAppServices
{
    public class BaseAppServices : IBaseAppServices
    {
        private readonly IBaseSevices _baseSevices;
        public BaseAppServices(IBaseSevices baseSevices)
        {
            _baseSevices = baseSevices;
        }
        public async Task<string> UploadImageAsync(IFormFile image)
          => await _baseSevices.UploadImage(image);
    }

}
