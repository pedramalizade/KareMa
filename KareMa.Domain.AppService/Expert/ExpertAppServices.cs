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

        public async Task<bool> Create(ExpertCreateDto expertCreateDto, IFormFile Image, CancellationToken cancellationToken)
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

        public async Task<bool> Delete(int expertId, CancellationToken cancellationToken)
          => await _expertServices.Delete(expertId, cancellationToken);

        public async Task<int> ExpertAverageScores(int id, CancellationToken cancellationToken)
          => await _expertServices.ExpertAverageScores(id, cancellationToken);

        public async Task<int> ExpertCommentCount(int id, CancellationToken cancellationToken)
          => await _expertServices.ExpertCommentCount(id, cancellationToken);

        public async Task<int> ExpertCount(CancellationToken cancellationToken)
          => await _expertServices.ExpertCount(cancellationToken);

        public async Task<int> ExpertOrderCount(int id, CancellationToken cancellationToken)
          => await _expertServices.ExpertOrderCount(id, cancellationToken);

        public async Task<ExpertUpdateDto> ExpertUpdateInfo(int id, CancellationToken cancellationToken)
       => await _expertServices.ExpertUpdateInfo(id, cancellationToken);

        public async Task<List<Expert>> GetAll(CancellationToken cancellationToken)
          => await _expertServices.GetAll(cancellationToken);
        public async Task<ExpertNameDto> GetExpertName(int id, CancellationToken cancellationToken)
  => await _expertServices.GetExpertName(id, cancellationToken);
        public async Task<Expert> GetById(int expertId, CancellationToken cancellationToken)
          => await _expertServices.GetById(expertId, cancellationToken);

        public async Task<ExpertSummaryDto> GetExpertSummary(int id, CancellationToken cancellationToken)
          => await _expertServices.GetExpertSummary(id, cancellationToken);

        public async Task<bool> Update(ExpertUpdateDto expertUpdateDto, IFormFile? image, CancellationToken cancellationToken)
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

        public async Task<Expert> GetExpertById(int expertId, CancellationToken cancellationToken)
        => await _expertServices.GetExpertById(expertId, cancellationToken);

        public async Task UpdateBalance(int expertId, decimal newBalance, CancellationToken cancellationToken)
        => await _expertServices.UpdateBalance(expertId, newBalance, cancellationToken);
    }
}
