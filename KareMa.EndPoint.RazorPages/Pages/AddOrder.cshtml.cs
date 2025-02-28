using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.Contracts;
using KareMa.Domain.Core.DTOs.OrderDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using KareMa.Domain.Core.DTOs.ServiceDTO;

namespace KareMa.EndPoint.RazorPages.Pages
{
    [Authorize(Roles = "Customer")]
    public class AddOrderModel : PageModel
    {
        private readonly IServiceAppServices _serviceAppServices;
        private readonly IOrderAppServices _orderAppServices;

        public AddOrderModel(IServiceAppServices serviceAppServices, IOrderAppServices orderAppServices)
        {
            _serviceAppServices = serviceAppServices;
            _orderAppServices = orderAppServices;
        }

        [BindProperty]
        public ServiceNameAndPriceDto Service { get; set; }

        [BindProperty]
        public OrderCreateDto Order { get; set; }

        [BindProperty]
        [Required(ErrorMessage = " عکس نمی‌تواند بدون مقدار باشد")]
        public IFormFile Image { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "تاریخ نمی‌تواند بدون مقدار باشد")]
        [RegularExpression("^((1[34]\\d{2}|140[0-3])\\/(0[1-9]|1[0-2])\\/(0[1-9]|[12]\\d|3[01]) (2[0-3]|[01]\\d):([0-5]\\d):([0-5]\\d))$", ErrorMessage = "فرمت تاریخ باید به صورت yyyy/mm/dd hh:mm:ss باشد.")]
        [Length(19, 19, ErrorMessage = "تاریخ نمی‌تواند کمتر یا بیشتر از 19 کاراکتر باشد")]
        public string Date { get; set; }
        public async Task OnGet(int id, CancellationToken cancellationToken)
        {
            Service = await _serviceAppServices.GetServiceNameAndPrice(id, cancellationToken);
        }

        public async Task<IActionResult> OnPostAdd(OrderCreateDto order, CancellationToken cancellationToken, IFormFile image, string date)
        {
            ModelState.Remove("Name");
            if (ModelState.IsValid)
            {
                var userCustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userCustomerId").Value);
                order.CustomerId = userCustomerId;
                await _orderAppServices.Create(order, image, date, cancellationToken);
                return LocalRedirect("/CustomerArea/CustomerOrders");
            }
            return RedirectToAction("OnGet");
        }
    }

}
