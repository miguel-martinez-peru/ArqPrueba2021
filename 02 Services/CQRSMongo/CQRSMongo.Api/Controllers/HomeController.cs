using Microsoft.AspNetCore.Mvc;

namespace AuthZ.Api.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}