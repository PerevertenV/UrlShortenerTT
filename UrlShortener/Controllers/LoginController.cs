using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UrlShortener.DataAccess.Repository.IRepository;
using UrlShortener.Models;
using UrlShortener.Service;

namespace CourseProjectDB.Areas.Customer.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public LoginController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;       
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(UserModel obj) 
        {
            bool success = true;

            List<UserModel> users = (await _unitOfWork.User.GetAllAsync()).ToList();

            foreach (var user in users) 
            {
                if(user.Login == obj.Login) 
                {
                    string Decodet = StaticData.DecryptString(user.Password);
                    success = false;
                    if(obj.Password == Decodet) 
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.Login),
                            new Claim(ClaimTypes.Role, user.Role),
                            new Claim("UserID", user.Id.ToString())
                        };

                        var claimsIdentity = new ClaimsIdentity(
                            claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = true
                        };

                        HttpContext.SignInAsync(
                           CookieAuthenticationDefaults.AuthenticationScheme,
                           new ClaimsPrincipal(claimsIdentity),
                           authProperties).GetAwaiter().GetResult();

                        return Redirect("Home/Index");
                    }
                    else 
                    {
                        ModelState.AddModelError("password", "Incorrect password try again");
                        return View();
                    }
                }
            }
            if (success) 
            {
                ModelState.AddModelError("login", "User with this login doesn't exist");
                return View();
            }
            return View();
        }
    }
}
