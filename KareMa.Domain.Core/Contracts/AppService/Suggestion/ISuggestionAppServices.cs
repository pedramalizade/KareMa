namespace KareMa.Domain.Core.Contracts.AppService
{
    public interface ISuggestionAppServices
    {
        /// <summary>
        /// ایجاد پیشنهاد جدید با تاریخ.
        /// </summary>
        Task<bool> CreateAsync(SuggestionCreateDto suggestionCreateDto, string suggestionDate, CancellationToken cancellationToken);

        /// <summary>
        /// بروزرسانی پیشنهاد.
        /// </summary>
        Task<bool> UpdateAsync(SuggestionUpdateDto suggestionUpdateDto, CancellationToken cancellationToken);

        /// <summary>
        /// حذف پیشنهاد بر اساس شناسه.
        /// </summary>
        Task<bool> DeleteAsync(int suggestionId, CancellationToken cancellationToken);

        /// <summary>
        /// دریافت پیشنهاد بر اساس شناسه.
        /// </summary>
        Task<Entities.Suggestion> GetByIdAsync(int suggestionId, CancellationToken cancellationToken);

        /// <summary>
        /// دریافت همه پیشنهادها.
        /// </summary>
        Task<List<Entities.Suggestion>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// پذیرش پیشنهاد و مرتبط کردن با سفارش.
        /// </summary>
        Task<bool> AcceptSuggestionAsync(int suggestionId, int orderId, CancellationToken cancellationToken);

        /// <summary>
        /// دریافت پیشنهادها بر اساس شناسه کارشناس.
        /// </summary>
        Task<List<SuggestionsByExpertIdDto>> GetSuggestionsByExperIdAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// دریافت جزئیات پیشنهاد بر اساس شناسه.
        /// </summary>
        Task<SuggestionDto> GetSuggestionByIdAsync(int suggestionId, CancellationToken cancellationToken);
    }
}
