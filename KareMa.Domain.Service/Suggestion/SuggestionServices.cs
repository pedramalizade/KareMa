namespace KareMa.Domain.Service
{
    public class SuggestionServices : ISuggestionServices
    {
        private readonly ISuggestionRepository _suggestionRepository;

        public SuggestionServices(ISuggestionRepository suggestionRepository)
        {
            _suggestionRepository = suggestionRepository;
        }
        /// <summary>
        /// پذیرش یک پیشنهاد و مرتبط کردن آن با سفارش
        /// </summary>
        public async Task<bool> AcceptSuggestionAsync(int suggestionId, int orderId, CancellationToken cancellationToken)
        => await _suggestionRepository.AcceptSuggestionAsync(suggestionId, orderId, cancellationToken);
        /// <summary>
        /// تغییر وضعیت سفارش مرتبط با پیشنهاد
        /// </summary>
        public async Task<bool> ChangeStatusAsync(StatusEnum status, int orderId, CancellationToken cancellationToken)
          => await _suggestionRepository.ChangeStatusAsync(status, orderId, cancellationToken);

        /// <summary>
        /// شمارش پیشنهادهای تایید شده برای یک سفارش
        /// </summary>
        public async Task<int> ConfrimedStatusCountAsync(int orderId, CancellationToken cancellationToken)
          => await _suggestionRepository.ConfrimedStatusCountAsync(orderId, cancellationToken);
        /// <summary>
        /// ایجاد یک پیشنهاد جدید
        /// </summary>
        public async Task<bool> CreateAsync(SuggestionCreateDto suggestionCreateDto, CancellationToken cancellationToken)
          => await _suggestionRepository.CreateAsync(suggestionCreateDto, cancellationToken);
        /// <summary>
        /// حذف یک پیشنهاد بر اساس شناسه
        /// </summary>
        public async Task<bool> DeleteAsync(int suggestionId, CancellationToken cancellationToken)
          => await _suggestionRepository.DeleteAsync(suggestionId, cancellationToken);

        /// <summary>
        /// علامت‌گذاری پیشنهاد به عنوان انجام شده
        /// </summary>
        public async Task DoneSuggestionAsync(int suggestionId, CancellationToken cancellationToken)
          => await _suggestionRepository.DoneSuggestionAsync(suggestionId, cancellationToken);
        /// <summary>
        /// دریافت تمام پیشنهادها
        /// </summary>
        public async Task<List<Suggestion>> GetAllAsync(CancellationToken cancellationToken)
          => await _suggestionRepository.GetAllAsync(cancellationToken);
        /// <summary>
        /// دریافت یک پیشنهاد بر اساس شناسه
        /// </summary>
        public async Task<Suggestion> GetByIdAsync(int suggestionId, CancellationToken cancellationToken)
          => await _suggestionRepository.GetByIdAsync(suggestionId, cancellationToken);
        /// <summary>
        /// دریافت یک پیشنهاد به صورت DTO بر اساس شناسه
        /// </summary>
        public async Task<SuggestionDto> GetSuggestionByIdAsync(int suggestionId, CancellationToken cancellationToken)
       => await _suggestionRepository.GetSuggestionByIdAsync(suggestionId, cancellationToken);
        /// <summary>
        /// دریافت تمام پیشنهادهای یک متخصص بر اساس شناسه
        /// </summary>
        public async Task<List<SuggestionsByExpertIdDto>> GetSuggestionsByExpertIdAsync(int expertId, CancellationToken cancellationToken)
          => await _suggestionRepository.GetSuggestionsByExpertIdAsync(expertId, cancellationToken);

        /// <summary>
        /// بروزرسانی یک پیشنهاد
        /// </summary>
        public async Task<bool> UpdateAsync(SuggestionUpdateDto suggestionUpdateDto, CancellationToken cancellationToken)
          => await _suggestionRepository.UpdateAsync(suggestionUpdateDto, cancellationToken);
    }
}
