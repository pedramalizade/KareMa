using KareMa.Domain.Core.Contracts.AppService;
using KareMa.Domain.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace KareMa.EndPoint.RazorPages.Areas.AdminArea.Pages
{
    [Authorize(Roles = "Admin")]
    public class AddCustomerModel : PageModel
    {
        private readonly ICustomerAppServices _customerAppServices;
        private readonly ICityAppServices _cityService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<AddCustomerModel> _logger;

        public AddCustomerModel(ICustomerAppServices customerAppServices, ICityAppServices cityService, UserManager<AppUser> userManager, ILogger<AddCustomerModel> logger)
        {
            _customerAppServices = customerAppServices ?? throw new ArgumentNullException(nameof(customerAppServices));
            _cityService = cityService ?? throw new ArgumentNullException(nameof(cityService));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [BindProperty]
        public CustomerCreateDto CustomerCreate { get; set; } = new CustomerCreateDto();

        [BindProperty]
        public IFormFile? Image { get; set; }

        public SelectList Cities { get; set; }

        public async Task OnGet(CancellationToken cancellationToken)
        {
            Console.WriteLine("OnGet called.");
            _logger.LogInformation("OnGet called.");
            CustomerCreate.Addresses = new Address();
            var cities = await _cityService.GetAll(cancellationToken);
            Cities = new SelectList(cities, "Id", "Name");
        }

        public async Task<IActionResult> OnPostAddCategory(CancellationToken cancellationToken)
        {
            Console.WriteLine("OnPostAddCategory started.");
            _logger.LogInformation("OnPostAddCategory started.");

            try
            {
                Console.WriteLine("Checking ModelState...");
                _logger.LogInformation("Checking ModelState...");

                if (!ModelState.IsValid)
                {
                    Console.WriteLine("ModelState is invalid.");
                    _logger.LogWarning("ModelState is invalid.");
                    var errorMessages = new List<string>();
                    foreach (var modelStateKey in ModelState.Keys)
                    {
                        var value = ModelState[modelStateKey];
                        foreach (var error in value.Errors)
                        {
                            var errorMessage = $"Key: {modelStateKey}, Error: {error.ErrorMessage}";
                            Console.WriteLine(errorMessage);
                            _logger.LogWarning(errorMessage);
                            errorMessages.Add(errorMessage);
                        }
                    }
                    ModelState.AddModelError("", "لطفاً خطاهای زیر را بررسی کنید: " + string.Join(" | ", errorMessages));
                    var cities = await _cityService.GetAll(cancellationToken);
                    Cities = new SelectList(cities, "Id", "Name");
                    Console.WriteLine("Returning page with validation errors.");
                    _logger.LogInformation("Returning page with validation errors.");
                    return Page();
                }

                Console.WriteLine("ModelState is valid. Proceeding to create customer...");
                _logger.LogInformation("ModelState is valid. Proceeding to create customer...");

                // چک کردن وضعیت لاگین
                Console.WriteLine($"IsAuthenticated: {User.Identity.IsAuthenticated}");
                _logger.LogInformation("IsAuthenticated: {IsAuthenticated}", User.Identity.IsAuthenticated);
                if (User.Identity.IsAuthenticated)
                {
                    Console.WriteLine($"User Claims: {string.Join(", ", User.Claims.Select(c => $"{c.Type}: {c.Value}"))}");
                    _logger.LogInformation("User Claims: {Claims}", string.Join(", ", User.Claims.Select(c => $"{c.Type}: {c.Value}")));
                }
                else
                {
                    Console.WriteLine("User is not authenticated.");
                    _logger.LogWarning("User is not authenticated.");
                }

                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    Console.WriteLine("No logged-in user found.");
                    _logger.LogWarning("No logged-in user found.");
                    ModelState.AddModelError("", "کاربر لاگین‌شده پیدا نشد. لطفاً اول وارد شوید.");
                    var cities = await _cityService.GetAll(cancellationToken);
                    Cities = new SelectList(cities, "Id", "Name");
                    return Page();
                }

                CustomerCreate.AppUserId = currentUser.Id;
                Console.WriteLine($"AppUserId set to {currentUser.Id}");
                _logger.LogInformation("AppUserId set to {AppUserId}", currentUser.Id);

                var result = await _customerAppServices.Create(CustomerCreate, Image, cancellationToken);
                if (!result)
                {
                    Console.WriteLine("Customer creation failed.");
                    _logger.LogWarning("Customer creation failed.");
                    ModelState.AddModelError("", "خطا در ثبت مشتری: مشکل در آپلود عکس یا ذخیره اطلاعات");
                    var cities = await _cityService.GetAll(cancellationToken);
                    Cities = new SelectList(cities, "Id", "Name");
                    return Page();
                }

                Console.WriteLine("Customer created successfully. Redirecting to Customers...");
                _logger.LogInformation("Customer created successfully. Redirecting to Customers...");
                return RedirectToPage("Customers");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                _logger.LogError(ex, "Error occurred in OnPostAddCategory: {Message}", ex.Message);
                ModelState.AddModelError("", $"خطا در ثبت مشتری: {ex.Message}");
                var cities = await _cityService.GetAll(cancellationToken);
                Cities = new SelectList(cities, "Id", "Name");
                return Page();
            }
        }
    }
}


//using KareMa.Domain.Core.Contracts.AppService;
//using KareMa.Domain.Core.Contracts.Service;
//using KareMa.Domain.Core.DTOs.CategoryDTO;
//using KareMa.Domain.Core.Entities;
//using KareMa.Domain.Service;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using System.ComponentModel.DataAnnotations;

//namespace KareMa.EndPoint.RazorPages.Areas.AdminArea.Pages
//{
//    public class AddCustomerModel : PageModel
//    {

//        private readonly ICustomerAppServices _customerAppServices;
//        private readonly ICityServices _cityService;
//        private readonly UserManager<AppUser> _userManager;
//        private readonly ILogger<AddCustomerModel> _logger; // برای دیباگ

//        public AddCustomerModel(ICustomerAppServices customerAppServices, ICityServices cityService, UserManager<AppUser> userManager, ILogger<AddCustomerModel> logger)
//        {
//            _customerAppServices = customerAppServices ?? throw new ArgumentNullException(nameof(customerAppServices));
//            _cityService = cityService ?? throw new ArgumentNullException(nameof(cityService));
//            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
//            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
//        }

//        [BindProperty]
//        public CustomerCreateDto CustomerCreate { get; set; } = new CustomerCreateDto();

//        [BindProperty]
//        public IFormFile? Image { get; set; }

//        public SelectList Cities { get; set; }

//        public async Task OnGet(CancellationToken cancellationToken)
//        {
//            _logger.LogInformation("OnGet called.");
//            CustomerCreate.Addresses = new Address();
//            var cities = await _cityService.GetAll(cancellationToken);
//            Cities = new SelectList(cities, "Id", "Name");
//        }

//        public async Task<IActionResult> OnPostAddCategory(CancellationToken cancellationToken)
//        {
//            _logger.LogInformation("OnPostAddCategory started.");

//            try
//            {
//                _logger.LogInformation("Checking ModelState...");
//                if (!ModelState.IsValid)
//                {
//                    _logger.LogWarning("ModelState is invalid.");
//                    foreach (var modelStateKey in ModelState.Keys)
//                    {
//                        var value = ModelState[modelStateKey];
//                        foreach (var error in value.Errors)
//                        {
//                            _logger.LogWarning($"Key: {modelStateKey}, Error: {error.ErrorMessage}");
//                        }
//                    }
//                    var cities = await _cityService.GetAll(cancellationToken);
//                    Cities = new SelectList(cities, "Id", "Name");
//                    return Page();
//                }

//                _logger.LogInformation("ModelState is valid. Proceeding to create customer...");

//                // گرفتن کاربر فعلی (مثلاً ادمین)
//                var currentUser = await _userManager.GetUserAsync(User);
//                if (currentUser == null)
//                {
//                    _logger.LogWarning("No logged-in user found.");
//                    ModelState.AddModelError("", "کاربر لاگین‌شده پیدا نشد. لطفاً اول وارد شوید.");
//                    var cities = await _cityService.GetAll(cancellationToken);
//                    Cities = new SelectList(cities, "Id", "Name");
//                    return Page();
//                }

//                // ست کردن AppUserId از کاربر فعلی
//                CustomerCreate.AppUserId = currentUser.Id;
//                _logger.LogInformation("AppUserId set to {AppUserId}", currentUser.Id);

//                var result = await _customerAppServices.Create(CustomerCreate, Image, cancellationToken);
//                if (!result)
//                {
//                    _logger.LogWarning("Customer creation failed.");
//                    ModelState.AddModelError("", "خطا در ثبت مشتری: مشکل در آپلود عکس یا ذخیره اطلاعات");
//                    var cities = await _cityService.GetAll(cancellationToken);
//                    Cities = new SelectList(cities, "Id", "Name");
//                    return Page();
//                }

//                _logger.LogInformation("Customer created successfully. Redirecting to Customers...");
//                return RedirectToPage("Customers");
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error occurred in OnPostAddCategory: {Message}", ex.Message);
//                ModelState.AddModelError("", $"خطا در ثبت مشتری: {ex.Message}");
//                var cities = await _cityService.GetAll(cancellationToken);
//                Cities = new SelectList(cities, "Id", "Name");
//                return Page();
//            }
//        }

//        //private readonly ICustomerAppServices _customerAppServices;
//        //private readonly ICityServices _cityService;
//        //private readonly UserManager<AppUser> _userManager;

//        //public AddCustomerModel(ICustomerAppServices customerAppServices, ICityServices cityService, UserManager<AppUser> userManager)
//        //{
//        //    _customerAppServices = customerAppServices;
//        //    _cityService = cityService;
//        //    _userManager = userManager;
//        //}

//        //[BindProperty]
//        //public CustomerCreateDto CustomerCreate { get; set; } = new CustomerCreateDto();

//        //[BindProperty]
//        //public IFormFile? Image { get; set; }

//        //public SelectList Cities { get; set; }

//        //public async Task OnGet(CancellationToken cancellationToken)
//        //{
//        //    CustomerCreate.Addresses = new Address();
//        //    var cities = await _cityService.GetAll(cancellationToken);
//        //    Cities = new SelectList(cities, "Id", "Name");
//        //}

//        //public async Task<IActionResult> OnPostAddCategory(CancellationToken cancellationToken)
//        //{
//        //    if (!ModelState.IsValid)
//        //    {
//        //        Console.WriteLine("ModelState is invalid. Errors:");
//        //        foreach (var modelStateKey in ModelState.Keys)
//        //        {
//        //            var value = ModelState[modelStateKey];
//        //            foreach (var error in value.Errors)
//        //            {
//        //                Console.WriteLine($"Key: {modelStateKey}, Error: {error.ErrorMessage}");
//        //            }
//        //        }
//        //        var cities = await _cityService.GetAll(cancellationToken);
//        //        Cities = new SelectList(cities, "Id", "Name");
//        //        return Page();
//        //    }

//        //    try
//        //    {
//        //        // گرفتن کاربر فعلی (مثلاً ادمین)
//        //        var currentUser = await _userManager.GetUserAsync(User);
//        //        if (currentUser == null)
//        //        {
//        //            ModelState.AddModelError("", "کاربر لاگین‌شده پیدا نشد. لطفاً اول وارد شوید.");
//        //            var cities = await _cityService.GetAll(cancellationToken);
//        //            Cities = new SelectList(cities, "Id", "Name");
//        //            return Page();
//        //        }

//        //        // ست کردن AppUserId از کاربر فعلی
//        //        CustomerCreate.AppUserId = currentUser.Id;

//        //        var result = await _customerAppServices.Create(CustomerCreate, Image, cancellationToken);
//        //        if (!result)
//        //        {
//        //            ModelState.AddModelError("", "خطا در ثبت مشتری: مشکل در آپلود عکس یا ذخیره اطلاعات");
//        //            var cities = await _cityService.GetAll(cancellationToken);
//        //            Cities = new SelectList(cities, "Id", "Name");
//        //            return Page();
//        //        }
//        //        return RedirectToPage("Customers");
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        Console.WriteLine($"Error: {ex.Message}");
//        //        ModelState.AddModelError("", $"خطا در ثبت مشتری: {ex.Message}");
//        //        var cities = await _cityService.GetAll(cancellationToken);
//        //        Cities = new SelectList(cities, "Id", "Name");
//        //        return Page();
//        //    }
//        //}
//        //private readonly ICustomerAppServices _customerAppServices;

//        //public AddCustomerModel(ICustomerAppServices customerAppServices)
//        //{
//        //    _customerAppServices = customerAppServices;
//        //}

//        //[BindProperty]

//        //public CustomerCreateDto CustomerCreate { get; set; } = new CustomerCreateDto();

//        //[BindProperty]
//        //[Required(ErrorMessage = " ??? ????????? ???? ????? ????")]
//        //public IFormFile Image { get; set; }

//        //public async Task OnGet(CancellationToken cancellationToken)
//        //{

//        //}
//        //public async Task<IActionResult> OnPostAddCustomer(CancellationToken cancellationToken)
//        //{
//        //    ModelState.Remove("CstomerCreate.Id"); 

//        //    if (Image == null || Image.Length == 0)
//        //    {
//        //        ModelState.AddModelError("Image", "مقدار عکس خالی است");
//        //    }

//        //    if (!ModelState.IsValid)
//        //    {
//        //        foreach (var modelStateKey in ModelState.Keys)
//        //        {
//        //            var value = ModelState[modelStateKey];
//        //            foreach (var error in value.Errors)
//        //            {
//        //                Console.WriteLine($"Key: {modelStateKey}, Error: {error.ErrorMessage}");
//        //            }
//        //        }
//        //        return Page();
//        //    }

//        //    await _customerAppServices.Create(CustomerCreate, Image, cancellationToken);
//        //    return RedirectToPage("Category");
//        //}
//    }
//}
