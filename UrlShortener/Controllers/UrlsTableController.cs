using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortener.DataAccess.Repository.IRepository;
using UrlShortener.Models;

namespace UrlShortener.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrlsTableController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UrlsTableController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UrlModel>>> GetUrls()
        {
            var urls = (await _unitOfWork.Url.GetAllAsync())
                .Select(url => new UrlModel
                {
                    LongUrl = url.LongUrl,
                    ShortUrl = url.ShortUrl
                }).ToList();

            return Ok(urls);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUrl(int id)
        {
            var url = await _unitOfWork.Url.GetFirstOrDefaultAsync(u=> u.Id == id);

            if (url == null)
            {
                return NotFound();
            }
            await _unitOfWork.Url.DeleteAsync(url);
            return NoContent();
        }
    }
}
