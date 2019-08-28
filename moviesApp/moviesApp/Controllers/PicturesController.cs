using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using moviesApp.Controllers.ActionResult;
using moviesApp.Services;

namespace moviesApp.Controllers
{
    [Route("api/[controller]")]
    public class PicturesController: Controller
    {
        IProxyService proxyService { get; set; }

        public PicturesController(IProxyService service)
        {
            proxyService = service;
        }

        [HttpGet("{**imageUrl}")]
        public async Task<IActionResult> MoviesList(string imageUrl)
        {
            var result = await proxyService.GetFromUrl($"https://picsum.photos/{imageUrl}");
            return new HttpResponseMessageResult(result);
        }
    }
}
