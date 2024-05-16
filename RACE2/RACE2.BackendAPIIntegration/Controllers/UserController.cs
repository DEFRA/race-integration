using Microsoft.AspNetCore.Mvc;

namespace RACE2.BackendAPIIntegration.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
