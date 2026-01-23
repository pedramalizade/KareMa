namespace KareMa.Domain.AppService
{
    public class SuggestionAppServices : ISuggestionAppServices
    {
        private readonly ISuggestionServices _suggestionServices;
        private readonly IOrderServices _orderServices;
        private readonly IBaseSevices _baseSevices;

        public SuggestionAppServices(ISuggestionServices suggestionServices, IOrderServices orderServices, IBaseSevices baseSevices)
        {
            _suggestionServices = suggestionServices;
            _orderServices = orderServices;
            _baseSevices = baseSevices;
        }
        /// <summary>
        /// پذیرش یک پیشنهاد بر اساس شناسه پیشنهاد و سفارش.
        /// </summary>
        public async Task<bool> AcceptSuggestionAsync(int suggestionId, int orderId, CancellationToken cancellationToken)
            => await _suggestionServices.AcceptSuggestionAsync(suggestionId, orderId, cancellationToken);

        /// <summary>
        /// ایجاد یک پیشنهاد جدید با تاریخ مشخص.
        /// </summary>
        public async Task<bool> CreateAsync(SuggestionCreateDto suggestionCreateDto, string suggestionDate, CancellationToken cancellationToken)
        {
            var gregorianDate = _baseSevices.PersianToGregorianAsync(suggestionDate);
            suggestionCreateDto.SuggastionDate = gregorianDate;
            return await _suggestionServices.CreateAsync(suggestionCreateDto, cancellationToken);
        }

        /// <summary>
        /// حذف یک پیشنهاد بر اساس شناسه.
        /// </summary>
        public async Task<bool> DeleteAsync(int suggestionId, CancellationToken cancellationToken)
            => await _suggestionServices.DeleteAsync(suggestionId, cancellationToken);

        /// <summary>
        /// دریافت همه پیشنهادها.
        /// </summary>
        public async Task<List<Suggestion>> GetAllAsync(CancellationToken cancellationToken)
            => await _suggestionServices.GetAllAsync(cancellationToken);

        /// <summary>
        /// دریافت پیشنهاد بر اساس شناسه.
        /// </summary>
        public async Task<Suggestion> GetByIdAsync(int suggestionId, CancellationToken cancellationToken)
            => await _suggestionServices.GetByIdAsync(suggestionId, cancellationToken);

        /// <summary>
        /// دریافت جزئیات پیشنهاد بر اساس شناسه.
        /// </summary>
        public async Task<SuggestionDto> GetSuggestionByIdAsync(int suggestionId, CancellationToken cancellationToken)
            => await _suggestionServices.GetSuggestionByIdAsync(suggestionId, cancellationToken);

        /// <summary>
        /// دریافت همه پیشنهادهای یک کارشناس با فرمت تاریخ شمسی.
        /// </summary>
        public async Task<List<SuggestionsByExpertIdDto>> GetSuggestionsByExpertIdAsync(int expertId, CancellationToken cancellationToken)
        {
            var Suggestions = await _suggestionServices.GetSuggestionsByExpertIdAsync(expertId, cancellationToken);
            foreach (var item in Suggestions)
            {
                item.SuggestedDateString = (DateTime.Parse(item.SuggestedDate.ToString())).ToPersianString("yyyy/MM/dd");
            }
            return Suggestions;
        }

        /// <summary>
        /// بروزرسانی یک پیشنهاد.
        /// </summary>
        public async Task<bool> UpdateAsync(SuggestionUpdateDto suggestionUpdateDto, CancellationToken cancellationToken)
            => await _suggestionServices.UpdateAsync(suggestionUpdateDto, cancellationToken);
    }
}
