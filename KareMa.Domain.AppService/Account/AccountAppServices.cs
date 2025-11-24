namespace KareMa.Domain.AppService.Account
{
    public class AccountAppServices : IAccountAppServices
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AccountAppServices(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        /// <summary>
        /// ثبت‌نام یک کاربر عادی (مشتری یا متخصص) و اختصاص نقش و Claim مناسب
        /// </summary>
        public async Task<List<IdentityError>> Register(AccountRegisterDto accountRegisterDto) 
        {
            var role = string.Empty; 
            var user = CreateUser();
            user.UserName = accountRegisterDto.Email; 
            user.Email = accountRegisterDto.Email; 
            if (accountRegisterDto.isExpert) 
            {
                role = "Expert"; 
                user.Expert = new Expert() 
                {
                    FirstName = accountRegisterDto.FirstName,
                    LastName = accountRegisterDto.LastName, 
                    Gender = accountRegisterDto.Gender 
                };
            }
            else 
            {
                role = "Customer"; 
                user.Customer = new Customer() 
                {
                    FirstName = accountRegisterDto.FirstName,
                    LastName = accountRegisterDto.LastName,
                    Gender = accountRegisterDto.Gender 
                };
            }
            var result = await _userManager.CreateAsync(user, accountRegisterDto.Password);
            if (accountRegisterDto.isExpert) 
            {
                var userExpertId = user.Expert!.Id;
                await _userManager.AddClaimAsync(user, new Claim("userExpertId", userExpertId.ToString())); 
            }
            else 
            {
                var userCustomerId = user.Customer!.Id; 
                await _userManager.AddClaimAsync(user, new Claim("userCustomerId", userCustomerId.ToString()));
            }
            if (result.Succeeded) await _userManager.AddToRoleAsync(user, role);
            return (List<IdentityError>)result.Errors;
        }
        /// <summary>
        /// ورود کاربر با ایمیل و رمز عبور
        /// </summary>
        public async Task<bool> Login(AccountLoginDto accountLoginDto) 
        {
            var result = await _signInManager.PasswordSignInAsync(accountLoginDto.Email, accountLoginDto.Password, false, lockoutOnFailure: false); 
            return result.Succeeded; 
        }

        /// <summary>
        /// ایجاد یک نمونه جدید از AppUser
        /// </summary>
        private AppUser CreateUser() 
        {
            try 
            {
                return Activator.CreateInstance<AppUser>(); 
            }
            catch 
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(AppUser)}'. " + $"Ensure that '{nameof(AppUser)}' is not an abstract class and has a parameterless constructor, or alternatively " + $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml"); 
            }
        }

        /// <summary>
        /// ثبت‌نام یک کاربر ادمین و اختصاص نقش Admin و Claim مربوطه
        /// </summary>
        public async Task<List<IdentityError>> AdminRegister(AccountAdminRegisterDto accountAdminRegisterDto)
        {
            var user = CreateUser();

            user.UserName = accountAdminRegisterDto.Email;
            user.Email = accountAdminRegisterDto.Email;

            user.Admin = new Admin()
            {
                FirstName = accountAdminRegisterDto.FirstName,
                LastName = accountAdminRegisterDto.LastName,
                Gender = accountAdminRegisterDto.Gender,
            };

            var result = await _userManager.CreateAsync(user, accountAdminRegisterDto.Password);

            var userAdminId = user.Admin!.Id;
            await _userManager.AddClaimAsync(user, new Claim("userAdminId", userAdminId.ToString()));

            if (result.Succeeded)
                await _userManager.AddToRoleAsync(user, "Admin");

            return (List<IdentityError>)result.Errors;
        }
        /// <summary>
        /// دریافت نقش‌های یک کاربر بر اساس ایمیل
        /// </summary>
        public async Task<IList<string>> GetUserRolesByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return new List<string>();

            return await _userManager.GetRolesAsync(user);
        }
    }
}
