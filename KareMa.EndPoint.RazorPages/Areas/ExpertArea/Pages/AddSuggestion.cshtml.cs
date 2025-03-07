using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.DTOs.SuggestionDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace KareMa.EndPoint.RazorPages.Areas.ExpertArea.Pages
{
    [Authorize(Roles = "Expert")]
    public class AddSuggestionModel : PageModel
    {
        private readonly ISuggestionAppServices _suggestionAppServices;

        public AddSuggestionModel(ISuggestionAppServices suggestionAppServices)
        {
            _suggestionAppServices = suggestionAppServices;
        }

        [BindProperty]
        public SuggestionCreateDto SuggestionCreate { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "تاریخ نمی‌تواند بدون مقدار باشد")]
        [RegularExpression("^((1[34]\\d{2}|140[0-3])\\/(0[1-9]|1[0-2])\\/(0[1-9]|[12]\\d|3[01]) (2[0-3]|[01]\\d):([0-5]\\d):([0-5]\\d))$", ErrorMessage = "فرمت تاریخ باید به صورت yyyy/mm/dd hh:mm:ss باشد.")]
        [Length(19, 19, ErrorMessage = "تاریخ نمی‌تواند کمتر یا بیشتر از 19 کاراکتر باشد")]
        public string SuggestionDate { get; set; }

        [BindProperty]
        public int OrderId { get; set; }

        public async Task OnGet(int Id, CancellationToken cancellationToken)
        {
            OrderId = Id;
        }

        public async Task<IActionResult> OnPostCreateSuggestion(SuggestionCreateDto suggestionCreate, string suggestionDate, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var expertId = int.Parse(User.Claims.FirstOrDefault(u => u.Type == "userExpertId").Value);
                suggestionCreate.ExpertId = expertId;
                await _suggestionAppServices.Create(suggestionCreate, suggestionDate, cancellationToken);

                return RedirectToPage("ExpertSuggestions");
            }
            return RedirectToAction("OnGet");
        }
    }
}
