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

        [HttpGet]
        public async Task<ActionResult<List<GetCommentsDto>>> GetAll(CancellationToken cancellationToken)
        {
            var comments = await _commentAppServices.GetAllAsync(cancellationToken);
            return Ok(comments);
        }

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
