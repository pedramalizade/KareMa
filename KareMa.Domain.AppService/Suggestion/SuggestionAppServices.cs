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
        public async Task<bool> AcceptSuggestionAsync(int suggestionId, int orderId, CancellationToken cancellationToken)
        => await _suggestionServices.AcceptSuggestionAsync(suggestionId, orderId, cancellationToken);
        public async Task<bool> CreateAsync(SuggestionCreateDto suggestionCreateDto, string suggestionDate, CancellationToken cancellationToken)
        {
            var gregorianDate = _baseSevices.PersianToGregorianAsync(suggestionDate);
            suggestionCreateDto.SuggastionDate = gregorianDate;
            return await _suggestionServices.CreateAsync(suggestionCreateDto, cancellationToken);
        }
        public async Task<bool> DeleteAsync(int suggestionId, CancellationToken cancellationToken)
          => await _suggestionServices.DeleteAsync(suggestionId, cancellationToken);
        public async Task<List<Suggestion>> GetAllAsync(CancellationToken cancellationToken)
          => await _suggestionServices.GetAllAsync(cancellationToken);
        public async Task<Suggestion> GetByIdAsync(int suggestionId, CancellationToken cancellationToken)
          => await _suggestionServices.GetByIdAsync(suggestionId, cancellationToken);
        public async Task<SuggestionDto> GetSuggestionByIdAsync(int suggestionId, CancellationToken cancellationToken)
       => await _suggestionServices.GetSuggestionByIdAsync(suggestionId, cancellationToken);
        public async Task<List<SuggestionsByExpertIdDto>> GetSuggestionsByExperIdAsync(int id, CancellationToken cancellationToken)
        {
            var Suggestions = await _suggestionServices.GetSuggestionsByExperIdAsync(id, cancellationToken);
            var suggetionDates = Suggestions.Select(s => s.SuggestedDate).ToList();
            foreach (var item in Suggestions)
            {
                item.SuggestedDateString = (DateTime.Parse(item.SuggestedDate.ToString())).ToPersianString("yyyy/MM/dd");
            }
            return Suggestions;
        }
        public async Task<bool> UpdateAsync(SuggestionUpdateDto suggestionUpdateDto, CancellationToken cancellationToken)
          => await _suggestionServices.UpdateAsync(suggestionUpdateDto, cancellationToken);
    }
}
