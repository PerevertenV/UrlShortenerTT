using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using UrlShortener.DataAccess.Repository.IRepository;
using UrlShortener.Models;
using UrlShortener.Service;

namespace UrlShortener.Controllers
{
    public class UrlController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UrlController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;       
        }

        public async Task<ActionResult> AddCheckShortUrl(int? id)
        { 
            if (id != null && id != 0)
            {
                var ShortUrlFromDb = await _unitOfWork.Url.GetFirstOrDefaultAsync(x => x.Id == id);

                ShortUrlFromDb.User = await _unitOfWork.User
                    .GetFirstOrDefaultAsync(x => x.Id == ShortUrlFromDb.IdOfUser);

                return View(ShortUrlFromDb);
            }
            else
            {
                return View(new UrlModel());
            }

        }
        [HttpPost]
        public async Task<ActionResult> AddCheckShortUrl(UrlModel obj)
        {
            if (!obj.LongUrl.Contains("https://"))
            {
                ModelState.AddModelError("LongUrl", "Incorrect long url");
                return View(obj);
            }

            HashSet<string> LongUrlListFromDb = ((await _unitOfWork.Url.GetAllAsync())
                .Select(u => u.LongUrl)).ToHashSet();

            HashSet< string > ShortUrlListFromDb = ((await _unitOfWork.Url.GetAllAsync())
                .Select(u => u.ShortUrl)).ToHashSet();

            if (LongUrlListFromDb.Contains(obj.LongUrl))
            {
                ModelState.AddModelError("LongUrl", "That url is already exist");
                return View(obj);
            }

            string? uniqueShortUrl;
            while (true) 
            {
                uniqueShortUrl = StaticData.CreateShortUrl();
                if (ShortUrlListFromDb.Contains(uniqueShortUrl)) 
                {
                    continue;
                }
                else 
                {
                    break;
                }
            }

            obj.ShortUrl = uniqueShortUrl;
            uniqueShortUrl = null;

            var user = HttpContext.User;
            int.TryParse(user.FindFirstValue("UserID"), out int userId);
            obj.IdOfUser = userId;

            obj.CreatedDate = DateOnly.FromDateTime(DateTime.Now);

            await _unitOfWork.Url.AddAsync(obj);
            return Redirect("/Home/Index");
        }
    }
}
