namespace KareMa.EndPoint.WebApi.Controllers
{
    using KareMa.Domain.Core.Contracts.AppService.WorkFlow;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;

    [Authorize(Roles = "Customer")]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrdersController : ControllerBase
    {
        private readonly IOrderAppServices _orderAppServices;
        private readonly ICustomerOrderWorkflowAppServices _workflow;
        private readonly IServiceAppServices _serviceAppServices;

        public CustomerOrdersController(
            IOrderAppServices orderAppServices,
            ICustomerOrderWorkflowAppServices workflow,
            IServiceAppServices serviceAppServices)
        {
            _orderAppServices = orderAppServices;
            _workflow = workflow;
            _serviceAppServices = serviceAppServices;
        }

        // ************* AddOrderModel معادل *************

        public class CreateOrderRequest
        {
            [Required]
            public OrderCreateDto Order { get; set; }

            [Required(ErrorMessage = "تاریخ نمی‌تواند بدون مقدار باشد")]
            [RegularExpression(@"^((1[34]\d{2}|140[0-3])/(0[1-9]|1[0-2])/(0[1-9]|[12]\d|3[01]) (2[0-3]|[01]\d):([0-5]\d):([0-5]\d))$",
                ErrorMessage = "فرمت تاریخ باید به صورت yyyy/mm/dd hh:mm:ss باشد.")]
            [StringLength(19, MinimumLength = 19, ErrorMessage = "تاریخ باید دقیقا 19 کاراکتر باشد")]
            public string Date { get; set; }

            public IFormFile? Image { get; set; }
        }

        /// <summary>
        /// گرفتن سرویس‌ها 
        /// </summary>
        [HttpGet("Services")]
        public async Task<IActionResult> GetServicesAsync(CancellationToken cancellationToken)
        {
            var services = await _serviceAppServices.GetServicesNameAsync(cancellationToken);
            return Ok(services);
        }

        /// <summary>
        /// ایجاد سفارش
        /// </summary>
        [HttpPost("AddOrder")]
        [RequestSizeLimit(10_000_000)] 
        public async Task<IActionResult> AddOrderAsync([FromForm] CreateOrderRequest model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userCustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userCustomerId")?.Value ?? "0");
            if (userCustomerId == 0)
                return Unauthorized("شناسه مشتری یافت نشد.");

            model.Order.CustomerId = userCustomerId;

            await _orderAppServices.CreateAsync(model.Order, model.Image, model.Date, cancellationToken);

            return Ok(new { Message = "سفارش با موفقیت ثبت شد." });
        }

        // ************* CustomerOrdersModel معادل *************

        /// <summary>
        /// گرفتن سفارش‌های مشتری
        /// </summary>
        [HttpGet("Orders")]
        public async Task<IActionResult> GetOrdersAsync(CancellationToken cancellationToken)
        {
            var userCustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userCustomerId")?.Value ?? "0");
            if (userCustomerId == 0)
                return Unauthorized("کاربر معتبر نیست.");

            var orders = await _orderAppServices.GetOrdersAsync(userCustomerId, cancellationToken);
            return Ok(orders);
        }

        public class AcceptSuggestionRequest
        {
            [Required]
            public int Id { get; set; } 
            [Required]
            public int OrderId { get; set; } 
        }

        /// <summary>
        /// تأیید پیشنهاد و انجام پرداخت (معادل OnPostAcceptSuggestionAsync)
        /// </summary>
        [HttpPost("AcceptSuggestion")]
        public async Task<IActionResult> AcceptSuggestionAsync([FromBody] AcceptSuggestionRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userCustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userCustomerId")?.Value ?? "0");
            if (userCustomerId == 0)
                return Unauthorized("کاربر معتبر نیست.");

            var result = await _workflow.AcceptSuggestionAndProcessPaymentAsync(
                request.Id, request.OrderId, userCustomerId, cancellationToken);

            if (!result.Success)
                return BadRequest(new { Error = result.ErrorMessage });

            return Ok(new { Message = "پیشنهاد با موفقیت تأیید و پرداخت انجام شد." });
        }
    }
}
