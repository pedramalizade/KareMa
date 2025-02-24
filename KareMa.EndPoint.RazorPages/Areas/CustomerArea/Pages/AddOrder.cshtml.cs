using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.Contracts;
using KareMa.Domain.Core.DTOs.OrderDTO;
using KareMa.Domain.Core.DTOs.ServiceDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace KareMa.EndPoint.RazorPages.Areas.CustomerArea.Pages
{
    [Authorize(Roles = "Customer")]
    public class AddOrderModel : PageModel
    {
        private readonly IOrderAppServices _orderAppServices;
        private readonly IServiceAppServices _serviceAppServices;

        public AddOrderModel(IOrderAppServices orderAppServices, IServiceAppServices serviceAppServices)
        {
            _orderAppServices = orderAppServices;
            _serviceAppServices = serviceAppServices;
        }

        [BindProperty]
        public OrderCreateDto Order { get; set; }

        [BindProperty]
        public List<ServicesNameDto> Services { get; set; } = new List<ServicesNameDto>();

        [BindProperty]
        [Required(ErrorMessage = " عکس نمی‌تواند بدون مقدار باشد")]
        public IFormFile Image { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "تاریخ نمی‌تواند بدون مقدار باشد")]
        [RegularExpression("^((1[34]\\d{2}|140[0-3])\\/(0[1-9]|1[0-2])\\/(0[1-9]|[12]\\d|3[01]) (2[0-3]|[01]\\d):([0-5]\\d):([0-5]\\d))$", ErrorMessage = "فرمت تاریخ باید به صورت yyyy/mm/dd hh:mm:ss باشد.")]
        [Length(19, 19, ErrorMessage = "تاریخ نمی‌تواند کمتر یا بیشتر از 19 کاراکتر باشد")]
        public string Date { get; set; }

        public async Task OnGet(CancellationToken cancellationToken)
        {
            Services = await _serviceAppServices.GetServicesName(cancellationToken);
        }

        public async Task<IActionResult> OnPostAddOrder(OrderCreateDto order, CancellationToken cancellationToken, IFormFile image, string date)
        {
            if (ModelState.IsValid)
            {
                var userCustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userCustomerId").Value);
                order.CustomerId = userCustomerId;
                await _orderAppServices.Create(order, image, date, cancellationToken);
                return RedirectToAction("CustomerProfile-Orders");
            }
            return Page();
        }
    }
}
