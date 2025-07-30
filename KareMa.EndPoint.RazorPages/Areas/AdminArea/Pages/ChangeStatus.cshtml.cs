namespace KareMa.EndPoint.RazorPages.Pages.Areas.AdminArea.Pages
{
    public class ChangeStatusModel : PageModel
    {
        private readonly IOrderAppServices _orderAppServices;

        public ChangeStatusModel(IOrderAppServices orderAppServices)
        {
            _orderAppServices = orderAppServices;
        }

        [BindProperty]
        public StatusEnum Status { get; set; }

        [BindProperty]
        public int OrderId { get; set; }

        [BindProperty]
        public Order Order { get; set; }

        public async Task<IActionResult> OnGet(int id, CancellationToken cancellationToken)
        {
            Order = await _orderAppServices.GetById(id, cancellationToken);

            if (Order == null)
            {
                TempData["ErrorMessage"] = "سفارش مورد نظر یافت نشد!";
                return RedirectToPage("Order");
            }

            OrderId = Order.Id;
            Status = Order.Status;

            return Page();
        }

        public async Task<IActionResult> OnPostChangeStatus(CancellationToken cancellationToken)
        {
            try
            {
                await _orderAppServices.ChangeStatus(Status, OrderId, cancellationToken);
                TempData["SuccessMessage"] = "وضعیت سفارش با موفقیت تغییر کرد!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "خطا در تغییر وضعیت سفارش: " + ex.Message;
            }

            return RedirectToPage("Order");
        }
    }
    //private readonly IOrderAppServices _orderAppServices;
    //public ChangeStatusModel(IOrderAppServices orderAppServices)
    //{
    //    _orderAppServices = orderAppServices;
    //}
    //[BindProperty]
    //public StatusEnum Status { get; set; }
    //[BindProperty]
    //public int OrderId { get; set; }
    //[BindProperty]
    //public Order Order { get; set; }
    //public async Task OnGet(int id, CancellationToken cancellationToken)
    //{
    //    Order = await _orderAppServices.GetById(id, cancellationToken);
    //}
    //public async Task<IActionResult> OnPostChangeStatus(StatusEnum status, int orderId, CancellationToken cancellationToken)
    //{
    //    await _orderAppServices.ChangeStatus(status, orderId, cancellationToken);
    //    return RedirectToPage("Order");
    //}
}
