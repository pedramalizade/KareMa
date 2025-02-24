using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.DTOs.CategoryDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KareMa.EndPoint.RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryAppServices _categoryAppServices;

        public IndexModel(ICategoryAppServices categoryAppServices)
        {
            _categoryAppServices = categoryAppServices;
        }

        [BindProperty]
        public List<CategoryNameDto> CategoryNames { get; set; } = new List<CategoryNameDto>();

        public async Task OnGet(CancellationToken cancellationToken)
        {
            CategoryNames = await _categoryAppServices.GetCategorisName(cancellationToken);
        }
    }
}
