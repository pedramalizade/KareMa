using KareMa.Domain.Core.Enums;

namespace KareMa.EndPoint.WebApi.Controllers
{
    [Authorize(Roles = "Admin,Expert,Customer")]
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderAppServices _orderAppServices;

        public OrdersController(IOrderAppServices orderAppServices)
        {
            _orderAppServices = orderAppServices;
        }

        /// <summary>
        /// دریافت یک سفارش بر اساس شناسه
        /// </summary>
        /// <param name="id">شناسه سفارش</param>
        /// <param name="cancellationToken">توکن لغو عملیات</param>
        /// <returns>جزئیات سفارش یا پیام خطا در صورت عدم وجود</returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Order>> GetOrderById(int id, CancellationToken cancellationToken)
        {
            var order = await _orderAppServices.GetByIdAsync(id, cancellationToken);
            if (order == null)
                return NotFound(new { message = "سفارش مورد نظر یافت نشد!" });

            return Ok(order);
        }

        /// <summary>
        /// تغییر وضعیت یک سفارش
        /// </summary>
        /// <param name="id">شناسه سفارش</param>
        /// <param name="newStatus">وضعیت جدید سفارش</param>
        /// <param name="cancellationToken">توکن لغو عملیات</param>
        /// <returns>پیام موفقیت یا خطا</returns>
        [HttpPut("{id:int}/status")]
        public async Task<IActionResult> ChangeOrderStatus(int id, [FromBody] StatusEnum newStatus, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _orderAppServices.GetByIdAsync(id, cancellationToken);
                if (order == null)
                    return NotFound(new { message = "سفارش مورد نظر یافت نشد!" });

                await _orderAppServices.ChangeStatusAsync(newStatus, id, cancellationToken);
                return Ok(new { message = "وضعیت سفارش با موفقیت تغییر کرد!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"خطا در تغییر وضعیت سفارش: {ex.Message}" });
            }
        }

        /// <summary>
        /// دریافت لیست تمام سفارش‌ها
        /// </summary>
        /// <param name="cancellationToken">توکن لغو عملیات</param>
        /// <returns>لیست سفارش‌ها</returns>
        [HttpGet]
        public async Task<ActionResult<List<GetOrderDto>>> GetAll(CancellationToken cancellationToken)
        {
            var orders = await _orderAppServices.GetAllAsync(cancellationToken);
            return Ok(orders);
        }

        /// <summary>
        /// حذف یک سفارش بر اساس شناسه
        /// </summary>
        /// <param name="id">شناسه سفارش</param>
        /// <param name="cancellationToken">توکن لغو عملیات</param>
        /// <returns>پیام موفقیت یا خطا</returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOrder(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _orderAppServices.DeleteAsync(id, cancellationToken);
                return Ok(new { message = "سفارش با موفقیت حذف شد." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"خطا در حذف سفارش: {ex.Message}" });
            }
        }
    }
}
