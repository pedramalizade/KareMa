namespace KareMa.EndPoint.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KareMaController : ControllerBase
    {
        private readonly ISubCategoryAppServices _subCategoryAppService;
        private readonly IOrderAppServices _orderAppService;
        private readonly IAccountAppServices _accountAppService;
        public KareMaController(ISubCategoryAppServices subCategoryAppServices, IOrderAppServices orderAppServices, IAccountAppServices accountAppServices)
        {
            _accountAppService = accountAppServices;
            _orderAppService = orderAppServices;
            _subCategoryAppService = subCategoryAppServices;
        }

        /// <summary>
        /// دریافت لیست تمام زیر‌دسته‌ها همراه با سرویس‌ها
        /// </summary>
        /// <param name="cancellationToken">توکن لغو عملیات</param>
        /// <returns>لیست SubCategory</returns>
        [HttpGet]
        [Route(nameof(GetServiceSubCategoryWithServices))]
        public async Task<List<SubCategory>> GetServiceSubCategoryWithServices(CancellationToken cancellationToken)
        {
            var subCategories = await _subCategoryAppService.GetAllAsync(cancellationToken);
            return subCategories;
        }

        /// <summary>
        /// دریافت تمام سفارش‌ها (محافظت‌شده با ApiKey)
        /// </summary>
        /// <param name="cancellationToken">توکن لغو عملیات</param>
        /// <returns>لیست سفارش‌ها</returns>
        [HttpGet]
        [Route(nameof(GetOrders))]
        [ServiceFilter(typeof(ApiKeyAuthFilter))]
        public async Task<List<GetOrderDto>> GetOrders(CancellationToken cancellationToken)
        {
            var requests = await _orderAppService.GetAllAsync(cancellationToken);
            return requests;
        }

        /// <summary>
        /// ثبت‌نام کاربر جدید
        /// </summary>
        /// <param name="accountRegister">مدل اطلاعات ثبت‌نام</param>
        /// <returns>متن نتیجه ثبت‌نام</returns>
        [HttpPost]
        [Route(nameof(RegisterUser))]
        public async Task<string> RegisterUser(AccountRegisterDto accountRegister)
        {
            var result = await _accountAppService.Register(accountRegister);
            if (result.Count == 0)
            {
                return "ثبت نام انجام شد";
            }
            else
            {
                return "خطا در ثبت نام";
            }
        }
    }
}
