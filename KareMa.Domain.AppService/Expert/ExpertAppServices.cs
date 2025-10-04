namespace KareMa.Domain.AppService
{
    public class ExpertAppServices : IExpertAppServices
    {
        private readonly IExpertServices _expertServices;
        private readonly IBaseSevices _baseSevices;
        public ExpertAppServices(
            IExpertServices expertServices,
            IBaseSevices baseServce)
        {
            _expertServices = expertServices;
            _baseSevices = baseServce;
        }

        public async Task<ExpertUpdateDto> GetExpertUpdateAsync(int expertId, CancellationToken cancellationToken)
        {
            var dto = await _expertServices.ExpertUpdateInfo(expertId, cancellationToken);
            return dto ?? new ExpertUpdateDto { Id = expertId };
        }

        public async Task<bool> UpdateProfileAsync(ExpertUpdateDto expertUpdateDto, IFormFile? image, string? birthDate, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(birthDate))
            {
                expertUpdateDto.BirthDate = ParsePersianBirthDate(birthDate);
            }

            if (image != null)
            {
                var imageUrl = await _baseSevices.UploadImage(image);
                if (string.IsNullOrEmpty(imageUrl))
                    throw new Exception("آپلود تصویر ناموفق بود");
                expertUpdateDto.Image = imageUrl;
            }
            var result = await _expertServices.Update(expertUpdateDto, cancellationToken);
            if (!result) throw new Exception("به‌روزرسانی اطلاعات کارشناس ناموفق بود");
            return true;
        }

        private DateTime ParsePersianBirthDate(string birthDate)
        {
            if (!Regex.IsMatch(birthDate, @"^\d{4}/\d{2}/\d{2}$"))
                throw new FormatException($"فرمت تاریخ '{birthDate}' اشتباه است؛ باید yyyy/MM/dd باشد.");

            var parts = birthDate.Split('/');
            var year = int.Parse(parts[0]);
            var month = int.Parse(parts[1]);
            var day = int.Parse(parts[2]);
            var persianCalendar = new PersianCalendar();

            return persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
        }

        public async Task<bool> CreateAsync(ExpertCreateDto expertCreateDto, IFormFile Image, CancellationToken cancellationToken)
        {
            string imageAddress = null;
            if (Image != null)
            {
                imageAddress = await _baseSevices.UploadImage(Image);
                if (string.IsNullOrEmpty(imageAddress))
                {
                    return false;
                }
            }
            else
            {
            }

            expertCreateDto.Image = imageAddress;
            try
            {
                return await _expertServices.Create(expertCreateDto, cancellationToken);
            }
            catch (Exception ex)
            {
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
            if (image != null)
            {
                var imageUrl = await _baseSevices.UploadImage(image);
                if (string.IsNullOrEmpty(imageUrl))
                    throw new Exception("آپلود تصویر ناموفق بود");
                expertUpdateDto.Image = imageUrl;
            }

            var result = await _expertServices.Update(expertUpdateDto, cancellationToken);
            if (!result)
                throw new Exception("به‌روزرسانی اطلاعات کارشناس ناموفق بود");

            return true;
        }
        public async Task<Expert> GetExpertByIdAsync(int expertId, CancellationToken cancellationToken)
        => await _expertServices.GetExpertById(expertId, cancellationToken);
        public async Task UpdateBalanceAsync(int expertId, decimal newBalance, CancellationToken cancellationToken)
        => await _expertServices.UpdateBalance(expertId, newBalance, cancellationToken);
    }
}
