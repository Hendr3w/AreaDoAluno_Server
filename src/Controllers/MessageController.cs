using Microsoft.AspNetCore.Mvc;

namespace AreaDoAluno.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
