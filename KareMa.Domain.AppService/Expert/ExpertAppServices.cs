namespace KareMa.Domain.AppService
{
    public class ExpertAppServices : IExpertAppServices
    {
        private readonly IExpertServices _expertServices;
        private readonly IBaseSevices _baseSevices;
        public ExpertAppServices(IExpertServices expertServices, IBaseSevices baseServce)
        {
            _expertServices = expertServices;
            _baseSevices = baseServce;
        }
        public async Task<bool> CreateAsync(ExpertCreateDto expertCreateDto, IFormFile Image, CancellationToken cancellationToken)
        {
            string imageAddress = null;
            if (Image != null)
            {
                imageAddress = await _baseSevices.UploadImage(Image);
                if (string.IsNullOrEmpty(imageAddress))
                {
                    Console.WriteLine("Image upload failed!");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("No image provided, proceeding without image.");
            }

            expertCreateDto.Image = imageAddress;
            try
            {
                return await _expertServices.Create(expertCreateDto, cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Create: {ex.Message}");
                throw;
            }
        }
        public async Task<bool> DeleteAsync(int expertId, CancellationToken cancellationToken)
          => await _expertServices.Delete(expertId, cancellationToken);
        public async Task<int> ExpertAverageScoresAsync(int id, CancellationToken cancellationToken)
          => await _expertServices.ExpertAverageScores(id, cancellationToken);
        public async Task<int> ExpertCommentCountAsync(int id, CancellationToken cancellationToken)
          => await _expertServices.ExpertCommentCount(id, cancellationToken);
        public async Task<int> ExpertCountAsync(CancellationToken cancellationToken)
          => await _expertServices.ExpertCount(cancellationToken);
        public async Task<int> ExpertOrderCountAsync(int id, CancellationToken cancellationToken)
          => await _expertServices.ExpertOrderCount(id, cancellationToken);
        public async Task<ExpertUpdateDto> ExpertUpdateInfoAsync(int id, CancellationToken cancellationToken)
       => await _expertServices.ExpertUpdateInfo(id, cancellationToken);
        public async Task<List<Expert>> GetAllAsync(CancellationToken cancellationToken)
          => await _expertServices.GetAll(cancellationToken);
        public async Task<ExpertNameDto> GetExpertNameAsync(int id, CancellationToken cancellationToken)
  => await _expertServices.GetExpertName(id, cancellationToken);
        public async Task<Expert> GetByIdAsync(int expertId, CancellationToken cancellationToken)
          => await _expertServices.GetById(expertId, cancellationToken);
        public async Task<ExpertSummaryDto> GetExpertSummaryAsync(int id, CancellationToken cancellationToken)
          => await _expertServices.GetExpertSummary(id, cancellationToken);
        public async Task<bool> UpdateAsync(ExpertUpdateDto expertUpdateDto, IFormFile? image, CancellationToken cancellationToken)
        {
            Console.WriteLine($"ExpertAppServices.Update started for ID: {expertUpdateDto.Id}");

            if (image != null)
            {
                var imageUrl = await _baseSevices.UploadImage(image);
                if (string.IsNullOrEmpty(imageUrl))
                    throw new Exception("آپلود تصویر ناموفق بود");
                expertUpdateDto.Image = imageUrl;
                Console.WriteLine($"Image uploaded successfully: {imageUrl}");
            }

            var result = await _expertServices.Update(expertUpdateDto, cancellationToken);
            if (!result)
                throw new Exception("به‌روزرسانی اطلاعات کارشناس ناموفق بود");

            Console.WriteLine("ExpertAppServices.Update completed successfully.");
            return true;
        }
        public async Task<Expert> GetExpertByIdAsync(int expertId, CancellationToken cancellationToken)
        => await _expertServices.GetExpertById(expertId, cancellationToken);
        public async Task UpdateBalanceAsync(int expertId, decimal newBalance, CancellationToken cancellationToken)
        => await _expertServices.UpdateBalance(expertId, newBalance, cancellationToken);
    }
}
