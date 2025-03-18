using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using UrlShortener.DataAccess.Repository.IRepository;
using UrlShortener.Service;
using UrlShortener.Models;

namespace CourseProjectDB.Areas.Customer.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
		public RegisterController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;  
        }
        public IActionResult Index()
        {
            Dictionary<string, string> list = new Dictionary<string, string>() 
            {
                { StaticData.RoleAdmin, "Admin" }, 
                { StaticData.RoleCustomer, "Customer" }
            };
            IEnumerable<SelectListItem> RoleList = list.Select(u => new SelectListItem
            {
                Text = u.Value,
                Value = u.Key
            });
            ViewBag.List = RoleList;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(UserModel obj, IFormCollection form)
        {
            string confirmPassword = form["confirmPassword"];

            List<UserModel> users =  (await _unitOfWork.User.GetAllAsync()).ToList();
            bool PasswordChecker = Regex.IsMatch(obj.Password, "[a-zA-Z]");

            foreach (UserModel user in users) 
            {
                if(obj.Login == user.Login) 
                {
                    ModelState.AddModelError("login", "User with this login is already exist");
                    return View();
                }
            }
            if (obj.Password.Length < 6 || obj.Password.Length > 15)
            {
                ModelState.AddModelError("password", "Password must contains from 5 to 15 characters");
                return View();
            }
            else if (!PasswordChecker)
            {
                ModelState.AddModelError("password", "Password must contain at least one letter");
                return View();
            }
            else if (confirmPassword != obj.Password)
            {
                ModelState.AddModelError("password", "Passwords must match");
                return View();
            }
            else
            {
                string WhichRole = User.IsInRole("Admin") ? obj.Role : StaticData.RoleCustomer;
                var UserToAdding = new UserModel
                {
                    Login = obj.Login,
                    Password = StaticData.PasswordHashCoder(obj.Password),
                    Role = WhichRole
                };

                await _unitOfWork.User.AddAsync(UserToAdding);
                var UserId = (await _unitOfWork.User.GetFirstOrDefaultAsync(u=>u.Login == obj.Login)).Id;

                if (!User.Identity.IsAuthenticated) 
                { 
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, obj.Login),
                        new Claim(ClaimTypes.Role, WhichRole),
                        new Claim("UserID", UserId.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(
                       CookieAuthenticationDefaults.AuthenticationScheme,
                       new ClaimsPrincipal(claimsIdentity),
                       authProperties);
                }
                return Redirect("Home/Index");
            }
        }

        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
