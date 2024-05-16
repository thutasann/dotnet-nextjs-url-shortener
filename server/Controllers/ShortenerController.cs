using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Dto;
using server.Models;

namespace server.Controllers
{
    [ApiController]
    [Route("api/shorturl")]
    public class ShortenerController(AppDbContext appDbContext, IHttpContextAccessor ctx) : ControllerBase
    {
        private readonly AppDbContext _db = appDbContext;
        private readonly IHttpContextAccessor _ctx = ctx;

        [HttpPost]
        public async Task<ActionResult<UrlShortResponseDto>> ShortUrl([FromForm] UrlDto urlDto)
        {
            try
            {
                if (!Uri.TryCreate(urlDto.Url, UriKind.Absolute, out var inputUrl))
                {
                    return BadRequest("Invalid URL has been provided");
                }

                var randomStr = GenerateRandom();

                var sUrl = new UrlManagement()
                {
                    Url = urlDto.Url,
                    ShortUrl = randomStr
                };

                await _db.Urls.AddAsync(sUrl);
                await _db.SaveChangesAsync();

                var httpContext = _ctx.HttpContext;
                string result = $"{httpContext!.Request.Scheme}://{httpContext.Request.Host}/{sUrl.ShortUrl}";
                return Ok(new UrlShortResponseDto() { Url = result });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Url shorten Error : {ex.Message}");
                throw;
            }
        }

        private static string GenerateRandom()
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890@az";
            var randomStr = new string(Enumerable.Repeat(chars, 8).Select(x => x[random.Next(x.Length)]).ToArray());
            return randomStr;
        }
    }
}