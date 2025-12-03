using KareMa.Domain.Core.DTOs.CommentDTO;

namespace KareMa.EndPoint.WebApi.Controllers
{
    [Authorize(Roles = "Admin,Expert")]
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentAppServices _commentAppServices;

        public CommentsController(ICommentAppServices commentAppServices)
        {
            _commentAppServices = commentAppServices;
        }

        /// <summary>
        /// دریافت تمام کامنت‌ها.
        /// </summary>
        /// <param name="cancellationToken">توکن لغو عملیات.</param>
        /// <returns>لیست کامل کامنت‌ها.</returns>
        [HttpGet]
        public async Task<ActionResult<List<GetCommentsDto>>> GetAll(CancellationToken cancellationToken)
        {
            var comments = await _commentAppServices.GetAllAsync(cancellationToken);
            return Ok(comments);
        }

        /// <summary>
        /// تأیید یک کامنت بر اساس شناسه.
        /// </summary>
        /// <param name="id">شناسه کامنت.</param>
        /// <param name="cancellationToken">توکن لغو عملیات.</param>
        /// <returns>پیام موفقیت یا خطا.</returns>
        [HttpPut("{id:int}/accept")]
        public async Task<IActionResult> AcceptComment(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _commentAppServices.AcceptCommentAsync(id, cancellationToken);
                return Ok(new { message = "کامنت با موفقیت پذیرفته شد." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"خطا در پذیرش کامنت: {ex.Message}" });
            }
        }

        /// <summary>
        /// رد یک کامنت بر اساس شناسه.
        /// </summary>
        /// <param name="id">شناسه کامنت.</param>
        /// <param name="cancellationToken">توکن لغو عملیات.</param>
        /// <returns>پیام موفقیت یا خطا.</returns>
        [HttpPut("{id:int}/reject")]
        public async Task<IActionResult> RejectComment(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _commentAppServices.RejectCommentAsync(id, cancellationToken);
                return Ok(new { message = "کامنت با موفقیت رد شد." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"خطا در رد کامنت: {ex.Message}" });
            }
        }

        /// <summary>
        /// حذف یک کامنت بر اساس شناسه.
        /// </summary>
        /// <param name="id">شناسه کامنت.</param>
        /// <param name="cancellationToken">توکن لغو عملیات.</param>
        /// <returns>پیام موفقیت یا خطا.</returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteComment(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _commentAppServices.DeleteAsync(id, cancellationToken);
                return Ok(new { message = "کامنت با موفقیت حذف شد." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"خطا در حذف کامنت: {ex.Message}" });
            }
        }
    }
}
